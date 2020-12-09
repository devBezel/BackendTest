using System.Threading.Tasks;
using PumoxRecruitmentTask.DAL.DataAccess;
using PumoxRecruitmentTask.DAL.Interfaces.Repositories.Company;
using PumoxRecruitmentTask.DAL.Repositories.Company;

namespace PumoxRecruitmentTask.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext Context { get; }
        
        private ICompanyRepository _companyRepository;

        public ICompanyRepository CompanyRepository => _companyRepository ??= new CompanyRepository(Context);

        public UnitOfWork(DataContext context)
        {
            Context = context;
        }

        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }
        
        public void Dispose()
        {
            Context.Dispose();
        }
    }
}