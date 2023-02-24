using DentalManagementSystem.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DentalManagementSystem.Models;

public partial class MaterialImport
{
    public long Id { get; set; }

    public long MaterialId { get; set; }

    public DateTime Date { get; set; }

    public int Amount { get; set; }

    public string SupplyName { get; set; } = null!;

    public int TotalPrice { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Material Material { get; set; } = null!;
}
