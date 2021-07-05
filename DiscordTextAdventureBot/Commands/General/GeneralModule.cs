/*
MIT License

Copyright(c) 2021 Kyle Givler
https://github.com/JoyfulReaper

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBotLib.DataAccess;
using DiscordBotLib.Helpers;
using DiscordBotLib.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    // So it turns out that the bot needs the Presence and Server member intent in order for
    // All of the members of a channel to be "in scope"
    [Name("General")]
    public class GeneralModule : ModuleBase<SocketCommandContext>
    {
        private readonly ILogger<GeneralModule> _logger;
        private readonly DiscordSocketClient _client;
        private readonly BannerImageService _bannerImageService;
        private readonly IServerService _servers;
        private readonly IUserTimeZonesRepository _userTimeZones;
        private readonly ISettings _settings;

        public GeneralModule(ILogger<GeneralModule> logger,
            DiscordSocketClient client,
            BannerImageService bannerImageService,
            IServerService servers,
            IUserTimeZonesRepository userTimeZones,
            ISettings settings)
        {
            _logger = logger;
            _client = client;
            _bannerImageService = bannerImageService;
            _servers = servers;
            _userTimeZones = userTimeZones;
            _settings = settings;
        }

        [Command("invite")]
        [Summary("invite the bot to your server!")]
        public async Task Invite()
        {
            await Context.Channel.TriggerTypingAsync();

            _logger.LogInformation("{username}#{discriminator} executed uptime: on {server}/{channel}",
                Context.User.Username, Context.User.Discriminator, Context.Guild?.Name ?? "DM", Context.Channel.Name);

            await Context.Channel.SendEmbedAsync("Invite Link", $"Follow the link to invite DiscordBot!\n{_settings.InviteLink}",
                ColorHelper.GetColor(await _servers.GetServer(Context.Guild)), ImageLookupUtility.GetImageUrl("INVITE_IMAGES"));

            //await ReplyAsync(_settings.InviteLink);
        }

        [Command("uptime")]
        [Alias("proc", "memory")]
        [Summary("Get bot uptime and memory usage")]
        public async Task ProcInfo()
        {
            await Context.Channel.TriggerTypingAsync();

            _logger.LogInformation("{username}#{discriminator} executed uptime: on {server}/{channel}",
                Context.User.Username, Context.User.Discriminator, Context.Guild?.Name ?? "DM", Context.Channel.Name);

            var process = Process.GetCurrentProcess();
            var memoryMb = Math.Round((double)process.PrivateMemorySize64 / (1e+6), 2);
            var startTime = process.StartTime;

            var upTime = DateTime.Now - startTime;

            await ReplyAsync($"Uptime: `{upTime}`\nMemory usage: `{memoryMb} MB`");
            /*
             * Not producing correct results :(
            if(LavaLinkHelper.isLavaLinkRunning())
            {
                var lavaMb = Math.Round((double)LavaLinkHelper.LavaLink.PrivateMemorySize64 / (1e+6), 2);
                await ReplyAsync($"Lavalink Memory: {lavaMb} MB");
            }
            */
            process.Dispose();
        }

        [Command("servers")]
        [Summary("Report the number of servers the bot it in")]
        public async Task Servers()
        {
            await Context.Channel.TriggerTypingAsync();

            _logger.LogInformation("{username}#{discriminator} executed servers: on {server}/{channel}",
                Context.User.Username, Context.User.Discriminator, Context.Guild?.Name ?? "DM", Context.Channel.Name);

            await ReplyAsync($"I am in {Context.Client.Guilds.Count} servers!");
        }

        [Command ("about")]
        [Summary("Information about the bot itself")]
        public async Task About()
        {
            await Context.Channel.TriggerTypingAsync();

            _logger.LogInformation("{username}#{discriminator} executed about on {server}/{channel}",
                Context.User.Username, Context.User.Discriminator, Context.Guild?.Name ?? "DM", Context.Channel.Name);

            var server = await _servers.GetServer(Context.Guild);
            var prefix = server?.Prefix;
            if(prefix == null)
            {
                prefix = string.Empty;
            }

            var builder = new EmbedBuilder()
                .WithThumbnailUrl(_client.CurrentUser.GetAvatarUrl() ?? _client.CurrentUser.GetDefaultAvatarUrl())
                .WithDescription("Dungeons And Discord\nMIT License Copyright(c) 2021 JoyfulReaper and KT-Kieser\nhttps://github.com/KT-Kieser/discord-text-adventure\n\n" +
                $"See `{prefix}invite` for the link to invite DiscordBot to your server!")
                .WithColor(ColorHelper.GetColor(server))
                .WithCurrentTimestamp();

            var embed = builder.Build();
            await ReplyAsync(null, false, embed);
        }

        [Command("owner")]
        [Summary("Retreive the server owner")]
        public async Task Owner()
        {
            await Context.Channel.TriggerTypingAsync();

            _logger.LogInformation("{username}#{discriminator} executed owner on {server}/{channel}",
                Context.User.Username, Context.User.Discriminator, Context.Guild?.Name ?? "DM", Context.Channel.Name);

            var server = await _servers.GetServer(Context.Guild);
            if(Context.Guild == null)
            {
                await Context.Channel.SendEmbedAsync("Dungeons and Discord", "DiscordBot was written by JoyfulReaper\nhttps://github.com/JoyfulReaper/DiscordBot", 
                    ColorHelper.RandomColor(), _client.CurrentUser.GetAvatarUrl() ?? _client.CurrentUser.GetDefaultAvatarUrl());
                return;
            }

            var builder = new EmbedBuilder()
                .WithThumbnailUrl(Context?.Guild?.Owner.GetAvatarUrl() ?? _client.CurrentUser.GetDefaultAvatarUrl())
                .WithDescription($"{Context?.Guild?.Owner.Username} is the owner of {Context.Guild.Name}")
                .WithColor(ColorHelper.GetColor(server))
                .WithCurrentTimestamp();

            var embed = builder.Build();
            await ReplyAsync(null, false, embed);
        }

        [Command("ping")]
        [Alias("latency")]
        [Summary ("Latency to server!")]
        public async Task Ping()
        {
            await Context.Channel.TriggerTypingAsync();

            _logger.LogInformation("{username}#{discriminator} executed ping on {server}/{channel}",
                Context.User.Username, Context.User.Discriminator, Context.Guild?.Name ?? "DM", Context.Channel.Name);

            var server = await _servers.GetServer(Context.Guild);

            var builder = new EmbedBuilder();
            builder
                .WithThumbnailUrl(_client.CurrentUser.GetAvatarUrl() ?? _client.CurrentUser.GetDefaultAvatarUrl())
                .WithTitle("Ping Results")
                .WithDescription("Pong!")
                .AddField("Round-trip latency to the WebSocket server (ms):", _client.Latency, false)
                .WithColor(ColorHelper.GetColor(server))
                .WithCurrentTimestamp();

            await ReplyAsync(null, false, builder.Build());
        }
    }
}
