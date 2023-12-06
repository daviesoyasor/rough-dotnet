using Ardalis.GuardClauses;
using Polaris.Entities;
using Polaris.Exceptions;
using Polaris.Extensions;
using Polaris.Extensions.Guards;
using Polaris.Shared;

namespace Polaris.Repositories
{
    public class UserRepository : IRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext dbcontext)
        {
            this._context = dbcontext;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            //Use Guards
            Protect.Against.NullOrEmpty(employee.Name);
            Ensure.NotNullOrContainWhiteSpace(employee.Name);
 
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
            //throw new CompanyNotFoundException("The company with name Visrand is not found"); 
        }

    }
}
