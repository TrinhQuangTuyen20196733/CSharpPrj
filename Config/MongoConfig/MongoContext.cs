using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHDStarBooking.Config.MongoConfig
{
    public class MongoContext : IMongoBaseContext
    {
        public IMongoClient Client { get; }

        public IMongoDatabase Database { get; }

        //IMongoClient IMongoBaseContext.Client => throw new NotImplementedException();

        //IMongoDatabase IMongoBaseContext.Database => throw new NotImplementedException();

        public MongoContext(IOptions<MongoSettings> mongoSettings)
        {
            Client = new MongoClient(mongoSettings.Value.ConnectionString);
            Database = Client.GetDatabase(mongoSettings.Value.DatabaseName);
        }
        public async void InsertDataX<T>(string collection_name, T insertData)
        {
            var collection = Database.GetCollection<T>(collection_name);
            await collection.InsertOneAsync(insertData);
            //return Task.CompletedTask;
        }
    }
}
