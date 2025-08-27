using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteController : ControllerBase
    {
        private readonly IReporteRepository _reporteRepository;

        public ReporteController(IReporteRepository reporteRepository)
        {
            _reporteRepository = reporteRepository;
        }

        // GET: api/reporte
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reportes = await _reporteRepository.ObtenerList();
            return Ok(reportes);
        }

        // GET: api/reporte/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var reporte = await _reporteRepository.ObtenerId(id);
            if (reporte == null)
                return NotFound("Reporte no encontrado");

            return Ok(reporte);
        }

        // POST: api/reporte
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Reporte reporte)
        {
            await _reporteRepository.Crear(reporte);
            return CreatedAtAction(nameof(GetById), new { id = reporte.Id }, reporte);
        }

        // PUT: api/reporte/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Reporte reporte)
        {
            var existente = await _reporteRepository.ObtenerId(id);
            if (existente == null)
                return NotFound("Reporte no encontrado");

            reporte.Id = id;
            await _reporteRepository.Actualizar(reporte);

            return NoContent();
        }
    }
}
