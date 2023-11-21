using Polaris.Entities;
using Polaris.Exceptions;

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
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            throw new CompanyNotFoundException("The company with name Visrand is not found"); 
        }

    }
}
