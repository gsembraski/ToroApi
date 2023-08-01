using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Toro.API.Domain.Services
{
    public abstract class SecurityIdentityBase
    {
        public CustomIdentity CustomIdentity { get; set; }
    }

    public class CustomIdentity
    {
        public ObjectId Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
