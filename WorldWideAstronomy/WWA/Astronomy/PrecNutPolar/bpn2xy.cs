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
        /// Extract from the bias-precession-nutation matrix the X,Y coordinates
        /// of the Celestial Intermediate Pole.
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
        /// <param name="rbpn">celestial-to-true matrix (Note 1)</param>
        /// <param name="x">Celestial Intermediate Pole (Note 2)</param>
        /// <param name="y">Celestial Intermediate Pole (Note 2)</param>
        public static void wwaBpn2xy(double[,] rbpn, ref double x, ref double y)
        {
            /* Extract the X,Y coordinates. */
            x = rbpn[2, 0];
            y = rbpn[2, 1];

            return;
        }
    }
}
