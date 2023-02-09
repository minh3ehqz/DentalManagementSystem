using System;
using System.Collections.Generic;

namespace DentalManagementSystem.Models;

public partial class Schedule
{
    public long Id { get; set; }

    public long PatientId { get; set; }

    public DateTime Date { get; set; }

    public int Status { get; set; }

    public bool Booked { get; set; }

    public virtual Patient Patient { get; set; } = null!;
}
