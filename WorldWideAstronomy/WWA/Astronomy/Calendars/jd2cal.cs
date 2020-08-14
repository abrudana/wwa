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

            long jd, i, l, n, k;
            double dj, f1, f2, d, s, cs, x, t, f;
            double[] v = new double[2];


            /* Verify date is acceptable. */
            dj = dj1 + dj2;
            if (dj < DJMIN || dj > DJMAX) return -1;

            /* Separate day and fraction (where -0.5 <= fraction < 0.5). */
            d = dnint(dj1);
            f1 = dj1 - d;
            jd = (long)d;
            d = dnint(dj2);
            f2 = dj2 - d;
            jd += (long)d;

            /* Compute f1+f2+0.5 using compensated summation (Klein 2006). */
            s = 0.5;
            cs = 0.0;
            v[0] = f1;
            v[1] = f2;
            for (i = 0; i < 2; i++)
            {
                x = v[i];
                t = s + x;
                cs += Math.Abs(s) >= Math.Abs(x) ? (s - t) + x : (x - t) + s;
                s = t;
                if (s >= 1.0)
                {
                    jd++;
                    s -= 1.0;
                }
            }
            f = s + cs;
            cs = f - s;

            /* Deal with negative f. */
            if (f < 0.0)
            {

                /* Compensated summation: assume that |s| <= 1.0. */
                f = s + 1.0;
                cs += (1.0 - f) + s;
                s = f;
                f = s + cs;
                cs = f - s;
                jd--;
            }

            /* Deal with f that is 1.0 or more (when rounded to double). */
            if ((f - 1.0) >= -DBL_EPSILON / 4.0)
            {

                /* Compensated summation: assume that |s| <= 1.0. */
                t = s - 1.0;
                cs += (s - t) - 1.0;
                s = t;
                f = s + cs;
                if (-DBL_EPSILON / 2.0 < f)
                {
                    jd++;
                    f = gmax(f, 0.0);
                }
            }

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