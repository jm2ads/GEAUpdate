using Commons.Commons.Entities;
using Storage.Commons;
using Storage.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Storage.Repository
{
    public class DefaultRepository<T> : IRepository<T> where T : SyncEntity, new()
    {
        protected readonly Database<T> database;

        public DefaultRepository(Database<T> database)
        {
            this.database = database;
        }

        public async Task<int> Save(T entity)
        {
            return await database.SaveAsync(entity);
        }

        public async Task SaveAll(List<T> entities)
        {
            await database.SaveAllAsync(entities);
        }

        public async Task<int> Insert(T entity)
        {
            return await database.InsertAsync(entity);
        }

        public async Task<int> InsertAll(List<T> entities)
        {
            int rowsAffected = 0;
            rowsAffected = await database.InsertAllAsync(entities);

            return rowsAffected;
        }

        public async Task Delete(T entity)
        {
            await database.DeleteAsync(entity);
        }
        public async Task Delete(string Id)
        {
            await database.DeleteAsync(Id);
        }

        public async Task Delete()
        {
            await database.Delete();
        }

        public async Task Format()
        {
            await database.Format();
        }

        public async Task<T> GetById(Int64 id)
        {
            return await database.GetWithChildren((Int32)id);
        }

        public async Task Update(T entity)
        {
            await database.Update(entity);
        }

        public async Task DropTableAsync()
        {
            await database.DropTable();
        }

        public async Task<List<T>> Where(Expression<Func<T, bool>> predicate)
        {
            return await database.Where(predicate);
        }

        public async Task<List<T>> GetAllWithChildren()
        {
            return await database.GetAllWithChildren();
        }

        public async Task<List<T>> GetAll()
        {
            return await database.GetAll();
        }

        public async Task<T> First()
        {
            return await database.First();
        }

        public Task<List<T>> FindWithChildren(Expression<Func<T, bool>> predicate)
        {
            return database.FindWithChildren(predicate);
        }

        public Task<List<T>> Query(string querySintax, params object[] args)
        {
            return database.Query(querySintax, args);
        }

        public Task UpdateAll(IList<T> entities)
        {
            return database.UpdateRangeAsync((List<T>)entities);
        }

        public Task DeleteAllByIdsAsync(IEnumerable<object> primaryKeys)
        {
            return database.DeleteAllByIdsAsync(primaryKeys);
        }
    }
}
