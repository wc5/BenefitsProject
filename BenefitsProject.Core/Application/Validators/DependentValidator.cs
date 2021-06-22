using BenefitsProject.Core.Application.Constants;
using BenefitsProject.Core.Domain.Entities;
using FluentValidation;

namespace BenefitsProject.Core.Application.Validators
{
    public class DependentValidator : AbstractValidator<Dependent>
    {
        public DependentValidator()
        {
            RuleFor(d => d.FirstName)
                .NotEmpty()
                .MaximumLength(32)
                .WithMessage("FirstName must be between 1-32 characters.");

            RuleFor(d => d.MiddleName)
                .MaximumLength(32)
                .WithMessage("MiddleName cannot be longer than 32 characters.");

            RuleFor(d => d.LastName)
                .NotEmpty()
                .MaximumLength(32)
                .WithMessage("LastName must be between 1-32 characdters.");

            RuleFor(d => d.Category)
                .IsInEnum()
                .WithMessage("Category must be 0 for Spouse or 1 for Child.");

            RuleFor(d => d.PersonalBenefitCostPerAnnum)
                .NotEmpty()
                .GreaterThanOrEqualTo(0)
                .WithMessage("PersonalBenefitCostPerAnnum must be set.");

            RuleFor(d => d)
                .Must(HaveDiscountedPersonalBenefitCostMarkedCorrectly)
                .WithMessage("HasDiscountedPersonalBenefitCost does not align with PersonalBenefitCostPerAnnum. ");
        }

        #region Custom Validation Helpers
        private bool HaveDiscountedPersonalBenefitCostMarkedCorrectly(Dependent dependent)
        {
            if (dependent.PersonalBenefitCostPerAnnum < PayAndBenefitConstants.DEFAULT_ANNUAL_COST_FOR_DEPENDENT)
            {
                return (dependent.HasDiscountedPersonalBenefitCost == true);
            }

            return (dependent.HasDiscountedPersonalBenefitCost == false);
        }
        #endregion
    }
}
