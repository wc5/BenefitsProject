using BenefitsProject.Core.Application.Constants;
using BenefitsProject.Core.Domain.Entities;
using BenefitsProject.Core.Domain.Enumerations;
using BenefitsProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace BenefitsProject.Infrastructure.Tests
{
    public static class DataHelpers
    {
        public static Employee CreateValidEmployee()
        {
            return new Employee
            {
                FirstName = $"Employee_FirstName",
                MiddleName = $"Employee_MiddleName",
                LastName = $"Employee_LastName",
                SalaryPerAnnum = Math.Round(PayAndBenefitConstants.DEFAULT_SALARY_PER_PAY_PERIOD * PayAndBenefitConstants.NUMBER_OF_PAY_PERIODS, 2),
                PersonalBenefitCostPerAnnum = PayAndBenefitConstants.DEFAULT_ANNUAL_COST_FOR_EMPLOYEE,
                HasDiscountedPersonalBenefitCost = false,
                TotalBenefitCostPerAnnum = Math.Round(PayAndBenefitConstants.DEFAULT_ANNUAL_COST_FOR_EMPLOYEE + (2 * PayAndBenefitConstants.DEFAULT_ANNUAL_COST_FOR_DEPENDENT), 2),
                Dependents = new HashSet<Dependent>
                {
                    new Dependent
                    {
                        Category = DependentCategory.Spouse,
                        FirstName = $"Dependent_FirstName",
                        MiddleName = $"Dependent_MiddleName",
                        LastName = $"Dependent_LastName",
                        PersonalBenefitCostPerAnnum = PayAndBenefitConstants.DEFAULT_ANNUAL_COST_FOR_DEPENDENT,
                        HasDiscountedPersonalBenefitCost = false
                    },
                    new Dependent
                    {
                        Category = DependentCategory.Child,
                        FirstName = $"Dependent_FirstName",
                        MiddleName = $"Dependent_MiddleName",
                        LastName = $"Dependent_LastName",
                        PersonalBenefitCostPerAnnum = PayAndBenefitConstants.DEFAULT_ANNUAL_COST_FOR_DEPENDENT,
                        HasDiscountedPersonalBenefitCost = false
                    }
                }
            };
        }

        public static IReadOnlyList<Employee> GetTwoEmployees()
        {
            return new List<Employee>
            {
                CreateValidEmployee(),
                CreateValidEmployee()
            };
        }

        public static DbContextOptions GetDbContextOptions(string databaseName)
        {
             return new DbContextOptionsBuilder<BenefitsDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;
        }
    }
}
