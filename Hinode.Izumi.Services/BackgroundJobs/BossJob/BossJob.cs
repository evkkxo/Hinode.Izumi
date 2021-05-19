﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Hangfire;
using Hinode.Izumi.Data.Enums;
using Hinode.Izumi.Data.Enums.DiscordEnums;
using Hinode.Izumi.Data.Enums.MessageEnums;
using Hinode.Izumi.Data.Enums.PropertyEnums;
using Hinode.Izumi.Data.Enums.ReputationEnums;
using Hinode.Izumi.Framework.Autofac;
using Hinode.Izumi.Services.BackgroundJobs.DiscordJob;
using Hinode.Izumi.Services.DiscordServices.DiscordEmbedService;
using Hinode.Izumi.Services.DiscordServices.DiscordGuildService;
using Hinode.Izumi.Services.EmoteService;
using Hinode.Izumi.Services.EmoteService.Impl;
using Hinode.Izumi.Services.RpgServices.ImageService;
using Hinode.Izumi.Services.RpgServices.InventoryService;
using Hinode.Izumi.Services.RpgServices.LocalizationService;
using Hinode.Izumi.Services.RpgServices.PropertyService;
using Hinode.Izumi.Services.RpgServices.ReputationService;
using Hinode.Izumi.Services.RpgServices.StatisticService;
using Box = Hinode.Izumi.Data.Enums.Box;
using Image = Hinode.Izumi.Data.Enums.Image;

namespace Hinode.Izumi.Services.BackgroundJobs.BossJob
{
    [InjectableService]
    public class BossJob : IBossJob
    {
        private readonly IDiscordEmbedService _discordEmbedService;
        private readonly IDiscordGuildService _discordGuildService;
        private readonly IEmoteService _emoteService;
        private readonly IPropertyService _propertyService;
        private readonly IStatisticService _statisticService;
        private readonly IInventoryService _inventoryService;
        private readonly IReputationService _reputationService;
        private readonly IImageService _imageService;
        private readonly ILocalizationService _local;

        private readonly Random _random = new();

        private readonly Location[] _spawnLocations =
            {Location.Capital, Location.Garden, Location.Seaport, Location.Castle, Location.Village};

        private const string AttackEmote = "⚔️";

        public BossJob(IDiscordEmbedService discordEmbedService, IDiscordGuildService discordGuildService,
            IEmoteService emoteService, IPropertyService propertyService, IStatisticService statisticService,
            IInventoryService inventoryService, IReputationService reputationService,
            IImageService imageService, ILocalizationService local)
        {
            _discordEmbedService = discordEmbedService;
            _discordGuildService = discordGuildService;
            _emoteService = emoteService;
            _propertyService = propertyService;
            _statisticService = statisticService;
            _inventoryService = inventoryService;
            _reputationService = reputationService;
            _imageService = imageService;
            _local = local;
        }

        public async Task Anons()
        {
            // получаем текущее событие
            var currentEvent = (Event) await _propertyService.GetPropertyValue(Property.CurrentEvent);
            // если текущее событие это майское событие - то босс не должен появляться
            if (currentEvent == Event.May) return;

            // получаем роли сервера
            var roles = await _discordGuildService.GetRoles();
            // получаем случайную локацию
            var randomLocation = (Location) _spawnLocations.GetValue(_random.Next(_spawnLocations.Length));

            var embed = new EmbedBuilder()
                .WithAuthor(IzumiEventMessage.DiaryAuthorField.Parse())
                // оповещаем о вторжении босса
                .WithDescription(IzumiEventMessage.BossNotify.Parse(
                    randomLocation.Localize(true)));

            await _discordEmbedService.SendEmbed(DiscordChannel.Diary, embed,
                // упоминаем роли события
                $"<@&{roles[DiscordRole.AllEvents].Id}> <@&{roles[DiscordRole.DailyEvents].Id}>");

            BackgroundJob.Schedule<IBossJob>(
                x => x.Spawn(randomLocation),
                TimeSpan.FromMinutes(
                    // получаем время оповещения о вторжении босса
                    await _propertyService.GetPropertyValue(Property.BossNotifyTime)));
        }


