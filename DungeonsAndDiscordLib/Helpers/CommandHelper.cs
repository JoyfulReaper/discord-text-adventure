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
            return  "\n\n*Valid Commands:* " + string.Join(", ", player.ValidCommands);
        }

        public static bool SetValidCommands(Player player, List<Command> commands)
        {
            // TODO Check enum options are valid

            player.ValidCommands = commands;
            return true;
        }
    }
}
