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
            
            model.Id = result.Id;
            return _mapper.Map<CreatedCompanyResponseDto>(model);
        }

        public async Task<CompanyDto> UpdateAsync(long id, CompanyDto dto)
        {
            var model = await _unitOfWork.CompanyRepository.GetSingleAsync(company => company.Id == id, i 
                => i.Include(company => company.Employees));
            _mapper.Map(dto, model);
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