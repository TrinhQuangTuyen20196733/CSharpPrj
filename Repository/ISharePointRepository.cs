using BHDStarBooking.Entity;
using Microsoft.SharePoint.Client;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace BHDStarBooking.Repository
{
    public interface ISharePointRepository<TEntity> where TEntity : BaseEntity
    {
        List list { get; }
        TEntity insertListItem(TEntity entity);
        List<TEntity> getAllListItem();
        TEntity getListItemById(string id);
        TEntity updateListItem(TEntity entity);
        void deleteListItemById(string id);
        int insertItemAndReturnID(TEntity entity);
        int getListItemIDByid(string id);
        int updateItemAndReturnID(TEntity entity);
    }
}
