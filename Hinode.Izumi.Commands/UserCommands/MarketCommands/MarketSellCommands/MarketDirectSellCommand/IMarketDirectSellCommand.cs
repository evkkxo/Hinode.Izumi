﻿using System.Threading.Tasks;
using Discord.Commands;

namespace Hinode.Izumi.Commands.UserCommands.MarketCommands.MarketSellCommands.MarketDirectSellCommand
{
    public interface IMarketDirectSellCommand
    {
        Task Execute(SocketCommandContext context, long requestId, long amount = 1);
    }
}
