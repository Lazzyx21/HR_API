using HR_API.DTO.Response;
using HR_API.DTO.Request;
namespace HR_API.Services.Interface
{
    public interface IEmployeeManagementServices
    {
        public Task<GenericApiResponse<List<EmployeeDetailsResponse>>> employeeProfileAsync();

        public Task<GenericApiResponse<AttendanceResponse>> createAttendanceAsync(AttendanceRequest attendanceRequest);
    }
}
