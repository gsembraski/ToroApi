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
    public class PersonRepositoryMock : IPersonRepository
    {
        private IEnumerable<Person> _people = new List<Person>
        {
            new Person(
                ObjectId.Parse("64c5f40fa35837fd299f734b"),
                "Geovana Teste",
                "98103732010",
                DateTime.UtcNow.AddYears(-30),
                "638262914713074086")
        };

        public Task DeleteByIdAsync(ObjectId id)
        {
            _people = _people.Where(p => p.Id != id);
            return Task.CompletedTask;
        }

        public Task DeleteOneAsync(Expression<Func<Person, bool>> filterExpression)
        {
            var lambdaDelegate = filterExpression.Compile();
            var item = _people.FirstOrDefault(lambdaDelegate);

            _people = _people.Where(p => p != item);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Person>> FilterByAsync(Expression<Func<Person, bool>> filterExpression)
        {
            var lambdaDelegate = filterExpression.Compile();
            return Task.FromResult(_people.Where(lambdaDelegate));
        }

        public Task<Person> FindByIdAsync(ObjectId id)
        {
            return Task.FromResult(_people.FirstOrDefault(x => x.Id == id));
        }

        public Task<Person> FindOneAsync(Expression<Func<Person, bool>> filterExpression)
        {
            var lambdaDelegate = filterExpression.Compile();
            return Task.FromResult(_people.FirstOrDefault(lambdaDelegate));
        }

        public Task InsertOneAsync(Person document)
        {
            _people.Append(document);
            return Task.CompletedTask;
        }

        public Task ReplaceOneAsync(Person document)
        {
            _people = _people.Select(x =>
            {
                if (x.Id == document.Id)
                    x.UpdateInfo(document.Name, document.AccountNumber);
                return x;
            });
            return Task.CompletedTask;
        }
    }
}
