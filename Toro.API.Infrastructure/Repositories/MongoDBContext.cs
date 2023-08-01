using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Toro.API.Infrastructure.AppSettings;
using Toro.API.Infrastructure.Repositories.Base;

namespace Toro.API.Infrastructure.Repositories;

public class MongoDBContext : MongoDBContextBase
{
    public MongoDBContext(IOptions<ApplicationOptions> optionsAccessor)
        : base(optionsAccessor.Value.MongoDBConnectionString, optionsAccessor.Value.MongoDBDatabaseName)
    {
    }
}
