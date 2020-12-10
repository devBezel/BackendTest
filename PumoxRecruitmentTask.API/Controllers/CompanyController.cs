using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PumoxRecruitmentTask.BLL;
using PumoxRecruitmentTask.BLL.Dtos;
using PumoxRecruitmentTask.BLL.Interfaces.Services;
using PumoxRecruitmentTask.DAL.Enums;

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
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var result = await _companyService.InsertAsync(dto);
            return Created("", result.Id);
        }
        
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] long id, [FromBody] CompanyDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!await _companyService.ContainsAsync(id))
            {
                return NotFound();
            }

            var result = await _companyService.UpdateAsync(id, dto);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] long id)
        {
            if (!await _companyService.ContainsAsync(id))
            {
                return NotFound();
            }
            
            await _companyService.RemoveAsync(id);
            return NoContent();
        }
    }
}