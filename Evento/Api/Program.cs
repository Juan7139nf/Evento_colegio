using Domain.Interfaces;
using Infraestructure.Data;
using Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Aplication.UsesCases;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// 🔧 Servicios
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Evita errores de serialización por referencias circulares
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🌐 CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("Cors", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// 🗄️ Base de datos
builder.Services.AddDbContext<BdContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("conn"),
    b => b.MigrationsAssembly("Infraestructure"))
);

// 📦 Repositorios
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IEventoRepository, EventoRepository>();
builder.Services.AddScoped<IInscripcionRepository, InscripcionRepository>();
builder.Services.AddScoped<IPagoRepository, PagoRepository>();
builder.Services.AddScoped<IEncuestaRepository, EncuestaRepository>();
builder.Services.AddScoped<INotificacionRepository, NotificacionRepository>();
builder.Services.AddScoped<IReporteRepository, ReporteRepository>();

// 🧠 Casos de uso
builder.Services.AddScoped<UsuarioUseCases>();
builder.Services.AddScoped<EventoUseCases>();
builder.Services.AddScoped<InscripcionUseCases>();
builder.Services.AddScoped<PagoUseCases>();
builder.Services.AddScoped<EncuestaUseCases>();
builder.Services.AddScoped<NotificacionUseCases>();
builder.Services.AddScoped<ReporteUseCases>();
builder.Services.AddScoped<DashboardUseCases>();

var app = builder.Build();

// 🚀 Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/error"); // Manejo de errores en producción
}

app.UseHttpsRedirection();
app.UseCors("Cors");
app.UseAuthorization();
app.MapControllers();

// 📁 Archivos estáticos
var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
if (!Directory.Exists(uploadsPath))
{
    Directory.CreateDirectory(uploadsPath);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(uploadsPath),
    RequestPath = "/uploads"
});

app.Run();
