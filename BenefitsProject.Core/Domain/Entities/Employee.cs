using System.Collections.Generic;

namespace BenefitsProject.Core.Domain.Entities
{
    public class Employee
    {
        public Employee()
        {
            Dependents = new HashSet<Dependent>();
        }

        public int Id { get; set; }
        
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        
        public decimal SalaryPerAnnum { get; set; }
        
        public decimal PersonalBenefitCostPerAnnum { get; set; }
        public bool HasDiscountedPersonalBenefitCost { get; set; }
        
        public decimal TotalBenefitCostPerAnnum { get; set; }

        public ICollection<Dependent> Dependents { get; set; }
    }
}
