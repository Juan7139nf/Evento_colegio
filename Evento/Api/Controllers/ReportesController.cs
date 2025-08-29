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
    public class ReportesController : ControllerBase
    {
        private readonly IReporteRepository _reporteRepository;

        public ReportesController(IReporteRepository reporteRepository)
        {
            _reporteRepository = reporteRepository ?? throw new ArgumentNullException(nameof(reporteRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reporte>>> ObtenerTodos()
        {
            var reportes = await _reporteRepository.ObtenerList();
            return Ok(reportes);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Reporte>> ObtenerPorId(Guid id)
        {
            var reporte = await _reporteRepository.ObtenerId(id);
            if (reporte is null) return NotFound();
            return Ok(reporte);
        }

        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] Reporte reporte)
        {
            await _reporteRepository.Crear(reporte);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = reporte.Id }, reporte);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Actualizar(Guid id, [FromBody] Reporte reporte)
        {
            var existente = await _reporteRepository.ObtenerId(id);
            if (existente is null) return NotFound();

            reporte.Id = id;
            await _reporteRepository.Actualizar(reporte);
            return NoContent();
        }
    }
}
