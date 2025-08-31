using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplication.UsesCases
{
    public class PagoUseCases
    {
        private readonly IPagoRepository _pagoRepository;
        private readonly IInscripcionRepository _inscripcionRepository;
        private readonly IEventoRepository _eventoRepository;

        public PagoUseCases(
            IPagoRepository pagoRepository,
            IInscripcionRepository inscripcionRepository,
            IEventoRepository eventoRepository)
        {
            _pagoRepository = pagoRepository;
            _inscripcionRepository = inscripcionRepository;
            _eventoRepository = eventoRepository;
        }

        public async Task<IEnumerable<Pago>> ObtenerPagos()
        {
            return await _pagoRepository.ObtenerList();
        }

        public async Task<Pago?> ObtenerPagoPorId(Guid id)
        {
            return await _pagoRepository.ObtenerId(id);
        }

        public async Task<Pago> RegistrarPago(Pago pago)
        {
            if (pago.Monto == null || pago.Monto <= 0)
                throw new Exception("El monto debe ser mayor a 0");

            if (string.IsNullOrWhiteSpace(pago.Metodo))
                throw new Exception("El método de pago es obligatorio");

            if (string.IsNullOrWhiteSpace(pago.Estado))
                throw new Exception("El estado del pago es obligatorio");

            // Validar duplicado
            var pagosExistentes = await _pagoRepository.ObtenerList();
            bool duplicado = pagosExistentes.Any(p =>
                p.Id_Inscripcion == pago.Id_Inscripcion &&
                p.Monto == pago.Monto &&
                p.Metodo.Equals(pago.Metodo, StringComparison.OrdinalIgnoreCase) &&
                p.Estado.Equals(pago.Estado, StringComparison.OrdinalIgnoreCase));

            if (duplicado)
                throw new Exception("Ya existe un pago con los mismos datos para esta inscripción.");

            // Validar monto contra el costo del evento
            var inscripcion = await _inscripcionRepository.ObtenerId(pago.Id_Inscripcion);
            if (inscripcion == null)
                throw new Exception("Inscripción no encontrada.");

            var evento = await _eventoRepository.ObtenerId(inscripcion.Id_Evento);
            if (evento == null)
                throw new Exception("Evento no encontrado.");

            if (evento.Costo != pago.Monto)
                throw new Exception($"El monto del pago ({pago.Monto}) no coincide con el costo del evento ({evento.Costo}).");

            await _pagoRepository.Crear(pago);
            return pago;
        }

        public async Task<Pago> ActualizarPago(Pago pago)
        {
            var existente = await _pagoRepository.ObtenerId(pago.Id);
            if (existente == null)
                throw new Exception("Pago no encontrado");

            await _pagoRepository.Actualizar(pago);
            return pago;
        }
    }
}
