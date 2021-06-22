using BenefitsProject.Core.Application.Contracts;
using BenefitsProject.Core.Application.Services;
using BenefitsProject.Core.Domain.Contracts;
using BenefitsProject.Core.Domain.Entities;
using FluentValidation;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BenefitsProject.Core.UnitTests.EmployeeServiceTests
{
    public class Update_Async_Should
    {
        #region Test Setup
        private readonly Mock<IEmployeeRepository> _fakeRepository;
        private readonly Mock<IBenefitCalculator> _fakeCalculator;
        private readonly Mock<IValidator<Employee>> _fakeValidator;
        private readonly IEmployeeService _employeeService;
        private readonly int _id = 1;

        public Update_Async_Should()
        {
            _fakeRepository = new Mock<IEmployeeRepository>();
            _fakeRepository
                .Setup(r => r.Update_Async(It.IsAny<Employee>()))
                .ReturnsAsync(DataHelpers.GetEmployeeById(_id));

            _fakeCalculator = new Mock<IBenefitCalculator>();
            _fakeValidator = new Mock<IValidator<Employee>>();

            _employeeService = new EmployeeService(_fakeRepository.Object, _fakeCalculator.Object, _fakeValidator.Object);
        }
        #endregion

        [Fact]
        public async Task InvokeOnceBenefitCalculatorCalculateAndSetSalaryAndBenefitData()
        {
            var newEmployee = new Employee();

            var employee = await _employeeService.Update_Async(newEmployee);

            _fakeCalculator.Verify(c => c.CalculateAndSetSalaryAndBenefitData(newEmployee), Times.Once);
        }

        [Fact]
        public async Task InvokeOnceEmployeeRepositoryUpdate_Async()
        {
            var newEmployee = new Employee();

            var employee = await _employeeService.Update_Async(newEmployee);

            _fakeRepository.Verify(r => r.Update_Async(newEmployee), Times.Once);
        }

        [Fact]
        public async Task ReturnExpectedEmployee()
        {
            var employee = await _employeeService.Update_Async(DataHelpers.GetEmployeeById(_id));

            Assert.NotNull(employee);
            Assert.Equal(_id, employee.Id);
        }

        [Fact]
        public async Task ReturnDependentsWithEmployee()
        {
            var employee = await _employeeService.Update_Async(DataHelpers.GetEmployeeById(_id));

            Assert.Equal(2, employee.Dependents.Count);
            Assert.Equal(_id, employee.Dependents.First().EmployeeId);
        }
    }
}