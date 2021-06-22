using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenefitsProject.WebApi.Models
{
    public class EmployeeUpdateDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public IReadOnlyList<DependentUpdateDto> Dependents { get; set; }
    }
}
