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
            
            CreateMap<EmployeeDto, EmployeeModel>()
                .PreserveReferences()
                .ForMember(model => model.JobTitle, options => 
                    options.MapFrom(model => Enum.Parse(typeof(JobTitle), model.JobTitle.ToString())))
                .ForAllMembers(options => 
                    options.Condition((src, dest, sourceMember) => sourceMember != null));
            
            CreateMap<EmployeeModel, EmployeeDto>()
                .PreserveReferences()
                .ForMember(dto => dto.JobTitle, options => 
                    options.MapFrom(model => model.JobTitle.ToString()))
                .ForAllMembers(options => 
                    options.Condition((src, dest, sourceMember) => sourceMember != null));
            
                
        }
    }
}