using System;
using System.Threading.Tasks;
using PumoxRecruitmentTask.DAL.Interfaces.Repositories.Company;

namespace PumoxRecruitmentTask.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ICompanyRepository CompanyRepository { get; }

        Task SaveAsync();
        void Save();
    }
}