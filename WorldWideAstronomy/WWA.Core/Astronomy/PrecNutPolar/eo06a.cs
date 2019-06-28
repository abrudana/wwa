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
        /// Equation of the origins, IAU 2006 precession and IAU 2000A nutation.
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
        /// <returns>equation of the origins in radians</returns>
        public static double wwaEo06a(double date1, double date2)
        {
            double[,] r = new double[3, 3];
            double x = 0, y = 0, s, eo;


            /* Classical nutation x precession x bias matrix. */
            wwaPnm06a(date1, date2, r);

            /* Extract CIP coordinates. */
            wwaBpn2xy(r, ref x, ref y);

            /* The CIO locator, s. */
            s = wwaS06(date1, date2, x, y);

            /* Solve for the EO. */
            eo = wwaEors(r, s);

            return eo;
        }
    }
}
