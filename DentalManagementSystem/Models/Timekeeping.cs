using System;
using System.Collections.Generic;

namespace DentalManagementSystem.Models;

public partial class Timekeeping
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public DateTime TimeCheckin { get; set; }

    public DateTime TimeCheckout { get; set; }

    public virtual User User { get; set; } = null!;
}
