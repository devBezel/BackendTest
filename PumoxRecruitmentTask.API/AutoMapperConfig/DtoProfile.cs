using System;
using AutoMapper;
using PumoxRecruitmentTask.BLL.Dtos;
using PumoxRecruitmentTask.BLL.Dtos.Responses;
using PumoxRecruitmentTask.DAL.DataAccess.Models;
using PumoxRecruitmentTask.DAL.Enums;

namespace PumoxRecruitmentTask.API.AutoMapperConfig
{
    public class DtoProfile : Profile
    {
        public DtoProfile()
        {
            CreateMap<CompanyDto, CompanyModel>()
                .PreserveReferences()
                .ForAllMembers(options => 
                    options.Condition((src, dest, sourceMember) => sourceMember != null));
            CreateMap<CompanyModel, CompanyDto>()
                .PreserveReferences()
                .ForAllMembers(options =>
                    options.Condition((src, dest, sourceMember) => sourceMember != null));

            CreateMap<CompanyDto, CreatedCompanyResponseDto>();
            CreateMap<EmployeeDto, CreatedCompanyResponseDto>();

            CreateMap<EmployeeModel, EmployeeDto>()
                .PreserveReferences()
                .ForMember(
                    employeeDto => employeeDto.JobTitle,
                    opt => opt.MapFrom(
                        model => model.JobTitle.ToString()))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<EmployeeDto, EmployeeModel>()
                .PreserveReferences()
                .ForMember(
                    employeeModel => employeeModel.JobTitle,
                    opt => opt.MapFrom(
                        dto => Enum.Parse(typeof(JobTitle), dto.JobTitle)))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            
                
        }
    }
}