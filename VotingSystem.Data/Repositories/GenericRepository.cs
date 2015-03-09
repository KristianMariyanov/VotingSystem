namespace VotingSystem.Data.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Linq.Expressions;
    
    class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected IVotingSystemDbContext context;

        public GenericRepository(IVotingSystemDbContext votingSystemDbContext)
        {
            this.context = votingSystemDbContext;
        }
        public IQueryable<T> All()
        {
            return this.context.Set<T>();
        }

        public IQueryable<T> Search(Expression<Func<T, bool>> conditions)
        {
            return this.All().Where(conditions);
        }

        public T GetById(object id)
        {
            return this.context.Set<T>().Find(id);
        }

        public void Add(T entity)
        {
            DbEntityEntry entry = this.context.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.context.Set<T>().Add(entity);
            }
        }

        public void Update(T entity)
        {
            DbEntityEntry entry = this.context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.context.Set<T>().Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        public T Delete(T entity)
        {
            DbEntityEntry entry = this.context.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this.context.Set<T>().Remove(entity);
            }

            return entity;
        }


        public void SaveChanges()
        {
            this.context.SaveChanges();
        }
    }
}
