using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DentalManagementSystem.Models;

public partial class MaterialImport
{
    public long Id { get; set; }

    public long MaterialId { get; set; }

    public DateTime Date { get; set; }

    [Range(minimum: 1, maximum: 1000, ErrorMessage = "Please enter a valid no between 1 & 1000")]
    public int Amount { get; set; }

    public string SupplyName { get; set; } = null!;

    [Range(minimum: 1, maximum: 10000000000, ErrorMessage = "Please enter a valid no between 1 & 1000000000000")]
    public int TotalPrice { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Material Material { get; set; } = null!;
}
