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
        /// Equation of the equinoxes, compatible with IAU 2000 resolutions and
        /// IAU 2006/2000A precession-nutation.
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
        public static double wwaEe06a(double date1, double date2)
        {
            double gst06a, gmst06, ee;


            /* Apparent and mean sidereal times. */
            gst06a = wwaGst06a(0.0, 0.0, date1, date2);
            gmst06 = wwaGmst06(0.0, 0.0, date1, date2);

            /* Equation of the equinoxes. */
            ee = wwaAnpm(gst06a - gmst06);

            return ee;
        }
    }
}
