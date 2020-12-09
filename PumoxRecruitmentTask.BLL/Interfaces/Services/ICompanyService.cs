using System;
using System.Threading.Tasks;
using PumoxRecruitmentTask.BLL.Dtos;
using PumoxRecruitmentTask.BLL.Dtos.Responses;

namespace PumoxRecruitmentTask.BLL.Interfaces.Services
{
    public interface ICompanyService : IDisposable
    {
        Task<CreatedCompanyResponseDto> InsertAsync(CompanyDto model);
        Task<CompanyDto> UpdateAsync(long id, CompanyDto dto);
        Task RemoveAsync(long id);
    }
}