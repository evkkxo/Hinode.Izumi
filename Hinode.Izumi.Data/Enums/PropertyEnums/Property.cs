﻿using System;

namespace Hinode.Izumi.Data.Enums.PropertyEnums
{
    /// <summary>
    /// Свойство (настройка) игрового мира.
    /// </summary>
    public enum Property
    {
        /// <summary>
        /// Потребление энергии при исследование.
        /// </summary>
        EnergyCostExplore = 1,

        /// <summary>
        /// Потребление энергии при перемещении.
        /// </summary>
        EnergyCostTransit = 2,

        /// <summary>
        /// Потребление энергии при изготовлении и приготовлении.
        /// </summary>
        EnergyCostCraft = 3,

        /// <summary>
        /// Потребление энергии при посадке семян на ячейку участка.
        /// </summary>
        EnergyCostFieldPlant = 7,

        /// <summary>
        /// Потребление энергии при сборе урожая с ячейки участка.
        /// </summary>
        EnergyCostFieldCollect = 8,

        /// <summary>
        /// Потребление энергии при поливке одной ячейки участка.
        /// </summary>
        EnergyCostFieldWater = 9,

        /// <summary>
        /// Потребление энергии при выкапывании ячейки участка.
        /// </summary>
        EnergyCostFieldDig = 10,

        /// <summary>
        /// Длительность поливки одной ячейки участка.
        /// </summary>
        ActionTimeFieldWater = 11,

        /// <summary>
        /// Длительность рыбалки.
        /// </summary>
        ActionTimeFishing = 12,

        /// <summary>
        /// Уменьшение длительности рыбалки при наличии рыбацкой лодки.
        /// </summary>
        ActionTimeReduceFishingBoat = 13,

        /// <summary>
        /// Текущий сезон.
        /// </summary>
        CurrentSeason = 14,

        /// <summary>
        /// Погода сегодня.
        /// </summary>
        WeatherToday = 15,

        /// <summary>
        /// Погода завтра.
        /// </summary>
        WeatherTomorrow = 16,

        /// <summary>
        /// Дебафф от вторжения босса.
        /// </summary>
        BossDebuff = 17,

        /// <summary>
        /// Стартовый капитал.
        /// </summary>
        EconomyStartupCapital = 18,

        /// <summary>
        /// Количество ежедневных иен.
        /// </summary>
        EconomyDailyIncome = 19,

        /// <summary>
        /// Награда за прохождение обучения.
        /// </summary>
        EconomyTrainingAward = 20,

        /// <summary>
        /// Количество необходимых пользователей с лотерейным билетом для розыгрыша.
        /// </summary>
        LotteryRequireUsers = 21,

        /// <summary>
        /// Награда за победу в лотерее.
        /// </summary>
        LotteryAward = 22,

        /// <summary>
        /// Количество потраченных иен за прохождение обучения.
        /// </summary>
        EconomyTrainingCost = 23,

        /// <summary>
        /// Кулдаун обновления информации в профиле.
        /// </summary>
        CooldownUpdateAbout = 24,

        /// <summary>
        /// Кулдаун ставки в казино.
        /// </summary>
        CooldownCasinoBet = 25,

        /// <summary>
        /// Стоимость изготовления (% от стоимости предмета).
        /// </summary>
        CraftingCost = 26,

        /// <summary>
        /// Умножение стоимости изготавливаемого предмета на указанный % для определения цены NPC.
        /// </summary>
        CraftingMarkup = 27,

        /// <summary>
        /// Умножение стоимости алкоголя на указанный % для определения цены NPC.
        /// </summary>
        AlcoholMarkup = 28,

        /// <summary>
        /// Умножение стоимости напитка на указанный % для определения цены NPC.
        /// </summary>
        DrinkMarkup = 29,

        /// <summary>
        /// Умножение стоимости блюда на указанный % для определения цены NPC.
        /// </summary>
        FoodMarkup = 30,

        /// <summary>
        /// Количество продаж блюда для окупаемости рецепта.
        /// </summary>
        RecipePaybackSales = 31,

        /// <summary>
        /// Минимальная ставка в казино.
        /// </summary>
        BinBet = 32,

        /// <summary>
        /// Максимальная ставка в казино.
        /// </summary>
        MaxBet = 33,

        /// <summary>
        /// Стоимость лотерейного билета.
        /// </summary>
        LotteryPrice = 34,

        /// <summary>
        /// Стоимость отправки лотерейного билета в подарок.
        /// </summary>
        LotteryDeliveryPrice = 35,

        /// <summary>
        /// Стоимость участка земли.
        /// </summary>
        FieldPrice = 36,

        /// <summary>
        /// Максимальное количество заявок на рынке в одной категории.
        /// </summary>
        MarketMaxRequests = 37,

        /// <summary>
        /// Необходимое количестов пользователей семьи для завершения этапа регистрации.
        /// </summary>
        FamilyRegistrationUsers = 38,

        /// <summary>
        /// Стоимость одного часа строительства.
        /// </summary>
        BuildingHourPrice = 39,

        /// <summary>
        /// Длительность паузы между оповещением и вторжением ежедневного босса.
        /// </summary>
        BossNotifyTime = 40,

        /// <summary>
        /// Количество репутации за убийство ежедневного босса.
        /// </summary>
        BossReputationReward = 41,

