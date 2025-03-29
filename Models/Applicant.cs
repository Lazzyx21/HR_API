using System;
using System.Collections.Generic;

namespace HR_API.Models;

public partial class Applicant
{
    public int ApplicantId { get; set; }

    public int? JobId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public DateOnly? ApplicationDate { get; set; }

    public string? Status { get; set; }

    public virtual JobOpening? Job { get; set; }
}
