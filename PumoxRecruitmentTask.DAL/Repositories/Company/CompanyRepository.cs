using PumoxRecruitmentTask.DAL.DataAccess;
using PumoxRecruitmentTask.DAL.DataAccess.Models;
using PumoxRecruitmentTask.DAL.Interfaces.Repositories.Company;

namespace PumoxRecruitmentTask.DAL.Repositories.Company
{
    public class CompanyRepository : GenericRepository<DataContext, CompanyModel>, ICompanyRepository
    {
        public CompanyRepository(DataContext context) : base(context)
        {
        }
    }
}