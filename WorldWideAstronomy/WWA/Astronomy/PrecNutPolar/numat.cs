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
        /// Form the matrix of nutation.
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
        /// <param name="epsa">mean obliquity of date (Note 1)</param>
        /// <param name="dpsi">nutation (Note 2)</param>
        /// <param name="deps">nutation (Note 2)</param>
        /// <param name="rmatn">nutation matrix (Note 3)</param>
        public static void wwaNumat(double epsa, double dpsi, double deps, double[,] rmatn)
        {
            /* Build the rotation matrix. */
            wwaIr(rmatn);
            wwaRx(epsa, rmatn);
            wwaRz(-dpsi, rmatn);
            wwaRx(-(epsa + deps), rmatn);

            return;
        }
    }
}
