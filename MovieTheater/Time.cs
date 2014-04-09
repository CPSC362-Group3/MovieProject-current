using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieTheatre
{
    class Time
    {
        public uint hours { get; set; }
        public uint minutes { get; set; }
        public uint seconds { get; set; }

        public Time( uint pHours = 0, uint pMinutes = 0, uint pSeconds = 0)
        {
            hours = pHours;
            minutes = pMinutes;
            seconds = pSeconds;
        }
    }
}
