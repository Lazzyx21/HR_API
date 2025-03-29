using Azure.Core;
using HR_API.DTO.Request;
using HR_API.DTO.Response;
using HR_API.Models;
using HR_API.Services.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Serialization;

namespace HR_API.Services
{
    public class LeaveManagementServices : ILeaveManagementServices
    {
        private readonly ILogger<LeaveManagementServices> _logger;
        private readonly IConfiguration _configuration;
        private readonly ErptestingContext _dbContext;

        public LeaveManagementServices(ILogger<LeaveManagementServices> logger, IConfiguration configuration, ErptestingContext dbContext)
        {
            _logger = logger;
            _configuration = configuration;
            _dbContext = dbContext;
        }
        //Create Leave Request System(employees apply, managers approve).
        

        public async Task<GenericApiResponse<LeaveResponse>> createLeaveRequest(LeaveReq addLeave)
        {
            GenericApiResponse<LeaveResponse> response = new();
            try
            {
                if (addLeave == null)
                {
                    response.status = -1;
                    response.ErrorMessage = "Null input";
                    _logger.LogInformation("Input is null");
                }
                else
                {
                    LeaveRequest leave = new()
                    {
                        EmployeeId = addLeave.EmployeeId,
                        LeaveDays = addLeave.LeaveDays,
                        LeaveType = addLeave.LeaveType,
                        StartDate = addLeave.StartDate,
                        EndDate = addLeave.EndDate,
                        Status = addLeave.Status
                    };
                    await _dbContext.LeaveRequests.AddAsync(leave);
                    await _dbContext.SaveChangesAsync();
                }
            }catch(Exception ex)
            {
                response.status = 400;
                response.ErrorMessage = "Error while creating LEAVE request!!";
                _logger.LogInformation(ex, "-Exception from " + ex.TargetSite.Name + " ERROR in Post method: " + ex.Message);
            }
            return response;
        }

        public async Task<GenericApiResponse<List<LeaveResponse>>> getLeaveRequests(int? id)
        {
            GenericApiResponse<List<LeaveResponse>> response = new();
            List<LeaveResponse> leaves = new();
            try
            {
                if(id == null)
                {
                    response.status = -1;
                    response.ErrorMessage = "Null input";
                    _logger.LogInformation("ID is null");
                }
                else
                {
                    leaves = await _dbContext.LeaveRequests
                        .Where(l => l.LeaveId == id)
                        .Select(l => new LeaveResponse
                        {
                            LeaveId = l.LeaveId,
                            EmployeeId = l.EmployeeId,
                            LeaveType = l.LeaveType,
                            StartDate = l.StartDate,
                            EndDate = l.EndDate,
                            Status = l.Status
                        }).ToListAsync();
                }
            }catch(Exception ex)
            {
                response.status = 400;
                response.ErrorMessage = "Error while fetching LEAVE details!!";
                _logger.LogInformation(ex, "-Exception from " + ex.TargetSite.Name + " ERROR in Get method: " + ex.Message);
            }
            return response;
        }

        public async Task<GenericApiResponse<List<LeaveResponse>>> listLeaveRequests()
        {
            GenericApiResponse<List<LeaveResponse>> response = new();
            List<LeaveResponse> leaves = new();
            try
            {
                leaves = await _dbContext.LeaveRequests
                    .Select(l => new LeaveResponse
                    {
                        LeaveId = l.LeaveId,
                        EmployeeId = l.EmployeeId,
                        LeaveType = l.LeaveType,
                        StartDate = l.StartDate,
                        EndDate = l.EndDate,
                        Status = l.Status
                    }).ToListAsync();
                response.status = 200;
                response.ErrorDesc = "Successfully Fetched!!";
                response.Data = leaves;
            }catch(Exception ex)
            {
                response.status = 400;
                response.ErrorMessage = "Error while fetching LEAVE details!!";
                _logger.LogError(ex, "-Exception from " + ex.TargetSite.Name + " ERROR in Get method: " + ex.Message);
            }
            return response;
        }


