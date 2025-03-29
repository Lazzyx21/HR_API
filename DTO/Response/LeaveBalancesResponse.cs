namespace HR_API.DTO.Response
{
    public class LeaveBalancesResponse
    {
        public int LeaveId { get; set; }
        public int EmployeeId { get; set; }
        public int? TotalLeaves { get; set; }
        public int? UsedLeaves { get; set; }
        public int? RemainingLeaves { get; set; }
    }
}
