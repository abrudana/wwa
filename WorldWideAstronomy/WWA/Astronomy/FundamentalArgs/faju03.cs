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
        /// mean longitude of Jupiter.
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
        /// <returns>mean longitude of Jupiter, radians (Note 2)</returns>
        public static double wwaFaju03(double t)
        {
            double a;

            /* Mean longitude of Jupiter (IERS Conventions 2003). */
            a = (0.599546497 + 52.9690962641 * t) % D2PI;

            return a;
        }
    }
}