namespace Company.Application.Interfaces
{
    public interface INotificationService
    {
        Task<string> SendSMS(int employeeId, string number);
        Task<string> SendMail(int employeeId, string mail);
    }
}
