using BHDStarBooking.Entity;
using MongoDB.Driver;

namespace BHDStarBooking.Config.MongoConfig
{
    public  class MongoIndex
    {
        private readonly  IConfiguration configuration;

        public MongoIndex(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public  void CreateUniqueIndex()
        {
            string connectionString = configuration.GetSection("myDB")["ConnectionString"];
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(configuration.GetSection("myDB")["DatabaseName"]);
            var collection = database.GetCollection<AccountEntity>("AccountEntity");
            var emailField = Builders<AccountEntity>.IndexKeys.Ascending(u => u.email); // Tạo chỉ mục cho trường Email
            var indexOptions = new CreateIndexOptions { Unique = true }; // Đặt option unique cho chỉ mục
            var indexModel = new CreateIndexModel<AccountEntity>(emailField, indexOptions); // Tạo index model
            collection.Indexes.CreateOne(indexModel); // Tạo chỉ mục trong MongoDB
            Console.WriteLine("Unique index for email created successfully.");

        }
    }
}
