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
        /// Precession matrix (including frame bias) from GCRS to a specified
        /// date, IAU 2006 model.
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
        /// <param name="rbp">bias-precession matrix (Note 2)</param>
        public static void wwaPmat06(double date1, double date2, double[,] rbp)
        {
            double gamb = 0, phib = 0, psib = 0, epsa = 0;


            /* Bias-precession Fukushima-Williams angles. */
            wwaPfw06(date1, date2, ref gamb, ref phib, ref psib, ref epsa);

            /* Form the matrix. */
            wwaFw2m(gamb, phib, psib, epsa, rbp);

            return;
        }
    }
}
