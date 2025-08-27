using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacionController : ControllerBase
    {
        private readonly INotificacionRepository _notificacionRepository;

        public NotificacionController(INotificacionRepository notificacionRepository)
        {
            _notificacionRepository = notificacionRepository;
        }

        // GET: api/notificacion
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var notificaciones = await _notificacionRepository.ObtenerList();
            return Ok(notificaciones);
        }

        // GET: api/notificacion/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var notificacion = await _notificacionRepository.ObtenerId(id);
            if (notificacion == null)
                return NotFound("Notificación no encontrada");

            return Ok(notificacion);
        }

        // POST: api/notificacion
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Notificacion notificacion)
        {
            await _notificacionRepository.Crear(notificacion);
            return CreatedAtAction(nameof(GetById), new { id = notificacion.Id }, notificacion);
        }

        // PUT: api/notificacion/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Notificacion notificacion)
        {
            var existente = await _notificacionRepository.ObtenerId(id);
            if (existente == null)
                return NotFound("Notificación no encontrada");

            notificacion.Id = id;
            await _notificacionRepository.Actualizar(notificacion);

            return NoContent();
        }
    }
}
