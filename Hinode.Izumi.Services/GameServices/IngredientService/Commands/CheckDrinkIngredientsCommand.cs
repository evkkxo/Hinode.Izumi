﻿using System.Threading;
using System.Threading.Tasks;
using Hinode.Izumi.Services.GameServices.IngredientService.Queries;
using MediatR;

namespace Hinode.Izumi.Services.GameServices.IngredientService.Commands
{
    public record CheckDrinkIngredientsCommand(long UserId, long DrinkId, long Amount = 1) : IRequest;

    public class CheckDrinkIngredientsHandler : IRequestHandler<CheckDrinkIngredientsCommand>
    {
        private readonly IMediator _mediator;

        public CheckDrinkIngredientsHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Unit> Handle(CheckDrinkIngredientsCommand request, CancellationToken cancellationToken)
        {
            var (userId, drinkId, amount) = request;
            var ingredients = await _mediator.Send(new GetDrinkIngredientsQuery(drinkId), cancellationToken);

            foreach (var ingredient in ingredients)
                await _mediator.Send(new CheckIngredientAmountCommand(
                        userId, ingredient.Category, ingredient.IngredientId, ingredient.Amount, amount),
                    cancellationToken);

            return new Unit();
        }
    }
}
