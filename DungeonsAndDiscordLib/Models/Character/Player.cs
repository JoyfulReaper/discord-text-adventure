using DungeonsAndDiscordLib.Models;
using System;

namespace DungeonsAndDiscordLib
{
    public class Player : ICharacter
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int HP { get; set; } = 100;

        public Player(string name, int level = 1, int hp = 100)
        {
            Name = name;
            Level = level;
            HP = hp;
        }
    }
}
