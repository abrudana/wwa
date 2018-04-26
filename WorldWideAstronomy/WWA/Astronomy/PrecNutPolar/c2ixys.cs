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
        /// Form the celestial to intermediate-frame-of-date matrix given the CIP
        /// X,Y and the CIO locator s.
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
        /// <param name="x">Celestial Intermediate Pole (Note 1)</param>
        /// <param name="y">Celestial Intermediate Pole (Note 1)</param>
        /// <param name="s">the CIO locator s (Note 2)</param>
        /// <param name="rc2i">celestial-to-intermediate matrix (Note 3)</param>
        public static void wwaC2ixys(double x, double y, double s, double[,] rc2i)
        {
            double r2, e, d;

            /* Obtain the spherical angles E and d. */
            r2 = x * x + y * y;
            //e = (r2 != 0.0) ? Math.Atan2(y, x) : 0.0;
            e = (r2 > 0.0) ? Math.Atan2(y, x) : 0.0;
            d = Math.Atan(Math.Sqrt(r2 / (1.0 - r2)));

            /* Form the matrix. */
            wwaIr(rc2i);
            wwaRz(e, rc2i);
            wwaRy(d, rc2i);
            wwaRz(-(e + s), rc2i);

            return;
        }
    }
}
