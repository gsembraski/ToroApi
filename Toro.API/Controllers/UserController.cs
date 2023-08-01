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
    public class UserController : ControllerBaseApi<UserController>
    {
        public UserController(ILogger<UserController> logger, IMediator mediator, IDomainNotificationContext domainNotificationContext) :base(logger, mediator, domainNotificationContext) { }
       
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateUserCommand command) => await TrySendCommand(command);

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthUserCommand command,
           [FromServices] SigningCredentials signingConfigurations) => await TrySendCommand(command.SetAuthConfigs(signingConfigurations));
    }
}
