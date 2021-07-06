using DungeonsAndDiscordLib.DataAccess.Repositories;
using DungeonsAndDiscordLib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDiscordLib.Helpers
{
    public static class CommandHelper
    {
        public static string GetValidCommandString(Player player)
        {
            return "\n\n*Valid Commands:* " + player.ValidCommands.ToString();
        }

        public static void SetValidCommands(Player player, Command commands, IDndPlayerRepository dndPlayerRepository)
        {
            if(!commands.HasFlag(Command.Start))
            {
                commands |= Command.Start;
            }

            if(player.Level > 1 && !commands.HasFlag(Command.Info))
            {
                commands |= Command.Info;
            }

            player.ValidCommands = commands;
            dndPlayerRepository.EditAsync(player);
        }
    }
}
