using System.Collections;
using System.Collections.Generic;
using PumoxRecruitmentTask.DAL.Interfaces;

namespace PumoxRecruitmentTask.DAL.DataAccess.Models
{
    public class CompanyModel : IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int EstablishmentYear { get; set; }
        public ICollection<EmployeeModel> Employees { get; set; }
    }
}