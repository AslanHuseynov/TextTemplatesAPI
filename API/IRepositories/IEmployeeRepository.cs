using Company.Model.Models;

namespace API.IRepositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllEmployees();
        Task<Employee?> GetEmployee(int id);
        Task<List<Employee>> CreateEmployee(Employee employee);
        Task<List<Employee>?> UpdateEmployee(int id, Employee req);
        Task<List<Employee>?> DeleteEmployee(int id);
    }
}
