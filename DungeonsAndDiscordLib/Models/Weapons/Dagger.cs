using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDiscordLib.Models.Weapons
{
    public class Dagger : IWeapon
    {
        private static Random _random = new();

        public string Name { get; set; } = "Dagger";
        public int BaseDamage { get; set; } = 5;
        public int DamageRange { get; set; } = 5;

        public int GetTotalDamage()
        {
            int randomDamage = _random.Next(0, DamageRange + 1);
            return randomDamage + BaseDamage;
        }
    }
}
