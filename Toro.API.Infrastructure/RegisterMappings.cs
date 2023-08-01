using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using Toro.API.Domain.Entities.Base;

namespace Toro.API.Infrastructure
{

    public static class RegisterMappings
    {
        public static void Register()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Entity<ObjectId>)))
            {
                BsonClassMap.RegisterClassMap<Entity<ObjectId>>(map =>
                {
                    map.AutoMap();
                    map.SetIgnoreExtraElements(true);
                });
            }
            var conventionPack = new ConventionPack { new IgnoreExtraElementsConvention(true) };
            ConventionRegistry.Register("IgnoreExtraElements", conventionPack, type => true);

        }
        public static void RegisterClass<T>() where T : class
        {
            BsonClassMap.RegisterClassMap<T>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
            });
        }
    }
}
