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
        /// For an observer whose geocentric position and velocity are known,
        /// prepare star-independent astrometry parameters for transformations
        /// between ICRS and GCRS.  The Earth ephemeris is supplied by the caller. 
        /// 
        /// The parameters produced by this function are required in the space
        /// motion, parallax, light deflection and aberration parts of the
        /// astrometric transformation chain.
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
        /// <param name="date1">TDB as a 2-part...</param>
        /// <param name="date2">...Julian Date (Note 1)</param>
        /// <param name="pv">observer's geocentric pos/vel (m, m/s)</param>
        /// <param name="ebpv">Earth barycentric PV (au, au/day)</param>
        /// <param name="ehp">Earth heliocentric P (au)</param>
        /// <param name="astrom">star-independent astrometry parameters</param>
        public static void wwaApcs(double date1, double date2, double[,] pv,double[,] ebpv, double[] ehp, ref wwaASTROM astrom)
        {
            /* au/d to m/s */
            const double AUDMS = DAU / DAYSEC;

            /* Light time for 1 AU (day) */
            const double CR = AULT / DAYSEC;

            int i;
            double dp, dv, v2, w;
            double[] pb = new double[3];
            double[] vb = new double[3];
            double[] ph = new double[3];

            /* Time since reference epoch, years (for proper motion calculation). */
            astrom.pmt = ( (date1 - DJ00) + date2 ) / DJY;

            /* Adjust Earth ephemeris to observer. */
            for (i = 0; i < 3; i++) 
            {
                dp = pv[0, i] / DAU;
                dv = pv[1, i] / AUDMS;
                pb[i] = ebpv[0, i] + dp;
                vb[i] = ebpv[1, i] + dv;
                ph[i] = ehp[i] + dp;
            }

            /* Barycentric position of observer (au). */
            wwaCp(pb, astrom.eb);

            /* Heliocentric direction and distance (unit vector and au). */
            wwaPn(ph, ref astrom.em, astrom.eh);

            /* Barycentric vel. in units of c, and reciprocal of Lorenz factor. */
            v2 = 0.0;

            if (astrom.v == null)
                astrom.v = new double[3];

            for (i = 0; i < 3; i++)
            {
                w = vb[i] * CR;
                astrom.v[i] = w;
                v2 += w*w;
            }
            astrom.bm1 = Math.Sqrt(1.0 - v2);

            /* Reset the NPB matrix. */
            wwaIr(astrom.bpn);
        }
    }
}