using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PumoxRecruitmentTask.BLL.Dtos;
using PumoxRecruitmentTask.BLL.Dtos.Responses;
using PumoxRecruitmentTask.BLL.Interfaces.Services;
using PumoxRecruitmentTask.DAL.DataAccess.Models;
using PumoxRecruitmentTask.DAL.UnitOfWork;

namespace PumoxRecruitmentTask.BLL.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CompanyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreatedCompanyResponseDto> InsertAsync(CompanyDto dto)
        {
            var model = _mapper.Map<CompanyModel>(dto);
            var result = await _unitOfWork.CompanyRepository.InsertAsync(model);
            await _unitOfWork.SaveAsync();
            
            dto.Id = result.Id;
            return _mapper.Map<CreatedCompanyResponseDto>(dto);
        }

        public async Task<CompanyDto> UpdateAsync(long id, CompanyDto dto)
        {
            var model = await _unitOfWork.CompanyRepository.GetSingleAsync(company => company.Id == id, i 
                => i.Include(company => company.Employees));
            _mapper.Map(dto, model);
            await _unitOfWork.SaveAsync();


            return dto;
        }

        public async Task<IEnumerable<CompanyDto>> SearchAsync(SearchCompanyDto dto)
        {
            var companies = await _unitOfWork.CompanyRepository.GetAllAsync(null, 
                i => i.Include(x => x.Employees));
            
            if (!string.IsNullOrEmpty(dto.Keyword))
            {
                foreach (var keyword in dto.Keyword.Split(' ').Select(x => x.ToLower()))
                {
                    companies = companies.Where(x => x.Name.ToLower().Contains(keyword));
                }
            }

            if (dto.EmployeeDateOfBirthFrom.HasValue)
            {
                companies = companies.Where(company => company.Employees.Any(employee => 
                    employee.DateOfBirth >= dto.EmployeeDateOfBirthFrom));
            }

            if (dto.EmployeeDateOfBirthTo.HasValue)
            {
                companies = companies.Where(company =>
                    company.Employees.Any(employee => employee.DateOfBirth <= dto.EmployeeDateOfBirthTo));
            }
            
            if (dto.EmployeeJobTitles != null)
            {
                companies = companies.Where(company => company.Employees.Any(t => 
                    dto.EmployeeJobTitles.Contains(t.JobTitle.ToString())));
            }

            return _mapper.Map<IEnumerable<CompanyDto>>(companies);
        }

        public async Task<bool> ContainsAsync(long id)
        {
            return await _unitOfWork.CompanyRepository.ContainsAsync(id);
        }

        public async Task RemoveAsync(long id)
        {
            var model = await _unitOfWork.CompanyRepository.GetSingleAsync(company => company.Id == id,
                i => i.Include(company => company.Employees));
            
            _unitOfWork.CompanyRepository.Remove(model);
            await _unitOfWork.SaveAsync();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}