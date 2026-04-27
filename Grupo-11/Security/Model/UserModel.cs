using System.Text.Json.Serialization;

namespace Grupo11.Security.Model
{
    public class UserModel
    {
        public int Id_User { get; set; }
        public string? username { get; set; }
        
        [JsonIgnore]
        public string? password { get; set; }
        
        [JsonIgnore]
        public string? SessionKey { get; set; }
        public Permissions[]? Permissions { get; set; }
    }
}
