using Aplication.DTOs;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleEventoController : ControllerBase
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly IInscripcionRepository _inscripcionRepository;
        private readonly IPagoRepository _pagoRepository;
        private readonly IEncuestaRepository _encuestaRepository;
        public DetalleEventoController(
            IEventoRepository eventoRepository,
            IInscripcionRepository inscripcionRepository,
            IPagoRepository pagoRepository,
            IEncuestaRepository encuestaRepository)
        {
            _eventoRepository = eventoRepository;
            _inscripcionRepository = inscripcionRepository;
            _pagoRepository = pagoRepository;
            _encuestaRepository = encuestaRepository;
        }

        [HttpGet("{idEvento:guid}/{idUsuario:guid}")]
        public async Task<ActionResult<EventoDetalleDto>> ObtenerDetalle(Guid idEvento, Guid idUsuario)
        {
            // 1. Obtener Evento
            var evento = await _eventoRepository.ObtenerId(idEvento);
            if (evento is null) return NotFound("Evento no encontrado");

            // 2. Buscar inscripción del usuario
            var inscripcion = await _inscripcionRepository.ObtenerPorEventoYUsuario(idEvento, idUsuario);

            // 3. Buscar pago (si hay inscripción)
            var pago = inscripcion != null
                ? await _pagoRepository.ObtenerPorInscripcion(inscripcion.Id)
                : null;

            // 4. Buscar encuesta (si hay inscripción)
            var encuesta = inscripcion != null
                ? await _encuestaRepository.ObtenerPorEventoEInscripcion(idEvento, inscripcion.Id)
                : null;

            // 5. Armar DTO
            var detalle = new EventoDetalleDto
            {
                Evento = MapEvento(evento),
                Inscripcion = inscripcion != null ? MapInscripcion(inscripcion) : null,
                Pago = pago != null ? MapPago(pago) : null,
                Encuesta = encuesta != null ? MapEncuesta(encuesta) : null
            };

            return Ok(detalle);
        }

        // 🔹 Mappers (idealmente usar AutoMapper, aquí te lo pongo manual)
        private EventoDto MapEvento(Domain.Entities.Evento e) => new EventoDto
        {
            Id = e.Id,
            Nombre_Evento = e.Nombre_Evento,
            Tipo = e.Tipo,
            Fecha = e.Fecha,
            Lugar = e.Lugar,
            Capacidad_Max = e.Capacidad_Max,
            Estado = e.Estado,
            Encuesta = e.Encuesta,
            Costo = e.Costo,
            Content = e.Content?.ConvertAll(c => new SeccionDto { Orden = c.Orden, Title = c.Title, Content = c.Content }),
            Archivos = e.Archivos?.ConvertAll(a => new ArchivoDto { Orden = a.Orden, Url = a.Url, Tipo = a.Tipo })
        };

        private InscripcionDto MapInscripcion(Domain.Entities.Inscripcion i) => new InscripcionDto
        {
            Id = i.Id,
            Fecha_Inscripcion = i.Fecha_Inscripcion,
            Estado = i.Estado,
            Id_Usuario = i.Id_Usuario,
            Id_Evento = i.Id_Evento
        };

        private PagoDto MapPago(Domain.Entities.Pago p) => new PagoDto
        {
            Id = p.Id,
            Monto = p.Monto,
            Fecha_Pago = p.Fecha_Pago,
            Metodo = p.Metodo,
            Estado = p.Estado,
            Id_Inscripcion = p.Id_Inscripcion
        };

        private EncuestaDto MapEncuesta(Domain.Entities.Encuesta e) => new EncuestaDto
        {
            Id = e.Id,
            Fecha_Creacion = e.Fecha_Creacion,
            Value = e.Value,
            Id_Evento = e.Id_Evento,
            Id_Inscripcion = e.Id_Inscripcion
        };
    }
}
