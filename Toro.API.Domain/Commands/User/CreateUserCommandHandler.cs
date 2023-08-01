using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Toro.API.Domain.Repositories;
using Toro.API.Domain.Resources;
using Toro.API.Domain.Resources.Notification;
using Toro.API.Domain.Resources.Extensions;
using Toro.API.Domain.Resources.Result;

namespace Toro.API.Domain.Commands.User;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, GenericCommandResult>
{
    private readonly IDomainNotificationContext _notification;
    private readonly IUserRepository _userRepository;
    private readonly IPersonRepository _personRepository;

    public CreateUserCommandHandler(
        IDomainNotificationContext notification,
        IUserRepository userRepository,
        IPersonRepository personRepository)
    {
        _notification = notification;
        _userRepository = userRepository;
        _personRepository = personRepository;
    }

    public async Task<GenericCommandResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindOneAsync(x => x.Email.Equals(request.Email));
        if (user != null)
        {
            _notification.NotifyError("Esse e-mail esta vinculado a uma conta de usuário.");
            return new GenericCommandResult(false);
        }

        var person = await _personRepository.FindOneAsync(x => x.CPF.Equals(request.CPF));
        if (person != null)
        {
            _notification.NotifyError("Esse CPF esta vinculado a uma conta de usuário.");
            return new GenericCommandResult(false);
        }

        person = new Entities.Person(request.Name, request.CPF, request.Birth, DateTime.UtcNow.Ticks.ToString());
        user = new Entities.User(request.Email, request.Password.EncryptMD5(), new Entities.PersonUser
        {
            Id = person.Id,
            Name = person.Name,
            CPF = person.CPF,
            AccountNumber = person.AccountNumber
        });
        await _personRepository.InsertOneAsync(person);
        await _userRepository.InsertOneAsync(user);

        return new GenericCommandResult(true, ResourceMessage.SuccessCreateItem, new
        {
            UniqueId = user.Id.AsGuid(),
            Name = user.Person.Name,
            Email = user.Email,
            Password = user.Password,
            CreatedAt = user.Id.CreationTime.ToLocalTime(),
        });
    }
}

