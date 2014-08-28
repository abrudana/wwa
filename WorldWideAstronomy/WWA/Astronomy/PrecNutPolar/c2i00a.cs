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
        /// Form the celestial-to-intermediate matrix for a given date using the
        /// IAU 2000A precession-nutation model.
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
        /// <param name="date1">TT as a 2-part Julian Date (Note 1)</param>
        /// <param name="date2">TT as a 2-part Julian Date (Note 1)</param>
        /// <param name="rc2i">celestial-to-intermediate matrix (Note 2)</param>
        public static void wwaC2i00a(double date1, double date2, double[,] rc2i)
        {
            double[,] rbpn = new double[3, 3];

            /* Obtain the celestial-to-true matrix (IAU 2000A). */
            wwaPnm00a(date1, date2, rbpn);

            /* Form the celestial-to-intermediate matrix. */
            wwaC2ibpn(date1, date2, rbpn, rc2i);

            return;
        }
    }
}
