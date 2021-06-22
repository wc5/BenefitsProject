using BenefitsProject.Core.Domain.Entities;

namespace BenefitsProject.Core.Domain.Contracts
{
    public interface IBenefitCalculator
    {
        void CalculateAndSetSalaryAndBenefitData(Employee employee);
    }
}
