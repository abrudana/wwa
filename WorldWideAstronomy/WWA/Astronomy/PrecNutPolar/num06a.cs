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
        /// Form the matrix of nutation for a given date, IAU 2006/2000A model.
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
        public static void wwaNum06a(double date1, double date2, double[,] rmatn)
        {
            double eps, dp = 0, de = 0;


            /* Mean obliquity. */
            eps = wwaObl06(date1, date2);

            /* Nutation components. */
            wwaNut06a(date1, date2, ref dp, ref de);

            /* Nutation matrix. */
            wwaNumat(eps, dp, de, rmatn);

            return;
        }
    }
}
