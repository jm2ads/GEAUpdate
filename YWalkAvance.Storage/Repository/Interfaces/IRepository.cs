using Commons.Commons.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Storage.Repository.Interfaces
{
    public interface IRepository<T> where T : SyncEntity
    {
        Task<T> GetById(Int64 id);

        Task<int> Save(T entity);

        Task SaveAll(List<T> entity);

        Task<int> Insert(T entity);

        Task<int> InsertAll(List<T> entity);

        Task Delete(T entity);
        
        Task Delete(string Id);

        Task Update(T entity);

        Task<List<T>> Where(Expression<Func<T, bool>> predicate);

        Task DropTableAsync();

        Task<List<T>> GetAllWithChildren();

        Task<List<T>> GetAll();

        Task<T> First();

        Task Delete();

        Task Format();

        Task<List<T>> FindWithChildren(Expression<Func<T, bool>> predicate);
        Task<List<T>> Query(string querySintax, params object[] args);
        Task UpdateAll(IList<T> entities);
        Task DeleteAllByIdsAsync(IEnumerable<object> primaryKeys);
        //Task<T> First(Expression<Func<T, bool>> predicate);
        //Task<T> FirstWithChildren();
        //Task<T> FirstWithChildren(Expression<Func<T, bool>> predicate);
        //Task SaveRange(IList<T> list);
        //Task UpdateWithChildren(T entity);
        //Task InsertAllWithChildren(IList<T> entities, bool recursive = false);
        //Task UpdateAll(IList<T> entities);
        //Task<T> FindFirstWithChildren(Expression<Func<T, bool>> predicate);
        //Task<T> SaveWithChildren(T entity);
        //Task SaveAllWithChildren(IList<T> entities);
        //Task<IList<T>> GetAllByIds(IList<int> idList);
    }
}
