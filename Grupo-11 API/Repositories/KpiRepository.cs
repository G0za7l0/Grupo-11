using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Grupo11.Interfaces;
using Grupo11.Models;

namespace Grupo11.Repositories
{
    public class KpiRepository : IKpiRepository
    {
        private readonly IDbConnection _dbConnection;

        public KpiRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        /// <summary>
        /// Retorna el dataset completo para el KPI de Cumplimiento de Órdenes.
        /// Cada fila representa una operación con su estado, permitiendo que
        /// Power BI calcule: (Completadas / Total) * 100
        /// </summary>
        public async Task<IEnumerable<CumplimientoOrdenDto>> GetCumplimientoOrdenesAsync()
        {
            const string sql = @"
                SELECT 
                    f.IdOperacion,
                    t.Fecha,
                    t.Anio,
                    t.Mes,
                    t.Dia,
                    c.NombreCliente,
                    s.NombreServicio,
                    tc.NombreTecnico,
                    e.NombreEstado
                FROM Fact_Operaciones f
                INNER JOIN Dim_Tiempo t   ON f.IdTiempo   = t.IdTiempo
                INNER JOIN Dim_Cliente c  ON f.IdCliente  = c.IdCliente
                INNER JOIN Dim_Servicio s ON f.IdServicio  = s.IdServicio
                INNER JOIN Dim_Tecnico tc ON f.IdTecnico   = tc.IdTecnico
                INNER JOIN Dim_Estado e   ON f.IdEstado    = e.IdEstado
                ORDER BY t.Fecha DESC";

            return await _dbConnection.QueryAsync<CumplimientoOrdenDto>(sql);
        }

        /// <summary>
        /// Retorna el dataset completo para el KPI de Tiempo Promedio de Ejecución.
        /// Cada fila incluye el tiempo de ejecución en días, permitiendo que
        /// Power BI calcule: SUM(TiempoEjecucionDias) / COUNT(*)
        /// </summary>
        public async Task<IEnumerable<TiempoEjecucionDto>> GetTiempoPromedioAsync()
        {
            const string sql = @"
                SELECT 
                    f.IdOperacion,
                    t.Fecha,
                    t.Anio,
                    t.Mes,
                    t.Dia,
                    c.NombreCliente,
                    s.NombreServicio,
                    tc.NombreTecnico,
                    e.NombreEstado,
                    f.Tiempo_Ejecucion_Dias AS TiempoEjecucionDias
                FROM Fact_Operaciones f
                INNER JOIN Dim_Tiempo t   ON f.IdTiempo   = t.IdTiempo
                INNER JOIN Dim_Cliente c  ON f.IdCliente  = c.IdCliente
                INNER JOIN Dim_Servicio s ON f.IdServicio  = s.IdServicio
                INNER JOIN Dim_Tecnico tc ON f.IdTecnico   = tc.IdTecnico
                INNER JOIN Dim_Estado e   ON f.IdEstado    = e.IdEstado
                ORDER BY t.Fecha DESC";

            return await _dbConnection.QueryAsync<TiempoEjecucionDto>(sql);
        }

        /// <summary>
        /// Retorna el dataset completo para el KPI de Rotación de Inventario.
        /// Cada fila incluye equipos utilizados y disponibles, permitiendo que
        /// Power BI calcule: (SUM(EquiposUtilizados) / SUM(EquiposDisponibles)) * 100
        /// </summary>
        public async Task<IEnumerable<RotacionInventarioDto>> GetRotacionInventarioAsync()
        {
            const string sql = @"
                SELECT 
                    f.IdOperacion,
                    t.Fecha,
                    t.Anio,
                    t.Mes,
                    t.Dia,
                    c.NombreCliente,
                    s.NombreServicio,
                    tc.NombreTecnico,
                    e.NombreEstado,
                    f.Equipos_Utilizados AS EquiposUtilizados,
                    f.Equipos_Disponibles AS EquiposDisponibles
                FROM Fact_Operaciones f
                INNER JOIN Dim_Tiempo t   ON f.IdTiempo   = t.IdTiempo
                INNER JOIN Dim_Cliente c  ON f.IdCliente  = c.IdCliente
                INNER JOIN Dim_Servicio s ON f.IdServicio  = s.IdServicio
                INNER JOIN Dim_Tecnico tc ON f.IdTecnico   = tc.IdTecnico
                INNER JOIN Dim_Estado e   ON f.IdEstado    = e.IdEstado
                ORDER BY t.Fecha DESC";

            return await _dbConnection.QueryAsync<RotacionInventarioDto>(sql);
        }
    }
}
