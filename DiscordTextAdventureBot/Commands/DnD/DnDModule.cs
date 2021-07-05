using Discord;
using Discord.Commands;
using DiscordBotLib.DataAccess;
using DiscordBotLib.Helpers;
using DungeonsAndDiscordLib.DataAccess.Repositories;
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

        public DnDModule(IServerRepository serverRepository,
            IDndPlayerRepository dndPlayerRepository)
        {
            _serverRepository = serverRepository;
            _dndPlayerRepository = dndPlayerRepository;
        }

        [Command("start")]
        [Summary("Start the game")]
        public async Task Start()
        {
            var server = await ServerHelper.GetOrAddServer(Context.Guild.Id, _serverRepository);

            Room room = new Room("Starting Room", "The room you start in");
            await Context.Channel.SendEmbedAsync(room.Title, room.Description, ColorHelper.GetColor(server));
        }

        [Command("info")]
        [Summary("Game Info")]
        public async Task Info()
        {
            var builder = new EmbedBuilder();
        }
    }
}
