using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toro.API.Domain.Resources.Notification;

public interface IDomainNotificationContext
{
    public bool HasErrorNotifications { get; }
    void NotifyError(string value);
    void NotifyError(IEnumerable<string> values);
    IEnumerable<DomainNotification> GetErrorNotifications();

    void ClearNotifications();
}
