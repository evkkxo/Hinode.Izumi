﻿using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Hinode.Izumi.Data.Enums;
using Hinode.Izumi.Framework.Database;
using MediatR;

namespace Hinode.Izumi.Services.GameServices.CollectionService.Commands
{
    public record AddCollectionToUserCommand(long UserId, CollectionCategory Category, long ItemId) : IRequest;

    public class AddCollectionToUserHandler : IRequestHandler<AddCollectionToUserCommand>
    {
        private readonly IConnectionManager _con;

        public AddCollectionToUserHandler(IConnectionManager con)
        {
            _con = con;
        }

        public async Task<Unit> Handle(AddCollectionToUserCommand request, CancellationToken cancellationToken)
        {
            var (userId, category, itemId) = request;

            await _con.GetConnection()
                .ExecuteAsync(@"
                    insert into user_collections(user_id, category, item_id)
                    values (@userId, @category, @itemId)
                    on conflict (user_id, category, item_id) do nothing",
                    new {userId, category, itemId});

            return new Unit();
        }
    }
}
