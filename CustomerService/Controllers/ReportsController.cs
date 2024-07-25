using CustomerService.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CustomerService.Controllers
{
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _service;

        public ReportsController(IReportService service)
        {
            _service = service;
        }

        /// <summary>
        /// Generates a CSV report for a logged-in Agent.
        /// </summary>
        /// <returns>A CSV report for a logged-in Agent.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAsCsv()
        {
            var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            var res = await _service.GenerateReportAsCsvForEndpointAsync(email);
            if (res.IsFailure)
            {
                return Ok(res.Error);
            }
            var memoryStream = res.Value;
            var fileName = "CouponUsageReport.csv";

            return File(memoryStream, "text/csv", fileName);
        }

        /// <summary>
        /// Generates and returns a report that contains all users that have used their coupon, no matter the Agent.
        /// </summary>
        /// <returns>A report for all Agents.</returns>
        [HttpGet("all-agents-report")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
