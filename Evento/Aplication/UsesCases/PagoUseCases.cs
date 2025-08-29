using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplication.UsesCases
{
    public class PagoUseCases
    {
        private readonly IPagoRepository _pagoRepository;

        public PagoUseCases(IPagoRepository pagoRepository)
        {
            _pagoRepository = pagoRepository;
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
