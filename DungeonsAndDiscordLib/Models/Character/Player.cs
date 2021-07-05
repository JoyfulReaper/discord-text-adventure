using DungeonsAndDiscordLib.Models;
using System;

namespace DungeonsAndDiscordLib
{
    public class Player : ICharacter
    {
        public string Name { get; set; }
        public int Level { get; set; }
    }
}
