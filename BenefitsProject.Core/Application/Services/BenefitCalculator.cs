using BenefitsProject.Core.Application.Constants;
using BenefitsProject.Core.Domain.Contracts;
using BenefitsProject.Core.Domain.Entities;
using System;

namespace BenefitsProject.Core.Application.Services
{
    public class BenefitCalculator : IBenefitCalculator
    {
        public void CalculateAndSetSalaryAndBenefitData(Employee employee)
        {
            if (employee.SalaryPerAnnum == 0)
            {
                SetDefaultSalary(employee);
            }

            SetDefaultBenefitCosts(employee);

            ApplyDiscounts(employee);

            CalculateAndSetTotalCost(employee); 
        }

        #region Helpers
        private static void SetDefaultSalary(Employee employee)
        {
            employee.SalaryPerAnnum = Math.Round(PayAndBenefitConstants.DEFAULT_SALARY_PER_PAY_PERIOD * PayAndBenefitConstants.NUMBER_OF_PAY_PERIODS, 2);
        }
        
        private static void SetDefaultBenefitCosts(Employee employee)
        {
            employee.PersonalBenefitCostPerAnnum = PayAndBenefitConstants.DEFAULT_ANNUAL_COST_FOR_EMPLOYEE;
            
            foreach (var dependent in employee.Dependents)
            {
                dependent.PersonalBenefitCostPerAnnum = PayAndBenefitConstants.DEFAULT_ANNUAL_COST_FOR_DEPENDENT;
            }
        }
        
        private static void ApplyDiscounts(Employee employee)
        {
            ApplyDiscountForFirstLetterBeingA(employee);
        }
        
        private static void CalculateAndSetTotalCost(Employee employee)
        {
            var totalCostPerAnnum = employee.PersonalBenefitCostPerAnnum;
            
            foreach (var dependent in employee.Dependents)
            {
                totalCostPerAnnum += dependent.PersonalBenefitCostPerAnnum;
            }

            employee.TotalBenefitCostPerAnnum = Math.Round(totalCostPerAnnum, 2);
        }
        #endregion
        
        #region Discounts
        private static void ApplyDiscountForFirstLetterBeingA(Employee employee)
        {
            var magicLetter = 'A';
            var discountUsefulForMultiplication = (1 - PayAndBenefitConstants.DISCOUNT_FOR_FIRST_LETTER_BEING_A);

            if (employee.FirstName.ToUpper().StartsWith(magicLetter))
            {
                employee.PersonalBenefitCostPerAnnum = Math.Round(employee.PersonalBenefitCostPerAnnum * discountUsefulForMultiplication, 2);
                employee.HasDiscountedPersonalBenefitCost = true;
            }

            foreach (var dependent in employee.Dependents)
            {
                if (dependent.FirstName.ToUpper().StartsWith(magicLetter))
                {
                    dependent.PersonalBenefitCostPerAnnum = Math.Round(dependent.PersonalBenefitCostPerAnnum * discountUsefulForMultiplication, 2);
                    dependent.HasDiscountedPersonalBenefitCost = true;
                }
            }
        }
        #endregion
    }
}
