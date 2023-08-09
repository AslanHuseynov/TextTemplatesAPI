﻿using AutoMapper;
using Company.Application.Interfaces;
using Company.Model.Models;
using System;
using System.Text;

namespace Company.Persistence.Repositories
{
    public class TemplateOperationsRepository : ITemplateOperationsRepository
    {
        private readonly ITemplateRepository _templateRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IVacationRepository _vacationRepository;
        private readonly INotificationRepository _notificationRepository;

        public TemplateOperationsRepository(ITemplateRepository templateRepository, IEmployeeRepository employeeRepository,
            IVacationRepository vacationRepository, INotificationRepository notificationRepository)
        {
            _templateRepository = templateRepository;
            _employeeRepository = employeeRepository;
            _vacationRepository = vacationRepository;
            _notificationRepository = notificationRepository;
        }
        public async Task<string> SendMail(int templateId, int employeeId, string mail, string user)
        {
            var currentVacation = await _vacationRepository.GetCurrentVacation(employeeId);
            if (currentVacation == null)
                return string.Empty;

            var template = await _templateRepository.GetEntity(templateId);
            var employee = await _employeeRepository.GetEntity(employeeId);
            var secondEmployee = await _employeeRepository.GetEntity(currentVacation.DutyEmployeeId);


            //@FirstFullName მოგესალმებით გაცნობებთ, რომ ჩემი შვებულების პერიოდში(@StartDate -@EndDate) თქვენი პერსონალური სამედიცინო მენეჯერი არდიში იქნება ჩემი კოლეგა @SecondFullName.
            //    გთხოვთ, ნებისმიერ საკითხზე დაუკავშირდეთ მას ნომერზე @XXXXXXXXXX, ასევე Viber - ით, WhatsApp - ით ან ელფოსტით: @XXXXXXXXX@ardi.ge.საუკეთესო სურვილებით, @FirstFullName.

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

            var notification = new Notification() { SavedBy = user, Text = result.ToString(), Addressee = mail };

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
            var days = (int)(end - from).TotalDays;
            return days;
        }

        public async Task<string> SendSMS(int templateId, int employeeId, string number, string user)
        {
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

            // მოგესალმებით, გაცნობებთ რომ, თქვენ დაგრჩათ @NumberOfLeftDays დღე შვებულება.

            var templateData = new
            {
                NumberOfLeftDays = totalDays
            };

            var result = GenerateText(templateData, template.Text);

            var notification = new Notification() { SavedBy = user, Text = result.ToString(), Addressee = number };

            await _notificationRepository.AddEntity(notification);

            return result.ToString();
        }
    }
}
