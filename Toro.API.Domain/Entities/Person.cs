using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using Toro.API.Domain.Entities.Base;

namespace Toro.API.Domain.Entities;

[BsonIgnoreExtraElements]
public class Person : Entity<ObjectId>
{
    public Person() { }
    public Person(string name, string cpf, DateTime birth, string accountNumber, PersonWallet wallet)
    {
        Name = name;
        CPF = cpf;
        Birth = birth;
        AccountNumber = accountNumber;
        Wallet = wallet;
    }
    public Person(ObjectId id,string name, string cpf, DateTime birth, string accountNumber, PersonWallet wallet)
    {
        Id = id;
        Name = name;
        CPF = cpf;
        Birth = birth;
        AccountNumber = accountNumber;
        Wallet = wallet;
    }

    public string Name { get; private set; }
    public string CPF { get; private set; }
    public string AccountNumber { get; private set; }
    public DateTime Birth { get; private set; }
    public PersonWallet Wallet { get; private set; }

    public void UpdateInfo(string name, string accountNumber)
    {
        Name = name;
        AccountNumber = accountNumber;
    }
}

public struct PersonWallet
{
    public PersonWallet(decimal balance, IEnumerable<PersonAsset> assets)
    {
        Balance = balance;
        Assets = assets;
    }

    public decimal Balance { get; private set; }
    public IEnumerable<PersonAsset> Assets { get; private set; }
}

public struct PersonAsset
{
    public PersonAsset(string code, string name, short amount)
    {
        Code = code;
        Name = name;
        Amount = amount;
    }

    public string Code { get; private set; }
    public string Name { get; private set; }
    public Int16 Amount { get; private set; }
}