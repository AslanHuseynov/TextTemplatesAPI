namespace Company.Application.Interfaces
{
    public interface INotificationService
    {
        Task<string> SendSMS(int employeeId, string number, string user);
        Task<string> SendMail(int employeeId, string mail, string user);
    }
}
