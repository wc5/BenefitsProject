using BenefitsProject.Infrastructure.Persistence;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BenefitsProject.Infrastructure.Tests.EmployeeRepositoryTests
{
    public class Add_Async_Should
    {
        [Fact]
        public async Task InsertAnEmployeeIntoTheDatabase()
        {
            var options = DataHelpers.GetDbContextOptions("Add_Async_Should_1");

            using (var context = new BenefitsDbContext(options))
            {
                var _employeeRepository = new EmployeeRepository(context);

                var employee = await _employeeRepository.Add_Async(DataHelpers.CreateValidEmployee());

                Assert.NotNull(employee);
                Assert.Equal(1, employee.Id);
                Assert.Equal(2, employee.Dependents.Count);
                Assert.Equal(1, employee.Dependents.First().EmployeeId);
            }

            using (var context = new BenefitsDbContext(options))
            {
                Assert.Equal(1, context.Employees.Count());
                Assert.Equal(2, context.Dependents.Count());
            }
        }
    }
}
