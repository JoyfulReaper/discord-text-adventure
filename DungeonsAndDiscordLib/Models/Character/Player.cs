using DungeonsAndDiscordLib.Models;
using System;

namespace DungeonsAndDiscordLib
{
    public class Player : DatabaseEntity, ICharacter
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Hp { get; set; } = 100;
        public ulong UserId { get; set; }

        public Player()
        {
            // For Dapper
        }

        public Player(string name, ulong userId, int level = 1, int hp = 100)
        {
            Name = name;
            Level = level;
            Hp = hp;
            UserId = userId;
        }
    }
}
