using BenefitsProject.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BenefitsProject.Infrastructure.Persistence
{
    public class BenefitsDbContext : DbContext
    {
        public BenefitsDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Dependent> Dependents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BenefitsDbContext).Assembly);
        }
    }
}