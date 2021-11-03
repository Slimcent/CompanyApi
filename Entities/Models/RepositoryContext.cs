using Entities.Data;
using Microsoft.EntityFrameworkCore;

namespace Entities.Models
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        { 
            modelBuilder.ApplyConfiguration(new CompanyData()); 
            modelBuilder.ApplyConfiguration(new EmployeeData()); 
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
