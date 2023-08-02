using Company.Application.Interfaces;
using Company.Model.Models;
using Company.Persistence.DB;

namespace Company.Persistence.Repositories
{
    public class TemplateRepository : GenericRepository<Template>, ITemplateRepository
    {
        private readonly DataContext _db;

        public TemplateRepository(DataContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Template> AddEntity(Template template, string userName)
        {
            var templateAuditTrail = new TemplateAuditTrail() { ChangeDate = DateTime.Now
            , TemplateId = template.Id, UserName = userName, Action = Application.TemplateAction.Add };

            _db.AuditTrails.Add(templateAuditTrail);

            var entity = await AddEntity(template);
            return entity;
        }

        public async Task<Template> UpdateEntity(int id, Template req, string userName)
        {
            
            var entity = await GetEntity(id);
            var oldText = entity.Text;
            var templateAuditTrail = new TemplateAuditTrail() { ChangeDate = DateTime.Now, UpdatedContent = oldText, UserName = userName, TemplateId = req.Id };
            _db.AuditTrails.Add(templateAuditTrail);
            var updatedEntity = await base.UpdateEntity(id, req);
            return updatedEntity;
        }

        public async Task<List<Template>?> DeleteEntity(int id, string userName)
        {
            var templateAuditTrail = new TemplateAuditTrail()
            {
                ChangeDate = DateTime.Now
            ,
                TemplateId = id,
                UserName = userName,
                Action = Application.TemplateAction.Delete
            };
            _db.AuditTrails.Add(templateAuditTrail);
            var data = await DeleteEntity(id);
            return data;
        }


    }
}
