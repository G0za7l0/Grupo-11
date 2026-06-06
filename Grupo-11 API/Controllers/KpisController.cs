using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Grupo11.Interfaces;

namespace Grupo11.Controllers
{
    [ApiController]
    [Route("api/kpis")]
    public class KpisController : ControllerBase
    {
        private readonly IKpiRepository _kpiRepository;

        public KpisController(IKpiRepository kpiRepository)
        {
            _kpiRepository = kpiRepository;
        }

        [HttpGet("cumplimiento-ordenes")]
        public async Task<IActionResult> GetCumplimientoOrdenes()
        {
            try
            {
                var valor = await _kpiRepository.GetCumplimientoOrdenesAsync();
                return Ok(new { kpi = "Cumplimiento de Órdenes", valor = valor, unidad = "%" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener el cumplimiento de órdenes", error = ex.Message });
            }
        }

        [HttpGet("tiempo-promedio")]
        public async Task<IActionResult> GetTiempoPromedio()
        {
            try
            {
                var valor = await _kpiRepository.GetTiempoPromedioAsync();
                return Ok(new { kpi = "Tiempo Promedio de Ejecución", valor = valor, unidad = "Días" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener el tiempo promedio de ejecución", error = ex.Message });
            }
        }

        [HttpGet("rotacion-inventario")]
        public async Task<IActionResult> GetRotacionInventario()
        {
            try
            {
                var valor = await _kpiRepository.GetRotacionInventarioAsync();
                return Ok(new { kpi = "Rotación de Inventario", valor = valor, unidad = "%" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener la rotación de inventario", error = ex.Message });
            }
        }
    }
}
