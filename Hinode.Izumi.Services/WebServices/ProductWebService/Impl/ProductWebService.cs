﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Hinode.Izumi.Framework.Autofac;
using Hinode.Izumi.Framework.Database;
using Hinode.Izumi.Services.WebServices.ProductWebService.Models;
using Microsoft.Extensions.Caching.Memory;
using CacheExtensions = Hinode.Izumi.Services.Extensions.CacheExtensions;

namespace Hinode.Izumi.Services.WebServices.ProductWebService.Impl
{
    [InjectableService]
    public class ProductWebService : IProductWebService
    {
        private readonly IConnectionManager _con;
        private readonly IMemoryCache _cache;

        public ProductWebService(IConnectionManager con, IMemoryCache cache)
        {
            _con = con;
            _cache = cache;
        }

        public async Task<IEnumerable<ProductWebModel>> GetAllProducts() =>
            await _con.GetConnection()
                .QueryAsync<ProductWebModel>(@"
                    select * from products
                    order by id");

        public async Task<ProductWebModel> Get(long id) =>
            await _con.GetConnection()
                .QueryFirstOrDefaultAsync<ProductWebModel>(@"
                    select * from products
                    where id = @id",
                    new {id});

        public async Task<ProductWebModel> Upsert(ProductWebModel model)
        {
            // сбрасываем кэш
            _cache.Remove(string.Format(CacheExtensions.ProductKey, model.Id));

            var query = model.Id == 0
                ? @"
                    insert into products(name, price)
                    values (@name, @price)
                    returning *"
                : @"
                    update products
                    set name = @name,
                        price = @price,
                        updated_at = now()
                    where id = @id
                    returning *";

            // обновляем базу
            return await _con.GetConnection()
                .QueryFirstOrDefaultAsync<ProductWebModel>(query,
                    new
                    {
                        id = model.Id,
                        name = model.Name,
                        price = model.Price
                    });
        }

        public async Task Remove(long id) =>
            await _con.GetConnection()
                .ExecuteAsync(@"
                    delete from products
                    where id = @id",
                    new {id});
    }
}
