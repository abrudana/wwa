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
        /// Equation of the equinoxes, IAU 1994 model.
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
        /// <param name="date1">TDB date (Note 1)</param>
        /// <param name="date2">TDB date (Note 1)</param>
        /// <returns>equation of the equinoxes (Note 2)</returns>
        public static double wwaEqeq94(double date1, double date2)
        {
            double t, om, dpsi = 0, deps = 0, eps0, ee;


            /* Interval between fundamental epoch J2000.0 and given date (JC). */
            t = ((date1 - DJ00) + date2) / DJC;

            /* Longitude of the mean ascending node of the lunar orbit on the */
            /* ecliptic, measured from the mean equinox of date. */
            om = wwaAnpm((450160.280 + (-482890.539
                    + (7.455 + 0.008 * t) * t) * t) * DAS2R
                    //+ fmod(-5.0 * t, 1.0) * D2PI);
                    + Math.IEEERemainder(-5.0 * t, 1.0) * D2PI);

            /* Nutation components and mean obliquity. */
            wwaNut80(date1, date2, ref dpsi, ref deps);
            eps0 = wwaObl80(date1, date2);

            /* Equation of the equinoxes. */
            ee = dpsi * Math.Cos(eps0) + DAS2R * (0.00264 * Math.Sin(om) + 0.000063 * Math.Sin(om + om));

            return ee;
        }
    }
}
