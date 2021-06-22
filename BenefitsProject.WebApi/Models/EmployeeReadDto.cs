using System.Collections.Generic;

namespace BenefitsProject.WebApi.Models
{
    public class EmployeeReadDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public decimal SalaryPerAnnum { get; set; }

        public decimal PersonalBenefitCostPerAnnum { get; set; }
        public bool HasDiscountedPersonalBenefitCost { get; set; }

        public decimal TotalBenefitCostPerAnnum { get; set; }

        public IReadOnlyList<DependentReadDto> Dependents { get; set; }
    }
}