using System;
using System.Collections.Generic;
using System.Net;

namespace DentalManagementSystem.Models;

public partial class PatientRecord
{
    public long Id { get; set; }

    public string Reason { get; set; } = null!;

    public string Diagnostic { get; set; } = null!;

    public string Causal { get; set; } = null!;

    public DateTime Date { get; set; }

    public string TreatmentName { get; set; } = null!;

    public string MarrowRecord { get; set; } = null!;

    public string Debit { get; set; } = null!;

    public string Note { get; set; } = null!;

    public long TreatmentId { get; set; }

    public long UserId { get; set; }

    public long PatientId { get; set; }

    public string Prescription { get; set; } = null!;

    public virtual ICollection<MaterialExport> MaterialExports { get; } = new List<MaterialExport>();

    public virtual Patient Patient { get; set; } = null!;

    public virtual ICollection<PatientRecordServiceMap> PatientRecordServiceMaps { get; } = new List<PatientRecordServiceMap>();

    public virtual ICollection<TreatmentServiceMap> TreatmentServiceMaps { get; } = new List<TreatmentServiceMap>();

    public virtual User User { get; set; } = null!;
    public void Trim()
    {
        Reason = Reason?.Trim();
        Causal = Causal?.Trim();
        Diagnostic = Diagnostic?.Trim();
        TreatmentName = TreatmentName?.Trim();
        MarrowRecord = MarrowRecord?.Trim();
        Debit = Debit?.Trim();
        Note = Note?.Trim();
        Prescription = Prescription?.Trim();
    }
}
