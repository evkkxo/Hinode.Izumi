﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using MediatR;

namespace Hinode.Izumi.Services.DiscordServices.DiscordEmbedService.Commands
{
    public record SendEmbedToUserCommand(
            SocketUser SocketUser,
            EmbedBuilder EmbedBuilder,
            string Message = "")
        : IRequest<IUserMessage>;

    public class SendEmbedToUserHandler : IRequestHandler<SendEmbedToUserCommand, IUserMessage>
    {
        private readonly IMediator _mediator;

        public SendEmbedToUserHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IUserMessage> Handle(SendEmbedToUserCommand request, CancellationToken ct)
        {
            var (socketUser, embedBuilder, message) = request;

            if (socketUser is null) return null;

            var embed = await _mediator.Send(new BuildEmbedCommand(embedBuilder, (long) socketUser.Id), ct);

            try
            {
                return await socketUser.SendMessageAsync(message, false, embed);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }
    }
}
