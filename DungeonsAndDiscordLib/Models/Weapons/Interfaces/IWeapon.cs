using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDiscordLib.Models.Weapons
{
    public interface IWeapon
    {
        public String Name { get; set; }
        public int BaseDamage { get; set; }
        public int DamageRange { get; set; }

        public int GetTotalDamage();
    }
}
