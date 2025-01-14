﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Hinode.Izumi.Data.Enums.MessageEnums;
using Hinode.Izumi.Framework.Database;
using Hinode.Izumi.Services.GameServices.UserService.Records;
using MediatR;

namespace Hinode.Izumi.Services.GameServices.UserService.Queries
{
    public record GetUserByNamePatternQuery(string NamePattern) : IRequest<UserRecord>;

    public class GetUserByNamePatternHandler : IRequestHandler<GetUserByNamePatternQuery, UserRecord>
    {
        private readonly IConnectionManager _con;

        public GetUserByNamePatternHandler(IConnectionManager con)
        {
            _con = con;
        }

        public async Task<UserRecord> Handle(GetUserByNamePatternQuery request, CancellationToken cancellationToken)
        {
            var user = await _con.GetConnection()
                .QueryFirstOrDefaultAsync<UserRecord>(@"
                    select * from users
                    where name ilike '%'||@namePattern||'%'",
                    new {namePattern = request.NamePattern});

            if (user is null)
                await Task.FromException(new Exception(IzumiNullableMessage.UserWithName.Parse()));

            return user;
        }
    }
}
