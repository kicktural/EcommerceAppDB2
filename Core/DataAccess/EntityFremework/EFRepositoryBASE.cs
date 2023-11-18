using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.DataAccess.EntityFremeworkCore
{
    public class EFRepositoryBASE<TEntity, TContext> : IReposiToryBase<TEntity>
        where TEntity : class
        where TContext :  DbContext, new()
        
    {
        
        public void Add(TEntity entity)
        {
           using TContext context  = new();
            var addentity = context.Entry(entity);
            addentity.State = EntityState.Added;
            context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            using TContext context = new();
            var deleteentity = context.Entry(entity);
            deleteentity.State = EntityState.Deleted;
            context.SaveChanges();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> expression)
        {
            using TContext context = new();
           return context.Set<TEntity>().FirstOrDefault(expression);
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>>? expression = null)
        {
            using TContext context = new();
            return expression == null
                ? context.Set<TEntity>().ToList()
                : context.Set<TEntity>().Where(expression).ToList();
        }

        public void Update(TEntity entity)
        {
            using TContext context = new();
            var updateeentity = context.Entry(entity);
            updateeentity.State = EntityState.Modified;
            context.SaveChanges();
        }

        TEntity IReposiToryBase<TEntity>.Get(Expression<Func<TEntity, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
