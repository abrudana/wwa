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
        /// Form the celestial to terrestrial matrix given the date, the UT1,
        /// the nutation and the polar motion.  IAU 2000.
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
        /// <param name="dpsi">nutation (Note 2)</param>
        /// <param name="deps">nutation (Note 2)</param>
        /// <param name="xp">coordinates of the pole (radians, Note 3)</param>
        /// <param name="yp">coordinates of the pole (radians, Note 3)</param>
        /// <param name="rc2t">celestial-to-terrestrial matrix (Note 4)</param>
        public static void wwaC2tpe(double tta, double ttb, double uta, double utb, double dpsi, double deps, double xp, double yp, double[,] rc2t)
        {
            double epsa = 0;
            double[,] rb = new double[3, 3];
            double[,] rp = new double[3, 3];
            double[,] rbp = new double[3, 3];
            double[,] rn = new double[3, 3];
            double[,] rbpn = new double[3, 3];
            double gmst, ee, sp;
            double[,] rpom = new double[3, 3];

            /* Form the celestial-to-true matrix for this TT. */
            wwaPn00(tta, ttb, dpsi, deps, ref epsa, rb, rp, rbp, rn, rbpn);

            /* Predict the Greenwich Mean Sidereal Time for this UT1 and TT. */
            gmst = wwaGmst00(uta, utb, tta, ttb);

            /* Predict the equation of the equinoxes given TT and nutation. */
            ee = wwaEe00(tta, ttb, epsa, dpsi);

            /* Estimate s'. */
            sp = wwaSp00(tta, ttb);

            /* Form the polar motion matrix. */
            wwaPom00(xp, yp, sp, rpom);

            /* Combine to form the celestial-to-terrestrial matrix. */
            wwaC2teqx(rbpn, gmst + ee, rpom, rc2t);

            return;
        }
    }
}
