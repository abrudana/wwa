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
