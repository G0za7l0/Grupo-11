using System;

namespace Grupo11.Models
{
    public class DetalleOperativo
    {
        public int IdOrden { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public string Servicio { get; set; }
        public string Tecnico { get; set; }
        public string Estado { get; set; }
        public double TiempoEjecucionDias { get; set; }
        public int EquiposUtilizados { get; set; }
        public int EquiposDisponibles { get; set; }
    }
}
