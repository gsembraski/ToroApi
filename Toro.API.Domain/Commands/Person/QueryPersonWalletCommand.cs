using MediatR;
using System;
using Toro.API.Domain.Resources.Result;

namespace Toro.API.Domain.Commands.Person;
public record QueryPersonWalletCommand : IRequest<GenericCommandResult>;
