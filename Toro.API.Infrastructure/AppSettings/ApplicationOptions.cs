using System;

namespace Toro.API.Infrastructure.AppSettings
{
    public class ApplicationOptions
    {
        public string MongoDBConnectionString { get; set; }
        public string MongoDBDatabaseName { get; set; }
    }
}

