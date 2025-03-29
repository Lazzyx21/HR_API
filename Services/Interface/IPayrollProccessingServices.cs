using HR_API.DTO.Request;
using HR_API.DTO.Response;

namespace HR_API.Services.Interface
{
    public interface IPayrollProccessingServices
    {
        public Task<GenericApiResponse<List<PayrollReponse>>> ListSalaryStructure();
        public Task<GenericApiResponse<List<PayrollReponse>>> GetSalaryStructure(int? id);
        public Task<GenericApiResponse<PayrollReponse>> CreateSalaryStructure(PayrollRequest addPayroll);
        public Task<GenericApiResponse<PayrollReponse>> UpdateSalaryStructure(PayrollRequest updatePayroll);
        public Task<GenericApiResponse<PayrollReponse>> DeleteSalaryStructure(int? id);
    }
}
