using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Toro.API.Domain.Resources.Notification;

public class DomainNotificationContext : IDomainNotificationContext
{
    private IList<DomainNotification> _notifications;
    public bool HasErrorNotifications => _notifications.Any((DomainNotification x) => x.Code == HttpStatusCode.InternalServerError);

    public DomainNotificationContext()
    {
        _notifications = new List<DomainNotification>();
    }

    public IEnumerable<DomainNotification> GetErrorNotifications()
    {
        return _notifications.Where(x => x.Code == HttpStatusCode.InternalServerError);
    }

    public void NotifySuccess(string value)
    {
        Notify(value, 200);
    }

    public void NotifyError(string value)
    {
        Notify(value, 500);
    }

    public void NotifyError(IEnumerable<string> values)
    {
        foreach (var value in values)
            Notify(value, 500);
    }

    private void Notify(string message, int code)
    {
        _notifications.Add(new DomainNotification((HttpStatusCode)code, message));
    }

    public void ClearNotifications()
    {
        _notifications.Clear();
    }
}
