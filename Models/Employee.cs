using System;
using System.Collections.Generic;

namespace HR_API.Models;

public partial class Employee
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

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();

    public virtual ICollection<Payroll> Payrolls { get; set; } = new List<Payroll>();
}
