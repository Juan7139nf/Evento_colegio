using Aplication.UsesCases;
using Domain.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("Cors")]

    public class PagosController : ControllerBase
    {
        private readonly PagoUseCases _pagoUseCases;

        public PagosController(PagoUseCases pagoUseCases)
        {
            _pagoUseCases = pagoUseCases ?? throw new ArgumentNullException(nameof(pagoUseCases));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pago>>> ObtenerTodos()
        {
            var pagos = await _pagoUseCases.ObtenerPagos();
            return Ok(pagos);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Pago>> ObtenerPorId(Guid id)
        {
            var pago = await _pagoUseCases.ObtenerPagoPorId(id);
            if (pago is null) return NotFound();
            return Ok(pago);
        }

        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] Pago pago)
        {
            try
            {
                var creado = await _pagoUseCases.RegistrarPago(pago);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, creado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Actualizar(Guid id, [FromBody] Pago pago)
        {
            pago.Id = id;
            try
            {
                var actualizado = await _pagoUseCases.ActualizarPago(pago);
                return Ok(actualizado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
