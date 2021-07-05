using DungeonsAndDiscordLib.Enums;
using DungeonsAndDiscordLib.Models;
using DungeonsAndDiscordLib.Models.Weapons;
using System;
using System.Collections.Generic;

namespace DungeonsAndDiscordLib
{
    public class Player : DatabaseEntity, ICharacter
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Hp { get; set; } = 100;
        public ulong UserId { get; set; }
        public IWeapon Weapon { get; set; } = null;
        public List<Command> ValidCommands = new List<Command>();

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
