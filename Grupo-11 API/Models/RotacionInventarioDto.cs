using System;

namespace Grupo11.Models
{
    /// <summary>
    /// Dataset para el KPI de Rotación de Inventario.
    /// Power BI calculará: (Equipos Utilizados / Equipos Disponibles) * 100
    /// </summary>
    public class RotacionInventarioDto
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
        public int EquiposUtilizados { get; set; }
        public int EquiposDisponibles { get; set; }
    }
}
