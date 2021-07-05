using Discord.WebSocket;
using DungeonsAndDiscordLib.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDiscordLib.Helpers
{
    public static class PlayerHelper
    {
        public static async Task<Player> GetOrAddPlayer(DiscordBotLib.Models.User user, 
            IDndPlayerRepository dndPlayerRepository)
        {
            var playerDb = await dndPlayerRepository.GetPlayerById(user.Id);
            if (playerDb == null)
            {
                playerDb = new Player(user.UserName, user.Id);
                await dndPlayerRepository.AddAsync(playerDb);
            }
            playerDb.Name = user.UserName;

            return playerDb;
        }
    }
}
