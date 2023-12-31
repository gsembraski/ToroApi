﻿using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toro.API.Domain.Resources.Extensions;

public static class BsonToGuid
{
    public static Guid AsGuid(this ObjectId oid)
    {
        var bytes = oid.ToByteArray().Concat(new byte[] { 5, 5, 5, 5 }).ToArray();
        Guid gid = new Guid(bytes);
        return gid;
    }

    public static ObjectId AsObjectId(this Guid gid)
    {
        var bytes = gid.ToByteArray().Take(12).ToArray();
        var oid = new ObjectId(bytes);
        return oid;
    }
}