        /// <summary>
        /// Длительность боя с ежедневным боссом.
        /// </summary>
        BossKillTime = 42,

        /// <summary>
        /// Необходимое количество пользователей для убийства ежедневного босса.
        /// </summary>
        BossRequiredUsers = 43,

        /// <summary>
        /// Длительность эффекта дебаффа от вторжения ежедневного босса.
        /// </summary>
        BossDebuffExpiration = 44,

        /// <summary>
        /// Уменьшение длительности приготовления блюда на семейной кухне.
        /// </summary>
        ActionTimeReduceKitchen = 45,

        /// <summary>
        /// Уменьшение длительности изготовления в семейной мастерской.
        /// </summary>
        ActionTimeReduceWorkshop = 46,

        /// <summary>
        /// Текущее событие.
        /// </summary>
        CurrentEvent = 47,

        /// <summary>
        /// Блюдо, которое выдается за участие в пикнике майского события.
        /// </summary>
        EventMayPicnicFoodId = 48,

        /// <summary>
        /// Количество выдаваемого блюда за участие в пикнике майского события.
        /// </summary>
        EventMayPicnicFoodAmount = 49,

        /// <summary>
        /// % ускорения перемещения во время события.
        /// </summary>
        EventReduceTransitTime = 50,

        /// <summary>
        /// Баннер, который выдается за победу над боссом майского события.
        /// </summary>
        EventMayBossBannerId = 51,

        /// <summary>
        /// Титул, который выдается за победу над боссом майского события.
        /// </summary>
        EventMayBossTitleId = 52,
    }

    public static class PropertyHelper
    {
        /// <summary>
        /// Возвращает категорию к которой относится это свойство.
        /// </summary>
        /// <param name="property">Свойство.</param>
        /// <returns>Категория свойств.</returns>
        public static PropertyCategory Category(this Property property) => property switch
        {
            Property.EnergyCostExplore => PropertyCategory.EnergyCost,
            Property.EnergyCostTransit => PropertyCategory.EnergyCost,
            Property.EnergyCostCraft => PropertyCategory.EnergyCost,
            Property.EnergyCostFieldPlant => PropertyCategory.EnergyCost,
            Property.EnergyCostFieldCollect => PropertyCategory.EnergyCost,
            Property.EnergyCostFieldWater => PropertyCategory.EnergyCost,
            Property.EnergyCostFieldDig => PropertyCategory.EnergyCost,
            Property.ActionTimeFieldWater => PropertyCategory.ActionTime,
            Property.ActionTimeFishing => PropertyCategory.ActionTime,
            Property.ActionTimeReduceFishingBoat => PropertyCategory.ActionTimeReduce,
            Property.CurrentSeason => PropertyCategory.WorldState,
            Property.WeatherToday => PropertyCategory.WorldState,
            Property.WeatherTomorrow => PropertyCategory.WorldState,
            Property.BossDebuff => PropertyCategory.WorldState,
            Property.EconomyStartupCapital => PropertyCategory.Economy,
            Property.EconomyDailyIncome => PropertyCategory.Economy,
            Property.EconomyTrainingAward => PropertyCategory.Economy,
            Property.LotteryRequireUsers => PropertyCategory.Economy,
            Property.LotteryAward => PropertyCategory.Economy,
            Property.EconomyTrainingCost => PropertyCategory.Economy,
            Property.CooldownUpdateAbout => PropertyCategory.Cooldown,
            Property.CooldownCasinoBet => PropertyCategory.Cooldown,
            Property.CraftingCost => PropertyCategory.Economy,
            Property.CraftingMarkup => PropertyCategory.Economy,
            Property.AlcoholMarkup => PropertyCategory.Economy,
            Property.DrinkMarkup => PropertyCategory.Economy,
            Property.FoodMarkup => PropertyCategory.Economy,
            Property.RecipePaybackSales => PropertyCategory.Economy,
            Property.BinBet => PropertyCategory.Economy,
            Property.MaxBet => PropertyCategory.Economy,
            Property.LotteryPrice => PropertyCategory.Economy,
            Property.LotteryDeliveryPrice => PropertyCategory.Economy,
            Property.FieldPrice => PropertyCategory.Economy,
            Property.MarketMaxRequests => PropertyCategory.Economy,
            Property.FamilyRegistrationUsers => PropertyCategory.Family,
            Property.BuildingHourPrice => PropertyCategory.Building,
            Property.BossNotifyTime => PropertyCategory.Boss,
            Property.BossReputationReward => PropertyCategory.Boss,
            Property.BossKillTime => PropertyCategory.Boss,
            Property.BossRequiredUsers => PropertyCategory.Boss,
            Property.BossDebuffExpiration => PropertyCategory.Boss,
            Property.ActionTimeReduceKitchen => PropertyCategory.ActionTimeReduce,
            Property.ActionTimeReduceWorkshop => PropertyCategory.ActionTimeReduce,
            Property.CurrentEvent => PropertyCategory.WorldState,
            Property.EventMayPicnicFoodId => PropertyCategory.Event,
            Property.EventMayPicnicFoodAmount => PropertyCategory.Event,
            Property.EventReduceTransitTime => PropertyCategory.Event,
            Property.EventMayBossBannerId => PropertyCategory.Event,
            Property.EventMayBossTitleId => PropertyCategory.Event,
            _ => throw new ArgumentOutOfRangeException(nameof(property), property, null)
        };
    }
}
