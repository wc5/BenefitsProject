using BenefitsProject.Core.Application.Validators;
using BenefitsProject.Core.Domain.Entities;
using BenefitsProject.Core.Domain.Enumerations;
using FluentValidation;
using FluentValidation.TestHelper;
using System.Threading.Tasks;
using Xunit;

namespace BenefitsProject.Core.UnitTests.ValidationTests
{
    public class EmployeeValidator_Should
    {
        private readonly IValidator<Employee> _employeeValidator;

        public EmployeeValidator_Should()
        {
            _employeeValidator = new EmployeeValidator();
        }

        [Fact]
        public async Task NotHaveValidationErrorsWhenValidModelIsUsed()
        {
            var employee = DataHelpers.GetEmployeeById(1);

            var result = await _employeeValidator.TestValidateAsync(employee);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public async Task ThrowIfEmployeeHasMoreThanOneSpouse()
        {
            var employee = DataHelpers.GetEmployeeById(1);

            foreach (var dependent in employee.Dependents)
            {
                dependent.Category = DependentCategory.Spouse;
            }

            var result = await _employeeValidator.TestValidateAsync(employee);

            result.ShouldHaveValidationErrorFor(employee => employee.Dependents);
        }
    }
}
