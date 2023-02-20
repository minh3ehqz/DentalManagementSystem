using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DentalManagementSystem.Models;

public partial class User
{
    public long Id { get; set; }

    public string Username { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime Birthday { get; set; }

    public string Phone { get; set; } = null!;

    [Range(0, 999999, ErrorMessage = "Salary can't be negative.")]
    public int Salary { get; set; }

    public long RoleId { get; set; }

    public bool Enable { get; set; }

    public string Email { get; set; } = null!;

    public virtual ICollection<PatientRecord> PatientRecords { get; } = new List<PatientRecord>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Timekeeping> Timekeepings { get; } = new List<Timekeeping>();
}
