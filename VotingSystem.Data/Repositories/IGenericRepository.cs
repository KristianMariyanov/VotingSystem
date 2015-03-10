namespace VotingSystem.Data.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.InteropServices.ComTypes;

    public interface IGenericRepository<T>
    {
        IQueryable<T> All();

        IQueryable<T> Search(Expression<Func<T, bool>> conditions);

        T GetById(object id);

        void Add(T entity);

        T Delete(T entity);

        void Update(T entity);

        void SaveChanges();
    }
}
