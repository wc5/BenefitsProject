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
    public class Delete_Async_Should
    {
        #region Test Setup
        private readonly Mock<IEmployeeRepository> _fakeRepository;
        private readonly Mock<IBenefitCalculator> _fakeCalculator;
        private readonly Mock<IValidator<Employee>> _fakeValidator;
        private readonly IEmployeeService _employeeService;

        public Delete_Async_Should()
        {
            _fakeRepository = new Mock<IEmployeeRepository>();
            _fakeRepository
                .Setup(r => r.Delete_Async(It.IsAny<Employee>()));

            _fakeCalculator = new Mock<IBenefitCalculator>();
            _fakeValidator = new Mock<IValidator<Employee>>();

            _employeeService = new EmployeeService(_fakeRepository.Object, _fakeCalculator.Object, _fakeValidator.Object);
        }
        #endregion

        [Fact]
        public async Task InvokeOnceEmployeeRepositoryDelete_Async()
        {
            var employee = new Employee();

            await _employeeService.Delete_Async(employee);

            _fakeRepository.Verify(r => r.Delete_Async(employee), Times.Once);
        }
    }
}