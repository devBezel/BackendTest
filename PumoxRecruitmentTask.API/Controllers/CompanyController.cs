using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PumoxRecruitmentTask.BLL.Dtos;
using PumoxRecruitmentTask.BLL.Dtos.Responses;
using PumoxRecruitmentTask.BLL.Interfaces.Services;
using ZNetCS.AspNetCore.Authentication.Basic;

namespace PumoxRecruitmentTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = BasicAuthenticationDefaults.AuthenticationScheme)]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost("search")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchAsync([FromBody] SearchCompanyDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(dto);
            }
            
            return Ok(new SearchResponseDto { Results = await _companyService.SearchAsync(dto) });
        }
        
        [HttpPost("create")]
        public async Task<IActionResult> InsertAsync([FromBody] CompanyDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(dto);
            }
            
            var result = await _companyService.InsertAsync(dto);
            return Created("", result.Id);
        }
        
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] long id, [FromBody] CompanyDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(dto);
            }

            if (!await _companyService.ContainsAsync(id))
            {
                return NotFound(id);
            }

            var result = await _companyService.UpdateAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] long id)
        {
            if (!await _companyService.ContainsAsync(id))
            {
                return NotFound(id);
            }
            
            await _companyService.RemoveAsync(id);
            return NoContent();
        }
    }
}