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
        /// Equation of the equinoxes, compatible with IAU 2000 resolutions.
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
        /// <returns>equation of the equinoxes (Note 2)</returns>
        public static double wwaEe00a(double date1, double date2)
        {
            double dpsipr = 0, depspr = 0, epsa, dpsi = 0, deps = 0, ee;


            /* IAU 2000 precession-rate adjustments. */
            wwaPr00(date1, date2, ref dpsipr, ref depspr);

            /* Mean obliquity, consistent with IAU 2000 precession-nutation. */
            epsa = wwaObl80(date1, date2) + depspr;

            /* Nutation in longitude. */
            wwaNut00a(date1, date2, ref dpsi, ref deps);

            /* Equation of the equinoxes. */
            ee = wwaEe00(date1, date2, epsa, dpsi);

            return ee;
        }
    }
}
