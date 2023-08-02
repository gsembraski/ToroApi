using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using Toro.API.Domain.Entities.Base;

namespace Toro.API.Domain.Entities;

[BsonIgnoreExtraElements]
public class Asset : Entity<ObjectId>
{
    public Asset() { }
    public Asset(string name, string code, string value)
    {
        Name = name;
        Code = code;
        Value = value;
    }

    public string Name { get; private set; }
    public string Code { get; private set; }
    public string Value { get; private set; }
}
