using System.Collections.Generic;

namespace PumoxRecruitmentTask.BLL.Dtos.Responses
{
    public class SearchResponseDto
    {
        public IEnumerable<CompanyDto> Results { get; set; }
    }
}