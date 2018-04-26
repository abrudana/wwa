using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldWideAstronomy
{
    public static partial class WWA
    {
        /// <summary>
        /// Encode date and time fields into 2-part Julian Date (or in the case
        /// of UTC a quasi-JD form that includes special provision for leap
        /// seconds).
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
        /// <param name="scale">time scale ID (Note 1)</param>
        /// <param name="iy">year, month, day in Gregorian calendar (Note 2)</param>
        /// <param name="im">year, month, day in Gregorian calendar (Note 2)</param>
        /// <param name="id">year, month, day in Gregorian calendar (Note 2)</param>
        /// <param name="ihr">hour, minute</param>
        /// <param name="imn">hour, minute</param>
        /// <param name="sec">seconds</param>
        /// Returned:
        /// <param name="d1">2-part Julian Date (Notes 3,4)</param>
        /// <param name="d2">2-part Julian Date (Notes 3,4)</param>
        /// <returns>status: +3 = both of next two
        /// +2 = time is after end of day (Note 5)
        /// +1 = dubious year (Note 6)
        /// 0 = OK
        /// -1 = bad year
        /// -2 = bad month
        /// -3 = bad day
        /// -4 = bad hour
        /// -5 = bad minute
        /// -6 = bad second (<0)
        /// </returns>
        public static int wwaDtf2d(string scale, int iy, int im, int id, int ihr, int imn, double sec, ref double d1, ref double d2)
        {
            int js, iy2 = 0, im2 = 0, id2 = 0;
            double dj = 0, w = 0, day, seclim, dat0 = 0, dat12 = 0, dat24 = 0, dleap, time;


            /* Today's Julian Day Number. */
            js = wwaCal2jd(iy, im, id, ref dj, ref w);
            if (js == 0 ? false : true) return js;
            dj += w;

            /* Day length and final minute length in seconds (provisional). */
            day = DAYSEC;
            seclim = 60.0;

            /* Deal with the UTC leap second case. */
            //if (!strcmp(scale, "UTC"))
            if (scale.ToString() == "UTC")
            {

                /* TAI-UTC at 0h today. */
                js = wwaDat(iy, im, id, 0.0, ref dat0);
                if (js < 0) return js;

                /* TAI-UTC at 12h today (to detect drift). */
                js = wwaDat(iy, im, id, 0.5, ref dat12);
                if (js < 0) return js;

                /* TAI-UTC at 0h tomorrow (to detect jumps). */
                js = wwaJd2cal(dj, 1.5, ref iy2, ref im2, ref id2, ref w);
                if (js == 0 ? false : true) return js;
                js = wwaDat(iy2, im2, id2, 0.0, ref dat24);
                if (js < 0) return js;

                /* Any sudden change in TAI-UTC between today and tomorrow. */
                dleap = dat24 - (2.0 * dat12 - dat0);

                /* If leap second day, correct the day and final minute lengths. */
                day += dleap;
                if (ihr == 23 && imn == 59) seclim += dleap;

                /* End of UTC-specific actions. */
            }

            /* Validate the time. */
            if (ihr >= 0 && ihr <= 23)
            {
                if (imn >= 0 && imn <= 59)
                {
                    if (sec >= 0)
                    {
                        if (sec >= seclim)
                        {
                            js += 2;
                        }
                    }
                    else
                    {
                        js = -6;
                    }
                }
                else
                {
                    js = -5;
                }
            }
            else
            {
                js = -4;
            }
            if (js < 0) return js;

            /* The time in days. */
            time = (60.0 * ((double)(60 * ihr + imn)) + sec) / day;

            /* Return the date and time. */
            d1 = dj;
            d2 = time;

            /* Status. */
            return js;
        }
    }
}
