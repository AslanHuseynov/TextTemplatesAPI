using Company.Model.Models;

namespace Company.Application.Interfaces
{
    public interface ITemplateRepository : IGenericRepository<Template>
    {
        Task<Template> UpdateEntity(int id, Template req, string userName);
    }
}
