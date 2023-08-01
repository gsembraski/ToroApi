using MongoDB.Driver;

namespace Toro.API.Infrastructure.Repositories.Base
{
    public abstract class MongoDBContextBase : IDisposable
    {
        public Guid SessionId { get; private set; }

        public IMongoDatabase Database { get; }

        protected MongoDBContextBase(string connectionString, string dbName)
        {
            SessionId = Guid.NewGuid();
            Database = new MongoClient(connectionString).GetDatabase(dbName);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}