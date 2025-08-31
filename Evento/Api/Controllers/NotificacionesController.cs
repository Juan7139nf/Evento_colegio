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

    public class NotificacionesController : ControllerBase
    {
        private readonly NotificacionUseCases _notificacionUseCases;

        public NotificacionesController(NotificacionUseCases notificacionUseCases)
        {
            _notificacionUseCases = notificacionUseCases ?? throw new ArgumentNullException(nameof(notificacionUseCases));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notificacion>>> ObtenerTodos()
        {
            var notificaciones = await _notificacionUseCases.ObtenerNotificaciones();
            return Ok(notificaciones);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Notificacion>> ObtenerPorId(Guid id)
        {
            var notificacion = await _notificacionUseCases.ObtenerNotificacionPorId(id);
            if (notificacion is null) return NotFound();
            return Ok(notificacion);
        }

        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] Notificacion notificacion)
        {
            try
            {
                var creada = await _notificacionUseCases.CrearNotificacion(notificacion);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = creada.Id }, creada);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Actualizar(Guid id, [FromBody] Notificacion notificacion)
        {
            notificacion.Id = id;
            try
            {
                var actualizada = await _notificacionUseCases.ActualizarNotificacion(notificacion);
                return Ok(actualizada);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
