using Company.Application.Interfaces;
using Company.Model.Models;

namespace Company.Persistence.Repositories
{
    public class TemplateOperationsRepository : ITemplateOperationsRepository
    {
        public Task<List<Template>> SendMail(Template temp)
        {
            throw new NotImplementedException();
        }

        public Task<List<Template>> SendSMS(Template temp)
        {
            throw new NotImplementedException();
        }
    }
}
