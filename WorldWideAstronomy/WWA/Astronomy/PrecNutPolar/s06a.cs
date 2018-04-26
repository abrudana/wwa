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
        /// The CIO locator s, positioning the Celestial Intermediate Origin on
        /// the equator of the Celestial Intermediate Pole, using the IAU 2006
        /// precession and IAU 2000A nutation models.
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
        /// Attila Abrud�n
        /// 
        /// Please read the ReadMe.1st text file for more information.
        /// </remarks>
        /// <param name="date1">TT as a 2-part Julian Date (Note 1)</param>
        /// <param name="date2">TT as a 2-part Julian Date (Note 1)</param>
        /// <returns>the CIO locator s in radians (Note 2)</returns>
        public static double wwaS06a(double date1, double date2)
        {
            double[,] rnpb = new double[3, 3];
            double x = 0, y = 0, s;

            /* Bias-precession-nutation-matrix, IAU 20006/2000A. */
            wwaPnm06a(date1, date2, rnpb);

            /* Extract the CIP coordinates. */
            wwaBpn2xy(rnpb, ref x, ref y);

            /* Compute the CIO locator s, given the CIP coordinates. */
            s = wwaS06(date1, date2, x, y);

            return s;
        }
    }
}
