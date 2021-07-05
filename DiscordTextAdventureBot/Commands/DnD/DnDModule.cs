using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBotLib.DataAccess;
using DiscordBotLib.Helpers;
using DungeonsAndDiscordLib.DataAccess.Repositories;
using DungeonsAndDiscordLib.Helpers;
using DungeonsAndDiscordLib.Models.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordTextAdventureBot.Commands.DnD
{
    public class DnDModule : ModuleBase<SocketCommandContext>
    {
        private readonly IServerRepository _serverRepository;
        private readonly IDndPlayerRepository _dndPlayerRepository;
        private readonly IUserRepository _userRepository;
        private readonly DiscordSocketClient _client;

        public DnDModule(IServerRepository serverRepository,
            IDndPlayerRepository dndPlayerRepository,
            IUserRepository userRepository,
            DiscordSocketClient client)
        {
            _serverRepository = serverRepository;
            _dndPlayerRepository = dndPlayerRepository;
            _userRepository = userRepository;
            _client = client;
        }

        [Command("start")]
        [Summary("Start the game")]
        public async Task Start()
        {
            var user = await UserHelper.GetOrAddUser(Context.User, _userRepository);
            var server = await ServerHelper.GetOrAddServer(Context.Guild.Id, _serverRepository);
            var player = await PlayerHelper.GetOrAddPlayer(user, _dndPlayerRepository);

            Room room = new Room("Starting Room", "The room you start in");
            await Context.Channel.SendEmbedAsync(room.Title, room.Description, ColorHelper.GetColor(server));
        }

        [Command("info")]
        [Summary("Game Info")]
        public async Task Info()
        {
            var user = await UserHelper.GetOrAddUser(Context.User, _userRepository);
            var server = await ServerHelper.GetOrAddServer(Context.Guild.Id, _serverRepository);
            var player = await PlayerHelper.GetOrAddPlayer(user, _dndPlayerRepository);


            var builder = new EmbedBuilder();
            builder.Title = "Player Information";
            builder.AddField("Name", player.Name, true);
            builder.AddField("Level", player.Level, true);
            builder.AddField("HP", player.Hp, true);
            builder.Color = ColorHelper.GetColor(server);
            builder.WithThumbnailUrl(Context?.User?.GetAvatarUrl() ?? _client.CurrentUser.GetDefaultAvatarUrl());

            await ReplyAsync(null, false, builder.Build());
        }
    }
}
