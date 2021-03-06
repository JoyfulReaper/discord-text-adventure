using DungeonsAndDiscordLib.Models.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDiscordLib
{
    interface ICharacter
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Hp { get; set; }
        public IWeapon Weapon { get; set; }
    }
}
