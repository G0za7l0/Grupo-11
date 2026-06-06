using System.Threading.Tasks;

namespace Grupo11.Interfaces
{
    public interface IKpiRepository
    {
        Task<double> GetCumplimientoOrdenesAsync();
        Task<double> GetTiempoPromedioAsync();
        Task<double> GetRotacionInventarioAsync();
    }
}
