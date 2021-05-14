﻿using System.Threading.Tasks;
using Discord.Commands;

namespace Hinode.Izumi.Commands.Administration
{
    public class PingCommand : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public async Task TestTask() => await ReplyAsync("Pong");
    }
}
