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
        /// Precession-nutation, IAU 2000 model:  a multi-purpose function,
        /// supporting classical (equinox-based) use directly and CIO-based
        /// use indirectly.
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
        /// <param name="dpsi">nutation (Note 2)</param>
        /// <param name="deps">nutation (Note 2)</param>
        /// <param name="epsa">mean obliquity (Note 3)</param>
        /// <param name="rb">frame bias matrix (Note 4)</param>
        /// <param name="rp">precession matrix (Note 5)</param>
        /// <param name="rbp">bias-precession matrix (Note 6)</param>
        /// <param name="rn">nutation matrix (Note 7)</param>
        /// <param name="rbpn">GCRS-to-true matrix (Note 8)</param>
        public static void wwaPn00(double date1, double date2, double dpsi, double deps,
             ref double epsa, double[,] rb, double[,] rp, double[,] rbp, double[,] rn, double[,] rbpn)
        {
            double dpsipr = 0, depspr = 0;
            double[,] rbpw = new double[3, 3];
            double[,] rnw = new double[3, 3];


            /* IAU 2000 precession-rate adjustments. */
            wwaPr00(date1, date2, ref dpsipr, ref depspr);

            /* Mean obliquity, consistent with IAU 2000 precession-nutation. */
            epsa = wwaObl80(date1, date2) + depspr;

            /* Frame bias and precession matrices and their product. */
            wwaBp00(date1, date2, rb, rp, rbpw);
            wwaCr(rbpw, rbp);

            /* Nutation matrix. */
            wwaNumat(epsa, dpsi, deps, rnw);
            wwaCr(rnw, rn);

            /* Bias-precession-nutation matrix (classical). */
            wwaRxr(rnw, rbpw, rbpn);

            return;
        }
    }
}
