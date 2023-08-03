using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Toro.API.Domain.Resources.Notification;
using Toro.API.Domain.Resources;

namespace Toro.API.Controllers.Base
{
    public abstract class ControllerBaseApi : ControllerBase
    {
        public readonly IMediator _mediator;

        public readonly IDomainNotificationContext _domainNotificationContext;

        protected ControllerBaseApi(IMediator mediator, IDomainNotificationContext domainNotificationContext)
        {
            _mediator = mediator;
            _domainNotificationContext = domainNotificationContext;
        }

        public async Task<IActionResult> TrySendCommand<TRequest>(TRequest command, int? statusCode = null)
        {
            object value = await _mediator.Send(command, default);
            AddModelStateErrorsInNotifications();
            if (_domainNotificationContext.HasErrorNotifications)
            {
                return BadRequestDomainError();
            }

            return StatusCode(statusCode ?? 200, value);
        }

        private void AddModelStateErrorsInNotifications()
        {
            if (base.ModelState.IsValid)
            {
                return;
            }

            foreach (var item in (from error in base.ModelState.SelectMany<KeyValuePair<string, ModelStateEntry>, ModelError>((KeyValuePair<string, ModelStateEntry> modelState) => modelState.Value.Errors)
                                  select new { error.ErrorMessage }).ToList())
            {
                _domainNotificationContext.NotifyError(item.ErrorMessage);
            }
        }

        private IActionResult BadRequestDomainError()
        {
            IEnumerable<string> errors = from x in _domainNotificationContext.GetErrorNotifications()
                                         select x.Value;
            return BadRequest(new
            {
                Success = false,
                Errors = errors
            });
        }
    }
}
