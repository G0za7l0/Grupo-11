using System.Diagnostics;

namespace Grupo11.Middleware
{
    /// <summary>
    /// Middleware de logging: registra cada petición HTTP (método, ruta, duración y código de estado).
    /// Se ejecuta en AMBAS direcciones del pipeline: antes de invocar al siguiente (request)
    /// y después de recibir la respuesta (response). Patrón: Chain of Responsibility.
    /// </summary>
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;         // Referencia al siguiente middleware en la cadena
        private readonly ILogger<LoggingMiddleware> _logger;

        // El constructor recibe el siguiente delegado vía DI (inyección de dependencias automática)
        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Método requerido por el pipeline de ASP.NET Core.
        /// Usa async/await para no bloquear hilos (buena práctica de rendimiento).
        /// </summary>
        public async Task InvokeAsync(HttpContext context)
        {
            var sw = Stopwatch.StartNew(); // Medir tiempo de respuesta

            // ── LÓGICA ANTES de pasar al siguiente middleware ──
            _logger.LogInformation(
                "[REQUEST]  {Method} {Path} | IP: {IP}",
                context.Request.Method,
                context.Request.Path,
                context.Connection.RemoteIpAddress);

            // Ceder el control al siguiente componente del pipeline
            await _next(context);

            // ── LÓGICA DESPUÉS de que la respuesta regresa ──
            sw.Stop();
            _logger.LogInformation(
                "[RESPONSE] {Method} {Path} | Status: {Status} | Duración: {Ms}ms",
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                sw.ElapsedMilliseconds);
        }
    }

    /// <summary>
    /// Extensión UseLogging() para registrar el middleware de forma expresiva en Program.cs.
    /// Sigue el patrón UseX de ASP.NET Core (p.ej. app.UseLogging()).
    /// </summary>
    public static class LoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogging(this IApplicationBuilder builder)
            => builder.UseMiddleware<LoggingMiddleware>();
    }
}
