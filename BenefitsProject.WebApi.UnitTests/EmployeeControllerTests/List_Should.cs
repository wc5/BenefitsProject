using BenefitsProject.Core.Domain.Contracts;
using BenefitsProject.WebApi.Controllers;
using BenefitsProject.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BenefitsProject.WebApi.UnitTests.EmployeeControllerTests
{
    public class List_Should
    {
        private readonly Mock<IEmployeeService> _fakeEmployeeService;
        private readonly EmployeeController _employeeController;

        public List_Should()
        {
            _fakeEmployeeService = new Mock<IEmployeeService>();
            _fakeEmployeeService
                .Setup(e => e.GetAll_Async())
                .ReturnsAsync(DataHelpers.GetTwoEmployees());

            _employeeController = new EmployeeController(_fakeEmployeeService.Object, AutoMapperSingleton.Mapper);
        }

        [Fact]
        public async Task InvokeOnceEmployeeServiceGetAll_Async()
        {
            var employees = await _employeeController.List();

            _fakeEmployeeService.Verify(e => e.GetAll_Async(), Times.Once);
        }

        [Fact]
        public async Task ReturnOkObjectResult()
        {
            var employees = await _employeeController.List();

            Assert.IsType<OkObjectResult>(employees.Result);
        }

        [Fact]
        public async Task ReturnReadOnlyListOfTypeEmployeeReadDto()
        {
            var employees = await _employeeController.List();

            var okObjectResult = Assert.IsType<OkObjectResult>(employees.Result);

            Assert.IsAssignableFrom<IReadOnlyList<EmployeeReadDto>>(okObjectResult.Value);
        }
    }
}
