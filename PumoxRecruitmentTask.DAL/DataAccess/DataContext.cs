using Microsoft.EntityFrameworkCore;
using PumoxRecruitmentTask.DAL.DataAccess.Models;

namespace PumoxRecruitmentTask.DAL.DataAccess
{
    public class DataContext : DbContext
    {
        public DbSet<CompanyModel> Companies { get; set; }
        public DbSet<EmployeeModel> Employees { get; set; }
        
        public DataContext(DbContextOptions options) : base(options) { }
    }
}