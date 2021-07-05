using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDiscordLib.Enums
{
    // Needs to be powers of 2
    [Flags]
    public enum Command
    {
        Start = 1,
        Info = 2,
        Proceed = 4,
        Attack = 8
    }
}
