﻿using System;
using System.Collections.Generic;

namespace DentalManagementSystem.Models;

public partial class Role
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<RolePermissionMap> RolePermissionMaps { get; } = new List<RolePermissionMap>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
