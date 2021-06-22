using BenefitsProject.Core.Application.Contracts;
using BenefitsProject.Core.Domain.Contracts;
using BenefitsProject.Core.Domain.Entities;
using FluentValidation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BenefitsProject.Core.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IBenefitCalculator _benefitCalculator;
        private readonly IValidator<Employee> _employeeValidator;

        public EmployeeService(IEmployeeRepository employeeRepository, IBenefitCalculator benefitCalculator, IValidator<Employee> employeeValidator)
        {
            _employeeRepository = employeeRepository;
            _benefitCalculator = benefitCalculator;
            _employeeValidator = employeeValidator;
        }

        public async Task<Employee> Add_Async(Employee employee)
        {
            _benefitCalculator.CalculateAndSetSalaryAndBenefitData(employee);
            await _employeeValidator.ValidateAndThrowAsync(employee);

            return await _employeeRepository.Add_Async(employee);
        }

        public async Task Delete_Async(Employee employee)
        {
            await _employeeRepository.Delete_Async(employee);
        }

        public async Task<IReadOnlyList<Employee>> GetAll_Async()
        {
            return await _employeeRepository.GetAll_Async();
        }

        public async Task<Employee> GetById_Async(int id)
        {
            return await _employeeRepository.GetById_Async(id);
        }

        public async Task<Employee> Update_Async(Employee employee)
        {
            _benefitCalculator.CalculateAndSetSalaryAndBenefitData(employee);
            await _employeeValidator.ValidateAndThrowAsync(employee);

            return await _employeeRepository.Update_Async(employee);
        }
    }
}
