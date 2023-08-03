using Company.Application.Interfaces;
using Company.Model.Models;
using Company.Persistence.DB;

namespace Company.Persistence.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DataContext db) : base(db)
        {
        }
    }
}
