using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Toro.API.Domain.Entities;
using Toro.API.Domain.Repositories;

namespace Toro.API.Test.Mocks
{
    public class UserRepositoryMock : IUserRepository
    {
        private IEnumerable<User> _users = new List<User>
        {
            new User(
                ObjectId.Parse("64c5f40fa35837fd299f734b"), 
                "geovana.nocera@gmail.com", 
                "6bc6921c1ac83529cb3bf23a7b1dc6ae", 
                new PersonUser(
                    ObjectId.Parse("64c5f40fa35837fd299f734b"), 
                    "Geovana Teste",
                    "98103732010",
                    "638262914713074086"))
        };

        public Task DeleteByIdAsync(ObjectId id)
        {
            _users = _users.Where(p => p.Id != id);
            return Task.CompletedTask;
        }

        public Task DeleteOneAsync(Expression<Func<User, bool>> filterExpression)
        {
            var lambdaDelegate = filterExpression.Compile();
            var item = _users.FirstOrDefault(lambdaDelegate);

            _users = _users.Where(p => p != item);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<User>> FilterByAsync(Expression<Func<User, bool>> filterExpression)
        {
            var lambdaDelegate = filterExpression.Compile();
            return Task.FromResult(_users.Where(lambdaDelegate));
        }

        public Task<User> FindByIdAsync(ObjectId id)
        {
            return Task.FromResult(_users.FirstOrDefault(x => x.Id == id));
        }

        public Task<User> FindOneAsync(Expression<Func<User, bool>> filterExpression)
        {
            var lambdaDelegate = filterExpression.Compile();

            return Task.FromResult(_users.FirstOrDefault(lambdaDelegate));
        }

        public Task InsertOneAsync(User document)
        {
            _users.Append(document);
            return Task.CompletedTask;
        }

        public Task ReplaceOneAsync(User document)
        {
            _users = _users.Select(x =>
            {
                if (x.Id == document.Id)
                    return document;
                return x;
            });
            return Task.CompletedTask;
        }
    }
}
