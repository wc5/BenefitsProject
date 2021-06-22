using BenefitsProject.Core.Application.Constants;
using BenefitsProject.Core.Domain.Entities;
using BenefitsProject.Core.Domain.Enumerations;
using System;
using System.Collections.Generic;

namespace BenefitsProject.Core.UnitTests
{
    public static class DataHelpers
    {
        public static Employee GetEmployeeById(int id)
        {
            return new Employee
            {
                Id = id,
                FirstName = $"Employee_FirstName_{id}",
                MiddleName = $"Employee_MiddleName_{id}",
                LastName = $"Employee_LastName_{id}",
                SalaryPerAnnum = Math.Round(PayAndBenefitConstants.DEFAULT_SALARY_PER_PAY_PERIOD * PayAndBenefitConstants.NUMBER_OF_PAY_PERIODS, 2),
                PersonalBenefitCostPerAnnum = PayAndBenefitConstants.DEFAULT_ANNUAL_COST_FOR_EMPLOYEE,
                HasDiscountedPersonalBenefitCost = false,
                TotalBenefitCostPerAnnum = Math.Round(PayAndBenefitConstants.DEFAULT_ANNUAL_COST_FOR_EMPLOYEE + (2 * PayAndBenefitConstants.DEFAULT_ANNUAL_COST_FOR_DEPENDENT), 2),
                Dependents = new HashSet<Dependent>
                {
                    new Dependent
                    {
                        Id = id,
                        Category = DependentCategory.Spouse,
                        FirstName = $"Dependent_FirstName_{id}",
                        MiddleName = $"Dependent_MiddleName_{id}",
                        LastName = $"Dependent_LastName_{id}",
                        PersonalBenefitCostPerAnnum = PayAndBenefitConstants.DEFAULT_ANNUAL_COST_FOR_DEPENDENT,
                        HasDiscountedPersonalBenefitCost = false,
                        EmployeeId = id
                    },
                    new Dependent
                    {
                        Id = id + 1,
                        Category = DependentCategory.Child,
                        FirstName = $"Dependent_FirstName_{id + 1}",
                        MiddleName = $"Dependent_MiddleName_{id + 1}",
                        LastName = $"Dependent_LastName_{id + 1}",
                        PersonalBenefitCostPerAnnum = PayAndBenefitConstants.DEFAULT_ANNUAL_COST_FOR_DEPENDENT,
                        HasDiscountedPersonalBenefitCost = false,
                        EmployeeId = id
                    }
                }
            };
        }

        public static IReadOnlyList<Employee> GetTwoEmployees()
        {
            return new List<Employee>
            {
                GetEmployeeById(1),
                GetEmployeeById(2)
            };
        }

        public static Employee EmployeeWithNoDependentOrDiscount()
        {
            return new Employee
            {
                FirstName = "Bbbbbbbbb",
                MiddleName = "",
                LastName = "",
            };
        }

        public static Employee EmployeeWithOneDependentAndZeroDiscounts()
        {
            var employee = new Employee
            {
                FirstName = "Bbbbbbbbb",
            };

            employee.Dependents.Add(new Dependent
            {
                FirstName = "Zzz",
            });

            return employee;
        }

        public static Employee EmployeeWithOneDependentAndTwoDiscounts()
        {
            var employee = new Employee
            {
                FirstName = "Alabama"
            };

            employee.Dependents.Add(new Dependent
            {
                FirstName = "arkansas"
            });

            return employee;
        }
    }
}
