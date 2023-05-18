using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BHDStarBooking.Entity
{

    public class AccountEntity : BaseEntity
    {

        [Required,EmailAddress]
        public string? email { get; set; }
        [Required,DataType(DataType.Password)]
        public string? password { get; set; }   
        
        public List<RoleEntity>? roles { get; set; }

    }
}
