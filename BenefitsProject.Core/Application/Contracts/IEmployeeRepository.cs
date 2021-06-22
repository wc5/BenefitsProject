using BenefitsProject.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BenefitsProject.Core.Application.Contracts
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetById_Async(int id);
        Task<IReadOnlyList<Employee>> GetAll_Async();
        Task<Employee> Add_Async(Employee employee);
        Task<Employee> Update_Async(Employee employee);
        Task Delete_Async(Employee employee);
    }
}