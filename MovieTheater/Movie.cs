using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieTheatre
{
    class Movie
    {
        public enum Genre
        {
            ACTION = 0,
            ADVENTURE = 1,
            ANIMATION = 2,
            BIOGRAPHY = 3,
            COMEDY = 4,
            CRIME = 5,
            DOCUMENTARY = 6,
            DRAMA = 7,
            FAMILY = 8,
            FANTASY = 9,
            HISTORY = 10,
            HORROR = 11,
            MUSICAL = 12,
            MYSTERY = 13,
            ROMANCE = 14,
            SCI_FI = 15,
            SPORTS = 16,
            THRILLER = 17,
            WAR = 18,
            WESTERN = 19,
            NONE = 20
        };

        public enum Rating
        {
            G       = 0,
            PG      = 1,
            PG_13   = 2,
            R       = 3,
            NC_17   = 4,
            NONE    = 5
        };

        public Time length;
        private List<Genre> genres;
        private List<Actor> actors;
        public string description { get; set; }
        public string summary { get; set; }
        public string title { get; set; }
        public string director { get; set; }
        public string studio { get; set; } // Warner Bro's etc...
        public Rating rating { get; set; }
        public Date release { get; set; }

        public Movie(string pDesciption = "desciption", string pSummary = "summary", string pTitle = "title", string pDirector = "director", string pStudio = "studio")
        {
            description = pDesciption;
            summary = pSummary;
            title = pTitle;
            director = pDirector;
            studio = pStudio;
            genres = null;
            actors = null;
            length = new Time();
            release = new Date();
            rating = Rating.NONE;
        }

        // if list does not contain the genre, insert the genre into the list
        public void attachGenre(Genre pGenre)
        {
            if (!genres.Contains(pGenre))
            {
                genres.Add(pGenre);
            }
        }

        // detach the first occurance of genre in the list
        public void detachGenre(Genre pGenre)
        {
            genres.Remove(pGenre);
        }

        // detach the genre at i index in the list
        public void detachGenreAt(int i)
        {
            genres.RemoveAt(i);
        }

        // if list does not contain the actor, insert the actor into the list
        public void attachActor( Actor pActor ){
            if (!actors.Contains(pActor))
            {
                actors.Add(pActor);
            }
        }

        // detach the first occurance of actor in the list
        public void detachActor(Actor pActor)
        {
            actors.Remove(pActor);
        }

        // detach the actor at i index in the list
        public void detachActorAt( int i )
        {
            actors.RemoveAt(i);
        }

    }
}
