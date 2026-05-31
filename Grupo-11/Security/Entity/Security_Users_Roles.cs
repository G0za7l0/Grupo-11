using Grupo11.Security.Infraestructura;

namespace Grupo11.Security.Entity
{
    public class Security_Users_Roles : EntityClass
    {
        [PrimaryKey(Identity = false)]
        public int? Id_Role { get; set; }
        [PrimaryKey(Identity = false)]
        public int? Id_User { get; set; }
        public string? Estado { get; set; }

        [ManyToOne(TableName = "Security_Roles", KeyColumn = "Id_Role", ForeignKeyColumn = "Id_Role")]
        public Security_Roles? Security_Role { get; set; }
    }
}
