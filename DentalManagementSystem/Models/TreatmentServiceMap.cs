using System;
using System.Collections.Generic;

namespace DentalManagementSystem.Models;

public partial class TreatmentServiceMap
{
    public long Id { get; set; }

    public long TreatmentId { get; set; }

    public long ServiceId { get; set; }

    public int CurrentPrice { get; set; }

    public double Discount { get; set; }

    public long PatientRecordId { get; set; }

    public virtual PatientRecord PatientRecord { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;

    public virtual Treatment Treatment { get; set; } = null!;
}
