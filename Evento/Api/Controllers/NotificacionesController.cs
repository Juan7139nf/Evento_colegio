using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificacionesController : ControllerBase
    {
        private readonly INotificacionRepository _notificacionRepository;

        public NotificacionesController(INotificacionRepository notificacionRepository)
        {
            _notificacionRepository = notificacionRepository ?? throw new ArgumentNullException(nameof(notificacionRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notificacion>>> ObtenerTodos()
        {
            var notificaciones = await _notificacionRepository.ObtenerList();
            return Ok(notificaciones);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Notificacion>> ObtenerPorId(Guid id)
        {
            var notificacion = await _notificacionRepository.ObtenerId(id);
            if (notificacion is null) return NotFound();
            return Ok(notificacion);
        }

        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] Notificacion notificacion)
        {
            await _notificacionRepository.Crear(notificacion);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = notificacion.Id }, notificacion);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Actualizar(Guid id, [FromBody] Notificacion notificacion)
        {
            var existente = await _notificacionRepository.ObtenerId(id);
            if (existente is null) return NotFound();

            notificacion.Id = id;
            await _notificacionRepository.Actualizar(notificacion);
            return NoContent();
        }

    }
}
