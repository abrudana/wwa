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
        /// Form the matrix of nutation for a given date, IAU 2000B model.
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
        /// <param name="rmatn">nutation matrix</param>
        public static void wwaNum00b(double date1, double date2, double[,] rmatn)
        {
            double dpsi = 0, deps = 0, epsa = 0;
            double[,] rb = new double[3, 3];
            double[,] rp = new double[3, 3];
            double[,] rbp = new double[3, 3];
            double[,] rbpn = new double[3, 3];


            /* Obtain the required matrix (discarding other results). */
            wwaPn00b(date1, date2, ref dpsi, ref deps, ref epsa, rb, rp, rbp, rmatn, rbpn);

            return;
        }
    }
}
