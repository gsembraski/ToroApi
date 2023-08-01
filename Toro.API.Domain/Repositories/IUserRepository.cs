using MongoDB.Bson;
using System;
using Toro.API.Domain.Entities;
using Toro.API.Domain.Repositories.Base;

namespace Toro.API.Domain.Repositories;

public interface IUserRepository : IBRepository<User, ObjectId>
{
}
