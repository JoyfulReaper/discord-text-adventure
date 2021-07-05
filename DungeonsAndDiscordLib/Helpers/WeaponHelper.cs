using DungeonsAndDiscordLib.Models.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDiscordLib.Helpers
{
    public static class WeaponHelper
    {
        public static IWeapon GetWeapon(Player player)
        {
            if (player.Level < 10)
            {
                return new Dagger();
            }


            // Default
            return new Dagger();
        }
    }
}
