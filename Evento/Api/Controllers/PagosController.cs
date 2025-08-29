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
    public class PagosController : ControllerBase
    {
        private readonly IPagoRepository _pagoRepository;

        public PagosController(IPagoRepository pagoRepository)
        {
            _pagoRepository = pagoRepository ?? throw new ArgumentNullException(nameof(pagoRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pago>>> ObtenerTodos()
        {
            var pagos = await _pagoRepository.ObtenerList();
            return Ok(pagos);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Pago>> ObtenerPorId(Guid id)
        {
            var pago = await _pagoRepository.ObtenerId(id);
            if (pago is null) return NotFound();
            return Ok(pago);
        }

        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] Pago pago)
        {
            await _pagoRepository.Crear(pago);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = pago.Id }, pago);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Actualizar(Guid id, [FromBody] Pago pago)
        {
            var existente = await _pagoRepository.ObtenerId(id);
            if (existente is null) return NotFound();

            pago.Id = id;
            await _pagoRepository.Actualizar(pago);
            return NoContent();
        }
    }
}
