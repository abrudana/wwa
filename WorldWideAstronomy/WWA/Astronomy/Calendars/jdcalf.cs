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
        /// Julian Date to Gregorian Calendar, expressed in a form convenient
        /// for formatting messages:  rounded to a specified precision.
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
        /// <param name="ndp">number of decimal places of days in fraction</param>
        /// <param name="dj1">dj1+dj2 = Julian Date (Note 1)</param>
        /// <param name="dj2">dj1+dj2 = Julian Date (Note 1)</param>
        /// <param name="iymdf">year, month, day, fraction in Gregorian calendar</param>
        /// <returns>
        /// status:
        /// -1 = date out of range
        /// 0 = OK
        /// +1 = NDP not 0-9 (interpreted as 0)
        /// </returns>
        public static int wwaJdcalf(int ndp, double dj1, double dj2, int[] iymdf)
        {
            int j, js;
            double denom, d1, d2, f1, f2, d, djd, f, rf;


            /* Denominator of fraction (e.g. 100 for 2 decimal places). */
            if ((ndp >= 0) && (ndp <= 9))
            {
                j = 0;
                denom = Math.Pow(10.0, ndp);
            }
            else
            {
                j = 1;
                denom = 1.0;
            }

            /* Copy the date, big then small. */
            if (Math.Abs(dj1) >= Math.Abs(dj2))
            {
                d1 = dj1;
                d2 = dj2;
            }
            else
            {
                d1 = dj2;
                d2 = dj1;
            }

            /* Realign to midnight (without rounding error). */
            d1 -= 0.5;

            /* Separate day and fraction (as precisely as possible). */
            d = dnint(d1);
            f1 = d1 - d;
            djd = d;
            d = dnint(d2);
            f2 = d2 - d;
            djd += d;
            d = dnint(f1 + f2);
            f = (f1 - d) + f2;
            if (f < 0.0)
            {
                f += 1.0;
                d -= 1.0;
            }
            djd += d;

            /* Round the total fraction to the specified number of places. */
            rf = dnint(f * denom) / denom;

            /* Re-align to noon. */
            djd += 0.5;

            /* Convert to Gregorian calendar. */
            js = wwaJd2cal(djd, rf, ref iymdf[0], ref iymdf[1], ref iymdf[2], ref f);
            if (js == 0)
            {
                iymdf[3] = (int)dnint(f * denom);
            }
            else
            {
                j = js;
            }

            /* Return the status. */
            return j;
        }
    }
}
