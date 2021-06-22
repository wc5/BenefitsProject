using BenefitsProject.Core.Domain.Contracts;
using BenefitsProject.Core.Domain.Entities;
using BenefitsProject.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace BenefitsProject.WebApi.UnitTests.EmployeeControllerTests
{
    public class Delete_Should
    {
        private readonly Mock<IEmployeeService> _fakeEmployeeService;
        private readonly EmployeeController _employeeController;
        private readonly int _id = 1;

        public Delete_Should()
        {
            _fakeEmployeeService = new Mock<IEmployeeService>();
            _fakeEmployeeService
                .Setup(e => e.GetById_Async(_id))
                .ReturnsAsync(DataHelpers.GetEmployeeById(_id));

            _employeeController = new EmployeeController(_fakeEmployeeService.Object, AutoMapperSingleton.Mapper);
        }

        [Fact]
        public async Task InvokeOnceEmployeeServiceGetById_Async()
        {
            var employees = await _employeeController.Delete(_id);

            _fakeEmployeeService.Verify(e => e.GetById_Async(_id), Times.Once);
        }

        [Fact]
        public async Task ReturnNotFoundIfEmployeeDoesNotExists()
        {
            var actionResult = await _employeeController.Delete(_id + 1);

            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public async Task InvokeOnceEmployeeServiceDelete_Async()
        {
            var _ = await _employeeController.Delete(_id);

            _fakeEmployeeService.Verify(e => e.Delete_Async(It.IsAny<Employee>()), Times.Once);
        }

        [Fact]
        public async Task ReturnNoContentAfterDeletion()
        {
            var actionResult = await _employeeController.Delete(_id);

            Assert.IsType<NoContentResult>(actionResult);
        }
    }
}
