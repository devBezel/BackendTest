using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PumoxRecruitmentTask.DAL.Interfaces;

namespace PumoxRecruitmentTask.DAL.DataAccess.Models
{
    public class CompanyModel : IEntity
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int EstablishmentYear { get; set; }
        public IEnumerable<EmployeeModel> Employees { get; set; }
    }
}