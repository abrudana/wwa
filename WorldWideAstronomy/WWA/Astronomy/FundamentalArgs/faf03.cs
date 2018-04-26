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
        /// Fundamental argument, IERS Conventions (2003):
        /// mean longitude of the Moon minus mean longitude of the ascending node.
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
        /// <param name="t">TDB, Julian centuries since J2000.0 (Note 1)</param>
        /// <returns>F, radians (Note 2)</returns>
        public static double wwaFaf03(double t)
        {
            double a, b;

            /* Mean longitude of the Moon minus that of the ascending node */
            /* (IERS Conventions 2003).                                    */
            //a = (335779.526232 +
            //          t * (1739527262.8478 +
            //          t * (-12.7512 +
            //          t * (-0.001037 +
            //          t * (0.00000417)))) % TURNAS) * DAS2R;

            b = (335779.526232 + t * (1739527262.8478 + t * (-12.7512 + t * (-0.001037 + t * (0.00000417)))));
            a = b % TURNAS;
            a = a * DAS2R;

            return a;
        }
    }
}