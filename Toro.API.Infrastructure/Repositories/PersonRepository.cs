using MongoDB.Bson;
using MongoDB.Driver;
using Toro.API.Domain.Entities;
using Toro.API.Domain.Models;
using Toro.API.Domain.Repositories;
using Toro.API.Infrastructure.Repositories.Base;

namespace Toro.API.Infrastructure.Repositories
{
    public class PersonRepository : BRepositoryBase<Person, ObjectId>, IPersonRepository
    {
        private readonly MongoDBContext _context;
        public PersonRepository(MongoDBContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<PersonWalletModel> QueryPersonWalletById(ObjectId id)
        {
            var aggregation = _collection.Aggregate()
                .Match(Builders<Person>.Filter
                        .Eq(x => x.Id, id))
                .AppendStage<BsonDocument>(new BsonDocument("$lookup",
                    new BsonDocument{
                        { "from", "Asset" },
                        { "localField", "Wallet.Assets.Code" },
                        { "foreignField", "Code" },
                        { "as", "Assets" }
                    }))
                .AppendStage<BsonDocument>(new BsonDocument("$addFields",
                    new BsonDocument("Assets",
                    new BsonDocument("$map",
                    new BsonDocument{
                        { "input", "$Wallet.Assets" },
                        { "as", "item" },
                        { "in", new BsonDocument("$mergeObjects", new BsonArray {
                                "$$item", new BsonDocument("$arrayElemAt", new BsonArray{
                                    new BsonDocument("$filter", new BsonDocument{
                                            { "input", "$Assets" },
                                            { "cond", new BsonDocument("$eq",new BsonArray
                                                {
                                                    "$$this.Code",
                                                    "$$item.Code"
                                                }) 
                                            }
                                        }), 0
                                    })
                                }) 
                        }
                    }))))
                .Project<PersonWalletModel>(new BsonDocument()
                     {
                        { "Name", 1 },
                        { "CPF", 1 },
                        { "Balance", "$Wallet.Balance" },
                        { "Assets", 1 }
                     });

            return await aggregation.FirstOrDefaultAsync();

        }
    }
}