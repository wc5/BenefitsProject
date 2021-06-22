using BenefitsProject.Core.Domain.Contracts;
using BenefitsProject.Core.Domain.Entities;
using BenefitsProject.WebApi.Controllers;
using BenefitsProject.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BenefitsProject.WebApi.UnitTests.EmployeeControllerTests
{
    public class Put_Should
    {
        private readonly Mock<IEmployeeService> _fakeEmployeeService;
        private readonly EmployeeController _employeeController;
        private readonly int _id = 1;

        public Put_Should()
        {
            _fakeEmployeeService = new Mock<IEmployeeService>();
            _fakeEmployeeService
                .Setup(e => e.Update_Async(It.IsAny<Employee>()))
                .Throws(It.IsAny<Exception>());

            _employeeController = new EmployeeController(_fakeEmployeeService.Object, AutoMapperSingleton.Mapper);
        }

        [Fact]
        public async Task ReturnBadRequestIfIdAndUpdateDtoIdDoNotMatch()
        {
            var actionResult = await _employeeController.Put(_id, new EmployeeUpdateDto { Id = _id + 1 });

            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async Task InvokeOnceEmployeeServiceUpdate_Async()
        {
            var employees = await _employeeController.Put(_id, new EmployeeUpdateDto { Id = _id });

            _fakeEmployeeService.Verify(e => e.Update_Async(It.IsAny<Employee>()), Times.Once);
        }

        [Fact]
        public async Task ReturnBadRequestObjectIfExceptionIsThrown()
        {
            var actionResult = await _employeeController.Put(_id, new EmployeeUpdateDto { Id = _id });

            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public async Task ReturnOkIfNoExceptionIsThrown()
        {
            var anotherFakeEmployeeService = new Mock<IEmployeeService>();
            anotherFakeEmployeeService
                .Setup(a => a.Update_Async(It.IsAny<Employee>()))
                .ReturnsAsync(DataHelpers.GetEmployeeById(_id));

            var anotherEmployeeController = new EmployeeController(anotherFakeEmployeeService.Object, AutoMapperSingleton.Mapper);
            
            var actionResult = await anotherEmployeeController.Put(_id, new EmployeeUpdateDto { Id = _id });

            Assert.IsType<OkResult>(actionResult);
        }
    }
}
