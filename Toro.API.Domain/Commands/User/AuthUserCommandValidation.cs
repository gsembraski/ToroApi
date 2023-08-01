using FluentValidation;
using Toro.API.Domain.Resources.Result;

namespace Toro.API.Domain.Commands.User;

public class AuthUserCommandValidator : AbstractValidator<AuthUserCommand>
{
    public AuthUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotNull()
            .Empty().When(x => string.IsNullOrEmpty(x.Email)).WithMessage(ResourceMessage.ErrorRequired)
            .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email)).WithMessage(ResourceMessage.ErrorInvalidValue);

        RuleFor(x => x.Password)
            .NotNull()
            .Empty().When(x => string.IsNullOrEmpty(x.Password)).WithMessage(ResourceMessage.ErrorRequired)
            .MinimumLength(8).When(x => !string.IsNullOrEmpty(x.Password)).WithMessage(ResourceMessage.ErrorMinLength);
    }
}
