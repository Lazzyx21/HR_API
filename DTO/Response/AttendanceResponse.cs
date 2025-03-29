namespace HR_API.DTO.Response
{
    public class AttendanceResponse
    {
        public int AttendanceId { get; set; }

        public int EmployeeId { get; set; }

        public DateOnly AttendanceDate { get; set; }

        public TimeOnly? CheckInTime { get; set; }

        public TimeOnly? CheckOutTime { get; set; }
    }
}
