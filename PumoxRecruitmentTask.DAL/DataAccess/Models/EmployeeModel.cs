using System;
using PumoxRecruitmentTask.DAL.Enums;
using PumoxRecruitmentTask.DAL.Interfaces;

namespace PumoxRecruitmentTask.DAL.DataAccess.Models
{
    public class EmployeeModel : IEntity
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public string FirstName { get; set; }
        public string  LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        
        public JobType JobTitle { get; set; }
    }
}