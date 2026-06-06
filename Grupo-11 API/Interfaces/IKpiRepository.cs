using System.Collections.Generic;
using System.Threading.Tasks;
using Grupo11.Models;

namespace Grupo11.Interfaces
{
    public interface IKpiRepository
    {
        Task<IEnumerable<CumplimientoOrdenDto>> GetCumplimientoOrdenesAsync();
        Task<IEnumerable<TiempoEjecucionDto>> GetTiempoPromedioAsync();
        Task<IEnumerable<RotacionInventarioDto>> GetRotacionInventarioAsync();
    }
}
