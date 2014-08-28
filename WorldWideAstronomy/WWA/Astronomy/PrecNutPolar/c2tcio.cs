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
        /// Assemble the celestial to terrestrial matrix from CIO-based
        /// components (the celestial-to-intermediate matrix, the Earth Rotation
        /// Angle and the polar motion matrix).
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
        /// <param name="rc2i">celestial-to-intermediate matrix</param>
        /// <param name="era">Earth rotation angle (radians)</param>
        /// <param name="rpom">polar-motion matrix</param>
        /// <param name="rc2t">celestial-to-terrestrial matrix</param>
        public static void wwaC2tcio(double[,] rc2i, double era, double[,] rpom, double[,] rc2t)
        {
            double[,] r = new double[3, 3];

            /* Construct the matrix. */
            wwaCr(rc2i, r);
            wwaRz(era, r);
            wwaRxr(rpom, r, rc2t);

            return;
        }
    }
}
