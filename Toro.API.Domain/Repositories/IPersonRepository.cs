using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Toro.API.Domain.Entities;
using Toro.API.Domain.Models;
using Toro.API.Domain.Repositories.Base;

namespace Toro.API.Domain.Repositories;

public interface IPersonRepository : IBRepository<Person, ObjectId>
{
    public Task<PersonWalletModel> QueryPersonWalletById(ObjectId id);
}
