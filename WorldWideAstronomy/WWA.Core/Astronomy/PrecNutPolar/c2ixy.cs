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
        /// Form the celestial to intermediate-frame-of-date matrix for a given
        /// date when the CIP X,Y coordinates are known.  IAU 2000.
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
        /// <param name="rc2i">celestial-to-intermediate matrix (Note 3)</param>
        public static void wwaC2ixy(double date1, double date2, double x, double y, double[,] rc2i)
        {
            /* Compute s and then the matrix. */
            wwaC2ixys(x, y, wwaS00(date1, date2, x, y), rc2i);

            return;
        }
    }
}
