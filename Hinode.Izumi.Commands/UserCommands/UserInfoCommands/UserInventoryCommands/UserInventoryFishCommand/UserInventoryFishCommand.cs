﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Hinode.Izumi.Data.Enums;
using Hinode.Izumi.Data.Enums.MessageEnums;
using Hinode.Izumi.Data.Enums.RarityEnums;
using Hinode.Izumi.Framework.Autofac;
using Hinode.Izumi.Services.DiscordServices.DiscordEmbedService.Commands;
using Hinode.Izumi.Services.EmoteService.Queries;
using Hinode.Izumi.Services.EmoteService.Records;
using Hinode.Izumi.Services.Extensions;
using Hinode.Izumi.Services.GameServices.InventoryService.Queries;
using Hinode.Izumi.Services.GameServices.InventoryService.Records;
using Hinode.Izumi.Services.GameServices.LocalizationService;
using Hinode.Izumi.Services.GameServices.TutorialService.Commands;
using Hinode.Izumi.Services.ImageService.Queries;
using MediatR;
using Image = Hinode.Izumi.Data.Enums.Image;

namespace Hinode.Izumi.Commands.UserCommands.UserInfoCommands.UserInventoryCommands.UserInventoryFishCommand
{
    [InjectableService]
    public class UserInventoryFishCommand : IUserInventoryFishCommand
    {
        private readonly IMediator _mediator;
        private readonly ILocalizationService _local;
        private Dictionary<string, EmoteRecord> _emotes;

        public UserInventoryFishCommand(IMediator mediator, ILocalizationService local)
        {
            _mediator = mediator;
            _local = local;
        }

        public async Task Execute(SocketCommandContext context)
        {
            // получаем все иконки из базы
            _emotes = await _mediator.Send(new GetEmotesQuery());
            // получаем рыбу пользователя
            var userFish = await _mediator.Send(new GetUserFishesQuery((long) context.User.Id));
            // разбиваем ее по редкости
            var commonFish = userFish.Where(x => x.Rarity == FishRarity.Common);
            var rareFish = userFish.Where(x => x.Rarity == FishRarity.Rare);
            var epicFish = userFish.Where(x => x.Rarity == FishRarity.Epic);
            var mythicalFish = userFish.Where(x => x.Rarity == FishRarity.Mythical);
            var legendaryFish = userFish.Where(x => x.Rarity == FishRarity.Legendary);

            var commonFishString = Display(commonFish);
            var rareFishString = Display(rareFish);
            var epicFishString = Display(epicFish);
            var mythicalFishString = Display(mythicalFish);
            var legendaryFishString = Display(legendaryFish);

            var embed = new EmbedBuilder()
                // баннер инвентаря
                .WithImageUrl(await _mediator.Send(new GetImageUrlQuery(Image.Inventory)))
                .WithDescription(IzumiReplyMessage.UserFishDesc.Parse())
                // обычная
                .AddField(FishRarity.Common.Localize(),
                    commonFishString.Length > 0
                        ? commonFishString.Remove(commonFishString.Length - 2)
                        : IzumiReplyMessage.InventoryNull.Parse())
                // редкая
                .AddField(FishRarity.Rare.Localize(),
                    rareFishString.Length > 0
                        ? rareFishString.Remove(rareFishString.Length - 2)
                        : IzumiReplyMessage.InventoryNull.Parse())
                // эпическая
                .AddField(FishRarity.Epic.Localize(),
                    epicFishString.Length > 0
                        ? epicFishString.Remove(epicFishString.Length - 2)
                        : IzumiReplyMessage.InventoryNull.Parse())
                // мифическая
                .AddField(FishRarity.Mythical.Localize(),
                    mythicalFishString.Length > 0
                        ? mythicalFishString.Remove(mythicalFishString.Length - 2)
                        : IzumiReplyMessage.InventoryNull.Parse())
                // легендарная
                .AddField(FishRarity.Legendary.Localize(),
                    legendaryFishString.Length > 0
                        ? legendaryFishString.Remove(legendaryFishString.Length - 2)
                        : IzumiReplyMessage.InventoryNull.Parse());

            await _mediator.Send(new SendEmbedToUserCommand(context.User, embed));
            // проверяем нужно ли двинуть прогресс обучения пользователя
            await _mediator.Send(new CheckUserTutorialStepCommand((long) context.User.Id, TutorialStep.CheckInventory));
            await Task.CompletedTask;
        }

        /// <summary>
        /// Возвращает локализированное отображение рыбы (иконка, количество, название) через запятую.
        /// </summary>
        /// <param name="fishInUser">Массив рыбы у пользователя.</param>
        /// <returns>Локализированная строка рыбы.</returns>
        private string Display(IEnumerable<UserFishRecord> fishInUser)
        {
            return fishInUser
                .Aggregate(string.Empty,
                    (current, fish) =>
                        current + (fish.Amount > 0
                            ? $"{_emotes.GetEmoteOrBlank(fish.Name)} {fish.Amount} {_local.Localize(fish.Name, fish.Amount)}, "
                            : ""));
        }
    }
}
