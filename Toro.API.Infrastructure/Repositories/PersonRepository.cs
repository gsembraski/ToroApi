using MongoDB.Bson;
using Toro.API.Domain.Entities;
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
    }
}