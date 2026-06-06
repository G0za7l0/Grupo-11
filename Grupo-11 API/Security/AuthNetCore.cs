using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using Dapper;
using Grupo11.Security.Model;

namespace Grupo11.Security
{
    public class AuthNetCore
    {
        public static string ConnectionString { get; set; } = "";

        // Mantendremos las sesiones activas en memoria para evitar consultar la BD en cada petición
        private static Dictionary<string, UserModel> _activeSessions = new Dictionary<string, UserModel>();

        public static bool Authenticate(string? sessionKey)
        {
            if (string.IsNullOrEmpty(sessionKey)) return false;
            return _activeSessions.ContainsKey(sessionKey);
        }

        public static void ClearSeason(string? sessionKey)
        {
            if (!string.IsNullOrEmpty(sessionKey) && _activeSessions.ContainsKey(sessionKey))
            {
                _activeSessions.Remove(sessionKey);
            }
        }

        public static object? Login(LoginRequest inst, string? sessionKey)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                // Buscar usuario (comprobando por Nombres o Mail)
                var queryUser = @"SELECT Id_User, Nombres as username, Password 
                                  FROM Security_Users 
                                  WHERE (Nombres = @Username OR Mail = @Username) 
                                    AND Password = @Password AND Estado = 'Activo'";
                
                var user = connection.QueryFirstOrDefault<UserModel>(queryUser, new { Username = inst.username, Password = inst.password });

                if (user != null)
                {
                    // Buscar permisos
                    var queryPerm = @"
                        SELECT p.Descripcion 
                        FROM Security_Users_Roles ur
                        INNER JOIN Security_Permissions_Roles pr ON ur.Id_Role = pr.Id_Role
                        INNER JOIN Security_Permissions p ON pr.Id_Permission = p.Id_Permission
                        WHERE ur.Id_User = @IdUser AND ur.Estado = 'Activo' AND p.Estado = 'Activo'";
                    
                    var permsStr = connection.Query<string>(queryPerm, new { IdUser = user.Id_User }).ToList();
                    
                    // Convertir strings a Enum Permissions
                    var enumPerms = new List<Permissions>();
                    foreach(var p in permsStr)
                    {
                        if (Enum.TryParse(p, out Permissions perm))
                        {
                            enumPerms.Add(perm);
                        }
                    }
                    user.Permissions = enumPerms.ToArray();

                    string newSessionKey = Guid.NewGuid().ToString();
                    _activeSessions[newSessionKey] = user;
                    return new { SessionKey = newSessionKey, User = user };
                }
            }
            return null;
        }

        public static bool HavePermission(string? token, Permissions[] permissionsList)
        {
            if (string.IsNullOrEmpty(token) || !_activeSessions.ContainsKey(token)) return false;
            var user = _activeSessions[token];
            if (user.Permissions == null) return false;

            return user.Permissions.Intersect(permissionsList).Any();
        }
    }
}
