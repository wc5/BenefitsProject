using BenefitsProject.Core.Domain.Enumerations;

namespace BenefitsProject.WebApi.Models
{
    public class DependentUpdateDto
    {
        public int Id { get; set; }

        public DependentCategory Category { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public int EmployeeId { get; set; }
    }
}
