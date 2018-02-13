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
        /// Time scale transformation:  Coordinated Universal Time, UTC, to
        /// International Atomic Time, TAI.
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
        /// <param name="utc1">UTC as a 2-part quasi Julian Date (Notes 1-4)</param>
        /// <param name="utc2">UTC as a 2-part quasi Julian Date (Notes 1-4)</param>
        /// Returned:
        /// <param name="tai1">TAI as a 2-part Julian Date (Note 5)</param>
        /// <param name="tai2">TAI as a 2-part Julian Date (Note 5)</param>
        /// <returns>status: +1 = dubious year (Note 3)
        /// 0 = OK
        /// -1 = unacceptable date
        /// </returns>
        public static int wwaUtctai(double utc1, double utc2, ref double tai1, ref double tai2)
        {
            bool big1;
            int iy = 0, im = 0, id = 0, j, iyt = 0, imt = 0, idt = 0;
            double u1, u2, fd = 0, dat0 = 0, dat12 = 0, w = 0, dat24 = 0, dlod, dleap, z1 = 0, z2 = 0, a2;

            /* Put the two parts of the UTC into big-first order. */
            big1 = (utc1 >= utc2 ? true : false);
            if (big1)
            {
                u1 = utc1;
                u2 = utc2;
            }
            else
            {
                u1 = utc2;
                u2 = utc1;
            }

            /* Get TAI-UTC at 0h today. */
            j = wwaJd2cal(u1, u2, ref iy, ref im, ref id, ref fd);
            if (j == 0 ? false : true) return j;
            j = wwaDat(iy, im, id, 0.0, ref dat0);
            if (j < 0) return j;

            /* Get TAI-UTC at 12h today (to detect drift). */
            j = wwaDat(iy, im, id, 0.5, ref dat12);
            if (j < 0) return j;

            /* Get TAI-UTC at 0h tomorrow (to detect jumps). */
            j = wwaJd2cal(u1 + 1.5, u2 - fd, ref iyt, ref imt, ref idt, ref w);
            if (j == 0 ? false : true) return j;
            j = wwaDat(iyt, imt, idt, 0.0, ref dat24);
            if (j < 0) return j;

            /* Separate TAI-UTC change into per-day (DLOD) and any jump (DLEAP). */
            dlod = 2.0 * (dat12 - dat0);
            dleap = dat24 - (dat0 + dlod);

            /* Remove any scaling applied to spread leap into preceding day. */
            fd *= (DAYSEC + dleap) / DAYSEC;

            /* Scale from (pre-1972) UTC seconds to SI seconds. */
            fd *= (DAYSEC + dlod) / DAYSEC;

            /* Today's calendar date to 2-part JD. */
            if (wwaCal2jd(iy, im, id, ref z1, ref z2) == 0 ? false : true) return -1;

            /* Assemble the TAI result, preserving the UTC split and order. */
            a2 = z1 - u1;
            a2 += z2;
            a2 += fd + dat0 / DAYSEC;
            if (big1)
            {
                tai1 = u1;
                tai2 = a2;
            }
            else
            {
                tai1 = a2;
                tai2 = u1;
            }

            /* Status. */
            return j;
        }
    }
}
