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
        /// mean elongation of the Moon from the Sun.
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
        /// <returns>
        /// D, radians (Note 2)
        /// </returns>
        public static double wwaFad03(double t)
        {
            double a, b;

            /* Mean elongation of the Moon from the Sun (IERS Conventions 2003). */
            //a = (1072260.703692 +
            //          t * (1602961601.2090 +
            //          t * (-6.3706 +
            //          t * (0.006593 +
            //          t * (-0.00003169)))) % TURNAS) * DAS2R;

            b = (1072260.703692 + t * (1602961601.2090 + t * (-6.3706 + t * (0.006593 + t * (-0.00003169)))));
            a = b % TURNAS;
            a = a * DAS2R;


            return a;
        }
    }
}