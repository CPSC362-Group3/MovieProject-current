using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieTheatre
{
    class Ticket
    {
        public Movie movie { get; set; } // i want this to be a pointer to a movie
        public uint dollars { get; set; }
        public uint pennies { get; set; }

        public Ticket(Movie pMovie = null, uint pDollars = 0, uint pPennies = 0)
        {
            movie = pMovie;
            dollars = pDollars;
            pennies = pPennies;
        }
    }
}
