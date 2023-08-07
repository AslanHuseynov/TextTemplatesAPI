using Company.Application.Interfaces;
using Company.Model.Models;
using Company.Persistence.DB;

namespace Company.Persistence.Repositories
{
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(DataContext db) : base(db)
        {
        }
    }
}
