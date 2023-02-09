using System;
using System.Collections.Generic;

namespace DentalManagementSystem.Models;

public partial class PatientRecordServiceMap
{
    public long Id { get; set; }

    public long PatientRecordId { get; set; }

    public long ServiceId { get; set; }

    public int Status { get; set; }

    public virtual PatientRecord PatientRecord { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;
}
