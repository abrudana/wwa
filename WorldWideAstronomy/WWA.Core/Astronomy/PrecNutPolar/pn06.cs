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
        /// Precession-nutation, IAU 2006 model:  a multi-purpose function,
        /// supporting classical (equinox-based) use directly and CIO-based use
        /// indirectly.
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
        public static void wwaPn06(double date1, double date2, double dpsi, double deps, ref double epsa,
             double[,] rb, double[,] rp, double[,] rbp, double[,] rn, double[,] rbpn)
        {
            double gamb = 0, phib = 0, psib = 0, eps = 0;
            double[,] r1 = new double[3, 3];
            double[,] r2 = new double[3, 3];
            double[,] rt = new double[3, 3];

            /* Bias-precession Fukushima-Williams angles of J2000.0 = frame bias. */
            wwaPfw06(DJM0, DJM00, ref gamb, ref phib, ref psib, ref eps);

            /* B matrix. */
            wwaFw2m(gamb, phib, psib, eps, r1);
            wwaCr(r1, rb);

            /* Bias-precession Fukushima-Williams angles of date. */
            wwaPfw06(date1, date2, ref gamb, ref phib, ref psib, ref eps);

            /* Bias-precession matrix. */
            wwaFw2m(gamb, phib, psib, eps, r2);
            wwaCr(r2, rbp);

            /* Solve for precession matrix. */
            wwaTr(r1, rt);
            wwaRxr(r2, rt, rp);

            /* Equinox-based bias-precession-nutation matrix. */
            wwaFw2m(gamb, phib, psib + dpsi, eps + deps, r1);
            wwaCr(r1, rbpn);

            /* Solve for nutation matrix. */
            wwaTr(r2, rt);
            wwaRxr(r1, rt, rn);

            /* Obliquity, mean of date. */
            epsa = eps;

            return;
        }
    }
}
