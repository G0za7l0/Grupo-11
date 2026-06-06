using Grupo11.Security;
using Grupo11.Interfaces;
using Grupo11.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inyección de Dependencias - Dapper
var telecomConnectionString = builder.Configuration.GetConnectionString("TelecomConnection");

// Registrar IDbConnection apuntando a TelecomConnection
builder.Services.AddTransient<System.Data.IDbConnection>(sp => 
    new Microsoft.Data.SqlClient.SqlConnection(telecomConnectionString));

// Registrar el repositorio
builder.Services.AddScoped<IKpiRepository, KpiRepository>();

// Sesiones en Memoria
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
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Inicializar la conexión de la BD en AuthNetCore
AuthNetCore.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseSession();

app.MapControllers();

app.Run();
