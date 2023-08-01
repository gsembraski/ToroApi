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
    public User(ObjectId id,string email, string password, PersonUser person)
    {
        Id = id;
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
    public PersonUser() { }
    public PersonUser(ObjectId id = default, string name = null, string cPF = null, string accountNumber = null)
    {
        Id = id;
        Name = name;
        CPF = cPF;
        AccountNumber = accountNumber;
    }

    public ObjectId Id { get; private set; }
    public string Name { get; private set; }
    public string CPF { get; private set; }
    public string AccountNumber { get; private set; }
}
