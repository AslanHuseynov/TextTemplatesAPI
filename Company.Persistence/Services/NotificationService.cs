using Company.Application.Interfaces;
using Company.Model.Models;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Company.Persistence.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ITemplateRepository _templateRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IVacationRepository _vacationRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IConfiguration _configuration;

        public NotificationService(ITemplateRepository templateRepository, IEmployeeRepository employeeRepository,
            IVacationRepository vacationRepository, INotificationRepository notificationRepository, IConfiguration configuration)
        {
            _templateRepository = templateRepository;
            _employeeRepository = employeeRepository;
            _vacationRepository = vacationRepository;
            _notificationRepository = notificationRepository;
            _configuration = configuration;
        }
        public async Task<string> SendMail(int employeeId, string mail)
        {
            var currentVacation = await _vacationRepository.GetCurrentVacation(employeeId);
            if (currentVacation == null)
                throw new Exception("The employee has no active or ongoing vacation!");

            var templateId = _configuration.GetValue<int>("MailTemplateId");

            var template = await _templateRepository.GetEntity(templateId);
            var employee = await _employeeRepository.GetEntity(employeeId);
            var secondEmployee = await _employeeRepository.GetEntity(currentVacation.DutyEmployeeId);

            var templateData = new
            {
                FirstFullName = employee.FullName,
                StartDate = currentVacation.StartDate.ToString("yyyy-MM-dd"),
                EndDate = currentVacation.EndDate.ToString("yyyy-MM-dd"),
                secondEmployee.PhoneNumber,
                secondEmployee.Email,
                SecondFullName = secondEmployee.FullName,
            };

            var result = GenerateText(templateData, template.Text);

            var notification = new Notification() { Text = result.ToString(), Addressee = mail };

            await _notificationRepository.AddEntity(notification);

            return result.ToString();
        }

        public async Task<string> SendSMS(int employeeId, string number)
        {
            var templateId = _configuration.GetValue<int>("SMSTemplateId");

            var template = await _templateRepository.GetEntity(templateId);
            var year = DateTime.Now.Year;
            var vacations = (await _vacationRepository.GetVacations(employeeId))
                .Where(x => x.StartDate.Year == year || x.EndDate.Year == year);

            int totalDays = 0;

            foreach (var vacation in vacations)
            {
                if (vacation.StartDate.Year == vacation.EndDate.Year)
                {
                    var days = GetTotalDays(vacation.EndDate, vacation.StartDate);
                    totalDays += days;
                }
                else
                {
                    if (vacation.StartDate.Year == year - 1)
                    {
                        var days = GetTotalDays(vacation.EndDate, new DateTime(year, 1, 1));
                        totalDays += days;
                    }
                    else if (vacation.EndDate.Year == year + 1)
                    {
                        var days = GetTotalDays(new DateTime(year, 12, 31), vacation.StartDate);
                        totalDays += days;
                    }
                }
            }

            var templateData = new
            {
                NumberOfLeftDays = totalDays
            };

            var result = GenerateText(templateData, template.Text);

            var notification = new Notification() { Text = result.ToString(), Addressee = number };

            await _notificationRepository.AddEntity(notification);

            return result.ToString();
        }

        private static string GenerateText(object obj, string text)
        {
            StringBuilder result = new StringBuilder(text);

            foreach (var propertyInfo in obj.GetType().GetProperties())
            {
                var placeholder = "@" + propertyInfo.Name;
                var value = propertyInfo.GetValue(obj)?.ToString() ?? string.Empty;
                result.Replace(placeholder, value);
            }
            return result.ToString();
        }
        private static int GetTotalDays(DateTime end, DateTime from)
        {
            var days =(24 - (int)(end - from).TotalDays);
            return days;
        }
    }
}
