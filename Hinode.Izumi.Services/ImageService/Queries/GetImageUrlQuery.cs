﻿using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Hinode.Izumi.Data.Enums;
using Hinode.Izumi.Framework.Database;
using MediatR;

namespace Hinode.Izumi.Services.ImageService.Queries
{
    public record GetImageUrlQuery(Image Type) : IRequest<string>;

    public class GetImageUrlHandler : IRequestHandler<GetImageUrlQuery, string>
    {
        private readonly IConnectionManager _con;

        public GetImageUrlHandler(IConnectionManager con)
        {
            _con = con;
        }

        public async Task<string> Handle(GetImageUrlQuery request, CancellationToken cancellationToken)
        {
            return await _con.GetConnection()
                .QueryFirstOrDefaultAsync<string>(@"
                    select url from images
                    where type = @type
                    order by random()
                    limit 1",
                    new {type = request.Type});
        }
    }
}
