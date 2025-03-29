using HR_API.DTO.Response;
using HR_API.Models;
using HR_API.Services.Interface;
using Microsoft.EntityFrameworkCore;
namespace HR_API.Services
{
    public class PayrollProccessing : IPayrollProccessingServices
    {
        private readonly ILogger<PayrollProccessing> _logger;
        private readonly IConfiguration _configuration;
        private readonly ErptestingContext _dbContext;

        public PayrollProccessing(ILogger<PayrollProccessing> logger, IConfiguration configuration, ErptestingContext dbContext)
        {
            _logger = logger;
            _configuration = configuration;
            _dbContext = dbContext;
        }

        //Configure Salary Structure(basic pay, deductions, bonuses).
        public async Task<GenericApiResponse<List<PayrollReponse>>> listPayrollData()
        {
            GenericApiResponse<List<PayrollReponse>> response = new();
            List<PayrollReponse> listpayroll = new();
            try
            {
                listpayroll = await _dbContext.Payrolls
                    .Select(p => new PayrollReponse
                    {
                        PayrollId = p.PayrollId,
                        EmployeeId = p.EmployeeId,  
                        SalaryMonth = p.SalaryMonth,
                        BaseSalary = p.BaseSalary,
                        Deductions = p.Deductions,
                        NetSalary = p.NetSalary,
                        Bonuses = p.Bonuses,
                        PaymentStatus = p.PaymentStatus
                    }).ToListAsync();
                response.status = 200;
                response.ErrorDesc = "Successfully Fetched!!!";
                response.Data = listpayroll;
            }catch(Exception ex)
            {
                response.status = -1;
                response.ErrorMessage = "Error while fetching PAYROLL details!!!";
                _logger.LogError(ex, "-Exception from " + ex.TargetSite.Name + " ERROR in Get method: " + ex.Message);
            }
            return response;
        }

        //Implement Payslip Generation(PDF/downloadable payslips).
        //Integrate Tax & Compliance(auto-calculate deductions).
    }
}
