using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplication.UsesCases
{
    public class ReporteUseCases
    {
        private readonly IReporteRepository _reporteRepository;

        public ReporteUseCases(IReporteRepository reporteRepository)
        {
            _reporteRepository = reporteRepository;
        }

        public async Task<IEnumerable<Reporte>> ObtenerReportes()
        {
            return await _reporteRepository.ObtenerList();
        }

        public async Task<Reporte?> ObtenerReportePorId(Guid id)
        {
            return await _reporteRepository.ObtenerId(id);
        }

        public async Task<Reporte> GenerarReporte(Reporte reporte)
        {
            if (string.IsNullOrWhiteSpace(reporte.Tipo))
                throw new Exception("El tipo de reporte es obligatorio");

            if (string.IsNullOrWhiteSpace(reporte.Archivo))
                throw new Exception("El archivo del reporte es obligatorio");

            await _reporteRepository.Crear(reporte);
            return reporte;
        }

        public async Task<Reporte> ActualizarReporte(Reporte reporte)
        {
            var existente = await _reporteRepository.ObtenerId(reporte.Id);
            if (existente == null)
                throw new Exception("Reporte no encontrado");

            await _reporteRepository.Actualizar(reporte);
            return reporte;
        }
    }
}
