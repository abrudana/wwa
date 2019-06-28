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
        /// mean anomaly of the Moon.
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
        /// <returns>l, radians (Note 2)</returns>
        public static double wwaFal03(double t)
        {
            double a, b;

            /* Mean anomaly of the Moon (IERS Conventions 2003). */
            //a = fmod(485868.249036 +
            // t * (1717915923.2178 +
            // t * (31.8792 +
            // t * (0.051635 +
            // t * (-0.00024470)))), TURNAS) * DAS2R;

            //a = Math.IEEERemainder(485868.249036 +
            // t * (1717915923.2178 +
            // t * (31.8792 +
            // t * (0.051635 +
            // t * (-0.00024470)))), TURNAS) * DAS2R;

            b = (485868.249036 + t * (1717915923.2178 + t * (31.8792 + t * (0.051635 + t * (-0.00024470)))));

            a =  b % TURNAS;

            a = a * DAS2R;

            return a;
        }
    }
}