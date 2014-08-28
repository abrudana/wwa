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
        /// mean longitude of the Moon's ascending node.
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
        /// <param name="t">TDB, Julian centuries since J2000.0 (Note 1)</param>
        /// <returns>Omega, radians (Note 2)</returns>
        public static double wwaFaom03(double t)
        {
            double a, b;

            /* Mean longitude of the Moon's ascending node */
            /* (IERS Conventions 2003).                    */
            //a = (450160.398036 +
            //          t * (-6962890.5431 +
            //          t * (7.4722 +
            //          t * (0.007702 +
            //          t * (-0.00005939)))) % TURNAS) * DAS2R;

            b = (450160.398036 + t * (-6962890.5431 + t * (7.4722 + t * (0.007702 + t * (-0.00005939)))));
            a = b % TURNAS;
            a = a * DAS2R;

            return a;
        }
    }
}