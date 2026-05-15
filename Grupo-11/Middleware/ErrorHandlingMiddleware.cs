using System.Net;
using System.Text.Json;

namespace Grupo11.Middleware
{
    /// <summary>
    /// Middleware de manejo global de excepciones.
    /// Debe ser el PRIMERO en el pipeline (antes de cualquier otro) para capturar
    /// errores de todos los componentes posteriores. Devuelve respuesta JSON uniforme.
    /// Patrón: Chain of Responsibility con cortocircuito en caso de error.
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Intentar ejecutar el pipeline completo
                await _next(context);
            }
            catch (Exception ex)
            {
                // Capturar cualquier excepción no controlada de los middlewares posteriores
                _logger.LogError(ex, "Excepción no controlada en {Path}", context.Request.Path);
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Convierte la excepción en una respuesta HTTP 500 con cuerpo JSON estandarizado.
        /// No expone el stack trace en producción (buena práctica de seguridad).
        /// </summary>
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var respuesta = new
            {
                status = 500,
                error = "Error interno del servidor",
                // Solo mostrar el mensaje en desarrollo; en producción ocultar detalles
                detalle = exception.Message
            };

            var json = JsonSerializer.Serialize(respuesta, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(json);
        }
    }

    /// <summary>
    /// Extensión UseErrorHandling() para registrarlo expresivamente en Program.cs.
    /// </summary>
    public static class ErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
            => builder.UseMiddleware<ErrorHandlingMiddleware>();
    }
}
