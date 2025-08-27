using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IEventoRepository _eventoRepository;

        public EventoController(IEventoRepository eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }

        // GET: api/evento
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var eventos = await _eventoRepository.ObtenerList();
            return Ok(eventos);
        }

        // GET: api/evento/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var evento = await _eventoRepository.ObtenerId(id);
            if (evento == null)
                return NotFound("Evento no encontrado");

            return Ok(evento);
        }

        // POST: api/evento
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Evento evento)
        {
            await _eventoRepository.Crear(evento);
            return CreatedAtAction(nameof(GetById), new { id = evento.Id }, evento);
        }

        // PUT: api/evento/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Evento evento)
        {
            var existente = await _eventoRepository.ObtenerId(id);
            if (existente == null)
                return NotFound("Evento no encontrado");

            evento.Id = id; // aseguramos que se actualice el correcto
            await _eventoRepository.Actualizar(evento);

            return NoContent();
        }
    }
}
