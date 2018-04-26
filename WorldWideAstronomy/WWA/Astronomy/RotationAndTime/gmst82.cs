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
        /// Universal Time to Greenwich mean sidereal time (IAU 1982 model).
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
        /// <param name="dj1">UT1 Julian Date (see note)</param>
        /// <param name="dj2">UT1 Julian Date (see note)</param>
        /// <returns>Greenwich mean sidereal time (radians)</returns>
        public static double wwaGmst82(double dj1, double dj2)
        {
            /* Coefficients of IAU 1982 GMST-UT1 model */
            double A = 24110.54841 - DAYSEC / 2.0;
            double B = 8640184.812866;
            double C = 0.093104;
            double D = -6.2e-6;

            /* Note: the first constant, A, has to be adjusted by 12 hours */
            /* because the UT1 is supplied as a Julian date, which begins  */
            /* at noon.                                                    */

            double d1, d2, t, f, gmst;


            /* Julian centuries since fundamental epoch. */
            if (dj1 < dj2)
            {
                d1 = dj1;
                d2 = dj2;
            }
            else
            {
                d1 = dj2;
                d2 = dj1;
            }
            t = (d1 + (d2 - DJ00)) / DJC;

            /* Fractional part of JD(UT1), in seconds. */
            //f = DAYSEC * (fmod(d1, 1.0) + fmod(d2, 1.0));
            f = DAYSEC * (Math.IEEERemainder(d1, 1.0) + Math.IEEERemainder(d2, 1.0));

            /* GMST at this UT1. */
            gmst = wwaAnp(DS2R * ((A + (B + (C + D * t) * t) * t) + f));

            return gmst;
        }
    }
}