        public async Task<GenericApiResponse<LeaveResponse>> rejectLeave(int id)
        {
            GenericApiResponse<LeaveResponse> response = new();
            try
            {
                var reject = await _dbContext.LeaveRequests.FindAsync(id);
                if (reject != null)
                {
                    reject.Status = "Rejected";
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    response.status = -1;
                    response.ErrorMessage = "Leave Request not found";
                    _logger.LogInformation("Leave Request not found for ID: " + id);
                }
            }
            catch (Exception ex)
            {
                response.status = 400;
                response.ErrorMessage = "Error while rejecting LEAVE request!!";
                _logger.LogError(ex, "Error in rejectLeave method: " + ex.Message);
            }
            return response;
        }
        public async Task<GenericApiResponse<LeaveResponse>> approveLeave(int id)
        {
            GenericApiResponse<LeaveResponse> response = new();
            try
            {
                var approve = await _dbContext.LeaveRequests.FindAsync(id);

                if (approve == null)
                {
                    response.status = -1;
                    response.ErrorMessage = "Null input";
                    _logger.LogInformation("Input is null");
                }
                else
                {
                    if (approve != null)
                    {
                        approve.Status = "Approved";
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        response.status = -1;
                        response.ErrorMessage = "Leave Request not found";
                        _logger.LogInformation("");
                    }

                }
            }
            catch (Exception ex)
            {
                response.status = 400;
                response.ErrorMessage = "Error while approving LEAVE request!!";
                _logger.LogInformation(ex, "-Exception from " + ex.TargetSite.Name + " ERROR in Post method: " + ex.Message);
            }
            return response;
        }



        //Implement Leave Balances(track used/remaining leaves).
        public async Task<GenericApiResponse<List<LeaveBalancesResponse>>> listUsedBalance()
        {
            GenericApiResponse<List<LeaveBalancesResponse>> response = new();
            List<LeaveBalancesResponse> listUsed = new();
            try
            {
                listUsed = await _dbContext.LeaveRequests
                    .Select(l => new LeaveBalancesResponse
                    {
                        LeaveId = l.LeaveId,
                        EmployeeId = l.EmployeeId,
                        TotalLeaves = l.TotalLeaves,
                        UsedLeaves = l.TotalLeaves - l.RemainingLeaves,
                        RemainingLeaves = l.RemainingLeaves
                    }).ToListAsync();
                response.status = 200;
                response.ErrorDesc = "Successfully fetched!!!";
                response.Data = listUsed;
            }
            catch (Exception ex)
            {
                response.status = 400;
                response.ErrorMessage = "Error while fetching LEAVE details!!";
                _logger.LogInformation(ex, "-Exception from " + ex.TargetSite.Name + " ERROR in Get method: " + ex.Message);
            }
            return response;
        }

        public async Task<GenericApiResponse<List<LeaveBalancesResponse>>> usedBalance(int id)
        {
            GenericApiResponse<List<LeaveBalancesResponse>> response = new();
            List<LeaveBalancesResponse> idUsedBalance = new();
            try
            {
                if(idUsedBalance == null)
                {
                    response.status = -1;
                    response.ErrorMessage = "Null input";
                    _logger.LogInformation("ID is null");
                }
                else
                {
                    idUsedBalance = await _dbContext.LeaveRequests
                        .Where(l => l.LeaveId == id)
                        .Select(l => new LeaveBalancesResponse
                        {
                            LeaveId = l.LeaveId,
                            EmployeeId = l.EmployeeId,
                            UsedLeaves = l.UsedLeaves
                        }).ToListAsync();
                    response.status = 200;
                    response.ErrorDesc = "Successfully Fetched!!";
                    response.Data = idUsedBalance;
                }
            }
            catch(Exception ex)
            {
                response.status = 400;
                response.ErrorMessage = "Error while fetching LEAVE details!!";
                _logger.LogInformation(ex, "-Exception from " + ex.TargetSite.Name + " ERROR in Get method: " + ex.Message);
            }
            return response;
        }

