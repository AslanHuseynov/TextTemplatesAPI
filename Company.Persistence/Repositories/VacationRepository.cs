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
    }
}
