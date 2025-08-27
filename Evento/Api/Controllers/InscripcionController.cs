using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InscripcionController : ControllerBase
    {
        private readonly IInscripcionRepository _inscripcionRepository;

        public InscripcionController(IInscripcionRepository inscripcionRepository)
        {
            _inscripcionRepository = inscripcionRepository;
        }

        // GET: api/inscripcion
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var inscripciones = await _inscripcionRepository.ObtenerList();
            return Ok(inscripciones);
        }

        // GET: api/inscripcion/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var inscripcion = await _inscripcionRepository.ObtenerId(id);
            if (inscripcion == null)
                return NotFound("Inscripción no encontrada");

            return Ok(inscripcion);
        }

        // POST: api/inscripcion
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Inscripcion inscripcion)
        {
            await _inscripcionRepository.Crear(inscripcion);
            return CreatedAtAction(nameof(GetById), new { id = inscripcion.Id }, inscripcion);
        }

        // PUT: api/inscripcion/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Inscripcion inscripcion)
        {
            var existente = await _inscripcionRepository.ObtenerId(id);
            if (existente == null)
                return NotFound("Inscripción no encontrada");

            inscripcion.Id = id;
            await _inscripcionRepository.Actualizar(inscripcion);

            return NoContent();
        }
    }
}
