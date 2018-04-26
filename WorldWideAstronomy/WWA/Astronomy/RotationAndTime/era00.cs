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
        /// Earth rotation angle (IAU 2000 model).
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
        /// <param name="dj1">UT1 as a 2-part Julian Date (see note)</param>
        /// <param name="dj2">UT1 as a 2-part Julian Date (see note)</param>
        /// <returns>Earth rotation angle (radians), range 0-2pi</returns>
        public static double wwaEra00(double dj1, double dj2)
        {
            double d1, d2, t, f, theta;

            /* Days since fundamental epoch. */
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
            t = d1 + (d2 - DJ00);

            /* Fractional part of T (days). */
            //f = fmod(d1, 1.0) + fmod(d2, 1.0);
            f = Math.IEEERemainder(d1, 1.0) + Math.IEEERemainder(d2, 1.0);

            /* Earth rotation angle at this UT1. */
            theta = wwaAnp(D2PI * (f + 0.7790572732640 + 0.00273781191135448 * t));

            return theta;
        }
    }
}
