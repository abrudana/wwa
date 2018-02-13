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
        /// The TIO locator s', positioning the Terrestrial Intermediate Origin
        /// on the equator of the Celestial Intermediate Pole.
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
        /// <returns>the TIO locator s' in radians (Note 2)</returns>
        public static double wwaSp00(double date1, double date2)
        {
            double t, sp;


            /* Interval between fundamental epoch J2000.0 and current date (JC). */
            t = ((date1 - DJ00) + date2) / DJC;

            /* Approximate s'. */
            sp = -47e-6 * t * DAS2R;

            return sp;
        }
    }
}
