using Aplication.DTOs;
using Aplication.UsesCases;
using Domain.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("Cors")]
    public class InscripcionesController : ControllerBase
    {
        private readonly InscripcionUseCases _inscripcionUseCases;

        public InscripcionesController(InscripcionUseCases inscripcionUseCases)
        {
            _inscripcionUseCases = inscripcionUseCases ?? throw new ArgumentNullException(nameof(inscripcionUseCases));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InscripcionDto>>> ObtenerTodos()
        {
            var inscripciones = await _inscripcionUseCases.ObtenerInscripciones();
            return Ok(inscripciones);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Inscripcion>> ObtenerPorId(Guid id)
        {
            var inscripcion = await _inscripcionUseCases.ObtenerInscripcionPorId(id);
            if (inscripcion is null)
                return NotFound(new { mensaje = "Inscripción no encontrada" });

            return Ok(inscripcion);
        }

        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] Inscripcion inscripcion)
        {
            try
            {
                var creada = await _inscripcionUseCases.CrearInscripcion(inscripcion);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = creada.Id }, new
                {
                    success = true,
                    message = "Inscripción creada correctamente",
                    data = creada
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Actualizar(Guid id, [FromBody] Inscripcion inscripcion)
        {
            inscripcion.Id = id;

            try
            {
                var actualizada = await _inscripcionUseCases.ActualizarInscripcion(inscripcion);
                return Ok(new
                {
                    success = true,
                    message = "Inscripción actualizada correctamente",
                    data = actualizada
                });
            }
            catch (Exception ex)
            {
                return NotFound(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
    }
}
