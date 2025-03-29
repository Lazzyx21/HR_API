namespace HR_API.DTO.Response
{
    public class EmployeeDetailsResponse
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string? JobTitle { get; set; }
        public string? Department { get; set; }
        public decimal Salary { get; set; }
        public DateOnly HireDate { get; set; }
    }
}
