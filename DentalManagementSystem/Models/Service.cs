using System;
using System.Collections.Generic;

namespace DentalManagementSystem.Models;

public partial class Service
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Unit { get; set; }

    public int MarketPrice { get; set; }

    public int Price { get; set; }

    public virtual ICollection<PatientRecordServiceMap> PatientRecordServiceMaps { get; } = new List<PatientRecordServiceMap>();

    public virtual ICollection<TreatmentServiceMap> TreatmentServiceMaps { get; } = new List<TreatmentServiceMap>();
}
