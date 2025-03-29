using HR_API.DTO.Response;

namespace HR_API.Services.Interface
{
    public interface IPayrollProccessingServices
    {
        public Task<GenericApiResponse<List<PayrollReponse>>> listPayrollData();
    }
}
