using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PumoxRecruitmentTask.DAL.Interfaces.Repositories
{
    public interface IGenericRepository<TModel>
    {
        Task<TModel> GetSingleAsync(Expression<Func<TModel, bool>> expression, Func<IQueryable<TModel>,
            IQueryable<TModel>> include = null);
        Task<TModel> InsertAsync(TModel model);
        void Update(TModel model);
        void Remove(TModel model);
        Task<int> SaveAsync();
    }
}