using Company.Application.Interfaces;
using Company.Model.Models;
using Company.Persistence.DB;
using Microsoft.EntityFrameworkCore;

namespace Company.Persistence.Repositories
{
    public class VacationRepository : IVacationRepository
    {
        private readonly DataContext _db;

        public VacationRepository(DataContext db)
        {
            _db = db;
        }

        public async Task<List<Vacation>> CreateVacation(Vacation vacation)
        {
            _db.Vacations.Add(vacation);
            await _db.SaveChangesAsync();
            return await _db.Vacations.ToListAsync();
        }

        public async Task<List<Vacation>?> DeleteVacation(int id)
        {
            var vac = await _db.Vacations.FindAsync(id);
            if (vac is null)
                return null;

            _db.Vacations.Remove(vac);
            await _db.SaveChangesAsync();

            return await _db.Vacations.ToListAsync();
        }

        public async Task<List<Vacation>> GetAllVacations()
        {
            var vac = await _db.Vacations.ToListAsync();
            return vac;
        }

        public async Task<Vacation?> GetVacation(int id)
        {
            var vac = await _db.Vacations.FindAsync(id);
            if (vac is null)
                return null;

            return vac;
        }

        public async Task<List<Vacation>?> UpdateVacation(int id, Vacation req)
        {
            var vac = await _db.Vacations.FindAsync(id);
            if (vac is null)
                return null;

            vac.Reason = req.Reason;
            vac.StartDate = req.StartDate;
            vac.EndDate = req.EndDate;
            vac.EmployeeId = req.EmployeeId;

            await _db.SaveChangesAsync();

            return await _db.Vacations.ToListAsync();
        }
    }
}
