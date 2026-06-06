using System.Data;
using System.Threading.Tasks;
using Dapper;
using Grupo11.Interfaces;

namespace Grupo11.Repositories
{
    public class KpiRepository : IKpiRepository
    {
        private readonly IDbConnection _dbConnection;

        public KpiRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<double> GetCumplimientoOrdenesAsync()
        {
            const string sql = "SELECT PorcentajeCumplimiento FROM vw_KPI_Consolidados";
            return await _dbConnection.ExecuteScalarAsync<double>(sql);
        }

        public async Task<double> GetTiempoPromedioAsync()
        {
            const string sql = "SELECT TiempoPromedioEjecucion FROM vw_KPI_Consolidados";
            return await _dbConnection.ExecuteScalarAsync<double>(sql);
        }

        public async Task<double> GetRotacionInventarioAsync()
        {
            const string sql = "SELECT RotacionInventario FROM vw_KPI_Consolidados";
            return await _dbConnection.ExecuteScalarAsync<double>(sql);
        }
    }
}
