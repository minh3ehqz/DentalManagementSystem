using System;
using System.Collections.Generic;

namespace DentalManagementSystem.Models
{
    public class NewView
    {
        public long Id { get; set; }

        public string Username { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public DateTime Birthday { get; set; }

        public string Phone { get; set; } = null!;

        public int Salary { get; set; }

        public long RoleId { get; set; }

        public bool Enable { get; set; }

        public string Email { get; set; } = null!;

        public string Name { get; set; } = null!;

    }
}
