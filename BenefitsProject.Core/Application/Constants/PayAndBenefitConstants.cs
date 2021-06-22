namespace BenefitsProject.Core.Application.Constants
{
    public static class PayAndBenefitConstants
    {
        // These are taken from challenge assumptions
        public const int NUMBER_OF_PAY_PERIODS = 26;

        public const decimal DEFAULT_SALARY_PER_PAY_PERIOD = 2000m;
        public const decimal DISCOUNT_FOR_FIRST_LETTER_BEING_A = 0.1m;

        public const decimal DEFAULT_ANNUAL_COST_FOR_EMPLOYEE = 1000m;
        public const decimal DEFAULT_ANNUAL_COST_FOR_DEPENDENT = 500m;
    }
}
