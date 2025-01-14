﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Hinode.Izumi.Data.Enums;
using Hinode.Izumi.Framework.Database;
using Hinode.Izumi.Services.GameServices.GatheringService.Records;
using MediatR;

namespace Hinode.Izumi.Services.GameServices.GatheringService.Queries
{
    public record GetGatheringsInLocationQuery(
            Location Location,
            Event CurrentEvent = Event.None)
        : IRequest<GatheringRecord[]>;

    public class GetGatheringsInLocationHandler : IRequestHandler<GetGatheringsInLocationQuery, GatheringRecord[]>
    {
        private readonly IConnectionManager _con;

        public GetGatheringsInLocationHandler(IConnectionManager con)
        {
            _con = con;
        }

        public async Task<GatheringRecord[]> Handle(GetGatheringsInLocationQuery request,
            CancellationToken cancellationToken)
        {
            var (location, currentEvent) = request;
            return (await _con.GetConnection()
                    .QueryAsync<GatheringRecord>(@"
                        select * from gatherings
                        where location = @location
                          and (event = @currentEvent
                           or event = @eventNone)",
                        new {location, currentEvent, eventNone = Event.None}))
                .ToArray();
        }
    }
}
