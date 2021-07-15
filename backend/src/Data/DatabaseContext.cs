using BenefitsApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BenefitsApp.Data
{
  public class DatabaseContext : DbContext
  {
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Dependent> Dependents { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Discount> Discounts { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.AddSopranos();
      modelBuilder.AddMoltisantis();
    }
  }
}