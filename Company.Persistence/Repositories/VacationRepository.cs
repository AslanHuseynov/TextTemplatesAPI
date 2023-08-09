using Company.Application.Interfaces;
using Company.Model.Models;
using Company.Persistence.DB;
using Microsoft.EntityFrameworkCore;

namespace Company.Persistence.Repositories
{
    public class VacationRepository : GenericRepository<Vacation>, IVacationRepository
    {
        public VacationRepository(DataContext db) : base(db)
        {
        }

        public async Task<List<Vacation>> GetVacations(int employeeId)
        {
            var vacations = await _dbContext.Vacations.Where(x => x.EmployeeId == employeeId).ToListAsync();
            return vacations;
        }

        public async Task<Vacation?> GetCurrentVacation(int employeeId)
        {
            var now = DateTime.Now.Date;
            var currentVacation = await _dbContext.Vacations.SingleOrDefaultAsync(x => 
            x.EmployeeId == employeeId 
            && x.StartDate <= now 
            && x.EndDate >= now);
            return currentVacation;
        }
    }
}
