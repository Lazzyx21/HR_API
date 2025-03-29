namespace HR_API.DTO.Request
{
    public class LeaveReq
    {
        public int LeaveId { get; set; }

        public int EmployeeId { get; set; }

        public string? LeaveType { get; set; }

        public int? LeaveDays { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public string? Status { get; set; }
    }
}
