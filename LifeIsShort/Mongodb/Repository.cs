using MongoDB.Driver;

namespace LifeIsShort.Mongodb
{
    public class Repository
    {
        protected IMongoClient _client;
        protected IMongoDatabase _database;

        public Repository(string connectionString)
        {
            var url = new MongoUrl(connectionString);
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(url.DatabaseName ?? "default");
        }

        public IMongoDatabase GetDatabase()
        {
            return _database;
        }

        public IMongoDatabase GetDatabase(string database)
        {
            return _client.GetDatabase(database);
        }

        public IMongoCollection<T> GetCollection<T>() where T : class, new()
        {
            return _database.GetCollection<T>(typeof(T).Name);
        }

        public IMongoCollection<T> GetCollection<T>(string name) where T : class, new()
        {
            return _database.GetCollection<T>(name);
        }

    }
}
