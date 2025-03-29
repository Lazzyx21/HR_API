namespace HR_API.DTO.Response
{
    public class LeaveResponse
    {
        public int LeaveId { get; set; }

        public int EmployeeId { get; set; }

        public string? LeaveType { get; set; }

        public int? LeaveDays { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public string? Status { get; set; }

        public int? TotalLeaves { get; set; }

        public int? UsedLeaves { get; set; }

        public int? RemainingLeaves { get; set; }
    }
}
