using MediatR;
using System;
using System.Collections.Generic;
using Toro.API.Domain.Resources.Result;

namespace Toro.API.Domain.Commands.Person;
public record CreatePersonCommand : IRequest<GenericCommandResult>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string CPF { get; set; }
    public DateTime Birth { get; set; }
    public WalletCommand Wallet { get; set; }
}

public record WalletCommand
{
    public decimal Balance { get; set; }
    public IEnumerable<AssetCommand> Assets { get; set; }
}

public record AssetCommand
{
    public string Code { get; set; }
    public string Name { get; set; }
    public Int16 Amount { get; set; }
}