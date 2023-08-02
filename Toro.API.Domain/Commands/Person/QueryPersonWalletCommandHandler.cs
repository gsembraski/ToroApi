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
using Toro.API.Domain.Services;

namespace Toro.API.Domain.Commands.Person;

public class QueryPersonWalletCommandHandler : IRequestHandler<QueryPersonWalletCommand, GenericCommandResult>
{
    private readonly IDomainNotificationContext _notification;
    private readonly IPersonRepository _personRepository;
    private readonly SecurityIdentityBase _securityIdentity;

    public QueryPersonWalletCommandHandler(
        IDomainNotificationContext notification,
        IPersonRepository personRepository,
        SecurityIdentityBase securityIdentity)
    {
        _notification = notification;
        _personRepository = personRepository;
        _securityIdentity = securityIdentity;
    }

    public async Task<GenericCommandResult> Handle(QueryPersonWalletCommand request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.QueryPersonWalletById(_securityIdentity.CustomIdentity.Id);
        if (person == null)
        {
            _notification.NotifyError(ResourceMessage.Error);
            return new GenericCommandResult(false);
        }

        return new GenericCommandResult(true, ResourceMessage.Success, person);
    }
}

