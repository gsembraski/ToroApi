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

    Task<IEnumerable<TEntity>> FilterByAsync(Expression<Func<TEntity, bool>> filterExpression);

    Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filterExpression);

    Task<TEntity> FindByIdAsync(TID id);

    Task InsertOneAsync(TEntity document);

    Task ReplaceOneAsync(TEntity document);

    Task DeleteOneAsync(Expression<Func<TEntity, bool>> filterExpression);

    Task DeleteByIdAsync(TID id);
}