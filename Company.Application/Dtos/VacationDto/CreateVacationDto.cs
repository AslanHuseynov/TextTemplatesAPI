﻿namespace Company.Application.Dtos.VacationDto
{
    public class CreateVacationDto : BaseVacationDto
    {
        public int EmployeeId { get; set; }
        public int DutyEmployeeId { get; set; }
    }
}
