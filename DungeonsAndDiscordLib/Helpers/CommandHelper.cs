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
    }
}
