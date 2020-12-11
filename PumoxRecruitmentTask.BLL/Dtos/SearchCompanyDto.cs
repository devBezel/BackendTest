using System;
using System.Collections.Generic;

namespace PumoxRecruitmentTask.BLL.Dtos
{
    public class SearchCompanyDto
    {
        public string Keyword { get; set; }
        public DateTime? EmployeeDateOfBirthFrom { get; set; }
        public DateTime? EmployeeDateOfBirthTo { get; set; }
        public ICollection<string> EmployeeJobTitles { get; set; }
    }
}