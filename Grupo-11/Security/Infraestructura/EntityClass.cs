using System;
using System.Collections.Generic;

namespace Grupo11.Security.Infraestructura
{
    public abstract class EntityClass
    {
        // Métodos base para el ORM que implementarás después con tu Base de Datos
        public virtual List<T> Get<T>(string condition = "") { throw new NotImplementedException("Falta conectar BD"); }
        public virtual List<T> Where<T>() { throw new NotImplementedException("Falta conectar BD"); }
        public virtual T? Find<T>() { throw new NotImplementedException("Falta conectar BD"); }
        public virtual bool Exists() { throw new NotImplementedException("Falta conectar BD"); }
        public virtual object? Save(bool fullInsert = true) { throw new NotImplementedException("Falta conectar BD"); }
        public virtual object Update() { throw new NotImplementedException("Falta conectar BD"); }
    }
}
