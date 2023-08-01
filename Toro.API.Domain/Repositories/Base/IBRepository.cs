using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Toro.API.Domain.Entities.Base;

namespace Toro.API.Domain.Repositories.Base;

public interface IBRepository<TEntity, TID> 
    where TID : struct 
    where TEntity : Entity<TID>
{
    IQueryable<TEntity> AsQueryable();

    IEnumerable<TEntity> FilterBy(Expression<Func<TEntity, bool>> filterExpression);

    Task<IEnumerable<TEntity>> FilterByAsync(Expression<Func<TEntity, bool>> filterExpression);

    IEnumerable<TProjected> FilterBy<TProjected>(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, TProjected>> projectionExpression);

    IEnumerable<TProjected> FilterByAsync<TProjected>(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, TProjected>> projectionExpression);

    TEntity FindOne(Expression<Func<TEntity, bool>> filterExpression);

    Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filterExpression);

    TEntity FindById(TID id);

    Task<TEntity> FindByIdAsync(TID id);

    void InsertOne(TEntity document);

    Task InsertOneAsync(TEntity document);

    void InsertMany(ICollection<TEntity> documents);

    Task InsertManyAsync(ICollection<TEntity> documents);

    void ReplaceOne(TEntity document);

    Task ReplaceOneAsync(TEntity document);

    void UpdateOne(TEntity document);

    Task UpdateOneAsync(TEntity document);

    void DeleteOne(Expression<Func<TEntity, bool>> filterExpression);

    Task DeleteOneAsync(Expression<Func<TEntity, bool>> filterExpression);

    void DeleteById(TID id);

    Task DeleteByIdAsync(TID id);

    void DeleteMany(Expression<Func<TEntity, bool>> filterExpression);

    Task DeleteManyAsync(Expression<Func<TEntity, bool>> filterExpression);
}