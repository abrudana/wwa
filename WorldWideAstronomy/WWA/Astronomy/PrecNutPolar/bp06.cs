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
        /// Frame bias and precession, IAU 2006.
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
        /// <param name="rb">frame bias matrix (Note 2)</param>
        /// <param name="rp">precession matrix (Note 3)</param>
        /// <param name="rbp">bias-precession matrix (Note 4)</param>
        public static void wwaBp06(double date1, double date2, double[,] rb, double[,] rp, double[,] rbp)
        {
            double gamb = 0, phib = 0, psib = 0, epsa = 0;
            double[,] rbpw = new double[3, 3];
            double[,] rbt = new double[3, 3];

            /* B matrix. */
            wwaPfw06(DJM0, DJM00, ref gamb, ref phib, ref psib, ref epsa);
            wwaFw2m(gamb, phib, psib, epsa, rb);

            /* PxB matrix (temporary). */
            wwaPmat06(date1, date2, rbpw);

            /* P matrix. */
            wwaTr(rb, rbt);
            wwaRxr(rbpw, rbt, rp);

            /* PxB matrix. */
            wwaCr(rbpw, rbp);

            return;
        }        
    }
}
