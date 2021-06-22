using BenefitsProject.Infrastructure.Persistence;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BenefitsProject.Infrastructure.Tests.EmployeeRepositoryTests
{
    public class GetAll_Async_Should
    {
        [Fact]
        public async Task GetAllEmployeesWithDependentsFromTheDatabase()
        {
            var options = DataHelpers.GetDbContextOptions("GetAll_Async_Should_1");

            using (var context = new BenefitsDbContext(options))
            {
                var _employeeRepository = new EmployeeRepository(context);

                foreach (var employee in DataHelpers.GetTwoEmployees())
                {
                    await _employeeRepository.Add_Async(employee);
                }
            }

            using (var context = new BenefitsDbContext(options))
            {
                var _employeeRepository = new EmployeeRepository(context);

                var employees = await _employeeRepository.GetAll_Async();

                Assert.Equal(2, context.Employees.Count());
                Assert.Equal(4, context.Dependents.Count());

                Assert.Equal(1, employees[0].Id);
                Assert.Equal(2, employees[1].Id);
            }
        }

        [Fact]
        public async Task ReturnEmptyListIfThereAreNoEmployeesInDatabase()
        {
            var options = DataHelpers.GetDbContextOptions("GetAll_Async_Should_2");

            using var context = new BenefitsDbContext(options);

            var _employeeRepository = new EmployeeRepository(context);

            var employees = await _employeeRepository.GetAll_Async();

            Assert.Equal(0, context.Employees.Count());
            Assert.Equal(0, context.Dependents.Count());

            Assert.Empty(employees);
        }
    }
}
