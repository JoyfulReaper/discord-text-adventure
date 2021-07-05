using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDiscordLib.Models.Rooms
{
    public class Room : IRoom
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public Room(string title, string desc)
        {
            Title = title;
            Description = desc;
        }
    }
}
