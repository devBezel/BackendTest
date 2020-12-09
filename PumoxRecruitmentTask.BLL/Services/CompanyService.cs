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
        public CompanyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CreatedCompanyResponseDto> InsertAsync(CompanyDto dto)
        {
            var model = Mapper.Map<CompanyModel>(dto);
            var result = await _unitOfWork.CompanyRepository.InsertAsync(model);
            await _unitOfWork.SaveAsync();
            
            model.Id = result.Id;
            return Mapper.Map<CreatedCompanyResponseDto>(model);
        }

        public async Task<CompanyDto> UpdateAsync(long id, CompanyDto dto)
        {
            var model = await _unitOfWork.CompanyRepository.GetSingleAsync(company => company.Id == id, i 
                => i.Include(company => company.Employees));
            Mapper.Map(dto, model);
            await _unitOfWork.SaveAsync();


            return dto;
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