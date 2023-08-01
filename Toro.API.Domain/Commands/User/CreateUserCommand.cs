﻿using MediatR;
using System;
using Toro.API.Domain.Resources.Result;

namespace Toro.API.Domain.Commands.User;
public record CreateUserCommand : IRequest<GenericCommandResult>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string CPF { get; set; }
    public DateTime Birth { get; set; }
}
