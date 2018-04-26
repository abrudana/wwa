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
        /// The equation of the equinoxes, compatible with IAU 2000 resolutions,
        /// given the nutation in longitude and the mean obliquity.
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
        /// <param name="epsa">mean obliquity (Note 2)</param>
        /// <param name="dpsi">nutation in longitude (Note 3)</param>
        /// <returns>equation of the equinoxes (Note 4)</returns>
        public static double wwaEe00(double date1, double date2, double epsa, double dpsi)
        {
            double ee;


            /* Equation of the equinoxes. */
            ee = dpsi * Math.Cos(epsa) + wwaEect00(date1, date2);

            return ee;
        }
    }
}
