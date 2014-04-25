using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MovieTheatre
{
    class Ticket
    {
        private string time1;
        private string date1;
        private string movieTitle;
        private int ticketTotal;
        private List<string> seatsSelected;

        public string movie { get; set; } // i want this to be a pointer to a movie

        //Note, properties to be deprecated and replaced with a single floating point field
        public string ticketType { get; set; }
        public string seat { get; set; }
        public string name { get; set; }
        public string time { get; set; }
        public string date { get; set; }

        // Check use of constructor. Current 3 arg constructor to be deprecated
        // New constructor usage to be new Ticket(Movie, Price, Seat, Time, Name)
        public Ticket(string pMovie = null, string ticketType = "", string seat = "", string time = "", string name = "", string date = "")
        {
            movie = pMovie;
            this.ticketType = ticketType;
            this.seat = seat;
            this.name = name;
            this.time = time;
            this.date = date;
        }


        public void ToXml(XmlDocument xmlRoot, XmlElement proot)
        {
            XmlElement Ticket = xmlRoot.CreateElement("Ticket");
            XmlElement Movie = xmlRoot.CreateElement("Movie");
            XmlElement Name = xmlRoot.CreateElement("Name");
            XmlElement Seat = xmlRoot.CreateElement("Seat");
            XmlElement Date = xmlRoot.CreateElement("Date");
            XmlElement Type = xmlRoot.CreateElement("Type");

            proot.AppendChild(Ticket);
            //xmlRoot.AppendChild(Ticket);
            Ticket.AppendChild(Movie);
            Ticket.AppendChild(Name);
            Ticket.AppendChild(Seat);
            Ticket.AppendChild(Date);
            Ticket.AppendChild(Type);

            Movie.InnerText = movieTitle;
            Name.InnerText = name;
            Seat.InnerText = seat;
            Date.InnerText = date;
            Type.InnerText = ticketType;


            //string XmlString = "<Ticket>";
            //XmlString += "<Movie>" + movieTitle + "</Movie>";
            //XmlString += "<Name>" + name + "</Name>";
            //XmlString += "<Seat>" + seat + "</Seat>";
            //XmlString += "<Date>" + date + "</Date>";
            //XmlString += "<Type>" + ticketType + "</Type>";
            //XmlString += "</Ticket>";
            //return XmlString;
        }



        public string ToPrintTemplateStr()
        {
            //TO DO
            return "";
        }
    }
}
