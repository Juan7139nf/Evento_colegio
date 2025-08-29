using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivosController : ControllerBase
    {
        private readonly string _rutaBase = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

        [HttpPost("subir")]
        public async Task<IActionResult> SubirArchivo(IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
                return BadRequest("No se ha enviado ningún archivo.");

            try
            {
                if (!Directory.Exists(_rutaBase))
                    Directory.CreateDirectory(_rutaBase);

                // 👉 Generar nombre único con Guid + extensión original
                var extension = Path.GetExtension(archivo.FileName);
                var nuevoNombre = $"{Guid.NewGuid()}{extension}";
                var rutaArchivo = Path.Combine(_rutaBase, nuevoNombre);

                using (var stream = new FileStream(rutaArchivo, FileMode.Create))
                {
                    await archivo.CopyToAsync(stream);
                }

                // 👉 Construir URL pública (ej: https://localhost:5001/uploads/{guid}.jpg)
                var urlArchivo = $"{Request.Scheme}://{Request.Host}/uploads/{nuevoNombre}";

                return Ok(new
                {
                    mensaje = "Archivo subido correctamente",
                    nombreOriginal = archivo.FileName,
                    nombreGuardado = nuevoNombre,
                    url = urlArchivo
                });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Error al subir archivo: {ex.Message}");
            }
        }
    }
}
