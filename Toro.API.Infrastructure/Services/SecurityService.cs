using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Toro.API.Domain.Resources.Extensions;
using Toro.API.Domain.Services;

namespace Toro.API.Infrastructure.Services
{
    public class SecurityService : SecurityIdentityBase
    {
        public bool IsAuthenticated
        {
            get
            {
                try
                {
                    if (_accessor.HttpContext == null)
                        return false;

                    return _accessor.HttpContext.User.Identity.IsAuthenticated;
                }
                catch
                {
                    return false;
                }
            }
        }

        private readonly IHttpContextAccessor _accessor;

        public SecurityService(
            IHttpContextAccessor accessor)
        {
            _accessor = accessor;

            if (IsAuthenticated)
            {
                var uniqueId = GetValueFromClaim<Guid>("UUID");

                CustomIdentity = new CustomIdentity()
                {
                    UserName = GetValueFromClaim<string>("UN"),
                    Email = GetValueFromClaim<string>("UID"),
                    Id = uniqueId.AsObjectId()
                };
            }
            else
            {
                CustomIdentity = new CustomIdentity();
            }
        }

        public string GetApplicationKey()
        {
            return _accessor.HttpContext?.Request?.Headers?["ApplicationKey"];
        }

        public string GetToken()
        {
            return _accessor.HttpContext.Request.Headers["Authorization"];
        }

        private T GetValueFromClaim<T>(string claim)
        {
            return GetValueFromClaim<T>(_accessor, claim);
        }

        private static T GetValueFromClaim<T>(IHttpContextAccessor accessor, string claim)
        {
            var claims = accessor.HttpContext.User.Claims;

            try
            {
                return ConvertTo<T>(claims.FirstOrDefault(x => x.Type.Equals(claim))?.Value);
            }
            catch
            {
                return default;
            }
        }

        private static T ConvertTo<T>(string value)
        {
            var type = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);

            if (typeof(T) == typeof(Guid))
                return (T)Convert.ChangeType(Guid.Parse(value), type);

            if (value == null)
                return default;

            return (T)Convert.ChangeType(value, type);
        }

    }
}
