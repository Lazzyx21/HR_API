using HR_API.Models;
using HR_API.Services.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;
using HR_API.DTO.Response;
using Microsoft.EntityFrameworkCore;
using HR_API.DTO.Request;
namespace HR_API.Services
{
    public class EmployeeManagementServices : IEmployeeManagementServices
    {
        private readonly ILogger<EmployeeManagementServices> _logger;
        private readonly IConfiguration _configuration;
        private readonly ErptestingContext _dbContext;

        public EmployeeManagementServices(ILogger<EmployeeManagementServices> logger, IConfiguration configuration, ErptestingContext dbContext)
        {
            _logger = logger;
            _configuration = configuration;
            _dbContext = dbContext;

        }


        //Create Employee Profiles(name, contact, department, salary).
        public async Task<GenericApiResponse<List<EmployeeDetailsResponse>>> employeeProfileAsync()
        {
            GenericApiResponse<List<EmployeeDetailsResponse>> response = new();
            List<EmployeeDetailsResponse> profile = new();
            try
            {
                profile = await _dbContext.Employees
                    .Select(p => new EmployeeDetailsResponse
                    {
                        EmployeeId = p.EmployeeId,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        Email = p.Email,
                        Phone = p.Phone,
                        JobTitle = p.JobTitle,
                        Department = p.Department,
                        Salary = p.Salary,
                        HireDate = p.HireDate
                    }).ToListAsync();
                response.status = 200;
                response.ErrorDesc = "Successfully Fetched!!";
                response.Data = profile;
            }catch(Exception ex)
            {
                response.status = -1;
                response.ErrorMessage = "Error while fetching EMPLOYEE details!!";
                _logger.LogError(ex, "-Exception from " + ex.TargetSite.Name + " ERROR in Get method: " + ex.Message);

            }
            return response;
        }

        //Implement Organizational Hierarchy(reporting structure).


        //Set up Attendance Tracking(store check-in/check-out data).
        public async Task<GenericApiResponse<AttendanceResponse>> createAttendanceAsync(AttendanceRequest attendanceRequest)
        {
            GenericApiResponse<AttendanceResponse> response = new();
            try
            {
                if(attendanceRequest == null)
                {
                    response.status = 400;
                    response.ErrorDesc = "Can't be null!!";
                    _logger.LogInformation("Attendance Request is null");
                }
                else
                {
                    Attendance att = new()
                    {
                        AttendanceId = attendanceRequest.AttendanceId,
                        EmployeeId = attendanceRequest.EmployeeId,
                        AttendanceDate = attendanceRequest.AttendanceDate,
                        CheckInTime = attendanceRequest.CheckInTime,
                        CheckOutTime = attendanceRequest.CheckOutTime
                    };
                    await _dbContext.Attendances.AddAsync(att);
                    await _dbContext.SaveChangesAsync();
                    response.status = 200;
                    response.ErrorDesc = "Successfully Created!!";
                }
            }catch(Exception ex)
            {
                response.status = 1;
                response.ErrorMessage = "Error while creating ATTENDANCE details!!";
                _logger.LogInformation(ex, "-Exception from " + ex.TargetSite.Name + " ERROR in Post method: " + ex.Message);
            }
            return response;
        }

    }
}
