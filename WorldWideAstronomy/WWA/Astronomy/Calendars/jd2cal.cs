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
        /// Julian Date to Gregorian year, month, day, and fraction of a day.
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
        /// <param name="dj1">Julian Date (Notes 1, 2)</param>
        /// <param name="dj2">Julian Date (Notes 1, 2)</param>
        /// <param name="iy">year</param>
        /// <param name="im">month</param>
        /// <param name="id">DAY</param>
        /// <param name="fd">fraction of day</param>
        /// <returns>
        /// status:
        /// 0 = OK
        /// -1 = unacceptable date (Note 3)
        /// </returns>
        public static int wwaJd2cal(double dj1, double dj2, ref int iy, ref int im, ref int id, ref double fd)
        {
            /* Minimum and maximum allowed JD */
            const double DJMIN = -68569.5;
            const double DJMAX = 1e9;

            long jd, l, n, i, k;
            double dj, d1, d2, f1, f2, f, d;


            /* Verify date is acceptable. */
            dj = dj1 + dj2;
            if (dj < DJMIN || dj > DJMAX) return -1;

            /* Copy the date, big then small, and re-align to midnight. */
            if (dj1 >= dj2)
            {
                d1 = dj1;
                d2 = dj2;
            }
            else
            {
                d1 = dj2;
                d2 = dj1;
            }
            d2 -= 0.5;

            /* Separate day and fraction. */
            f1 = d1 % 1.0;
            f2 = d2 % 1.0;
            f = Math.IEEERemainder(f1 + f2, 1.0);
            if (f < 0.0) f += 1.0;
            d = dnint(d1 - f1) + dnint(d2 - f2) + dnint(f1 + f2 - f);
            jd = (long)Math.Round(d, MidpointRounding.AwayFromZero) + 1L;

            /* Express day in Gregorian calendar. */
            l = jd + 68569L;
            n = (4L * l) / 146097L;
            l -= (146097L * n + 3L) / 4L;
            i = (4000L * (l + 1L)) / 1461001L;
            l -= (1461L * i) / 4L - 31L;
            k = (80L * l) / 2447L;
            id = (int)(l - (2447L * k) / 80L);
            l = k / 11L;
            im = (int)(k + 2L - 12L * l);
            iy = (int)(100L * (n - 49L) + i + l);
            fd = f;

            return 0;
        }
    }
}