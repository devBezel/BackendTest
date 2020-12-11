using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PumoxRecruitmentTask.BLL.Dtos;
using PumoxRecruitmentTask.BLL.Dtos.Responses;

namespace PumoxRecruitmentTask.BLL.Interfaces.Services
{
    public interface ICompanyService : IDisposable
    {
        Task<CreatedCompanyResponseDto> InsertAsync(CompanyDto model);
        Task<CompanyDto> UpdateAsync(long id, CompanyDto dto);
        Task<IEnumerable<CompanyDto>> SearchAsync(SearchCompanyDto dto);
        Task RemoveAsync(long id);
        Task<bool> ContainsAsync(long id);
    }
}