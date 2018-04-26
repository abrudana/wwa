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
        /// Greenwich mean sidereal time (consistent with IAU 2006 precession).
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
        /// <param name="uta">UT1 as a 2-part Julian Date (Notes 1,2)</param>
        /// <param name="utb">UT1 as a 2-part Julian Date (Notes 1,2)</param>
        /// <param name="tta">TT as a 2-part Julian Date (Notes 1,2)</param>
        /// <param name="ttb">TT as a 2-part Julian Date (Notes 1,2)</param>
        /// <returns>Greenwich mean sidereal time (radians)</returns>
        public static double wwaGmst06(double uta, double utb, double tta, double ttb)
        {
            double t, gmst;


            /* TT Julian centuries since J2000.0. */
            t = ((tta - DJ00) + ttb) / DJC;

            /* Greenwich mean sidereal time, IAU 2006. */
            gmst = wwaAnp(wwaEra00(uta, utb) +
                           (0.014506 +
                           (4612.156534 +
                           (1.3915817 +
                           (-0.00000044 +
                           (-0.000029956 +
                           (-0.0000000368)
                   * t) * t) * t) * t) * t) * DAS2R);

            return gmst;
        }
    }
}
