using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PumoxRecruitmentTask.DAL.Enums;
using PumoxRecruitmentTask.DAL.Interfaces;

namespace PumoxRecruitmentTask.DAL.DataAccess.Models
{
    public class EmployeeModel : IEntity
    {
        [Key]
        public long Id { get; set; }
        [ForeignKey("CompanyModel")]
        public long CompanyId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string  LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [EnumDataType(typeof(JobTitle))]
        public JobTitle JobTitle { get; set; }
        
        public virtual CompanyModel CompanyModel { get; set; }
    }
}