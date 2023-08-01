using MongoDB.Bson.Serialization.Attributes;

namespace Toro.API.Domain.Entities.Base
{
    public abstract class Entity<TId>
    {
        private int? _requestedHashCode;

        private TId _Id;

        [BsonId]
        public virtual TId Id
        {
            get
            {
                return _Id;
            }
            protected set
            {
                _Id = value;
            }
        }

        protected Entity()
        {
        }
    }
}
