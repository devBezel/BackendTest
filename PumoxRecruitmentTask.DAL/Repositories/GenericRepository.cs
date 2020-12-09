using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PumoxRecruitmentTask.DAL.Interfaces;
using PumoxRecruitmentTask.DAL.Interfaces.Repositories;


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
    }
}