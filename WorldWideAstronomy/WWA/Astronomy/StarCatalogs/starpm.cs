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
        /// Star proper motion:  update star catalog data for space motion.
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
        /// Given:
        /// <param name="ra1">right ascension (radians), before</param>
        /// <param name="dec1">declination (radians), before</param>
        /// <param name="pmr1">RA proper motion (radians/year), before</param>
        /// <param name="pmd1">Dec proper motion (radians/year), before</param>
        /// <param name="px1">parallax (arcseconds), before</param>
        /// <param name="rv1">radial velocity (km/s, +ve = receding), before</param>
        /// <param name="ep1a">"before" epoch, part A (Note 1)</param>
        /// <param name="ep1b">"before" epoch, part B (Note 1)</param>
        /// <param name="ep2a">"after" epoch, part A (Note 1)</param>
        /// <param name="ep2b">"after" epoch, part B (Note 1)</param>
        /// Returned:
        /// <param name="ra2">right ascension (radians), after</param>
        /// <param name="dec2">declination (radians), after</param>
        /// <param name="pmr2">RA proper motion (radians/year), after</param>
        /// <param name="pmd2">Dec proper motion (radians/year), after</param>
        /// <param name="px2">parallax (arcseconds), after</param>
        /// <param name="rv2">radial velocity (km/s, +ve = receding), after</param>
        /// <returns>status:
        /// -1 = system error (should not occur)
        /// 0 = no warnings or errors
        /// 1 = distance overridden (Note 6)
        /// 2 = excessive velocity (Note 7)
        /// 4 = solution didn't converge (Note 8)
        /// else = binary logical OR of the above warnings
        /// </returns>
        public static int wwaStarpm(double ra1, double dec1,
              double pmr1, double pmd1, double px1, double rv1,
              double ep1a, double ep1b, double ep2a, double ep2b,
              ref double ra2, ref double dec2,
              ref double pmr2, ref double pmd2, ref double px2, ref double rv2)
        {
            double[,] pv1 = new double[2, 3];
            double tl1, dt;
            double[,] pv = new double[2, 3];
            double r2, rdv, v2, c2mv2, tl2;
            double[,] pv2 = new double[2, 3];
            int j1, j2, j;

            /* RA,Dec etc. at the "before" epoch to space motion pv-vector. */
            j1 = wwaStarpv(ra1, dec1, pmr1, pmd1, px1, rv1, pv1);

            /* Light time when observed (days). */
            tl1 = wwaPm(CopyArray(pv1, 0)) / DC;

            /* Time interval, "before" to "after" (days). */
            dt = (ep2a - ep1a) + (ep2b - ep1b);

            /* Move star along track from the "before" observed position to the */
            /* "after" geometric position. */
            wwaPvu(dt + tl1, pv1, pv);

            /* From this geometric position, deduce the observed light time (days) */
            /* at the "after" epoch (with theoretically unneccessary error check). */
            r2 = wwaPdp(CopyArray(pv, 0), CopyArray(pv, 0));
            rdv = wwaPdp(CopyArray(pv, 0), CopyArray(pv, 1));
            v2 = wwaPdp(CopyArray(pv, 1), CopyArray(pv, 1));
            c2mv2 = DC * DC - v2;
            if (c2mv2 <= 0) return -1;
            tl2 = (-rdv + Math.Sqrt(rdv * rdv + c2mv2 * r2)) / c2mv2;

            /* Move the position along track from the observed place at the */
            /* "before" epoch to the observed place at the "after" epoch. */
            wwaPvu(dt + (tl1 - tl2), pv1, pv2);

            /* Space motion pv-vector to RA,Dec etc. at the "after" epoch. */
            j2 = wwaPvstar(pv2, ref ra2, ref dec2, ref pmr2, ref pmd2, ref px2, ref rv2);

            /* Final status. */
            j = (j2 == 0) ? j1 : -1;

            return j;
        }
    }
}
