using HR_API.DTO.Request;
using HR_API.DTO.Response;
using HR_API.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HR_API.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class EmployeeDetailsController : Controller
    {
        private readonly ILogger<EmployeeDetailsController> _logger;
        private IEmployeeManagementServices _eDetails;
        public EmployeeDetailsController(ILogger<EmployeeDetailsController> logger, IEmployeeManagementServices _services)
        {
            _logger = logger;
            _eDetails = _services;
        }
        [HttpGet("GET-Details")]
        public async Task<GenericApiResponse<List<EmployeeDetailsResponse>>> employeeProfileAsync()
        {
            _logger.LogInformation("Fetch Employee Details");
            return await _eDetails.employeeProfileAsync();
        }

        [HttpPost("POST-Attendance")]
        public async Task<GenericApiResponse<AttendanceResponse>> createAttendanceAsync(AttendanceRequest attendanceRequest)
        {
            _logger.LogInformation("Create Attendance");
            return await _eDetails.createAttendanceAsync(attendanceRequest);
        }

    }
}
