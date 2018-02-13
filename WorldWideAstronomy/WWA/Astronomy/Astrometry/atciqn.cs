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
        /// Quick ICRS, epoch J2000.0, to CIRS transformation, given precomputed
        /// star-independent astrometry parameters plus a list of light-
        /// deflecting bodies.
        /// 
        /// Use of this function is appropriate when efficiency is important and
        /// where many star positions are to be transformed for one date.  The
        /// star-independent parameters can be obtained by calling one of the
        /// functions iauApci[13], iauApcg[13], wwaApco[13] or wwaApcs[13].
        /// 
        /// If the only light-deflecting body to be taken into account is the
        /// Sun, the wwaAtciq function can be used instead.  If in addition the
        /// parallax and proper motions are zero, the wwaAtciqz function can be used.
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
        /// <param name="rc">ICRS RA,Dec at J2000.0 (radians)</param>
        /// <param name="dc">ICRS RA,Dec at J2000.0 (radians)</param>
        /// <param name="pr">RA proper motion (radians/year; Note 3)</param>
        /// <param name="pd">Dec proper motion (radians/year)</param>
        /// <param name="px">parallax (arcsec)</param>
        /// <param name="rv">radial velocity (km/s, +ve if receding)</param>
        /// <param name="astrom">star-independent astrometry parameters:</param>
        /// <param name="n">number of bodies (Note 3)</param>
        /// <param name="b">data for each of the n bodies (Notes 3,4):</param>
        /// <param name="ri">CIRS RA,Dec (radians)</param>
        /// <param name="di">CIRS RA,Dec (radians)</param>
        public static void wwaAtciqn(double rc, double dc, double pr, double pd, double px, double rv, ref wwaASTROM astrom, int n, wwaLDBODY[] b, ref double ri, ref double di)
        {
            double[] pco = new double[3] { 0, 0, 0 };
            double[] pnat = new double[3] { 0, 0, 0 };
            double[] ppr = new double[3] { 0, 0, 0 };
            double[] pi = new double[3];
            double w = 0;

            /* Proper motion and parallax, giving BCRS coordinate direction. */
            wwaPmpx(rc, dc, pr, pd, px, rv, astrom.pmt, astrom.eb, pco);

            /* Light deflection, giving BCRS natural direction. */
            wwaLdn(n, b, astrom.eb, pco, pnat);

            /* Aberration, giving GCRS proper direction. */
            wwaAb(pnat, astrom.v, astrom.em, astrom.bm1, ref ppr);

            /* Bias-precession-nutation, giving CIRS proper direction. */
            wwaRxp(astrom.bpn, ppr, pi);

            /* CIRS RA,Dec. */
            wwaC2s(pi, ref w, ref di);
            ri = wwaAnp(w);
        }
    }
}