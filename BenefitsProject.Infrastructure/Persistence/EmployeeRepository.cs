using BenefitsProject.Core.Application.Contracts;
using BenefitsProject.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace BenefitsProject.Infrastructure.Persistence
{
    public class EmployeeRepository : IEmployeeRepository
    {
        protected readonly BenefitsDbContext _context;

        public EmployeeRepository(BenefitsDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> Add_Async(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return employee;
        }
        public async Task<Employee> GetById_Async(int id)
        {
            return await _context.Employees.Include(e => e.Dependents).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IReadOnlyList<Employee>> GetAll_Async()
        {
            return await _context.Employees.Include(e => e.Dependents).ToListAsync();
        }

        public async Task<Employee> Update_Async(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        public async Task Delete_Async(Employee employee)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
    }
}
