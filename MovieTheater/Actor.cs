using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieTheatre
{
    class Actor
    {
        public string description { get; set; }
        public string name { get; set; }

        public Actor(string pDescription = "description", string pName = "name")
        {
            description = pDescription;
            name = pName;
        }

    }
}
