using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieTheatre
{
    class Date
    {
	    public enum Month 
        {
            NONE        =  0,
		    JANUARY		=  1,
		    FEBUARY		=  2,
		    MARCH		=  3,
		    APRIL		=  4,
		    MAY			=  5,
		    JUNE		=  6,
		    JULY		=  7,
	    	AUGUST		=  8,
		    SEPTEMBER	=  9,
		    OCTOBER		= 10,
		    NOVEMBER	= 11,
		    DECEMBER	= 12
	    };

        public Month month { get; set; }
        public uint day { get; set; }
        public uint year { get; set; }

        public Date( Month pMonth = Month.NONE, uint pYear = 9999, uint pDay = 0 ){
            month = pMonth;
            year = pYear;
            day = pDay;
        }

    }
}
