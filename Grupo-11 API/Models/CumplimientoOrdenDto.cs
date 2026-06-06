using System;

namespace Grupo11.Models
{
    /// <summary>
    /// Dataset para el KPI de Cumplimiento de Órdenes.
    /// Power BI calculará: (Órdenes Completadas / Órdenes Totales) * 100
    /// </summary>
    public class CumplimientoOrdenDto
    {
        public int IdOperacion { get; set; }
        public DateTime Fecha { get; set; }
        public int Anio { get; set; }
        public int Mes { get; set; }
        public int Dia { get; set; }
        public string NombreCliente { get; set; }
        public string NombreServicio { get; set; }
        public string NombreTecnico { get; set; }
        public string NombreEstado { get; set; }
    }
}
