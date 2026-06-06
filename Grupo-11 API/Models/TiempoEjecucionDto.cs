using System;

namespace Grupo11.Models
{
    /// <summary>
    /// Dataset para el KPI de Tiempo Promedio de Ejecución.
    /// Power BI calculará: Suma(Tiempo_Ejecucion_Dias) / Cantidad de órdenes
    /// </summary>
    public class TiempoEjecucionDto
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
        public decimal TiempoEjecucionDias { get; set; }
    }
}
