using BenefitsProject.Core.Application.Constants;
using BenefitsProject.Core.Application.Services;
using BenefitsProject.Core.Domain.Contracts;
using System;
using System.Linq;
using Xunit;

namespace BenefitsProject.Core.UnitTests.BenefitCalculatorTests
{
    public class CalculateAndSetSalaryBenefitData_Should
    {
        private readonly IBenefitCalculator _calculator;

        public CalculateAndSetSalaryBenefitData_Should()
        {
            _calculator = new BenefitCalculator();
        }

        [Fact]
        public void SetSalaryPerAnnumToDefaultFromApplicationConstantsIfNotAlreadySet()
        {
            var expectedSalary = Math.Round(PayAndBenefitConstants.DEFAULT_SALARY_PER_PAY_PERIOD * PayAndBenefitConstants.NUMBER_OF_PAY_PERIODS, 2);

            var employee = DataHelpers.EmployeeWithNoDependentOrDiscount();

            Assert.Equal(0, employee.SalaryPerAnnum);

            _calculator.CalculateAndSetSalaryAndBenefitData(employee);

            Assert.Equal(expectedSalary, employee.SalaryPerAnnum);
        }

        [Fact]
        public void NotSetSalaryPerAnnumToDefaultIfAlreadySet()
        {
            var nonDefaultSalary = 123.456m;

            var employee = DataHelpers.EmployeeWithNoDependentOrDiscount();

            employee.SalaryPerAnnum = nonDefaultSalary;

            _calculator.CalculateAndSetSalaryAndBenefitData(employee);

            Assert.Equal(nonDefaultSalary, employee.SalaryPerAnnum);
        }

        [Fact]
        public void SetBenefitCostsToDefaultFromApplicationConstants()
        {
            var expectedEmployeeBenefitCostWithoutDiscount = PayAndBenefitConstants.DEFAULT_ANNUAL_COST_FOR_EMPLOYEE;
            var expectedDependentBenefitCostWithoutDiscount = PayAndBenefitConstants.DEFAULT_ANNUAL_COST_FOR_DEPENDENT;

            var employee = DataHelpers.EmployeeWithOneDependentAndZeroDiscounts();

            _calculator.CalculateAndSetSalaryAndBenefitData(employee);

            Assert.Equal(expectedEmployeeBenefitCostWithoutDiscount, employee.PersonalBenefitCostPerAnnum);
            Assert.Equal(expectedDependentBenefitCostWithoutDiscount, employee.Dependents.First().PersonalBenefitCostPerAnnum);
        }

        [Fact]
        public void ApplyDiscountForHavingAFirstNameThatStartWithTheLetterARegardlessOfCase()
        {
            var expectedEmployeeBenefitCostWithDiscount = Math.Round(PayAndBenefitConstants.DEFAULT_ANNUAL_COST_FOR_EMPLOYEE * (1 - PayAndBenefitConstants.DISCOUNT_FOR_FIRST_LETTER_BEING_A), 2);
            var expectedDependentBenefitCostWithDiscount = Math.Round(PayAndBenefitConstants.DEFAULT_ANNUAL_COST_FOR_DEPENDENT * (1 - PayAndBenefitConstants.DISCOUNT_FOR_FIRST_LETTER_BEING_A), 2);

            var employee = DataHelpers.EmployeeWithOneDependentAndTwoDiscounts();

            _calculator.CalculateAndSetSalaryAndBenefitData(employee);

            Assert.Equal(expectedEmployeeBenefitCostWithDiscount, employee.PersonalBenefitCostPerAnnum);
            Assert.Equal(expectedDependentBenefitCostWithDiscount, employee.Dependents.First().PersonalBenefitCostPerAnnum);

            Assert.True(employee.HasDiscountedPersonalBenefitCost);
            Assert.True(employee.Dependents.First().HasDiscountedPersonalBenefitCost);
        }

        [Fact]
        public void NotApplyDiscountIfFirstNameDoesNotStartWithTheLetterA()
        {
            var expectedEmployeeBenefitCostWithDiscount = Math.Round(PayAndBenefitConstants.DEFAULT_ANNUAL_COST_FOR_EMPLOYEE * (1 - PayAndBenefitConstants.DISCOUNT_FOR_FIRST_LETTER_BEING_A), 2);
            var expectedDependentBenefitCostWithDiscount = Math.Round(PayAndBenefitConstants.DEFAULT_ANNUAL_COST_FOR_DEPENDENT * (1 - PayAndBenefitConstants.DISCOUNT_FOR_FIRST_LETTER_BEING_A), 2);

            var employee = DataHelpers.EmployeeWithOneDependentAndZeroDiscounts();

            _calculator.CalculateAndSetSalaryAndBenefitData(employee);

            Assert.NotEqual(expectedEmployeeBenefitCostWithDiscount, employee.PersonalBenefitCostPerAnnum);
            Assert.NotEqual(expectedDependentBenefitCostWithDiscount, employee.Dependents.First().PersonalBenefitCostPerAnnum);

            Assert.False(employee.HasDiscountedPersonalBenefitCost);
            Assert.False(employee.Dependents.First().HasDiscountedPersonalBenefitCost);
        }

        [Fact]
        public void CorrectlyTotalBenefitCosts()
        {
            var expectedTotal = Math.Round(PayAndBenefitConstants.DEFAULT_ANNUAL_COST_FOR_EMPLOYEE + PayAndBenefitConstants.DEFAULT_ANNUAL_COST_FOR_DEPENDENT, 2);

            var employee = DataHelpers.EmployeeWithOneDependentAndZeroDiscounts();

            _calculator.CalculateAndSetSalaryAndBenefitData(employee);

            Assert.Equal(expectedTotal, employee.TotalBenefitCostPerAnnum);
        }
    }
}
