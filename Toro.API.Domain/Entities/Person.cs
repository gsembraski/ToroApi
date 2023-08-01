using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using Toro.API.Domain.Entities.Base;

namespace Toro.API.Domain.Entities;

[BsonIgnoreExtraElements]
public class Person : Entity<ObjectId>
{
    public Person() { }
    public Person(string name, string cpf, DateTime birth, string accountNumber)
    {
        Name = name;
        CPF = cpf;
        Birth = birth;
        AccountNumber = accountNumber;
    }
    public Person(ObjectId id,string name, string cpf, DateTime birth, string accountNumber)
    {
        Id = id;
        Name = name;
        CPF = cpf;
        Birth = birth;
        AccountNumber = accountNumber;
    }

    public string Name { get; private set; }
    public string CPF { get; private set; }
    public string AccountNumber { get; private set; }
    public DateTime Birth { get; private set; }

    public void UpdateInfo(string name, string accountNumber)
    {
        Name = name;
        AccountNumber = accountNumber;
    }
}
