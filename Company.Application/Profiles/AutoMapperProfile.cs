using AutoMapper;
using Company.Application.Dtos.EmployeeDto;
using Company.Application.Dtos.TemplateDto;
using Company.Application.Dtos.VacationDto;
using Company.Model.Models;

namespace Company.Application.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Vacation, CreateVacationDto>().ReverseMap();
            CreateMap<Vacation, UpdateVacationDto>().ReverseMap();
            CreateMap<Template, CreateTemplateDto>().ReverseMap();
            CreateMap<Template, UpdateTemplateDto>().ReverseMap();
            CreateMap<Employee, CreateEmployeeDto>().ReverseMap();
            CreateMap<Employee, UpdateEmployeeDto>().ReverseMap();
        }
    }
}
