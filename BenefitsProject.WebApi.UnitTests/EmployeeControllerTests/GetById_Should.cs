using BenefitsProject.Core.Domain.Contracts;
using BenefitsProject.WebApi.Controllers;
using BenefitsProject.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace BenefitsProject.WebApi.UnitTests.EmployeeControllerTests
{
    public class GetById_Should
    {
        private readonly Mock<IEmployeeService> _fakeEmployeeService;
        private readonly EmployeeController _employeeController;
        private readonly int _id = 1;

        public GetById_Should()
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
            var employees = await _employeeController.GetById(_id);

            _fakeEmployeeService.Verify(e => e.GetById_Async(_id), Times.Once);
        }

        [Fact]
        public async Task ReturnOkObjectResultIfEmployeeExists()
        {
            var employee = await _employeeController.GetById(_id);

            Assert.IsType<OkObjectResult>(employee.Result);
        }

        [Fact]
        public async Task ReturnEmployeeReadDtoIfExmployeeExists()
        {
            var employee = await _employeeController.GetById(_id);

            var okObjectResult = Assert.IsType<OkObjectResult>(employee.Result);

            Assert.IsType<EmployeeReadDto>(okObjectResult.Value);
        }

        [Fact]
        public async Task ReturnNotFoundIfIdIsNotAnEmployeeId()
        {
            var employee = await _employeeController.GetById(_id + 1);

            Assert.IsType<NotFoundResult>(employee.Result);
        }
    }
}
