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

        /// <summary>
        /// Retorna el dataset completo de operaciones con su estado.
        /// Power BI calcula: (Órdenes Completadas / Órdenes Totales) * 100
        /// </summary>
        [HttpGet("cumplimiento-ordenes")]
        public async Task<IActionResult> GetCumplimientoOrdenes()
        {
            try
            {
                var datos = await _kpiRepository.GetCumplimientoOrdenesAsync();
                return Ok(new
                {
                    kpi = "Cumplimiento de Órdenes",
                    descripcion = "Órdenes completadas / Órdenes totales",
                    unidad = "%",
                    datos
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener los datos de cumplimiento de órdenes", error = ex.Message });
            }
        }

        /// <summary>
        /// Retorna el dataset completo de operaciones con su tiempo de ejecución.
        /// Power BI calcula: Tiempo total / Número de órdenes
        /// </summary>
        [HttpGet("tiempo-promedio")]
        public async Task<IActionResult> GetTiempoPromedio()
        {
            try
            {
                var datos = await _kpiRepository.GetTiempoPromedioAsync();
                return Ok(new
                {
                    kpi = "Tiempo Promedio de Ejecución",
                    descripcion = "Tiempo total / Número de órdenes",
                    unidad = "Días",
                    datos
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener los datos de tiempo de ejecución", error = ex.Message });
            }
        }

        /// <summary>
        /// Retorna el dataset completo de operaciones con equipos utilizados y disponibles.
        /// Power BI calcula: (Equipos utilizados / Equipos disponibles) * 100
        /// </summary>
        [HttpGet("rotacion-inventario")]
        public async Task<IActionResult> GetRotacionInventario()
        {
            try
            {
                var datos = await _kpiRepository.GetRotacionInventarioAsync();
                return Ok(new
                {
                    kpi = "Rotación de Inventario",
                    descripcion = "Equipos utilizados / Equipos disponibles",
                    unidad = "%",
                    datos
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener los datos de rotación de inventario", error = ex.Message });
            }
        }
    }
}
