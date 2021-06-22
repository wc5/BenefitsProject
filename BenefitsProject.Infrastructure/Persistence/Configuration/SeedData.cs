using BenefitsProject.Core.Application.Services;
using BenefitsProject.Core.Domain.Contracts;
using BenefitsProject.Core.Domain.Entities;
using BenefitsProject.Core.Domain.Enumerations;
using System.Collections.Generic;

namespace BenefitsProject.Infrastructure.Persistence.Configuration
{
    public static class SeedData
    {
        public static Employee ForEmployees()
        {
            var employee = new Employee
            {
                Id = 1,
                FirstName = "William",
                MiddleName = "Cory",
                LastName = "Samsonite",
            };

            var dependents = new List<Dependent>
            {
                new Dependent
                {
                    Id = 1,
                    Category = DependentCategory.Spouse,
                    FirstName = "Ashley",
                    MiddleName = "G",
                    LastName = "Samsonite",
                    EmployeeId = 1
                },
                new Dependent
                {
                    Id = 2,
                    Category = DependentCategory.Child,
                    FirstName = "William",
                    MiddleName = "Riggins",
                    LastName = "Samsonite",
                    EmployeeId = 1
                },
                new Dependent
                {
                    Id = 3,
                    Category = DependentCategory.Child,
                    FirstName = "Olivia",
                    MiddleName = "Hadley",
                    LastName = "Samonsite",
                    EmployeeId = 1
                }
            };

            foreach (var dependent in dependents)
            {
                employee.Dependents.Add(dependent);
            }

            IBenefitCalculator calculator = new BenefitCalculator();

            calculator.CalculateAndSetSalaryAndBenefitData(employee);

            return employee;
        }
    }
}
