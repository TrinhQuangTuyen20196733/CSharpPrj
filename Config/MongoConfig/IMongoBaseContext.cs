using MongoDB.Driver;

namespace BHDStarBooking.Config.MongoConfig
{
    public interface IMongoBaseContext
    {
        IMongoClient Client { get; }

        IMongoDatabase Database { get; }
        void InsertDataX<T>(string collection_name, T insertData);
    }
}
