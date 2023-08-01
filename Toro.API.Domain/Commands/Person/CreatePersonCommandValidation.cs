using FluentValidation;
using Toro.API.Domain.Resources.Result;

namespace Toro.API.Domain.Commands.Person;

public class CreateUserCommandValidator : AbstractValidator<CreatePersonCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .Empty().When(x => string.IsNullOrEmpty(x.Name)).WithMessage(ResourceMessage.ErrorRequired)
            .MinimumLength(5).When(x => !string.IsNullOrEmpty(x.Name)).WithMessage(ResourceMessage.ErrorMinLength);

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
