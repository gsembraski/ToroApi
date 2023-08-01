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
        protected class UpdateFieldDefinition
        {
            public string PropertyName { get; set; }

            public object PropertyValue { get; set; }

            public UpdateFieldDefinition(string propertyName, object propertyValue)
            {
                PropertyName = propertyName;
                PropertyValue = propertyValue;
            }
        }

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

        public virtual IEnumerable<TEntity> FilterBy(Expression<Func<TEntity, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).ToEnumerable();
        }

        public virtual Task<IEnumerable<TEntity>> FilterByAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            return Task.Run(() => _collection.Find(filterExpression).ToEnumerable());
        }

        public virtual IEnumerable<TProjected> FilterBy<TProjected>(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, TProjected>> projectionExpression)
        {
            return _collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
        }

        public virtual TEntity FindOne(Expression<Func<TEntity, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).FirstOrDefault();
        }

        public virtual Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            return Task.Run(() => _collection.Find(filterExpression).FirstOrDefaultAsync());
        }

        public virtual TEntity FindById(TID id)
        {
            new ObjectId(id.ToString());
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq((TEntity x) => x.Id, id);
            return _collection.Find(filter).SingleOrDefault();
        }

        public virtual Task<TEntity> FindByIdAsync(TID id)
        {
            return Task.Run(delegate
            {
                FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq((TEntity doc) => doc.Id, id);
                return _collection.Find(filter).SingleOrDefaultAsync();
            });
        }

        public virtual void InsertOne(TEntity document)
        {
            _collection.InsertOne(document);
        }

        public virtual Task InsertOneAsync(TEntity document)
        {
            return Task.Run(() => _collection.InsertOneAsync(document));
        }

        public void InsertMany(ICollection<TEntity> documents)
        {
            _collection.InsertMany(documents);
        }

        public virtual async Task InsertManyAsync(ICollection<TEntity> documents)
        {
            await _collection.InsertManyAsync(documents);
        }

        public void ReplaceOne(TEntity document)
        {
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq((TEntity doc) => doc.Id, document.Id);
            _collection.FindOneAndReplace(filter, document);
        }

        public virtual async Task ReplaceOneAsync(TEntity document)
        {
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq((TEntity doc) => doc.Id, document.Id);
            await _collection.FindOneAndReplaceAsync(filter, document);
        }

        public void UpdateOne(TEntity document)
        {
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq((TEntity doc) => doc.Id, document.Id);
            UpdateDefinition<TEntity> updateDefinition = getUpdateDefinition(document);
            FindOneAndUpdateOptions<TEntity> options = new FindOneAndUpdateOptions<TEntity>
            {
                ReturnDocument = ReturnDocument.After
            };
            _collection.FindOneAndUpdate(filter, updateDefinition, options);
        }

        public virtual async Task UpdateOneAsync(TEntity document)
        {
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq((TEntity doc) => doc.Id, document.Id);
            UpdateDefinition<TEntity> updateDefinition = getUpdateDefinition(document);
            FindOneAndUpdateOptions<TEntity> options = new FindOneAndUpdateOptions<TEntity>
            {
                ReturnDocument = ReturnDocument.After
            };
            await _collection.FindOneAndUpdateAsync(filter, updateDefinition, options);
        }

        private UpdateDefinition<T> getUpdateDefinition<T>(T entity)
        {
            IEnumerable<UpdateFieldDefinition> source = from x in entity.GetType().GetProperties()
                                                        where !new string[2] { "Id", "_id" }.Contains(x.Name) && x.CanWrite
                                                        select new UpdateFieldDefinition(x.Name, x.GetValue(entity));
            UpdateDefinitionBuilder<T> builder = new UpdateDefinitionBuilder<T>();
            IEnumerable<UpdateDefinition<T>> updates = source.Select((UpdateFieldDefinition TField) => builder.Set(TField.PropertyName, TField.PropertyValue));
            return builder.Combine(updates);
        }

        public void DeleteOne(Expression<Func<TEntity, bool>> filterExpression)
        {
            _collection.FindOneAndDelete(filterExpression);
        }

        public Task DeleteOneAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            return Task.Run(() => _collection.FindOneAndDeleteAsync(filterExpression));
        }

        public void DeleteById(TID id)
        {
            _collection.FindOneAndDelete((TEntity x) => x.Id.Equals(id));
        }

        public Task DeleteByIdAsync(TID id)
        {
            return Task.Run(() => _collection.FindOneAndDeleteAsync((TEntity x) => x.Id.Equals(id)));
        }

        public void DeleteMany(Expression<Func<TEntity, bool>> filterExpression)
        {
            _collection.DeleteMany(filterExpression);
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
