using BHDStarBooking.Entity;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BHDStarBooking.Repository
{
    public interface IMongoRepository<TEntity> where TEntity : BaseEntity
    {
        IMongoCollection<TEntity> Collection { get; }

        //Task<FilterResponse<TEntity>> FilterBy(
        //Expression<Func<TEntity, bool>> filterExpression, Pager param);
        TEntity GetById(string id);
        Task<TEntity> GetByIdAsync(string id);
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetByPageAsync(int pageNumber);
        Task<List<TEntity>> GetByCondition(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetOneByCondition(Expression<Func<TEntity, bool>> predicate);
        TEntity Insert(TEntity entity);
        Task<TEntity> InsertAsync(TEntity entity);
        void InsertMany(IEnumerable<TEntity> entities);
        Task<IEnumerable<TEntity>> InsertManyAsync(IEnumerable<TEntity> entities);
        TEntity Update(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        void UpdateMany(IEnumerable<TEntity> entities);
        Task<IEnumerable<TEntity>> UpdateManyAsync(IEnumerable<TEntity> entities);
        //Task Delete(TEntity entity);
        Task DeleteAsync(string id);
        Task<long> GetTotalItemAsync();
        //void DeleteMany(IEnumerable<TEntity> entities);
        //Task<IEnumerable<TEntity>> DeleteManyAsync(IEnumerable<TEntity> entities);

    }
}
