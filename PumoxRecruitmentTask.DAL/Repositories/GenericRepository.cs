using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PumoxRecruitmentTask.DAL.Interfaces;
using PumoxRecruitmentTask.DAL.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Query;


namespace PumoxRecruitmentTask.DAL.Repositories
{
    public class GenericRepository<TContext, TModel> : IGenericRepository<TModel>
        where TContext : DbContext
        where TModel : class, IEntity
    {

        protected readonly TContext Context;
        public GenericRepository(TContext context)
        {
            Context = context ?? throw new ArgumentException(nameof(Context));
        }

        public async Task<TModel> GetSingleAsync(Expression<Func<TModel, bool>> expression, Func<IQueryable<TModel>, 
            IQueryable<TModel>> include = null)
        {
            var query = GetQueryable(expression, include);

            return await query.FirstOrDefaultAsync();
        }

        public virtual async Task<TModel> InsertAsync(TModel model)
        {
            await Context.Set<TModel>().AddAsync(model);

            return model;
        }

        public virtual void Update(TModel model)
        {
            Context.Set<TModel>().Update(model);
        }

        public virtual void Remove(TModel model)
        {
            Context.Set<TModel>().Remove(model);
        }

        public virtual async Task<int> SaveAsync()
        {
            return await Context.SaveChangesAsync();
        }
        
        private IQueryable<TModel> GetQueryable(Expression<Func<TModel, bool>> expression = null, 
            Func<IQueryable<TModel>, IQueryable<TModel>> include = null)
        {
            IQueryable<TModel> query = Context.Set<TModel>();
            if (include != null)
            {
                query = include(query);
            }

            if (expression != null)
            {
                query = query.Where(expression);
            }

            return query;
        }
    }
}