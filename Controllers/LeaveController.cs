using HR_API.DTO.Request;
using HR_API.DTO.Response;
using HR_API.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HR_API.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class LeaveController : Controller
    {
        private readonly ILogger<LeaveController> _logger;
        private readonly ILeaveManagementServices _leave;

        public LeaveController(ILogger<LeaveController> logger, ILeaveManagementServices leave)
        {
            _logger = logger;
            _leave = leave;
        }

        [HttpGet("GET-Leave")]
        public async Task<GenericApiResponse<List<LeaveResponse>>> listLeaveRequests()
        {
            _logger.LogInformation("");
            return await _leave.listLeaveRequests();
        }

        [HttpGet("{id}/GET-id-Leave")]
        public async Task<GenericApiResponse<List<LeaveResponse>>> getLeaveRequests(int? id)
        {
            _logger.LogInformation("");
            return await _leave.getLeaveRequests(id);
        }

        [HttpPut("create-leave")]
        public async Task<GenericApiResponse<LeaveResponse>> createLeaveRequest(LeaveReq addLeave)
        {
            _logger.LogInformation("");
            return await _leave.createLeaveRequest(addLeave);
        }

        [HttpPost("{id}/Approve")]
        public async Task<GenericApiResponse<LeaveResponse>> approveLeave(int id)
        {
            _logger.LogInformation($"Approved leave request with ID: {id}");
            return await _leave.approveLeave(id);
        }
        [HttpPost("{id}/Reject")]
        public async Task<GenericApiResponse<LeaveResponse>> rejectLeave(int id)
        {
            _logger.LogInformation($"Rejecting leave request with ID: {id}");
            return await _leave.rejectLeave(id);
        }

        [HttpGet("List-Used-Balances")]
        public async Task<GenericApiResponse<List<LeaveBalancesResponse>>> listUsedBalance()
        {
            _logger.LogInformation("List Employee Used Leave Balance");
            return await _leave.listUsedBalance();
        }
        [HttpGet("{id}/Used-Balances")]
        public async Task<GenericApiResponse<List<LeaveBalancesResponse>>> usedBalance(int id)
        {
            _logger.LogInformation($"Employee{id} Used Leave Balance");
            return await _leave.usedBalance(id);
        }
        [HttpGet("List-Remaining-Balances")]
        public async Task<GenericApiResponse<List<LeaveBalancesResponse>>> listRemainingBalance()
        {
            _logger.LogInformation("List Employee Remaining leave balance");
            return await _leave.listRemainingBalance();
        }
        [HttpGet("{id}/Remaining-Balances")]
        public async Task<GenericApiResponse<List<LeaveBalancesResponse>>> remainingBalances(int id)
        {
            _logger.LogInformation($"Employee{id} Remaining leave balance");
            return await _leave.remainingBalances(id);
        }
        [HttpGet("{id}/Leave-History")]
        public async Task<GenericApiResponse<List<LeaveResponse>>> leaveHistory(int id)
        {
            _logger.LogInformation($"Employee{id} Leave History");
            return await _leave.leaveHistory(id);
        }


    }
}
