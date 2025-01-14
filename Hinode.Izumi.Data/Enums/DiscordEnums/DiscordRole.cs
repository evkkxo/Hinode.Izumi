﻿using System;

namespace Hinode.Izumi.Data.Enums.DiscordEnums
{
    public enum DiscordRole : byte
    {
        MusicBot = 1,
        Administration = 2,
        EventManager = 3,
        Moderator = 4,
        Nitro = 5, // роль nitro-boost создается дискордом по-умолчанию, нам нужно только получить ее
        Mute = 6,
        ContentProvider = 7,
        InVoice = 8,
        Premium = 9,
        DiscordEvent = 10,

        LocationInTransit = 50,
        LocationCapital = 51,
        LocationGarden = 52,
        LocationSeaport = 53,
        LocationCastle = 54,
        LocationVillage = 55,

        AllEvents = 100,
        DailyEvents = 101,
        WeeklyEvents = 102,
        MonthlyEvents = 103,
        YearlyEvents = 104,
        UniqueEvents = 105,

        GenshinImpact = 200,
        LeagueOfLegends = 201,
        TeamfightTactics = 202,
        Valorant = 203,
        ApexLegends = 204,
        LostArk = 205,
        Dota = 206,
        Osu = 207,
        AmongUs = 208,
        Rust = 209,
        CsGo = 210,
        HotS = 211,
        WildRift = 212,
        MobileLegends = 213
    }

    public static class DiscordRoleHelper
    {
        public static string Name(this DiscordRole role) => role switch
        {
            DiscordRole.MusicBot => "Музыкальные боты",
            DiscordRole.Administration => "Сёгунат",
            DiscordRole.EventManager => "Собаёри",
            DiscordRole.Moderator => "Родзю",
            DiscordRole.Nitro => "🤝 Поддержка сервера",
            DiscordRole.LocationInTransit => "🐫 " + Location.InTransit.Localize(),
            DiscordRole.LocationCapital => "🏯 " + Location.Capital.Localize(),
            DiscordRole.LocationGarden => "🌳 " + Location.Garden.Localize(),
            DiscordRole.LocationSeaport => "⛵ " + Location.Seaport.Localize(),
            DiscordRole.LocationCastle => "🏰 " + Location.Castle.Localize(),
            DiscordRole.LocationVillage => "🎑 " + Location.Village.Localize(),
            DiscordRole.AllEvents => "📅 Все события",
            DiscordRole.DailyEvents => "📅 Ежедневные события",
            DiscordRole.WeeklyEvents => "📅 Еженедельные события",
            DiscordRole.MonthlyEvents => "📅 Ежемесячные события",
            DiscordRole.YearlyEvents => "📅 Ежегодные события",
            DiscordRole.UniqueEvents => "📅 Уникальные события",
            DiscordRole.GenshinImpact => "Genshin Impact",
            DiscordRole.LeagueOfLegends => "League of Legends",
            DiscordRole.TeamfightTactics => "Teamfight Tactics",
            DiscordRole.Valorant => "Valorant",
            DiscordRole.ApexLegends => "Apex Legends",
            DiscordRole.LostArk => "LostArk",
            DiscordRole.Dota => "Dota 2",
            DiscordRole.Osu => "Osu!",
            DiscordRole.AmongUs => "Among Us",
            DiscordRole.Mute => "Блокировка чата",
            DiscordRole.ContentProvider => "❤️ Поставщик контента",
            DiscordRole.InVoice => "🎙️",
            DiscordRole.Rust => "Rust",
            DiscordRole.CsGo => "CSGO",
            DiscordRole.HotS => "HotS",
            DiscordRole.WildRift => "Wild Rift",
            DiscordRole.MobileLegends => "Mobile Legends",
            DiscordRole.Premium => "👑 Премиум",
            DiscordRole.DiscordEvent => "🥳 Мероприятия",
            _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
        };

        public static string Color(this DiscordRole role) => role switch
        {
            DiscordRole.Administration => "ffc7f5",
            DiscordRole.EventManager => "e99edb",
            DiscordRole.Moderator => "c072b2",
            DiscordRole.Nitro => "f47fff",
            DiscordRole.ContentProvider => "6fffc4",
            DiscordRole.Premium => "ffb71d",
            // для всех остальных используем значение по-умолчанию (прозрачный цвет дискорда)
            _ => "000000"
        };
    }
}
