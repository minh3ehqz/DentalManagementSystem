﻿using System;
using System.Collections.Generic;

namespace DentalManagementSystem.Models;

public partial class RolePermissionMap
{
    public long Id { get; set; }

    public long RoleId { get; set; }

    public long PermissionId { get; set; }

    public virtual Permission Permission { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
