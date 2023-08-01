using API.IRepositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace API.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _db;

        public EmployeeRepository(DataContext db)
        {
            _db = db;
        }

        public async Task<List<Employee>> CreateEmployee(Employee employee)
        {
            _db.Employees.Add(employee);
            await _db.SaveChangesAsync();
            return await _db.Employees.ToListAsync();
        }

        public async Task<List<Employee>?> DeleteEmployee(int id)
        {
            var employee = await _db.Employees.FindAsync(id);
            if (employee is null)
                return null;

            _db.Employees.Remove(employee);
            await _db.SaveChangesAsync();

            return await _db.Employees.ToListAsync();
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            var employees = await _db.Employees.ToListAsync();
            return employees;
        }

        public async Task<Employee?> GetEmployee(int id)
        {
            var employees = await _db.Employees.FindAsync(id);
            if (employees is null)
                return null;

            return employees;
        }

        public async Task<List<Employee>?> UpdateEmployee(int id, Employee req)
        {
            var employees = await _db.Employees.FindAsync(id);
            if (employees is null)
                return null;

            employees.FullName = req.FullName;
            employees.BirthDate = req.BirthDate;
            employees.BirthPlace = req.BirthPlace;
            employees.PhoneNumber = req.PhoneNumber;
            employees.Email = req.Email;

            await _db.SaveChangesAsync();

            return await _db.Employees.ToListAsync();
        }
    }
}
