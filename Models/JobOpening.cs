using System;
using System.Collections.Generic;

namespace HR_API.Models;

public partial class JobOpening
{
    public int JobId { get; set; }

    public string JobTitle { get; set; } = null!;

    public string? Department { get; set; }

    public string? JobDescription { get; set; }

    public DateOnly? PostingDate { get; set; }

    public DateOnly? ClosingDate { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Applicant> Applicants { get; set; } = new List<Applicant>();
}
