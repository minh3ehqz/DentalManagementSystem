using System;
using System.Collections.Generic;

namespace DentalManagementSystem.Models;

public partial class Patient
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Birthday { get; set; }

    public bool Gender { get; set; }

    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string BodyPrehistory { get; set; } = null!;

    public string TeethPrehistory { get; set; } = null!;

    public int Status { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<PatientRecord> PatientRecords { get; } = new List<PatientRecord>();

    public virtual ICollection<Schedule> Schedules { get; } = new List<Schedule>();

    public virtual ICollection<Treatment> Treatments { get; } = new List<Treatment>();
}
