using BenefitsProject.Infrastructure.Persistence;
using System.Threading.Tasks;
using Xunit;

namespace BenefitsProject.Infrastructure.Tests.EmployeeRepositoryTests
{
    public class Update_Async_Should
    {
        private readonly int _id = 1;
        private readonly string _updatedFirstName = "UpdatedFirstName";

        [Fact]
        public async Task UpdateAnEmployeeThatIsAlreadyInTheDatabase()
        {
            var options = DataHelpers.GetDbContextOptions("Update_Async_Should_1");

            using (var context = new BenefitsDbContext(options))
            {
                var _employeeRepository = new EmployeeRepository(context);

                var employee = await _employeeRepository.Add_Async(DataHelpers.CreateValidEmployee());
            }

            using (var context = new BenefitsDbContext(options))
            {
                var _employeeRepository = new EmployeeRepository(context);

                var employeeToUpdate = await _employeeRepository.GetById_Async(_id);

                Assert.NotEqual(_updatedFirstName, employeeToUpdate.FirstName);

                employeeToUpdate.FirstName = _updatedFirstName;

                await _employeeRepository.Update_Async(employeeToUpdate);
            }

            using (var context = new BenefitsDbContext(options))
            {
                var _employeeRepository = new EmployeeRepository(context);

                var updatedEmployee = await _employeeRepository.GetById_Async(_id);

                Assert.Equal(_updatedFirstName, updatedEmployee.FirstName);
            }
        }

        [Fact]
        public async Task ThrowAnExceptionIfEmployeeIsNotInTheDatabase()
        {
            var options = DataHelpers.GetDbContextOptions("Update_Async_Should_2");

            using var context = new BenefitsDbContext(options);

            var _employeeRepository = new EmployeeRepository(context);

            var employeeThatIsNotInDatabase = DataHelpers.CreateValidEmployee();
            employeeThatIsNotInDatabase.Id = _id;

            await Assert.ThrowsAnyAsync<Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException>(() => _employeeRepository.Update_Async(employeeThatIsNotInDatabase));
        }
    }
}
