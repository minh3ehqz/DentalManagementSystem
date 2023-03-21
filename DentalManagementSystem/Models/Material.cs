using System;
using System.Collections.Generic;
using System.Net;

namespace DentalManagementSystem.Models;

public partial class Material
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Unit { get; set; } = null!;

    public int Amount { get; set; }

    public int Price { get; set; }

    public virtual ICollection<MaterialExport> MaterialExports { get; } = new List<MaterialExport>();

    public virtual ICollection<MaterialImport> MaterialImports { get; } = new List<MaterialImport>();

	public override string ToString()
	{
		return (Id + " " + Name + " " + Unit + " " + Amount + " " + Price).Trim();
	}
}
