using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldWideAstronomy
{
    public static partial class WWA
    {
        /* Dates and Delta(AT)s */
        struct Changes
        {
            public int iyear, month;
            public double delat;
        }

        /// <summary>
        /// For a given UTC date, calculate delta(AT) = TAI-UTC.
        /// :------------------------------------------:
        /// :                                          :
        /// :                 IMPORTANT                :
        /// :                                          :
        /// :  A new version of this function must be  :
        /// :  produced whenever a new leap second is  :
        /// :  announced.  There are four items to     :
        /// :  change on each such occasion:           :
        /// :                                          :
        /// :  1) A new line must be added to the set  :
        /// :     of statements that initialize the    :
        /// :     array "changes".                     :
        /// :                                          :
        /// :  2) The constant IYV must be set to the  :
        /// :     current year.                        :
        /// :                                          :
        /// :  3) The "Latest leap second" comment     :
        /// :     below must be set to the new leap    :
        /// :     second date.                         :
        /// :                                          :
        /// :  4) The "This revision" comment, later,  :
        /// :     must be set to the current date.     :
        /// :                                          :
        /// :  Change (2) must also be carried out     :
        /// :  whenever the function is re-issued,     :
        /// :  even if no leap seconds have been       :
        /// :  added.                                  :
        /// :                                          :
        /// :  Latest leap second:  2016 December 31   :
        /// :                                          :
        /// :__________________________________________:
        /// </summary>
        /// 
        /// <remarks>
        /// World Wide Astronomy - WWA
        /// Set of C# algorithms and procedures that implement standard models used in fundamental astronomy.
        /// 
        /// This program is derived from the International Astronomical Union's
        /// SOFA (Standards of Fundamental Astronomy) software collection.
        /// http://www.iausofa.org
        /// 
        /// The WWA code does not itself constitute software provided by and/or endorsed by SOFA.
        /// This version is intended to retain identical functionality to the SOFA library, but
        /// made distinct through different function names (prefixes) and C# language specific
        /// modifications in code.
        /// 
        /// Contributor
        /// Attila Abrudán
        /// 
        /// Please read the ReadMe.1st text file for more information.
        /// </remarks>
        /// Given:
        /// <param name="iy">year (Notes 1 and 2)</param>
        /// <param name="im">month (Note 2)</param>
        /// <param name="id">day (Notes 2 and 3)</param>
        /// <param name="fd">fraction of day (Note 4)</param>
        /// Returned:
        /// <param name="deltat">TAI minus UTC, seconds</param>
        /// <returns>
        /// status (Note 5):
        /// 1 = dubious year (Note 1)
        /// 0 = OK
        /// -1 = bad year
        /// -2 = bad month
        /// -3 = bad day (Note 3)
        /// -4 = bad fraction (Note 4)
        /// -5 = internal error (Note 5)
        /// </returns>
        public static int wwaDat(int iy, int im, int id, double fd, ref double deltat)
        {
            /* Release year for this version of wwaDat */
            //enum { IYV = 2014};
            //const int IYV = 2015;
            //const int IYV = 2016;
            const int IYV = 2017;

            /* Reference dates (MJD) and drift rates (s/day), pre leap seconds */
            double[,] drift = new double[,] {
      { 37300.0, 0.0012960 },
      { 37300.0, 0.0012960 },
      { 37300.0, 0.0012960 },
      { 37665.0, 0.0011232 },
      { 37665.0, 0.0011232 },
      { 38761.0, 0.0012960 },
      { 38761.0, 0.0012960 },
      { 38761.0, 0.0012960 },
      { 38761.0, 0.0012960 },
      { 38761.0, 0.0012960 },
      { 38761.0, 0.0012960 },
      { 38761.0, 0.0012960 },
      { 39126.0, 0.0025920 },
      { 39126.0, 0.0025920 }
            };

            /* Number of Delta(AT) expressions before leap seconds were introduced */
            //enum { NERA1 = (int) (sizeof drift / sizeof (double) / 2) };
            int NERA1 = drift.GetLength(0); // by AA

            /* Dates and Delta(AT)s */
            Changes[] changes = new Changes[] {
      new Changes { iyear = 1960,  month = 1,  delat = 1.4178180 },
      new Changes { iyear = 1961,  month = 1,  delat = 1.4228180 },
      new Changes { iyear = 1961,  month = 8,  delat = 1.3728180 },
      new Changes { iyear = 1962,  month = 1,  delat = 1.8458580 },
      new Changes { iyear = 1963,  month = 11, delat = 1.9458580 },
      new Changes { iyear = 1964,  month = 1,  delat = 3.2401300 },
      new Changes { iyear = 1964,  month = 4,  delat = 3.3401300 },
      new Changes { iyear = 1964,  month = 9,  delat = 3.4401300 },
      new Changes { iyear = 1965,  month = 1,  delat = 3.5401300 },
      new Changes { iyear = 1965,  month = 3,  delat = 3.6401300 },
      new Changes { iyear = 1965,  month = 7,  delat = 3.7401300 },
      new Changes { iyear = 1965,  month = 9,  delat = 3.8401300 },
      new Changes { iyear = 1966,  month = 1,  delat = 4.3131700 },
      new Changes { iyear = 1968,  month = 2,  delat = 4.2131700 },
      new Changes { iyear = 1972,  month = 1,  delat = 10.0       },
      new Changes { iyear = 1972,  month = 7,  delat = 11.0       },
      new Changes { iyear = 1973,  month = 1,  delat = 12.0       },
      new Changes { iyear = 1974,  month = 1,  delat = 13.0       },
      new Changes { iyear = 1975,  month = 1,  delat = 14.0       },
      new Changes { iyear = 1976,  month = 1,  delat = 15.0       },
      new Changes { iyear = 1977,  month = 1,  delat = 16.0       },
      new Changes { iyear = 1978,  month = 1,  delat = 17.0       },
      new Changes { iyear = 1979,  month = 1,  delat = 18.0       },
      new Changes { iyear = 1980,  month = 1,  delat = 19.0       },
      new Changes { iyear = 1981,  month = 7,  delat = 20.0       },
      new Changes { iyear = 1982,  month = 7,  delat = 21.0       },
      new Changes { iyear = 1983,  month = 7,  delat = 22.0       },
      new Changes { iyear = 1985,  month = 7,  delat = 23.0       },
      new Changes { iyear = 1988,  month = 1,  delat = 24.0       },
      new Changes { iyear = 1990,  month = 1,  delat = 25.0       },
      new Changes { iyear = 1991,  month = 1,  delat = 26.0       },
      new Changes { iyear = 1992,  month = 7,  delat = 27.0       },
      new Changes { iyear = 1993,  month = 7,  delat = 28.0       },
      new Changes { iyear = 1994,  month = 7,  delat = 29.0       },
      new Changes { iyear = 1996,  month = 1,  delat = 30.0       },
      new Changes { iyear = 1997,  month = 7,  delat = 31.0       },
      new Changes { iyear = 1999,  month = 1,  delat = 32.0       },
      new Changes { iyear = 2006,  month = 1,  delat = 33.0       },
      new Changes { iyear = 2009,  month = 1,  delat = 34.0       },
      new Changes { iyear = 2012,  month = 7,  delat = 35.0       },
      new Changes { iyear = 2015,  month = 7,  delat = 36.0       },
      new Changes { iyear = 2017,  month = 1,  delat = 37.0       }
   };

            /* Number of Delta(AT) changes */
            //const int NDAT = sizeof changes / sizeof changes[0];
            //enum { NDAT = (int) (sizeof changes / sizeof changes[0]) }; // 2015 Február 27
            int NDAT = changes.GetLength(0); // by AA

            /* Miscellaneous local variables */
            int j, i, m;
            double da, djm0 = 0, djm = 0;


            /* Initialize the result to zero. */
            deltat = da = 0.0;

            /* If invalid fraction of a day, set error status and give up. */
            if (fd < 0.0 || fd > 1.0) return -4;

            /* Convert the date into an MJD. */
            j = wwaCal2jd(iy, im, id, ref djm0, ref djm);

            /* If invalid year, month, or day, give up. */
            if (j < 0) return j;

            /* If pre-UTC year, set warning status and give up. */
            if (iy < changes[0].iyear) return 1;

            /* If suspiciously late year, set warning status but proceed. */
            if (iy > IYV + 5) j = 1;

            /* Combine year and month to form a date-ordered integer... */
            m = 12 * iy + im;

            /* ...and use it to find the preceding table entry. */
            for (i = NDAT - 1; i >= 0; i--)
            {
                if (m >= (12 * changes[i].iyear + changes[i].month)) break;
            }

            /* Prevent underflow warnings. */
            if (i < 0) return -5;

            /* Get the Delta(AT). */
            da = changes[i].delat;

            /* If pre-1972, adjust for drift. */
            if (i < NERA1) da += (djm + fd - drift[i, 0]) * drift[i, 1];

            /* Return the Delta(AT) value. */
            deltat = da;

            /* Return the status. */
            return j;
        }
    }
}
