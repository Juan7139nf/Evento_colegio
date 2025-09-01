using Aplication.UsesCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardUseCases _dashboardUseCases;

        public DashboardController(DashboardUseCases dashboardUseCases)
        {
            _dashboardUseCases = dashboardUseCases;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboard()
        {
            var stats = await _dashboardUseCases.ObtenerEstadisticas();
            return Ok(stats);
        }
    }
}
