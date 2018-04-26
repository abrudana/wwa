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
        /// Time scale transformation:  International Atomic Time, TAI, to
        /// Coordinated Universal Time, UTC.
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
        /// <param name="tai1">TAI as a 2-part Julian Date (Note 1)</param>
        /// <param name="tai2">TAI as a 2-part Julian Date (Note 1)</param>
        /// Returned:
        /// <param name="utc1">UTC as a 2-part quasi Julian Date (Notes 1-3)</param>
        /// <param name="utc2">UTC as a 2-part quasi Julian Date (Notes 1-3)</param>
        /// <returns>status: 
        /// +1 = dubious year (Note 4)
        /// 0 = OK
        /// -1 = unacceptable date
        /// </returns>
        public static int wwaTaiutc(double tai1, double tai2, ref double utc1, ref double utc2)
        {
            bool big1;
            int i, j = 0;
            double a1, a2, u1, u2, g1 = 0, g2 = 0;

            /* Put the two parts of the TAI into big-first order. */
            big1 = (tai1 >= tai2 ? true : false);
            if (big1)
            {
                a1 = tai1;
                a2 = tai2;
            }
            else
            {
                a1 = tai2;
                a2 = tai1;
            }

            /* Initial guess for UTC. */
            u1 = a1;
            u2 = a2;

            /* Iterate (though in most cases just once is enough). */
            for (i = 0; i < 3; i++)
            {

                /* Guessed UTC to TAI. */
                j = wwaUtctai(u1, u2, ref g1, ref g2);
                if (j < 0) return j;

                /* Adjust guessed UTC. */
                u2 += a1 - g1;
                u2 += a2 - g2;
            }

            /* Return the UTC result, preserving the TAI order. */
            if (big1)
            {
                utc1 = u1;
                utc2 = u2;
            }
            else
            {
                utc1 = u2;
                utc2 = u1;
            }

            /* Status. */
            return j;
        }
    }
}
