namespace VotingSystem.Data.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Linq.Expressions;
    
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly IVotingSystemDbContext Context;

        public GenericRepository(IVotingSystemDbContext votingSystemDbContext)
        {
            this.Context = votingSystemDbContext;
        }

        public IQueryable<T> All()
        {
            return this.Context.Set<T>();
        }

        public IQueryable<T> Search(Expression<Func<T, bool>> conditions)
        {
            return this.All().Where(conditions);
        }

        public T GetById(object id)
        {
            return this.Context.Set<T>().Find(id);
        }

        public void Add(T entity)
        {
            DbEntityEntry entry = this.Context.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.Context.Set<T>().Add(entity);
            }
        }

        public void Update(T entity)
        {
            DbEntityEntry entry = this.Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.Context.Set<T>().Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        public T Delete(T entity)
        {
            DbEntityEntry entry = this.Context.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this.Context.Set<T>().Remove(entity);
            }

            return entity;
        }

        public void DeleteById(object id)
        {
            var entity = this.GetById(id);

            if (entity != null)
            {
                this.Delete(entity);
            }
        }

        public void SaveChanges()
        {
            this.Context.SaveChanges();
        }
    }
}
