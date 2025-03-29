using HR_API.Models;
using HR_API.Services.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;

namespace HR_API.Services
{
    public class HRReportingServices : IHRReportingServices
    {
        private readonly ILogger<HRReportingServices> _logger;
        private readonly IConfiguration _configuration;
        private readonly ErptestingContext _dbContext;

        public HRReportingServices(ILogger<HRReportingServices> logger, IConfiguration configuration, ErptestingContext dbContext)
        {
            _logger = logger;
            _configuration = configuration;
            _dbContext = dbContext;
        }

        //Generate Employee Reports(list, salaries, department-wise data).
        //Create Payroll Reports(salary distribution, deductions).
        //Develop Attendance Reports(daily/monthly attendance logs).

    }
}
