using PumoxRecruitmentTask.DAL.Interfaces.Company;

namespace PumoxRecruitmentTask.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        ICompanyRepository CompanyRepository { get; }
    }
}