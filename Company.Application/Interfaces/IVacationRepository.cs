using Company.Model.Models;

namespace Company.Application.Interfaces
{
    public interface IVacationRepository : IGenericRepository<Vacation>
    {
        Task<List<Vacation>> GetVacations(int employeeId);
        Task<Vacation?> GetCurrentVacation(int employeeId);
    }
}
