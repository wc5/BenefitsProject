using BenefitsProject.Core.Application.Contracts;
using BenefitsProject.Core.Application.Services;
using BenefitsProject.Core.Domain.Contracts;
using BenefitsProject.Core.Domain.Entities;
using FluentValidation;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace BenefitsProject.Core.UnitTests.EmployeeServiceTests
{
    public class GetAll_Async_Should
    {
        #region Test Setup
        private readonly Mock<IEmployeeRepository> _fakeRepository;
        private readonly Mock<IBenefitCalculator> _fakeCalculator;
        private readonly Mock<IValidator<Employee>> _fakeValidator;
        private readonly IEmployeeService _employeeService;

        public GetAll_Async_Should()
        {
            _fakeRepository = new Mock<IEmployeeRepository>();
            _fakeRepository
                .Setup(r => r.GetAll_Async())
                .ReturnsAsync(DataHelpers.GetTwoEmployees());
            
            _fakeCalculator = new Mock<IBenefitCalculator>();
            _fakeValidator = new Mock<IValidator<Employee>>();

            _employeeService = new EmployeeService(_fakeRepository.Object, _fakeCalculator.Object, _fakeValidator.Object);
        }
        #endregion

        [Fact]
        public async Task InvokeOnceEmployeeRepositoryGetAll_Async()
        {
            var employees = await _employeeService.GetAll_Async();

            _fakeRepository.Verify(r => r.GetAll_Async(), Times.Once);
        }

        [Fact]
        public async Task ReturnAllEmployees()
        {
            var employees = await _employeeService.GetAll_Async();

            Assert.Equal(2, employees.Count);
        }

        [Fact]
        public async Task ReturnDependentsWithEmployees()
        {
            var employees = await _employeeService.GetAll_Async();

            Assert.Equal(4, employees[0].Dependents.Count + employees[1].Dependents.Count);
        }
    }
}