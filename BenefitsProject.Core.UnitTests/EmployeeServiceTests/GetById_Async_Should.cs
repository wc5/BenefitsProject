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
    public class GetById_Async_Should
    {
        #region Test Setup
        private readonly Mock<IEmployeeRepository> _fakeRepository;
        private readonly Mock<IBenefitCalculator> _fakeCalculator;
        private readonly Mock<IValidator<Employee>> _fakeValidator;
        private readonly IEmployeeService _employeeService;
        private readonly int _id = 1;

        public GetById_Async_Should()
        {
            _fakeRepository = new Mock<IEmployeeRepository>();
            _fakeRepository
                .Setup(r => r.GetById_Async(_id))
                .ReturnsAsync(DataHelpers.GetEmployeeById(_id));

            _fakeCalculator = new Mock<IBenefitCalculator>();
            _fakeValidator = new Mock<IValidator<Employee>>();

            _employeeService = new EmployeeService(_fakeRepository.Object, _fakeCalculator.Object, _fakeValidator.Object);
        }
        #endregion

        [Fact]
        public async Task InvokeOnceEmployeeRepositoryGetById_Async()
        {
            var employees = await _employeeService.GetById_Async(_id);

            _fakeRepository.Verify(r => r.GetById_Async(_id), Times.Once);
        }

        [Fact]
        public async Task ReturnDesiredEmployee()
        {
            var employee = await _employeeService.GetById_Async(_id);

            Assert.NotNull(employee);
            Assert.Equal(_id, employee.Id);
        }

        [Fact]
        public async Task ReturnDependentsWithEmployee()
        {
            var employee = await _employeeService.GetById_Async(_id);

            Assert.Equal(2, employee.Dependents.Count);
            Assert.Equal(_id, employee.Dependents.First().EmployeeId);
        }
    }
}