using Polaris.Entities;

namespace Polaris.Repositories
{
    public interface IRepository
    {
        Task<Employee> AddEmployee(Employee employee);
    }
}
