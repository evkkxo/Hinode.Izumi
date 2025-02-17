﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Hinode.Izumi.Framework.Autofac;
using Hinode.Izumi.Framework.Database;
using Hinode.Izumi.Services.WebServices.GatheringWebService.Models;
using Microsoft.Extensions.Caching.Memory;
using CacheExtensions = Hinode.Izumi.Services.Extensions.CacheExtensions;

namespace Hinode.Izumi.Services.WebServices.GatheringWebService.Impl
{
    [InjectableService]
    public class GatheringWebService : IGatheringWebService
    {
        private readonly IConnectionManager _con;
        private readonly IMemoryCache _cache;

        public GatheringWebService(IConnectionManager con, IMemoryCache cache)
        {
            _con = con;
            _cache = cache;
        }

        public async Task<IEnumerable<GatheringWebModel>> GetAllGathering() =>
            await _con.GetConnection()
                .QueryAsync<GatheringWebModel>(@"
                    select * from gatherings
                    order by id");

        public async Task<GatheringWebModel> Get(long id) =>
            await _con.GetConnection()
                .QueryFirstOrDefaultAsync<GatheringWebModel>(@"
                    select * from gatherings
                    where id = @id",
                    new {id});

        public async Task<GatheringWebModel> Upsert(GatheringWebModel model)
        {
            // сбрасываем кэш
            _cache.Remove(string.Format(CacheExtensions.GatheringKey, model.Id));

            var query = model.Id == 0
                ? @"
                    insert into gatherings(name, price, location, event)
                    values (@name, @price, @location, @event)
                    returning *"
                : @"
                    update gatherings
                    set name = @name,
                        price = @price,
                        location = @location,
                        event = @event,
                        updated_at = now()
                    where id = @id
                    returning *";

            // обновляем базу
            return await _con.GetConnection()
                .QueryFirstOrDefaultAsync<GatheringWebModel>(query,
                    new
                    {
                        id = model.Id,
                        name = model.Name,
                        price = model.Price,
                        location = model.Location,
                        @event = model.Event
                    });
        }

        public async Task Remove(long id) =>
            await _con.GetConnection()
                .ExecuteAsync(@"
                    delete from gatherings
                    where id = @id",
                    new {id});
    }
}
