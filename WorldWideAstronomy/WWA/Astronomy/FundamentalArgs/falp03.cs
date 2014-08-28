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
        /// mean anomaly of the Sun.
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
        /// <returns>l', radians (Note 2)</returns>
        public static double wwaFalp03(double t)
        {
            double a, b;

            /* Mean anomaly of the Sun (IERS Conventions 2003). */
            //a = (1287104.793048 +
            //          t * (129596581.0481 +
            //          t * (-0.5532 +
            //          t * (0.000136 +
            //          t * (-0.00001149)))) % TURNAS) * DAS2R;

            b = (1287104.793048 + t * (129596581.0481 + t * (-0.5532 + t * (0.000136 + t * (-0.00001149)))));
            a = b % TURNAS;
            a = a * DAS2R;

            return a;
        }
    }
}