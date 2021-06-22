using BenefitsProject.Core.Domain.Entities;
using BenefitsProject.Infrastructure.Persistence;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BenefitsProject.Infrastructure.Tests.EmployeeRepositoryTests
{
    public class Delete_Async_Should
    {
        [Fact]
        public async Task DeleteGivenEmployeeFromTheDatabase()
        {
            var options = DataHelpers.GetDbContextOptions("Delete_Async_Should_1");
            Employee employee;

            #region Test Setup
            using (var context = new BenefitsDbContext(options))
            {
                var _employeeRepository = new EmployeeRepository(context);

                employee = await _employeeRepository.Add_Async(DataHelpers.CreateValidEmployee());
            }
            #endregion

            using (var context = new BenefitsDbContext(options))
            {
                var _employeeRepository = new EmployeeRepository(context);

                await _employeeRepository.Delete_Async(employee);
            }

            using (var context = new BenefitsDbContext(options))
            {
                Assert.Equal(0, context.Employees.Count());
                Assert.Equal(0, context.Dependents.Count());
            }
        }

        [Fact]
        public async Task ThrowExceptionIfEmployeeIsNotInDatabase()
        {
            var options = DataHelpers.GetDbContextOptions("Delete_Async_Should_2");

            using var context = new BenefitsDbContext(options);

            var _employeeRepository = new EmployeeRepository(context);

            var employeeThatIsNotInDatabase = DataHelpers.CreateValidEmployee();
            employeeThatIsNotInDatabase.Id = 1;

            await Assert.ThrowsAnyAsync<Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException>(() => _employeeRepository.Delete_Async(employeeThatIsNotInDatabase));
        }
    }
}
