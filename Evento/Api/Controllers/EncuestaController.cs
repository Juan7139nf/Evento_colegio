using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncuestaController : ControllerBase
    {
        private readonly IEncuestaRepository _encuestaRepository;

        public EncuestaController(IEncuestaRepository encuestaRepository)
        {
            _encuestaRepository = encuestaRepository;
        }

        // GET: api/encuesta
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var encuestas = await _encuestaRepository.ObtenerList();
            return Ok(encuestas);
        }

        // GET: api/encuesta/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var encuesta = await _encuestaRepository.ObtenerId(id);
            if (encuesta == null)
                return NotFound("Encuesta no encontrada");

            return Ok(encuesta);
        }

        // POST: api/encuesta
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Encuesta encuesta)
        {
            await _encuestaRepository.Crear(encuesta);
            return CreatedAtAction(nameof(GetById), new { id = encuesta.Id }, encuesta);
        }

        // PUT: api/encuesta/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Encuesta encuesta)
        {
            var existente = await _encuestaRepository.ObtenerId(id);
            if (existente == null)
                return NotFound("Encuesta no encontrada");

            encuesta.Id = id;
            await _encuestaRepository.Actualizar(encuesta);

            return NoContent();
        }
    }
}
