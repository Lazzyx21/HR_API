namespace HR_API.DTO.Request
{
    public class PayrollRequest
    {
        public int PayrollId { get; set; }

        public int EmployeeId { get; set; }

        public DateOnly SalaryMonth { get; set; }

        public decimal BaseSalary { get; set; }

        public decimal? Deductions { get; set; }

        public decimal NetSalary { get; set; }

        public decimal? Bonuses { get; set; }

        public string? PaymentStatus { get; set; }
    }
}
