using System;
using ASP.NET_Core3.x_WebApi_Demo.Entities;
using AutoMapper;
using Demo.Dto.Dtos;

namespace ASP.NET_Core3.x_WebApi_Demo.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.GenderDes
                    , opt => opt.MapFrom(src => src.Gender.ToString()))
                .ForMember(dest => dest.Age,
                    opt => opt.MapFrom(src => DateTime.Now.Year - src.BirthDay.Year));

            CreateMap<EmployeeAddDto, Employee>();
            CreateMap<EmployeeUpdateDto, Employee>();
            CreateMap<Employee, EmployeeUpdateDto>();
        }
    }
}
