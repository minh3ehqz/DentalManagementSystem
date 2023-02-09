using System;
using System.Collections.Generic;

namespace DentalManagementSystem.Models;

public partial class MaterialExport
{
    public long Id { get; set; }

    public long MaterialId { get; set; }

    public int Amount { get; set; }

    public int TotalPrice { get; set; }

    public long PatientRecordId { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Material Material { get; set; } = null!;

    public virtual PatientRecord PatientRecord { get; set; } = null!;
}
