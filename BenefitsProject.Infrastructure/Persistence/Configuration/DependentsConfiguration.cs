using BenefitsProject.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BenefitsProject.Infrastructure.Persistence.Configuration
{
    public class DependentsConfiguration : IEntityTypeConfiguration<Dependent>
    {
        // opinionated table config, etc.
        public void Configure(EntityTypeBuilder<Dependent> builder)
        {
            builder.Property(e => e.FirstName)
                .IsRequired();

            builder.Property(e => e.LastName)
                .IsRequired();

            builder.Property(e => e.PersonalBenefitCostPerAnnum)
                .HasPrecision(2);

            builder
                .HasOne(d => d.Employee);
        }
    }
}
