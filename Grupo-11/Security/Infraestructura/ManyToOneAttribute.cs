using System;

namespace Grupo11.Security.Infraestructura
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ManyToOneAttribute : Attribute
    {
        public string TableName { get; set; } = string.Empty;
        public string KeyColumn { get; set; } = string.Empty;
        public string ForeignKeyColumn { get; set; } = string.Empty;
    }
}
