using Grupo11.Security;
using Grupo11.Middleware;

var builder = WebApplication.CreateBuilder(args);

// ── REGISTRO DE SERVICIOS ─────────────────────────────────────────────────────
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Sesiones en Memoria (requerido por SessionAuthMiddleware)
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(40);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Configuración CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

// Inicializar la conexión de la BD en AuthNetCore
AuthNetCore.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

// ── PIPELINE DE MIDDLEWARES (el orden es CRÍTICO) ─────────────────────────────
//
//  Request  →  [1]ErrorHandling  →  [2]Logging  →  [3]HTTPS  →  [4]CORS
//           →  [5]Swagger        →  [6]Session  →  [7]SessionAuth
//           →  [8]Controllers    →  Response regresa en orden inverso
//
// [1] Manejo global de errores — DEBE ser el primero para capturar excepciones
//     de todos los componentes posteriores del pipeline.
app.UseErrorHandling();

// [2] Logging de peticiones y respuestas
app.UseLogging();

// [3] Redirección a HTTPS
app.UseHttpsRedirection();

// [4] CORS — debe ir antes de cualquier middleware que genere respuestas con cabeceras
app.UseCors("AllowAll");

// [5] Swagger solo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// [6] Sesiones — DEBE ir antes de SessionAuthMiddleware (que lee la sesión)
app.UseSession();

// [7] Autenticación por sesión personalizada (middleware propio)
//     Valida que exista una sesión activa antes de llegar al controlador.
//     Rutas públicas (/security/login, /swagger) se omiten automáticamente.
app.UseSessionAuth();

// [8] Controladores MVC
app.MapControllers();

app.Run();
