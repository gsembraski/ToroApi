using System;
using System.Collections.Generic;
using System.Linq;

namespace Toro.API.Domain.Models;

public class PersonWalletModel
{
    public string Name { get; set; }
    public string CPF { get; set; }
    public decimal Balance { get; set; }
    public IEnumerable<AssetModel> Assets { get; set; }
    public decimal Overrun
    {
        get
        {
            return this.Balance + this.Assets.Sum(x => x.Value * x.Amount);
        }
    }
}

public record AssetModel
{
    public string Code { get; set; }
    public string Name { get; set; }
    public Int32 Amount { get; set; }
    public decimal Value { get; set; }
}
