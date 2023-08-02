using MongoDB.Bson;
using System.Linq.Expressions;
using System.Text;
using Toro.API.Domain.Entities;
using Toro.API.Domain.Models;
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
                "638262914713074086",
                new PersonWallet((decimal)3000.0, new List<PersonAsset>
                {
                    new PersonAsset("TES001", "Test 001", 150),
                    new PersonAsset("TES002", "Test 002", 150)
                }))
        };

        private IEnumerable<Asset> _assets = new List<Asset>
        {
            new Asset(ObjectId.Parse("64c9c54ca6d209b85b84289e"), "Test 001", "TES001", (decimal)15.5),
            new Asset(ObjectId.Parse("64c9c586a6d209b85b84289f"), "Test 002", "TES002", (decimal)13.5)
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

        public Task<PersonWalletModel> QueryPersonWalletById(ObjectId id)
        {
            var person = _people.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(new PersonWalletModel
            {
                Name = person.Name,
                CPF = person.CPF,
                Balance = person.Wallet.Balance,
                Assets = person.Wallet.Assets.Select(x => new AssetModel
                {
                    Name = x.Name,
                    Amount = x.Amount,
                    Code = x.Code,
                    Value = _assets.First(y => x.Code == y.Code).Value
                })
            });
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
