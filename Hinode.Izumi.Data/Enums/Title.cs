﻿using System;

namespace Hinode.Izumi.Data.Enums
{
    public enum Title : byte
    {
        Newbie = 1, // титул по-умолчанию
        Lucky = 2,
        ResourcefulCatcher = 3,
        DescendantAristocracy = 4,
        DescendantOcean = 5,
        KeeperGrove = 6,
        ReliableWorkaholic = 7,
        SereneExcavator = 8,
        AgileEarner = 9,
        Handyman = 10,
        WineSamurai = 11,
        StockyFarmer = 12,
        SeaPoet = 13,
        CulinaryIdol = 14,
        Toxic = 15,
        KingExcitement = 16,
        BelievingInLuck = 17,
        FirstSamurai = 18,
        Yatagarasu = 19,
        HarbingerOfSummer = 20,
        Wanderer = 255 // титул для Изуми
    }

    public static class TitleHelper
    {
        public static string Localize(this Title title) => title switch
        {
            Title.Newbie => "Новичок",
            Title.Lucky => "Приносящий удачу",
            Title.ResourcefulCatcher => "Находчивый ловец",
            Title.DescendantAristocracy => "Потомок аристократии",
            Title.DescendantOcean => "Потомок океана",
            Title.KeeperGrove => "Хранитель рощи",
            Title.ReliableWorkaholic => "Надежный трудяга",
            Title.SereneExcavator => "Безмятежный землекоп",
            Title.AgileEarner => "Проворный добытчик",
            Title.Handyman => "Мастер на все руки",
            Title.WineSamurai => "Винный самурай",
            Title.StockyFarmer => "Запасливый фермер",
            Title.SeaPoet => "Морской поэт",
            Title.CulinaryIdol => "Кулинарный идол",
            Title.Toxic => "Токсичный",
            Title.KingExcitement => "Король азарта",
            Title.BelievingInLuck => "Верящий в удачу",
            Title.Wanderer => "Странница",
            Title.FirstSamurai => "Первый самурай",
            Title.Yatagarasu => "Ятагарасу",
            Title.HarbingerOfSummer => "Предвестник лета",
            _ => throw new ArgumentOutOfRangeException(nameof(title), title, null)
        };

        public static string Emote(this Title title) => "Title" + title;
    }
}
