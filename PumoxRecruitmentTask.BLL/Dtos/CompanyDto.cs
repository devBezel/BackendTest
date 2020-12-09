using System.Collections.Generic;
using PumoxRecruitmentTask.DAL.DataAccess.Models;

namespace PumoxRecruitmentTask.BLL.Dtos
{
    public class CompanyDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int EstablishmentYear { get; set; }
        public ICollection<EmployeeModel> Employees { get; set; }
    }
}