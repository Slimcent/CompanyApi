using Entities.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;

namespace Entities.Models
{
    public class Role : IdentityRole, ITracker
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedBy { get; set; }
        public DateTime UpdatedBy { get; set; }
    }
}
