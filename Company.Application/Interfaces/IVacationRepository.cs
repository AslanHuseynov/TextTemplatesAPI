using Company.Model.Models;

namespace Company.Application.Interfaces
{
    public interface IVacationRepository
    {
        Task<List<Vacation>> GetAllVacations();
        Task<Vacation?> GetVacation(int id);
        Task<List<Vacation>> CreateVacation(Vacation vacation);
        Task<List<Vacation>?> UpdateVacation(int id, Vacation req);
        Task<List<Vacation>?> DeleteVacation(int id);
    }
}
