using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieTheatre
{
    class Seat
    {
        public bool occupied { get; set; }

        public Seat(bool pOccupied = false)
        {
            occupied = pOccupied;
        }
    }
}
