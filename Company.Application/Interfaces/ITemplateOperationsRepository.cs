using Company.Model.Models;

namespace Company.Application.Interfaces
{
    public interface ITemplateOperationsRepository
    {
        Task<string> SendSMS(int employeeId, string number, string user);
        Task<string> SendMail(int employeeId, string mail, string user);
    }
}
