using BenefitsProject.Core.Application.Constants;
using BenefitsProject.Core.Domain.Entities;
using BenefitsProject.Core.Domain.Enumerations;
using FluentValidation;
using System.Collections.Generic;

namespace BenefitsProject.Core.Application.Validators
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(e => e.FirstName)
                .NotEmpty()
                .MaximumLength(32)
                .WithMessage("FirstName must be between 1-32 characters.");

            RuleFor(e => e.MiddleName)
                .MaximumLength(32)
                .WithMessage("MiddleName cannot be longer than 32 characters.");

            RuleFor(e => e.LastName)
                .NotEmpty()
                .MaximumLength(32)
                .WithMessage("LastName must be between 1-32 characters.");

            RuleFor(e => e.SalaryPerAnnum)
                .NotEmpty()
                .WithMessage("SalaryPerAnnum must be set.");

            RuleFor(e => e.PersonalBenefitCostPerAnnum)
                .NotEmpty()
                .WithMessage("PersonalBenefitCostPerAnnum must be set.");

            RuleFor(e => e.TotalBenefitCostPerAnnum)
                .NotEmpty()
                .WithMessage("TotalBenefitCostPerAnnum must be set.");

            RuleFor(e => e.TotalBenefitCostPerAnnum)
                .GreaterThanOrEqualTo(0)
                .WithMessage("TotalBeefitCostPerAnnum can not be less than zero.");

            RuleFor(e => e)
                .Must(CheckIfHasDiscountedPersonalBenefitCostIsMarkedCorrectly)
                .WithMessage("HasDiscountedPersonalBenefitCost does not align with PersonalBenefitCostPerAnnum.");

            RuleForEach(e => e.Dependents)
                .SetValidator(new DependentValidator());

            RuleFor(e => e.Dependents)
                .Must(NotHaveMultipleSpouses)
                .WithMessage("Only one dependent can be a spouse.");
        }

        #region Custom Validation Helpers
        private bool CheckIfHasDiscountedPersonalBenefitCostIsMarkedCorrectly(Employee employee)
        {
            if (employee.PersonalBenefitCostPerAnnum < PayAndBenefitConstants.DEFAULT_ANNUAL_COST_FOR_EMPLOYEE)
            {
                return (employee.HasDiscountedPersonalBenefitCost == true);
            }

            return (employee.HasDiscountedPersonalBenefitCost == false);
        }

        private bool NotHaveMultipleSpouses(ICollection<Dependent> dependents)
        {
            var spouseCount = 0;

            foreach (var dependent in dependents)
            {
                if (dependent.Category == DependentCategory.Spouse)
                {
                    ++spouseCount;

                    if (spouseCount > 1)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        #endregion
    }
}
