using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BHDStarBooking.Entity
{
    [CollectionName("users")]
    public class UserEntity : BaseEntity
    { 
        public AccountEntity? account { get; set; }
        [Required]
        public string? lastName { get; set; }
        [Required]
        public string? firstName { get; set; }
        [Required,DataType(DataType.PhoneNumber)]
        public string? phoneNumber { get; set; }
        [Required]
        public string? address { get; set; }
        [Required,DataType(DataType.DateTime)]
        public DateTime? birthDay { get; set; }
    }
}
