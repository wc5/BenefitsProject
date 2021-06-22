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
    public class Add_Should
    {
        private readonly Mock<IEmployeeService> _fakeEmployeeService;
        private readonly EmployeeController _employeeController;
        private readonly int _id = 1;

        public Add_Should()
        {
            _fakeEmployeeService = new Mock<IEmployeeService>();
            _fakeEmployeeService
                .Setup(e => e.Add_Async(It.IsAny<Employee>()))
                .ReturnsAsync(DataHelpers.GetEmployeeById(_id));

            _employeeController = new EmployeeController(_fakeEmployeeService.Object, AutoMapperSingleton.Mapper);
        }

        [Fact]
        public async Task InvokeOnceEmployeeServiceAdd_Async()
        {
            var actionResult = await _employeeController.Add(new EmployeeCreateDto());
            
            _fakeEmployeeService.Verify(e => e.Add_Async(It.IsAny<Employee>()), Times.Once);
        }

        [Fact]
        public async Task ReturnCreatedAtActionResultIfEmployeeCreateDtoIsValid()
        {
            var actionResult = await _employeeController.Add(new EmployeeCreateDto());

            Assert.IsType<CreatedAtActionResult>(actionResult.Result);
        }

        [Fact]
        public async Task ReturnBadRequestIfEmployeeCreateDtoIsNotValid()
        {
            var anotherFakeEmployeeService = new Mock<IEmployeeService>();
            anotherFakeEmployeeService
                .Setup(a => a.Add_Async(It.IsAny<Employee>()))
                .ThrowsAsync(It.IsAny<Exception>());

            var anotherEmployeeController = new EmployeeController(anotherFakeEmployeeService.Object, AutoMapperSingleton.Mapper);

            var actionResult = await anotherEmployeeController.Add(new EmployeeCreateDto());

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }
    }
}