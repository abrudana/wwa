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
        /// Form the celestial to terrestrial matrix given the date, the UT1 and
        /// the polar motion, using the IAU 2000A nutation model.
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
        /// <param name="tta">TT as a 2-part Julian Date (Note 1)</param>
        /// <param name="ttb">TT as a 2-part Julian Date (Note 1)</param>
        /// <param name="uta">UT1 as a 2-part Julian Date (Note 1)</param>
        /// <param name="utb">UT1 as a 2-part Julian Date (Note 1)</param>
        /// <param name="xp">coordinates of the pole (radians, Note 2)</param>
        /// <param name="yp">coordinates of the pole (radians, Note 2)</param>
        /// <param name="rc2t">celestial-to-terrestrial matrix (Note 3)</param>
        public static void wwaC2t00a(double tta, double ttb, double uta, double utb, double xp, double yp, double[,] rc2t)
        {
            double[,] rc2i = new double[3, 3];
            double era, sp;
            double[,] rpom = new double[3, 3];


            /* Form the celestial-to-intermediate matrix for this TT (IAU 2000A). */
            wwaC2i00a(tta, ttb, rc2i);

            /* Predict the Earth rotation angle for this UT1. */
            era = wwaEra00(uta, utb);

            /* Estimate s'. */
            sp = wwaSp00(tta, ttb);

            /* Form the polar motion matrix. */
            wwaPom00(xp, yp, sp, rpom);

            /* Combine to form the celestial-to-terrestrial matrix. */
            wwaC2tcio(rc2i, era, rpom, rc2t);

            return;
        }
    }
}
