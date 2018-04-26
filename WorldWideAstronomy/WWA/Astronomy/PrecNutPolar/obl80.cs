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
        /// Mean obliquity of the ecliptic, IAU 1980 model.
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
        /// <param name="date1">TT as a 2-part Julian Date (Note 1)</param>
        /// <param name="date2">TT as a 2-part Julian Date (Note 1)</param>
        /// <returns>obliquity of the ecliptic (radians, Note 2)</returns>
        public static double wwaObl80(double date1, double date2)
        {
            double t, eps0;


            /* Interval between fundamental epoch J2000.0 and given date (JC). */
            t = ((date1 - DJ00) + date2) / DJC;

            /* Mean obliquity of date. */
            eps0 = DAS2R * (84381.448 +
                           (-46.8150 +
                           (-0.00059 +
                           (0.001813) * t) * t) * t);

            return eps0;
        }
    }
}
