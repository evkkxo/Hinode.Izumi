﻿using System.Threading.Tasks;
using Discord.Commands;
using Hinode.Izumi.Commands.UserCommands.UserInfoCommands.UserReputationCommands.UserReputationInfoCommand;
using Hinode.Izumi.Commands.UserCommands.UserInfoCommands.UserReputationCommands.UserReputationListCommand;
using Hinode.Izumi.Data.Enums;
using Hinode.Izumi.Data.Enums.ReputationEnums;
using Hinode.Izumi.Services.WebServices.CommandWebService.Attributes;

namespace Hinode.Izumi.Commands.UserCommands.UserInfoCommands.UserReputationCommands
{
    [CommandCategory(CommandCategory.UserInfo)]
    [IzumiRequireRegistry]
    public class UserReputationCommands : ModuleBase<SocketCommandContext>
    {
        private readonly IUserReputationListCommand _userReputationListCommand;
        private readonly IUserReputationInfoCommand _userReputationInfoCommand;

        public UserReputationCommands(IUserReputationListCommand userReputationListCommand,
            IUserReputationInfoCommand userReputationInfoCommand)
        {
            _userReputationListCommand = userReputationListCommand;
            _userReputationInfoCommand = userReputationInfoCommand;
        }

        [Command("репутация"), Alias("reputation")]
        [Summary("Посмотреть свою репутацию в городах")]
        public async Task UserReputationTask() =>
            await _userReputationListCommand.Execute(Context);

        [Command("репутация"), Alias("reputation")]
        [Summary("Посмотреть свой прогресс указанной репутации")]
        [CommandUsage("!репутация 1", "!репутация 5")]
        public async Task UserReputationTask(
            [Summary("Номер репутации")] Reputation reputation) =>
            await _userReputationInfoCommand.Execute(Context, reputation);
    }
}
