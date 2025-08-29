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
    public class EventosController : ControllerBase
    {
        private readonly EventoUseCases _eventoUseCases;

        public EventosController(EventoUseCases eventoUseCases)
        {
            _eventoUseCases = eventoUseCases ?? throw new ArgumentNullException(nameof(eventoUseCases));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evento>>> ObtenerTodos()
        {
            var eventos = await _eventoUseCases.ObtenerEventos();
            return Ok(eventos);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Evento>> ObtenerPorId(Guid id)
        {
            var evento = await _eventoUseCases.ObtenerEventoPorId(id);
            if (evento is null) return NotFound();
            return Ok(evento);
        }

        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] Evento evento)
        {
            var creado = await _eventoUseCases.CrearEvento(evento);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, creado);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Actualizar(Guid id, [FromBody] Evento evento)
        {
            evento.Id = id;
            try
            {
                var actualizado = await _eventoUseCases.ActualizarEvento(evento);
                return Ok(actualizado);
            }
            catch (Exception ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
        }
    }
}
