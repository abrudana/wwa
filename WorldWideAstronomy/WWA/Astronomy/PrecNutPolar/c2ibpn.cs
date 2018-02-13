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
        /// Form the celestial-to-intermediate matrix for a given date given
        /// the bias-precession-nutation matrix.  IAU 2000.
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
        /// <param name="rbpn">celestial-to-true matrix (Note 2)</param>
        /// <param name="rc2i">celestial-to-intermediate matrix (Note 3)</param>
        public static void wwaC2ibpn(double date1, double date2, double[,] rbpn, double[,] rc2i)
        {
            double x = 0, y = 0;


            /* Extract the X,Y coordinates. */
            wwaBpn2xy(rbpn, ref x, ref y);

            /* Form the celestial-to-intermediate matrix (n.b. IAU 2000 specific). */
            wwaC2ixy(date1, date2, x, y, rc2i);

            return;
        }
    }
}
