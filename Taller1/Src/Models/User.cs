using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

namespace Taller1.Src.Models
{
    public class User: IdentityUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Thelephone { get; set; }
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastAccess { get; set; }
        public bool IsActive { get; set; } = true;
        public string? DeactivationReason { get; set; }

        public ShippingAddres? ShippingAddres { get; set; }
    }
}