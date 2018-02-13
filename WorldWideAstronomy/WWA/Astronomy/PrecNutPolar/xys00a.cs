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
        /// For a given TT date, compute the X,Y coordinates of the Celestial
        /// Intermediate Pole and the CIO locator s, using the IAU 2000A
        /// precession-nutation model.
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
        /// <param name="x">Celestial Intermediate Pole (Note 2)</param>
        /// <param name="y">Celestial Intermediate Pole (Note 2)</param>
        /// <param name="s">the CIO locator s (Note 2)</param>
        public static void wwaXys00a(double date1, double date2, ref double x, ref double y, ref double s)
        {
            double[,] rbpn = new double[3, 3];


            /* Form the bias-precession-nutation matrix, IAU 2000A. */
            wwaPnm00a(date1, date2, rbpn);

            /* Extract X,Y. */
            wwaBpn2xy(rbpn, ref x, ref y);

            /* Obtain s. */
            s = wwaS00(date1, date2, x, y);

            return;
        }
    }
}
