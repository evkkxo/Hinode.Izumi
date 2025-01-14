﻿using System.Threading.Tasks;
using Discord.Commands;

namespace Hinode.Izumi.Commands.UserCommands.MarketCommands.MarketRequestCommands.MarketRequestCancelCommand
{
    public interface IMarketRequestCancelCommand
    {
        Task Execute(SocketCommandContext context, long requestId);
    }
}
