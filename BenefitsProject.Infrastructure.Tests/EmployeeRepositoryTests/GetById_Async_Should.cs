using BenefitsProject.Infrastructure.Persistence;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BenefitsProject.Infrastructure.Tests.EmployeeRepositoryTests
{
    public class GetById_Async_Should
    {
        [Fact]
        public async Task GetExpectedEmployeeWithDependentsIfExists()
        {
            var options = DataHelpers.GetDbContextOptions("GetById_Async_Should_1");

            using (var context = new BenefitsDbContext(options))
            {
                var _employeeRepository = new EmployeeRepository(context);

                _ = await _employeeRepository.Add_Async(DataHelpers.CreateValidEmployee());
            }

            using (var context = new BenefitsDbContext(options))
            {
                var _employeeRepository = new EmployeeRepository(context);

                var employee = await _employeeRepository.GetById_Async(1);

                Assert.NotNull(employee);
                Assert.Equal(1, employee.Id);

                Assert.NotNull(employee.Dependents);
                Assert.Equal(1, employee.Dependents.First().EmployeeId);
            }
        }

        [Fact]
        public async Task ReturnNullIfNoEmployeeWithGivenIdExistsInDatabase()
        {
            var options = DataHelpers.GetDbContextOptions("GetById_Async_Should_2");

            using (var context = new BenefitsDbContext(options))
            {
                var _employeeRepository = new EmployeeRepository(context);

                _ = await _employeeRepository.Add_Async(DataHelpers.CreateValidEmployee());
            }

            using (var context = new BenefitsDbContext(options))
            {
                var _employeeRepository = new EmployeeRepository(context);

                var employee = await _employeeRepository.GetById_Async(2);

                Assert.Null(employee);
            }
        }
    }
}
