using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Toro.API.Controllers.Base;
using Toro.API.Domain.Commands.Person;
using Toro.API.Domain.Resources.Notification;

namespace Toro.API.Controllers
{
    [Route("api/person")]
    [ApiController]
    public class PersonController : ControllerBaseApi<PersonController>
    {
        public PersonController(ILogger<PersonController> logger, IMediator mediator, IDomainNotificationContext domainNotificationContext) :base(logger, mediator, domainNotificationContext) { }
       
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreatePersonCommand command) => await TrySendCommand(command);

        [HttpGet("wallet")]
        public async Task<IActionResult> GetAsync() => await TrySendCommand(new QueryPersonWalletCommand());
    }
}
