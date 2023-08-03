using Company.Application.Interfaces;
using Company.Model.Models;
using Company.Persistence.DB;

namespace Company.Persistence.Repositories
{
    public class TemplateRepository : GenericRepository<Template>, ITemplateRepository
    {
        public TemplateRepository(DataContext db) : base(db)
        {
        }
        public async Task<Template> AddEntity(Template template, string userName)
        {
            var entity = await AddEntity(template);

            var templateAuditTrail = new TemplateAuditTrail()
            {
                ChangeDate = DateTime.Now,
                UpdatedContent = string.Empty,
                TemplateId = template.Id,
                UserName = userName,
                Action = Application.TemplateAction.Add
            };

            _dbContext.AuditTrails.Add(templateAuditTrail);

            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<Template> UpdateEntity(int id, Template req, string userName)
        {
            var entity = await GetEntity(id);
            var oldText = entity.Text;
            var templateAuditTrail = new TemplateAuditTrail()
            {
                ChangeDate = DateTime.Now,
                UpdatedContent = oldText,
                Action = Application.TemplateAction.Update,
                UserName = userName,
                TemplateId = req.Id
            };
            _dbContext.AuditTrails.Add(templateAuditTrail);
            var updatedEntity = await base.UpdateEntity(id, req);
            return updatedEntity;
        }

        public async Task<List<Template>?> DeleteEntity(int id, string userName)
        {
            var entity = await GetEntity(id);
            var templateAuditTrail = new TemplateAuditTrail()
            {
                ChangeDate = DateTime.Now,
                TemplateId = entity.Id,
                UpdatedContent = entity.Text,
                UserName = userName,
                Action = Application.TemplateAction.Delete
            };
            _dbContext.AuditTrails.Add(templateAuditTrail);

            await _dbContext.SaveChangesAsync();

            var data = await base.DeleteEntity(id);

            return data;
        }
    }
}
