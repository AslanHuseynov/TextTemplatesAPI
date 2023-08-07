using Company.Model.Models;

namespace Company.Application.Interfaces
{
    public interface ITemplateOperationsRepository
    {
        Task<List<Template>> SendSMS(Template temp);
        Task<List<Template>> SendMail(Template temp);
    }
}
