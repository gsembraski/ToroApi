using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using Toro.API.Domain.Entities.Base;

namespace Toro.API.Domain.Entities;

[BsonIgnoreExtraElements]
public class User : Entity<ObjectId>
{
    public User() { }
    public User(string email, string password, PersonUser person)
    {
        Email = email;
        Password = password;
        Person = person;
    }

    public string Email { get; private set; }
    public string Password { get; private set; }
    public PersonUser Person { get; private set; }
}

public struct PersonUser
{
    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public string CPF { get; set; }
    public string AccountNumber { get; set; }
}
