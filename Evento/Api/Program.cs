using Domain.Interfaces;
using Infraestructure.Data;
using Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Aplication.UsesCases; // 👈 importa tu namespace de UseCases

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Conexion Base Datos
builder.Services.AddDbContext<BdContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("conn"),
    b => b.MigrationsAssembly("Infraestructure"))
);

// Registro del repositorio
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IEventoRepository, EventoRepository>();
builder.Services.AddScoped<IInscripcionRepository, InscripcionRepository>();
builder.Services.AddScoped<IPagoRepository, PagoRepository>();
builder.Services.AddScoped<IEncuestaRepository, EncuestaRepository>();
builder.Services.AddScoped<INotificacionRepository, NotificacionRepository>();
builder.Services.AddScoped<IReporteRepository, ReporteRepository>();

// Registro de los casos de uso (UseCases) 👇
builder.Services.AddScoped<UsuarioUseCases>();
builder.Services.AddScoped<EventoUseCases>();
builder.Services.AddScoped<InscripcionUseCases>();
builder.Services.AddScoped<PagoUseCases>();
builder.Services.AddScoped<EncuestaUseCases>();
builder.Services.AddScoped<NotificacionUseCases>();
builder.Services.AddScoped<ReporteUseCases>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); 

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
if (!Directory.Exists(uploadsPath))
{
    Directory.CreateDirectory(uploadsPath);
}
// Servir archivos estáticos desde /Uploads
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(uploadsPath),
    RequestPath = "/uploads"
});

app.Run();
