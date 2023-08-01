using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Toro.API.Controllers.Base;
using Toro.API.Domain.Commands.User;
using Toro.API.Domain.Resources.Notification;

namespace Toro.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBaseApi<PersonController>
    {
        public UserController(ILogger<PersonController> logger, IMediator mediator, IDomainNotificationContext domainNotificationContext) : base(logger, mediator, domainNotificationContext) { }

        [AllowAnonymous]
        [HttpPost("auth")]
        public async Task<IActionResult> PostAsync([FromBody] AuthUserCommand command,
           [FromServices] SigningCredentials signingConfigurations) => await TrySendCommand(command.SetAuthConfigs(signingConfigurations));
    }
}
