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
        /// Greenwich mean sidereal time (model consistent with IAU 2000
        /// resolutions).
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
        /// <param name="uta">UT1 as a 2-part Julian Date (Notes 1,2)</param>
        /// <param name="utb">UT1 as a 2-part Julian Date (Notes 1,2)</param>
        /// <param name="tta">TT as a 2-part Julian Date (Notes 1,2)</param>
        /// <param name="ttb">TT as a 2-part Julian Date (Notes 1,2)</param>
        /// <returns>Greenwich mean sidereal time (radians)</returns>
        public static double wwaGmst00(double uta, double utb, double tta, double ttb)
        {
            double t, gmst;


            /* TT Julian centuries since J2000.0. */
            t = ((tta - DJ00) + ttb) / DJC;

            /* Greenwich Mean Sidereal Time, IAU 2000. */
            gmst = wwaAnp(wwaEra00(uta, utb) +
                            (0.014506 +
                            (4612.15739966 +
                            (1.39667721 +
                            (-0.00009344 +
                            (0.00001882)
                   * t) * t) * t) * t) * DAS2R);

            return gmst;
        }
    }
}
