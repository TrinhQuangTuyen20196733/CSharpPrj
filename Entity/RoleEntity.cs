
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Driver;
using MongoDbGenericRepository.Attributes;

namespace BHDStarBooking.Entity
{
    
    public class RoleEntity :  BaseEntity
    {
        public string? name {  get; set; }
        public string? code { get; set; }

    }
}
