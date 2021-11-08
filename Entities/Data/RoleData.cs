using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Data
{
    public class RoleData : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
            new Role
            {
                Name = "Manager",
                NormalizedName = "MANAGER",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new Role
            {
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            });
        }
    }       
}
