using System;
using MongoDB.Driver;

namespace buckstore.products.service.infrastructure.Data.Context
{
    public class MongoDbContext
    {
        public IMongoDatabase MongoDatabaseWriteCluster { get; set; }
        public string ConnectionStringPrimaryWriteCluster = Environment.GetEnvironmentVariable("MongoConfiguration__ConnectionString");
        private readonly string _dbName = Environment.GetEnvironmentVariable("MongoConfiguration__DatabaseName");

        public MongoDbContext()
        {
            MongoDatabaseWriteCluster = new MongoClient(
                    new MongoUrl(ConnectionStringPrimaryWriteCluster))
                .GetDatabase(_dbName);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return MongoDatabaseWriteCluster.GetCollection<T>(collectionName);
        }
    }
}
