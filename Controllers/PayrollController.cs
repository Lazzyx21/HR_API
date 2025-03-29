using HR_API.DTO.Request;
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
        public async Task<GenericApiResponse<List<PayrollReponse>>> ListSalaryStructure()
        {
            _logger.LogInformation("Fetch Payroll Details");
            return await _payroll.ListSalaryStructure();
        }

        [HttpGet("{id}/Get-id-Payroll")]
        public async Task<GenericApiResponse<List<PayrollReponse>>>GetSalaryStructure(int? id)
        {
            _logger.LogInformation("Fetch Payroll Details");
            return await _payroll.GetSalaryStructure(id);
        }

        [HttpPut("Create-Payroll")]
        public async Task<GenericApiResponse<PayrollReponse>> CreateSalaryStructure(PayrollRequest addPayroll)
        {
            _logger.LogInformation("Create Payroll");
            return await _payroll.CreateSalaryStructure(addPayroll);
        }

        [HttpPost("POST-Payroll")]
        public async Task<GenericApiResponse<PayrollReponse>> UpdateSalaryStructure(PayrollRequest updatePayroll)
        {
            _logger.LogInformation("Update Payroll");
            return await _payroll.UpdateSalaryStructure(updatePayroll);
        }

        [HttpDelete("{id}/DELETE-Payroll")]
        public async Task<GenericApiResponse<PayrollReponse>> DeleteSalaryStructure(int? id)
        {
            _logger.LogInformation("Delete Payroll");
            return await _payroll.DeleteSalaryStructure(id);
        }




    }
}
