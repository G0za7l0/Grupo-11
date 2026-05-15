using Grupo11.Security;

namespace Grupo11.Middleware
{
    /// <summary>
    /// Middleware de autenticación por sesión.
    /// Complementa el ActionFilter existente (AuthControllerAttribute) validando la sesión
    /// a nivel de pipeline ANTES de que llegue al controlador. Rutas públicas se excluyen
    /// mediante una lista blanca (whitelist) para no cortocircuitar el login.
    ///
    /// Flujo: Request → ErrorHandling → Logging → SessionAuth → Controlador → Response
    /// </summary>
    public class SessionAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<SessionAuthMiddleware> _logger;

        // Rutas que NO requieren sesión activa (lista blanca pública)
        private static readonly HashSet<string> _publicRoutes = new(StringComparer.OrdinalIgnoreCase)
        {
            "/security/login",
            "/swagger",
            "/swagger/index.html",
            "/swagger/v1/swagger.json"
        };

        public SessionAuthMiddleware(RequestDelegate next, ILogger<SessionAuthMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value ?? string.Empty;

            // Verificar si la ruta es pública (no requiere autenticación)
            bool esRutaPublica = _publicRoutes.Any(r => path.StartsWith(r, StringComparison.OrdinalIgnoreCase));

            if (!esRutaPublica)
            {
                // Leer la clave de sesión almacenada en la cookie/sesión
                var sessionKey = context.Session.GetString("sessionKey");

                if (!AuthNetCore.Authenticate(sessionKey))
                {
                    // ── CORTOCIRCUITO: no llamar a _next, devolver 401 directamente ──
                    _logger.LogWarning(
                        "[AUTH] Acceso denegado a {Path} — sesión inválida o inexistente", path);

                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsJsonAsync(new
                    {
                        status = 401,
                        error = "No autorizado",
                        detalle = "Debe iniciar sesión para acceder a este recurso"
                    });
                    return; // Detener el pipeline (cortocircuito)
                }
            }

            // Sesión válida o ruta pública → continuar al siguiente middleware
            await _next(context);
        }
    }

    /// <summary>
    /// Extensión UseSessionAuth() para registrarlo en Program.cs.
    /// </summary>
    public static class SessionAuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseSessionAuth(this IApplicationBuilder builder)
            => builder.UseMiddleware<SessionAuthMiddleware>();
    }
}
