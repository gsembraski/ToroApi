using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Toro.API.Domain.Entities.Base;
using Toro.API.Domain.Repositories.Base;

namespace Toro.API.Infrastructure.Repositories.Base
{
    public abstract class BRepositoryBase<TEntity, TID> : IBRepository<TEntity, TID>
        where TEntity : Entity<TID>
        where TID : struct
    {
        protected readonly IMongoCollection<TEntity> _collection;

        public BRepositoryBase(MongoDBContext dbContext)
        {
            _collection = dbContext.Database.GetCollection<TEntity>(GetCollectionName(typeof(TEntity)));
        }

        public virtual string GetCollectionName(Type documentType)
        {
            return documentType.Name;
        }

        public virtual IQueryable<TEntity> AsQueryable()
        {
            return _collection.AsQueryable();
        }

        public virtual Task<IEnumerable<TEntity>> FilterByAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            return Task.Run(() => _collection.Find(filterExpression).ToEnumerable());
        }

        public virtual Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            return Task.Run(() => _collection.Find(filterExpression).FirstOrDefaultAsync());
        }

        public virtual Task<TEntity> FindByIdAsync(TID id)
        {
            return Task.Run(delegate
            {
                FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq((TEntity doc) => doc.Id, id);
                return _collection.Find(filter).SingleOrDefaultAsync();
            });
        }

        public virtual Task InsertOneAsync(TEntity document)
        {
            return Task.Run(() => _collection.InsertOneAsync(document));
        }

        public virtual async Task InsertManyAsync(ICollection<TEntity> documents)
        {
            await _collection.InsertManyAsync(documents);
        }

        public virtual async Task ReplaceOneAsync(TEntity document)
        {
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq((TEntity doc) => doc.Id, document.Id);
            await _collection.FindOneAndReplaceAsync(filter, document);
        }

        public Task DeleteOneAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            return Task.Run(() => _collection.FindOneAndDeleteAsync(filterExpression));
        }

        public Task DeleteByIdAsync(TID id)
        {
            return Task.Run(() => _collection.FindOneAndDeleteAsync((TEntity x) => x.Id.Equals(id)));
        }

        public Task DeleteManyAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            return Task.Run(() => _collection.DeleteManyAsync(filterExpression));
        }

        public IEnumerable<TProjected> FilterByAsync<TProjected>(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, TProjected>> projectionExpression)
        {
            return _collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
        }
    }
}
