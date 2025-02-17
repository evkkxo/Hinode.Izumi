﻿using System.Threading;
using System.Threading.Tasks;
using Hinode.Izumi.Data.Enums.PropertyEnums;
using Hinode.Izumi.Services.Extensions;
using Hinode.Izumi.Services.GameServices.PropertyService.Queries;
using MediatR;

namespace Hinode.Izumi.Services.GameServices.CalculationService.Queries
{
    public record GetTradingSeedDiscountQuery(long UserTradingMastery) : IRequest<long>;

    public class GetTradingSeedDiscountHandler : IRequestHandler<GetTradingSeedDiscountQuery, long>
    {
        private readonly IMediator _mediator;

        public GetTradingSeedDiscountHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<long> Handle(GetTradingSeedDiscountQuery request, CancellationToken cancellationToken)
        {
            return (await _mediator.Send(
                    new GetMasteryPropertiesQuery(MasteryProperty.TradingMasterySeedDiscount), cancellationToken))
                .MasteryMaxValue(request.UserTradingMastery);
        }
    }
}
