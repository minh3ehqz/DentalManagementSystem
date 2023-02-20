using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DentalManagementSystem.Models;

public partial class Service
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    [Range(0, 999999, ErrorMessage = "Unit can't be negative.")]
    public int Unit { get; set; }

    [Range(0, 999999, ErrorMessage = "MarketPrice can't be negative.")]
    public int MarketPrice { get; set; }

    [Range(0, 999999, ErrorMessage = "Price can't be negative.")]
    public int Price { get; set; }

    public virtual ICollection<PatientRecordServiceMap> PatientRecordServiceMaps { get; } = new List<PatientRecordServiceMap>();

    public virtual ICollection<TreatmentServiceMap> TreatmentServiceMaps { get; } = new List<TreatmentServiceMap>();
}
