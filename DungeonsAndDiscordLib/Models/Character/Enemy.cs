using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDiscordLib
{
    class Enemy : ICharacter
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int HP { get; set; }

        public Enemy(string name, int level = 1, int hp = 10)
        {
            Name = name;
            Level = level;
            HP = hp;
        }
    }
}
