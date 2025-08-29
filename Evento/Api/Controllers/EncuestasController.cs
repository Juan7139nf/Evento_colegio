using Aplication.UsesCases;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EncuestasController : ControllerBase
    {
        private readonly EncuestaUseCases _encuestaUseCases;

        public EncuestasController(EncuestaUseCases encuestaUseCases)
        {
            _encuestaUseCases = encuestaUseCases ?? throw new ArgumentNullException(nameof(encuestaUseCases));
        }

        /// <summary>
        /// Obtener todas las encuestas
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Encuesta>>> ObtenerTodos()
        {
            var encuestas = await _encuestaUseCases.ObtenerEncuestas();
            return Ok(encuestas);
        }

        /// <summary>
        /// Obtener una encuesta por su ID
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Encuesta>> ObtenerPorId(Guid id)
        {
            var encuesta = await _encuestaUseCases.ObtenerEncuestaPorId(id);
            if (encuesta is null) return NotFound($"Encuesta con Id {id} no encontrada");
            return Ok(encuesta);
        }

        /// <summary>
        /// Crear una nueva encuesta
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Encuesta>> Crear([FromBody] Encuesta encuesta)
        {
            var creada = await _encuestaUseCases.CrearEncuesta(encuesta);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = creada.Id }, creada);
        }

        /// <summary>
        /// Actualizar una encuesta existente
        /// </summary>
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Encuesta>> Actualizar(Guid id, [FromBody] Encuesta encuesta)
        {
            if (id != encuesta.Id) return BadRequest("El ID de la URL no coincide con el de la encuesta");

            var actualizada = await _encuestaUseCases.ActualizarEncuesta(encuesta);
            return Ok(actualizada);
        }
    }
}
