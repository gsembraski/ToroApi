using MongoDB.Bson;
using Toro.API.Domain.Entities;
using Toro.API.Domain.Repositories;
using Toro.API.Infrastructure.Repositories.Base;

namespace Toro.API.Infrastructure.Repositories
{
    public class UserRepository : BRepositoryBase<User, ObjectId>, IUserRepository
    {
        private readonly MongoDBContext _context;
        public UserRepository(MongoDBContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }
    }
}