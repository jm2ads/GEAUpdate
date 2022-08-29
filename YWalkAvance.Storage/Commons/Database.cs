using Commons.Commons.Entities;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Storage.Commons
{
    public class Database<T> where T : SyncEntity, new()
    {
        private readonly SQLiteAsyncConnection connection;

        public Database(SQLiteAsyncConnection connection)
        {
            this.connection = connection;
            CreateTable();
        }

        private async void CreateTable()
        {
            await connection.CreateTableAsync<T>();
        }

        public async Task DropTable()
        {
            await connection.DropTableAsync<T>();
        }

        public async Task<List<T>> GetAllWithChildren()
        {
            return await connection.GetAllWithChildrenAsync<T>(recursive: true);
        }

        public async Task<List<T>> GetAll()
        {
            return await connection.Table<T>().ToListAsync();
        }

        public async Task<int> SaveAsync(T entity)
        {
            //TODO: terminar la logica de updatear o insertar.
            var rowsAffected = await connection.UpdateAsync(entity);
            if (rowsAffected == 0) {
                rowsAffected = await this.connection.InsertAsync(entity);
            }

            return rowsAffected;
            //return await this.connection.InsertOrReplaceAsync(entity);
        }

        public async Task<int> InsertAsync(T entity)
        {
            var rowsAffected = await this.connection.InsertAsync(entity);

            return rowsAffected;
        }

        public async Task<int> InsertAllAsync(List<T> entities)
        {
            int rowsAffected = 0;

            foreach (var entity in entities)
                rowsAffected = await this.connection.InsertAsync(entity);

            return rowsAffected;
        }

        public async Task SaveAllAsync(List<T> entities)
        {
            foreach (var entity in entities) {
                var rowsAffected = await connection.UpdateAsync(entity);
                if (rowsAffected == 0)
                {
                    rowsAffected = await this.connection.InsertAsync(entity);
                }
            }
                //await this.connection.InsertOrReplaceAsync(entity);
        }

        public async Task<List<T>> Where(Expression<Func<T, bool>> predicate)
        {
            return await connection.Table<T>().Where(predicate).ToListAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            await connection.DeleteAsync(entity, recursive: true);
        }
        public async Task DeleteAsync(string Id)
        {
            var listIds = new List<object>();
            listIds.Add(Id);
            await connection.DeleteAllIdsAsync<T>(listIds);
        }

        public async Task DeleteAllByIdsAsync(IEnumerable<object> primaryKeys)
        {
            await connection.DeleteAllIdsAsync<T>(primaryKeys);
        }

        public async Task Update(T entity)
        {
            try
            {
                await connection.UpdateAsync(entity);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task Format()
        {
            await this.connection.DropTableAsync<T>();
            await this.connection.CreateTableAsync<T>();
        }

        public async Task Delete()
        {
            await this.connection.DropTableAsync<T>();
        }

        public async Task UpdateRangeAsync(List<T> entities)
        {
            try
            {
                var rowsAffected = await connection.UpdateAllAsync(entities);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> Find(Expression<Func<T, bool>> predicate)
        {
            return await this.connection.Table<T>().Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<T> First()
        {
           return await connection.Table<T>().FirstOrDefaultAsync();
        }

        public async Task UpdateWithChildren(T entity)
        {
            await connection.UpdateWithChildrenAsync(entity);
        }

        public async Task<List<T>> FindWithChildren(Expression<Func<T, bool>> predicate)
        {
            return await connection.GetAllWithChildrenAsync<T>(predicate, recursive: true);
        }

        public async Task SaveWithChildren(T entity)
        {
            await connection.InsertOrReplaceWithChildrenAsync(entity, recursive: true);
        }

        public async Task SaveAllWithChildren(List<T> entities)
        {
            await connection.InsertOrReplaceAllWithChildrenAsync(entities, recursive: true);
        }

        /// <summary>
        /// Soporte de string queries
        /// </summary>
        /// <param name="querySintax"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task<List<T>> Query(string querySintax, params object[] args)
        {
            List<T> borrar = await connection.QueryAsync<T>(querySintax, args);
            return await connection.QueryAsync<T>(querySintax, args);
        }

        public async Task<T> GetWithChildren(int id)
        {
            return await connection.GetWithChildrenAsync<T>(id, recursive: true);
        }
    }

}
