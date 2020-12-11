using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PumoxRecruitmentTask.DAL.DataAccess.Models;

namespace PumoxRecruitmentTask.DAL.Interfaces.Repositories
{
    public interface IGenericRepository<TModel>
    {
        Task<TModel> GetSingleAsync(Expression<Func<TModel, bool>> expression, Func<IQueryable<TModel>,
            IQueryable<TModel>> include = null);

        Task<IEnumerable<TModel>> GetAllAsync(Expression<Func<TModel, bool>> expression, Func<IQueryable<TModel>,
            IQueryable<TModel>> include = null);
        Task<TModel> InsertAsync(TModel model);
        void Update(TModel model);
        Task<bool> ContainsAsync(long id);
        void Remove(TModel model);
        Task<int> SaveAsync();
    }
}