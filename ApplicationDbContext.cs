using Microsoft.EntityFrameworkCore;
using Employee_Management_System.Models;

namespace Employee_Management_System.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<EmployeeBasicDetails> EmployeeBasicDetails { get; set; }
        public DbSet<EmployeeAdditionalDetails> EmployeeAdditionalDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure models, relationships, etc.
            base.OnModelCreating(modelBuilder);
        }
    }
}
