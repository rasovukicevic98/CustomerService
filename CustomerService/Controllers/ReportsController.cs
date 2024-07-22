using CustomerService.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _service;

        public ReportsController(IReportService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsCsv()
        {
            var res = await _service.GenerateReportAsCsv();
            if (res.IsFailure)
            {
                return Ok(res.Error);
            }
            var memoryStream = res.Value;
            var fileName = "CouponUsageReport.csv";

            return File(memoryStream, "text/csv", fileName);
        }

        [HttpGet("all-agents-report")]
        public async Task<IActionResult> GetAllAgents()
        {
            var res = await _service.GenerateReportForAllAgents();
            if (res.IsFailure)
            {
                return BadRequest(res.Error);
            }
            return Ok(res.Value);
        }
    }
}
