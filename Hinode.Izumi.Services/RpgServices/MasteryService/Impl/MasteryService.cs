﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Hinode.Izumi.Data.Enums;
using Hinode.Izumi.Framework.Autofac;
using Hinode.Izumi.Framework.Database;
using Hinode.Izumi.Services.RpgServices.MasteryService.Models;
using Hinode.Izumi.Services.RpgServices.ReputationService;

namespace Hinode.Izumi.Services.RpgServices.MasteryService.Impl
{
    [InjectableService]
    public class MasteryService : IMasteryService
    {
        private readonly IConnectionManager _con;
        private readonly IReputationService _reputationService;

        public MasteryService(IConnectionManager con, IReputationService reputationService)
        {
            _con = con;
            _reputationService = reputationService;
        }

        public async Task<UserMasteryModel> GetUserMastery(long userId, Mastery mastery) =>
            await _con.GetConnection()
                .QueryFirstOrDefaultAsync<UserMasteryModel>(@"
                    select * from user_masteries
                    where user_id = @userId
                      and mastery = @mastery",
                    new {userId, mastery})
            ?? new UserMasteryModel {UserId = userId, Mastery = mastery, Amount = 0};

        public async Task<Dictionary<Mastery, UserMasteryModel>> GetUserMastery(long userId) =>
            (await _con.GetConnection()
                .QueryAsync<UserMasteryModel>(@"
                    select * from user_masteries
                    where user_id = @userId",
                    new {userId}))
            .ToDictionary(x => x.Mastery);

        public async Task AddMasteryToUser(long userId, Mastery mastery, double amount)
        {
            // получаем репутации пользователя
            var userReputations = await _reputationService.GetUserReputation(userId);
            // получаем максимальное мастерство пользователя
            var userMaxMastery = _reputationService.UserMaxMastery(userReputations);
            // добавляем мастерство пользователю убеждаясь что новое мастерство не будет больше максимального
            await _con.GetConnection()
                .ExecuteAsync(@"
                    insert into user_masteries as um (user_id, mastery, amount)
                    values (@userId, @mastery, @amount)
                    on conflict (user_id, mastery) do update
                        set amount = (
                            case when um.amount + @amount <= @userMaxMastery then um.amount + @amount
                                 else @userMaxMastery
                            end),
                            updated_at = now()",
                    new {userId, mastery, amount, userMaxMastery});
        }

        public async Task RemoveMasteryFromUser(long userId, Mastery mastery, double amount) =>
            await _con.GetConnection()
                .ExecuteAsync(@"
                    update user_masteries
                    set amount = amount - @amount,
                        updated_at = now()
                    where user_id = @userId
                      and mastery = @mastery",
                    new {userId, mastery, amount});
    }
}
