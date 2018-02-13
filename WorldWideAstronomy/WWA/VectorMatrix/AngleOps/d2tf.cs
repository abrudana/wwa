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
        /// Decompose days to hours, minutes, seconds, fraction.
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
        /// <param name="ndp">resolution (Note 1)</param>
        /// <param name="days">interval in days</param>
        /// Returned:
        /// <param name="sign">'+' or '-'</param>
        /// <param name="ihmsf">hours, minutes, seconds, fraction</param>
        public static void wwaD2tf(int ndp, double days, ref char sign, int[] ihmsf)
        {
            int nrs, n;
            double rs, rm, rh, a, w, ah, am, as1, af;

            /* Handle sign. */
            sign = (char)((days >= 0.0) ? '+' : '-');

            /* Interval in seconds. */
            //a = DAYSEC * fabs(days);
            a = DAYSEC * Math.Abs(days);

            /* Pre-round if resolution coarser than 1s (then pretend ndp=1). */
            if (ndp < 0)
            {
                nrs = 1;
                for (n = 1; n <= -ndp; n++)
                {
                    nrs *= (n == 2 || n == 4) ? 6 : 10;
                }
                rs = (double)nrs;
                w = a / rs;
                a = rs * dnint(w);
            }

            /* Express the unit of each field in resolution units. */
            nrs = 1;
            for (n = 1; n <= ndp; n++)
            {
                nrs *= 10;
            }
            rs = (double)nrs;
            rm = rs * 60.0;
            rh = rm * 60.0;

            /* Round the interval and express in resolution units. */
            a = dnint(rs * a);

            /* Break into fields. */
            ah = a / rh;
            ah = dint(ah);
            a -= ah * rh;
            am = a / rm;
            am = dint(am);
            a -= am * rm;
            as1 = a / rs;
            as1 = dint(as1);
            af = a - as1 * rs;

            /* Return results. */
            ihmsf[0] = (int)ah;
            ihmsf[1] = (int)am;
            ihmsf[2] = (int)as1;
            ihmsf[3] = (int)af;

            return;
        }
    }
}
