using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBotLib.DataAccess;
using DiscordBotLib.Helpers;
using DungeonsAndDiscordLib;
using DungeonsAndDiscordLib.DataAccess.Repositories;
using DungeonsAndDiscordLib.Helpers;
using DungeonsAndDiscordLib.Models.Rooms;
using DungeonsAndDiscordLib.Models.Weapons;
using DungeonsAndDiscordLib.Enums;
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

            CommandHelper.SetValidCommands(player, Command.Info | Command.Proceed, _dndPlayerRepository);

            if (player.Level == 1)
            {
                IRoom room = new Room("Starting Room", $"Welcome {Context.User.Mention} to `Dungeons and Discord!`\n\n" +
                    $"You find yourself at the entrance to a dark and lonely dungeon. You happen to see a `{player.Weapon.Name}` at your feet. You pick it up.");

                var commands = CommandHelper.GetValidCommandString(player);
                await Context.Channel.SendEmbedAsync(room.Title, room.Description + commands, ColorHelper.GetColor(server));
            }
            else
            {
                await ReplyAsync("Wecome back!");
                // Generate room the player starts in and display it
                await ReplyAsync("TODO: Actually do something...");
            }
            
        }

        [Command("Proceed")]
        [Summary("Proceed to the next room")]
        public async Task Proceed()
        {
            var user = await UserHelper.GetOrAddUser(Context.User, _userRepository);
            var server = await ServerHelper.GetOrAddServer(Context.Guild.Id, _serverRepository);
            var player = await PlayerHelper.GetOrAddPlayer(user, _dndPlayerRepository);

            if (!player.ValidCommands.HasFlag(Command.Proceed))
            {
                await ReplyAsync("You can't use this command right now");
                return;
            }

            await ReplyAsync("If this command was written you would have entered the next room...");
        }

        [Command("info")]
        [Summary("Game Info")]
        public async Task Info()
        {
            var user = await UserHelper.GetOrAddUser(Context.User, _userRepository);
            var server = await ServerHelper.GetOrAddServer(Context.Guild.Id, _serverRepository);
            var player = await PlayerHelper.GetOrAddPlayer(user, _dndPlayerRepository);

            if(!player.ValidCommands.HasFlag(Command.Info))
            {
                await ReplyAsync($"Please use the `{server?.Prefix ?? string.Empty}start` command to start the game");
                return;
            }

            var builder = new EmbedBuilder();
            builder.Title = "Player Information";
            builder.AddField("Name", player.Name, true);
            builder.AddField("Level", player.Level, true);
            builder.AddField("HP", player.Hp, true);
            builder.AddField("Weapon", player.Weapon.Name, true);
            builder.AddField("Weapon Base Damage", player.Weapon.BaseDamage);
            builder.AddField("Weapon Damage Range", player.Weapon.DamageRange);
            builder.Color = ColorHelper.GetColor(server);
            builder.WithThumbnailUrl(Context?.User?.GetAvatarUrl() ?? _client.CurrentUser.GetDefaultAvatarUrl());

            await ReplyAsync(null, false, builder.Build());
        }
    }
}
