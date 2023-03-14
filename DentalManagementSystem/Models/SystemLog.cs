using System;
using System.Collections.Generic;

namespace DentalManagementSystem.Models;

public partial class SystemLog
{
    public long Id { get; set; }

    public DateTime CreatedDate { get; set; }

    public string Content { get; set; } = null!;

    public long OwnerId { get; set; }

    public virtual User Owner { get; set; } = null!;
}