        public async Task Spawn(Location location)
        {
            // получаем иконки из базы
            var emotes = await _emoteService.GetEmotes();
            // получаем роли сервера
            var roles = await _discordGuildService.GetRoles();
            // получаем репутацию этой локации
            var reputation = _reputationService.GetReputationByLocation(location);
            // получаем количество получаемой репутации за убийство босса
            var reputationReward = await _propertyService.GetPropertyValue(Property.BossReputationReward);

            // заполняем канал
            DiscordChannel channel;
            // заполняем нпс
            Npc npc;
            // заполняем изображение босса
            Image bossImage;
            // заполняем коробку
            Box box;

            // заполняем данные в зависимости от репутации
            switch (reputation)
            {
                case Reputation.Capital:

                    channel = DiscordChannel.CapitalEvents;
                    npc = Npc.Toredo;
                    bossImage = Image.BossCapital;
                    box = Box.Capital;

                    break;
                case Reputation.Garden:

                    channel = DiscordChannel.GardenEvents;
                    npc = Npc.Nari;
                    bossImage = Image.BossGarden;
                    box = Box.Garden;

                    break;
                case Reputation.Seaport:

                    channel = DiscordChannel.SeaportEvents;
                    npc = Npc.Ivao;
                    bossImage = Image.BossSeaport;
                    box = Box.Seaport;

                    break;
                case Reputation.Castle:

                    channel = DiscordChannel.CastleEvents;
                    npc = Npc.Ioshiro;
                    bossImage = Image.BossCastle;
                    box = Box.Castle;

                    break;
                case Reputation.Village:

                    channel = DiscordChannel.VillageEvents;
                    npc = Npc.Kio;
                    bossImage = Image.BossVillage;
                    box = Box.Village;

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var embed = new EmbedBuilder()
                // имя нпс
                .WithAuthor(npc.Name())
                // изображение нпс
                .WithThumbnailUrl(await _imageService.GetImageUrl(npc.Image()))
                // описание вторжения ежедневного босса
                .WithDescription(
                    IzumiEventMessage.BossHere.Parse(
                        location.Localize(), AttackEmote) +
                    $"\n{emotes.GetEmoteOrBlank("Blank")}")
                // ожидаемая награда
                .AddField(IzumiEventMessage.BossRewardFieldName.Parse(),
                    IzumiEventMessage.BossRewardReputation.Parse(
                        emotes.GetEmoteOrBlank(reputation.Emote(long.MaxValue)), reputationReward,
                        location.Localize(true)) +
                    $"{emotes.GetEmoteOrBlank(box.Emote())} {_local.Localize(box.ToString())}")
                // изображение босса
                .WithImageUrl(await _imageService.GetImageUrl(bossImage))
                // сколько времени дается на убийство ежедневного босса
                .WithFooter(IzumiEventMessage.BossHereFooter.Parse(
                    // получаем длительность боя с ежедневным боссом
                    await _propertyService.GetPropertyValue(Property.BossKillTime)));

            // отправляем сообщение
            var message = await _discordEmbedService.SendEmbed(channel, embed,
                // упоминаем роли события
                $"<@&{roles[DiscordRole.AllEvents].Id}> <@&{roles[DiscordRole.DailyEvents].Id}>");
            // добавляем реакцию для атаки
            await message.AddReactionAsync(new Emoji(AttackEmote));

            // запускаем джобу с убийством босса
            BackgroundJob.Schedule<IBossJob>(x => x.Kill(
                    (long) message.Channel.Id, (long) message.Id, reputation, npc, bossImage, box),
                TimeSpan.FromMinutes(
                    // получаем длительность боя с ежедневным боссом
                    await _propertyService.GetPropertyValue(Property.BossKillTime)));
        }


        public async Task Kill(long channelId, long messageId, Reputation reputation, Npc npc, Image bossImage, Box box)
        {
            // получаем иконки из базы
            var emotes = await _emoteService.GetEmotes();
            // получаем сообщение
            var message = await _discordGuildService.GetIUserMessage(channelId, messageId);
            // получаем пользователей нажавших на реакцию
            var reactionUsers = await message
                .GetReactionUsersAsync(new Emoji(AttackEmote), int.MaxValue)
                .FlattenAsync();
            // получаем из всех пользователей только людей (без ботов)
            var users = reactionUsers.Where(x => x.IsBot == false).ToArray();
            // получаем необходимое количество пользователей для убийства ежедневного босса
            var requiredUsersLength = await _propertyService.GetPropertyValue(Property.BossRequiredUsers);

            // снимаем все реакции с сообщения
            await message.RemoveAllReactionsAsync();

            var embed = new EmbedBuilder()
                // имя нпс
                .WithAuthor(npc.Name())
                // изображение нпс
                .WithThumbnailUrl(await _imageService.GetImageUrl(npc.Image()))
                // изображение босса
                .WithImageUrl(await _imageService.GetImageUrl(bossImage));

            if (users.Length < requiredUsersLength)
            {
                // получаем дебафф от втождения ежедневного босса
                var debuff = reputation switch
                {
                    // определяем дебафф
                    Reputation.Capital => BossDebuff.CapitalStop,
                    Reputation.Garden => BossDebuff.GardenStop,
                    Reputation.Seaport => BossDebuff.SeaportStop,
                    Reputation.Castle => BossDebuff.CastleStop,
                    Reputation.Village => BossDebuff.VillageStop,
                    _ => throw new ArgumentOutOfRangeException(nameof(reputation), reputation, null)
                };

                embed.WithDescription(IzumiEventMessage.BossNotKilled.Parse(
                    debuff.Localize()));

                // обновляем свойство мира на новый дебаф
                await _propertyService.UpdateProperty(Property.BossDebuff, debuff.GetHashCode());

                // добавляем джобу для сброса дебафа
                BackgroundJob.Schedule<IBossJob>(x => x.ResetDebuff(),
                    TimeSpan.FromHours(
                        // получаем длительность эффекта дебаффа от вторжения ежедневного босса
                        await _propertyService.GetPropertyValue(Property.BossDebuffExpiration)));
            }
            else
            {
                // получаем id пользователей
                var usersId = users.Select(x => (long) x.Id).ToArray();
                // получаем получаемую репутацию за убийство ежедневного босса
                var reputationReward = await _propertyService.GetPropertyValue(Property.BossReputationReward);

                // добавляем пользователям коробки
                await _inventoryService.AddItemToUser(usersId, InventoryCategory.Box, box.GetHashCode());
                // добавляем пользователям репутацию
                await _reputationService.AddReputationToUser(usersId, reputation, reputationReward);
                // добавляем пользователям статистику
                await _statisticService.AddStatisticToUser(usersId, Statistic.BossKilled);

                // подтверждаем убийтство босса
                embed.WithDescription(IzumiEventMessage.BossKilled.Parse());

                // создаем строку с наградой
                var rewardString =
                    IzumiEventMessage.ReputationAdded.Parse(
                        emotes.GetEmoteOrBlank(reputation.Emote(long.MaxValue)), reputationReward,
                        reputation.Location().Localize(true)) +
                    $", {emotes.GetEmoteOrBlank(box.Emote())} {_local.Localize(box.ToString())}";

                var embedReward = new EmbedBuilder()
                    .WithAuthor(IzumiEventMessage.DiaryAuthorField.Parse())
                    // описываем полученные награды
                    .WithDescription(IzumiEventMessage.BossRewardNotify.Parse(
                        reputation.Location().Localize(true), rewardString));

                await _discordEmbedService.SendEmbed(DiscordChannel.Diary, embedReward);
            }

            // изменяем сообщение
            await _discordEmbedService.ModifyEmbed(message, embed);
            // запускаем джобу с удалением сообщения
            BackgroundJob.Schedule<IDiscordJob>(x =>
                    x.DeleteMessage(channelId, messageId),
                TimeSpan.FromHours(24));
        }


        public async Task ResetDebuff() =>
            await _propertyService.UpdateProperty(Property.BossDebuff, BossDebuff.None.GetHashCode());
    }
}
