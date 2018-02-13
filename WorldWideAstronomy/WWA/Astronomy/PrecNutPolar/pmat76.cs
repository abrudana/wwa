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
        /// Precession matrix from J2000.0 to a specified date, IAU 1976 model.
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
        /// <param name="date1">ending date, TT (Note 1)</param>
        /// <param name="date2">ending date, TT (Note 1)</param>
        /// <param name="rmatp">precession matrix, J2000.0 -> date1+date2</param>
        public static void wwaPmat76(double date1, double date2, double[,] rmatp)
        {
            double zeta = 0, z = 0, theta = 0;
            double[,] wmat = new double[3, 3];

            /* Precession Euler angles, J2000.0 to specified date. */
            wwaPrec76(DJ00, 0.0, date1, date2, ref zeta, ref z, ref theta);

            /* Form the rotation matrix. */
            wwaIr(wmat);
            wwaRz(-zeta, wmat);
            wwaRy(theta, wmat);
            wwaRz(-z, wmat);
            wwaCr(wmat, rmatp);

            return;
        }
    }
}
