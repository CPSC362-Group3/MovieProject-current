using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieTheatre
{
    class Ticket
    {
        public Movie movie { get; set; } // i want this to be a pointer to a movie

        //Note, properties to be deprecated and replaced with a single floating point field
        public uint dollars { get; set; }
        public uint pennies { get; set; }

        public string seat { get; set; }
        public string name { get; set; }
        public Time time { get; set; }

        // Check use of constructor. Current 3 arg constructor to be deprecated
        // New constructor usage to be new Ticket(Movie, Price, Seat, Time, Name)
        public Ticket(Movie pMovie = null, uint pDollars = 0, uint pPennies = 0)
        {
            movie = pMovie;
            dollars = pDollars;
            pennies = pPennies;
        }


        public string ToXml()
        {
            //TO DO
            return "";
        }



        public string ToPrintTemplateStr()
        {
            //TO DO
            return "";
        }
    }
}
