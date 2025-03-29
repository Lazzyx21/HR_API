using HR_API.DTO.Response;
using HR_API.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HR_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayrollController : Controller
    {
        private readonly ILogger<PayrollController> _logger;
        private readonly IPayrollProccessingServices _payroll;

        
        public PayrollController(ILogger<PayrollController> logger, IPayrollProccessingServices _services)
        {
            _logger = logger;
            _payroll = _services;
        }
        [HttpGet("GET-Payroll")]
        public async Task<GenericApiResponse<List<PayrollReponse>>> listPayrollData()
        {
            _logger.LogInformation("Fetch Payroll Details");
            return await _payroll.listPayrollData();
        }

    }
}
