using BenefitsProject.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BenefitsProject.Infrastructure.Persistence.Configuration
{
    public class EmployeesConfiguration : IEntityTypeConfiguration<Employee>
    {
        // opinionated table config, etc.
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.FirstName)
                .IsRequired();

            builder.Property(e => e.LastName)
                .IsRequired();

            builder.Property(e => e.SalaryPerAnnum)
                .HasPrecision(2);

            builder.Property(e => e.PersonalBenefitCostPerAnnum)
                .HasPrecision(2);

            builder.Property(e => e.TotalBenefitCostPerAnnum)
                .HasPrecision(2);

            builder
                .HasMany(e => e.Dependents)
                .WithOne(d => d.Employee)
                .HasForeignKey(d => d.EmployeeId);

            // builder.HasData(SeedData.ForEmployees());
        }
    }
}
