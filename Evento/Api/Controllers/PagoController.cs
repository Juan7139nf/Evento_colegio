using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoController : ControllerBase
    {
        private readonly IPagoRepository _pagoRepository;

        public PagoController(IPagoRepository pagoRepository)
        {
            _pagoRepository = pagoRepository;
        }

        // GET: api/pago
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pagos = await _pagoRepository.ObtenerList();
            return Ok(pagos);
        }

        // GET: api/pago/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var pago = await _pagoRepository.ObtenerId(id);
            if (pago == null)
                return NotFound("Pago no encontrado");

            return Ok(pago);
        }

        // POST: api/pago
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Pago pago)
        {
            await _pagoRepository.Crear(pago);
            return CreatedAtAction(nameof(GetById), new { id = pago.Id }, pago);
        }

        // PUT: api/pago/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Pago pago)
        {
            var existente = await _pagoRepository.ObtenerId(id);
            if (existente == null)
                return NotFound("Pago no encontrado");

            pago.Id = id;
            await _pagoRepository.Actualizar(pago);

            return NoContent();
        }
    }
}
