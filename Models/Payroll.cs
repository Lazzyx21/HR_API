using System;
using System.Collections.Generic;

namespace HR_API.Models;

public partial class Payroll
{
    public int PayrollId { get; set; }

    public int EmployeeId { get; set; }

    public DateOnly SalaryMonth { get; set; }

    public decimal BaseSalary { get; set; }

    public decimal? Deductions { get; set; }

    public decimal NetSalary { get; set; }

    public decimal? Bonuses { get; set; }

    public string? PaymentStatus { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
