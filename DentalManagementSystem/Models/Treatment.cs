using System;
using System.Collections.Generic;

namespace DentalManagementSystem.Models;

public partial class Treatment
{
    public long Id { get; set; }

    public long PatientId { get; set; }

    public string Name { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;

    public virtual ICollection<TreatmentServiceMap> TreatmentServiceMaps { get; } = new List<TreatmentServiceMap>();
}
