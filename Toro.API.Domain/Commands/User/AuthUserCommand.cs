using MediatR;
using System.Text.Json.Serialization;
using Toro.API.Domain.Resources.Result;
using Microsoft.IdentityModel.Tokens;

namespace Toro.API.Domain.Commands.User;
public record AuthUserCommand : IRequest<GenericCommandResult>
{
    public string Email { get; set; }
    public string Password { get; set; }
    [JsonIgnore]
    public SigningCredentials SigningConfigurations { get; set; }

    public AuthUserCommand SetAuthConfigs(SigningCredentials signingConfigurations)
    {
        SigningConfigurations = signingConfigurations;

        return this;
    }
}
