using HR_API.DTO.Request;
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
        public async Task<GenericApiResponse<List<PayrollReponse>>> ListSalaryStructure()
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
                        BaseSalary = p.BaseSalary,
                        Deductions = p.Deductions,
                        NetSalary = p.NetSalary,
                        Bonuses = p.Bonuses,
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

        async Task<GenericApiResponse<List<PayrollReponse>>> IPayrollProccessingServices.GetSalaryStructure(int? id)
        {
            GenericApiResponse<List<PayrollReponse>> response = new();
            List<PayrollReponse> listpayroll = new();
            try
            {
                if(id == null)
                {
                    response.status = -1;
                    response.ErrorMessage = "Empty ID";
                    _logger.LogInformation("ID is null");
                }
                else
                {
                    listpayroll = await _dbContext.Payrolls
                        .Where(p => p.PayrollId == id)
                        .Select(p => new PayrollReponse
                        {
                            PayrollId = p.PayrollId,
                            EmployeeId = p.EmployeeId,
                            BaseSalary = p.BaseSalary,
                            Deductions = p.Deductions,
                            NetSalary = p.NetSalary,
                            Bonuses = p.Bonuses,
                        }).ToListAsync();
                    response.status = 200;
                    response.ErrorDesc = "Successfully Fetched!!!";
                    response.Data = listpayroll;
                }
            }catch(Exception ex)
            {
                response.status = 400;
                response.ErrorMessage = "Error while fetching PAYROLL details!!!";
                _logger.LogInformation(ex, "-Exception from " + ex.TargetSite.Name + " ERROR in Get method: " + ex.Message);
            }
            return response;
        }


        async Task<GenericApiResponse<PayrollReponse>> IPayrollProccessingServices.CreateSalaryStructure(PayrollRequest addPayroll)
        {
            GenericApiResponse<PayrollReponse> response = new();
            try
            {
                if(addPayroll == null)
                {
                    response.status = -1;
                    response.ErrorMessage = "Empty Request";
                    _logger.LogInformation("Request is null");
                }
                else
                {
                    Payroll payroll = new()
                    {
                        EmployeeId = addPayroll.EmployeeId,
                        BaseSalary = addPayroll.BaseSalary,
                        Deductions = addPayroll.Deductions,
                        NetSalary = addPayroll.NetSalary,
                        Bonuses = addPayroll.Bonuses,
                    };
                    await _dbContext.Payrolls.AddAsync(payroll);
                    await _dbContext.SaveChangesAsync();
                    response.status = 200;
                    response.ErrorDesc = "Successfully Added!!!";
                }
            }catch(Exception ex)
            {
                response.status = 400;
                response.ErrorMessage = "Error while adding PAYROLL details!!!";
                _logger.LogInformation(ex, "-Exception from " + ex.TargetSite.Name + " ERROR in Post method: " + ex.Message);
            }
            return response;
        }

        async Task<GenericApiResponse<PayrollReponse>> IPayrollProccessingServices.DeleteSalaryStructure(int? id)
        {
            GenericApiResponse<PayrollReponse> response = new();
            try
            {
                var delete = await _dbContext.Payrolls.FirstOrDefaultAsync(p => p.PayrollId == id);
                if(delete != null)
                {
                    _dbContext.Payrolls.Remove(delete);
                    await _dbContext.SaveChangesAsync();
                    response.status = 200;
                    response.ErrorDesc = "Successfully Deleted!!!";
                }
                else
                {
                    response.status = 1;
                    response.ErrorDesc = "Not Found!!!";
                }
            }
            catch(Exception ex)
            {
                response.status = 400;
                response.ErrorMessage = "Error while deleting PAYROLL details!!!";
                _logger.LogInformation(ex, "-Exception from " + ex.TargetSite.Name + " ERROR in Delete method: " + ex.Message);
            }
            return response;
        } 
        async Task<GenericApiResponse<PayrollReponse>> IPayrollProccessingServices.UpdateSalaryStructure(PayrollRequest updatePayroll)
        {
            GenericApiResponse<PayrollReponse> response = new();
            try
            {
                var update =  _dbContext.Payrolls.Where(p => p.PayrollId == updatePayroll.PayrollId).First();
                if (update != null)
                {
                    update.EmployeeId = updatePayroll.EmployeeId;
                    update.BaseSalary = updatePayroll.BaseSalary;
                    update.SalaryMonth = updatePayroll.SalaryMonth;
                    update.Deductions = updatePayroll.Deductions;
                    update.NetSalary = updatePayroll.NetSalary;
                    update.Bonuses = updatePayroll.Bonuses;
                    await _dbContext.SaveChangesAsync();
                    response.status = 200;
                    response.ErrorDesc = "Successfully Updated!!!";
                }
                else
                {
                    response.status = 1;
                    response.ErrorDesc = "Not Found!!!";
                }
            }
            catch (Exception ex)
            {
                response.status = 400;
                response.ErrorMessage = "Error while updating PAYROLL details!!!";
                _logger.LogInformation(ex, "-Exception from " + ex.TargetSite.Name + " ERROR in Update method: " + ex.Message);
            }
            return response;
        }

        //Implement Payslip Generation(PDF/downloadable payslips).
        //Integrate Tax & Compliance(auto-calculate deductions).
    }
}
