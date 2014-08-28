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
        /// Form the matrix of polar motion for a given date, IAU 2000.
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
        /// <param name="xp">coordinates of the pole (radians, Note 1)</param>
        /// <param name="yp">coordinates of the pole (radians, Note 1)</param>
        /// <param name="sp">the TIO locator s' (radians, Note 2)</param>
        /// <param name="rpom">polar-motion matrix (Note 3)</param>
        public static void wwaPom00(double xp, double yp, double sp, double[,] rpom)
        {

            /* Construct the matrix. */
            wwaIr(rpom);
            wwaRz(sp, rpom);
            wwaRy(-xp, rpom);
            wwaRx(-yp, rpom);

            return;
        }
    }
}
