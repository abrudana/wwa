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
        /// Form the matrix of precession/nutation for a given date, IAU 1976
        /// precession model, IAU 1980 nutation model.
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
        /// <param name="date1">TDB date (Note 1)</param>
        /// <param name="date2">TDB date (Note 1)</param>
        /// <param name="rmatpn">combined precession/nutation matrix</param>
        public static void wwaPnm80(double date1, double date2, double[,] rmatpn)
        {
            double[,] rmatp = new double[3, 3];
            double[,] rmatn = new double[3, 3];


            /* Precession matrix, J2000.0 to date. */
            wwaPmat76(date1, date2, rmatp);

            /* Nutation matrix. */
            wwaNutm80(date1, date2, rmatn);

            /* Combine the matrices:  PN = N x P. */
            wwaRxr(rmatn, rmatp, rmatpn);

            return;
        }
    }
}
