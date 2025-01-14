﻿using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Hinode.Izumi.Data.Enums;
using Hinode.Izumi.Framework.Database;
using MediatR;

namespace Hinode.Izumi.Services.GameServices.BuildingService.Queries
{
    public record CheckBuildingInFamilyQuery(long FamilyId, Building Type) : IRequest<bool>;

    public class CheckBuildingInFamilyHandler : IRequestHandler<CheckBuildingInFamilyQuery, bool>
    {
        private readonly IConnectionManager _con;

        public CheckBuildingInFamilyHandler(IConnectionManager con)
        {
            _con = con;
        }

        public async Task<bool> Handle(CheckBuildingInFamilyQuery request, CancellationToken cancellationToken)
        {
            var (familyId, type) = request;

            return await _con.GetConnection()
                .QueryFirstOrDefaultAsync<bool>(@"
                    select 1 from family_buildings
                    where family_id = @familyId
                      and building_id = (
                          select id from buildings
                          where type = @type
                      )",
                    new {familyId, type});
        }
    }
}
