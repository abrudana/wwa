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
        /// Assemble the celestial to terrestrial matrix from equinox-based
        /// components (the celestial-to-true matrix, the Greenwich Apparent
        /// Sidereal Time and the polar motion matrix).
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
        /// <param name="rbpn">celestial-to-true matrix</param>
        /// <param name="gst">Greenwich (apparent) Sidereal Time (radians)</param>
        /// <param name="rpom">polar-motion matrix</param>
        /// <param name="rc2t">celestial-to-terrestrial matrix (Note 2)</param>
        public static void wwaC2teqx(double[,] rbpn, double gst, double[,] rpom, double[,] rc2t)
        {
            double[,] r = new double[3, 3];

            /* Construct the matrix. */
            wwaCr(rbpn, r);
            wwaRz(gst, r);
            wwaRxr(rpom, r, rc2t);

            return;
        }
    }
}
