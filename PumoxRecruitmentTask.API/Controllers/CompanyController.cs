using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PumoxRecruitmentTask.BLL;
using PumoxRecruitmentTask.BLL.Dtos;
using PumoxRecruitmentTask.BLL.Interfaces.Services;

namespace PumoxRecruitmentTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost("create")]
        [AllowAnonymous]
        public async Task<IActionResult> InsertAsync([FromBody] CompanyDto dto)
        {
            DomainException.ThrowIf(ModelState.IsValid == false, DomainExceptionCode.IncorrectData, 
                "You have provided incorrect data or not filled in the valid text fields.");

            var result = await _companyService.InsertAsync(dto);
            return Created("", result);
        }
    }
}