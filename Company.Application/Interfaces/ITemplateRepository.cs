using Company.Application.Dtos.TemplateDto;
using Company.Model.Models;

namespace Company.Application.Interfaces
{
    public interface ITemplateRepository : IGenericRepository<Template>
    {
        Task<Template> AddEntity(Template template, string userName);
        Task<Template> UpdateEntity(int id, Template req, string userName);
        Task<List<Template>?> DeleteEntity(int id, string userName);
    }
}
