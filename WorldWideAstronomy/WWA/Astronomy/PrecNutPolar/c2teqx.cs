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
        /// This function is derived from the International Astronomical Union's
        /// SOFA (Standards of Fundamental Astronomy) software collection.
        /// http://www.iausofa.org
        /// The code does not itself constitute software provided by and/or endorsed by SOFA.
        /// This version is intended to retain identical functionality to the SOFA library, but
        /// made distinct through different function names (prefixes) and C# language specific
        /// modifications in code.
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