        public async Task<GenericApiResponse<List<LeaveBalancesResponse>>> listRemainingBalance()
        {
            GenericApiResponse<List<LeaveBalancesResponse>> response = new();
            List<LeaveBalancesResponse > listremaining = new();
            try
            {
                listremaining = await  _dbContext.LeaveRequests
                    .Select(r => new LeaveBalancesResponse
                    {
                        LeaveId = r.LeaveId,
                        EmployeeId = r.EmployeeId,
                        RemainingLeaves = r.RemainingLeaves
                    }).ToListAsync();
                response.status = 200;
                response.ErrorDesc = "Successfully fetched!!!";
                _logger.LogInformation("Fetched Remaining Data");
                response.Data = listremaining;
            }
            catch(Exception ex)
            {
                response.status = 400;
                response.ErrorDesc = "Error while fetching LEAVE details!!";
                _logger.LogInformation(ex, "-Exception from " + ex.TargetSite.Name + " ERROR in Get method: " + ex.Message);
            }
            return response;
        }

        public async Task<GenericApiResponse<List<LeaveBalancesResponse>>> remainingBalances(int id)
        {
            GenericApiResponse<List<LeaveBalancesResponse>> response = new();
            List<LeaveBalancesResponse> remainingUsedID = new();
            try
            {
                if (remainingUsedID != null)
                {
                    remainingUsedID = await _dbContext.LeaveRequests
                        .Where(r => r.LeaveId == id)
                        .Select(r => new LeaveBalancesResponse
                        {
                            LeaveId = r.LeaveId,
                            EmployeeId = r.EmployeeId,
                            RemainingLeaves = r.RemainingLeaves
                        }).ToListAsync();
                    response.status = 200;
                    response.ErrorDesc = "Successfully fetched remaining leave balance according to the Employee ID!!!";
                    _logger.LogInformation("Fetched Remaining Data");
                    response.Data = remainingUsedID;
                }
                else
                {

                    response.status = -1;
                    response.ErrorMessage = "Null input";
                    _logger.LogInformation("ID is null");
                }
            }catch(Exception ex)
            {
                response.status = 400;
                response.ErrorDesc = "Error while fetching remaining LEAVE details!!";
                _logger.LogInformation(ex, "-Exception from " + ex.TargetSite.Name + " ERROR in Get method: " + ex.Message);
            }
            return response;
        }


        //Generate Leave Reports(employee-wise leave history).
        public async Task<GenericApiResponse<List<LeaveResponse>>> leaveHistory(int id)
        {
            GenericApiResponse<List<LeaveResponse>> response = new();

            try
            {
                var leaveHistory = await _dbContext.LeaveRequests
                    .Where(l => l.EmployeeId == id)  // Filter by EmployeeId
                    .OrderBy(l => l.StartDate)       // Order by StartDate instead of EmployeeId
                    .Select(l => new LeaveResponse
                    {
                        EmployeeId = l.EmployeeId,
                        StartDate = l.StartDate,
                        EndDate = l.EndDate,
                        TotalLeaves = l.TotalLeaves,
                        UsedLeaves = l.UsedLeaves,
                        RemainingLeaves = l.RemainingLeaves
                    })
                    .ToListAsync();

                if (!leaveHistory.Any()) // Check if the list is empty
                {
                    response.status = 404;
                    response.ErrorDesc = "No leave history found for the given Employee ID.";
                    _logger.LogInformation($"No leave history found for Employee ID: {id}");
                }
                else
                {
                    response.status = 200;
                    response.ErrorDesc = "Successfully Fetched!!";
                    response.Data = leaveHistory;
                }
            }
            catch (Exception ex)
            {
                response.status = 400;
                response.ErrorMessage = $"Issue while fetching leave history of Employee {id}";
                _logger.LogError(ex, $"Exception in leaveHistory method: {ex.Message}");
            }

            return response;
        }


    }
}
