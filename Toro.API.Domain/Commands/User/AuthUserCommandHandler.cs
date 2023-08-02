using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Toro.API.Domain.Repositories;
using Toro.API.Domain.Resources.Extensions;
using Toro.API.Domain.Resources.Notification;
using Toro.API.Domain.Resources.Result;

namespace Toro.API.Domain.Commands.User;

public class AuthUserCommandHandler : IRequestHandler<AuthUserCommand, GenericCommandResult>
{
    private readonly IDomainNotificationContext _notification;
    private readonly IUserRepository _userRepository;

    public AuthUserCommandHandler(
        IDomainNotificationContext notification,
        IUserRepository userRepository)
    {
        _notification = notification;
        _userRepository = userRepository;
    }

    public async Task<GenericCommandResult> Handle(AuthUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindOneAsync(x => x.Email.Equals(request.Email) && x.Password.Equals(request.Password.EncryptMD5()));
        if (user == null)
        {
            _notification.NotifyError("Email ou senha incorretos.");
            return new GenericCommandResult(false);
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescription = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddDays(8),
            SigningCredentials = request.SigningConfigurations,
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("UID", user.Email),
                new Claim("UN", user.Person.Name),
                new Claim("UUID", user.Person.Id.AsGuid().ToString())
            })
        };

        var token = tokenHandler.CreateToken(tokenDescription);

        return new GenericCommandResult(true, ResourceMessage.Success, tokenHandler.WriteToken(token));
    }
}

