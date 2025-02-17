﻿using System.Threading.Tasks;
using Discord.Commands;
using Hinode.Izumi.Commands.UserCommands.UserInfoCommands.UserAchievementsCommands.UserAchievementsCategoryCommand;
using Hinode.Izumi.Commands.UserCommands.UserInfoCommands.UserAchievementsCommands.UserAchievementsCommand;
using Hinode.Izumi.Data.Enums;
using Hinode.Izumi.Data.Enums.AchievementEnums;
using Hinode.Izumi.Services.WebServices.CommandWebService.Attributes;

namespace Hinode.Izumi.Commands.UserCommands.UserInfoCommands.UserAchievementsCommands
{
    [CommandCategory(CommandCategory.UserInfo, CommandCategory.Achievements)]
    [Group("достижения"), Alias("achievements")]
    [IzumiRequireRegistry]
    public class UserAchievementsCommands : ModuleBase<SocketCommandContext>
    {
        private readonly IUserAchievementsCommand _userAchievementsCommand;
        private readonly IUserAchievementsCategoryCommand _userAchievementsCategoryCommand;

        public UserAchievementsCommands(IUserAchievementsCommand userAchievementsCommand,
            IUserAchievementsCategoryCommand userAchievementsCategoryCommand)
        {
            _userAchievementsCommand = userAchievementsCommand;
            _userAchievementsCategoryCommand = userAchievementsCategoryCommand;
        }

        [Command("")]
        [Summary("Посмотреть доступные категории достижений")]
        public async Task UserAchievementsCommandTask() =>
            await _userAchievementsCommand.Execute(Context);

        [Command("")]
        [Summary("Посмотреть свои достижения в указанной категории")]
        [CommandUsage("!достижения 1", "!достижения 5")]
        public async Task UserAchievementsCategoryTask(
            [Summary("Номер категории")] AchievementCategory category) =>
            await _userAchievementsCategoryCommand.Execute(Context, category);
    }
}
