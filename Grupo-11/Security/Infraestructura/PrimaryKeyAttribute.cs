using System;

namespace Grupo11.Security.Infraestructura
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKeyAttribute : Attribute
    {
        public bool Identity { get; set; } = true;
    }
}
