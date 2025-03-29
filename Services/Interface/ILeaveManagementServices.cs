using HR_API.DTO.Response;
using HR_API.DTO.Request;

namespace HR_API.Services.Interface
{
    public interface ILeaveManagementServices
    {
        public Task<GenericApiResponse<List<LeaveResponse>>> listLeaveRequests();
        public Task<GenericApiResponse<List<LeaveResponse>>> getLeaveRequests(int? id);
        public Task<GenericApiResponse<LeaveResponse>> createLeaveRequest(LeaveReq addLeave);
        public Task<GenericApiResponse<LeaveResponse>> approveLeave(int id);
        public Task<GenericApiResponse<LeaveResponse>> rejectLeave(int id);
        public Task<GenericApiResponse<List<LeaveBalancesResponse>>> listUsedBalance();
        public Task<GenericApiResponse<List<LeaveBalancesResponse>>> usedBalance(int id);
        public Task<GenericApiResponse<List<LeaveBalancesResponse>>> listRemainingBalance();
        public Task<GenericApiResponse<List<LeaveBalancesResponse>>> remainingBalances(int id);
        public Task<GenericApiResponse<List<LeaveResponse>>> leaveHistory(int id);

    }
}
