﻿using System;
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
        public int HP { get; set; }
    }
}
