using System.Collections.Generic;

namespace BenefitsProject.WebApi.Models
{
    public class EmployeeCreateDto
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public ICollection<DependentCreateDto> Dependents { get; set; }
    }
}
