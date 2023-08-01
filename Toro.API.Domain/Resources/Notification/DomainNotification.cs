using MediatR;
using System.Net;

namespace Toro.API.Domain.Resources.Notification;

public class DomainNotification : INotification
{

    public HttpStatusCode Code { get; protected set; }

    public string Value { get; protected set; }

    public DomainNotification(HttpStatusCode code, string value)
    {
        Code = code;
        Value = value;
    }
}
