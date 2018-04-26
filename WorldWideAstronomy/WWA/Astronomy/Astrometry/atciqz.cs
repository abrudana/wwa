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
        /// Quick ICRS to CIRS transformation, given precomputed star-
        /// independent astrometry parameters, and assuming zero parallax and
        /// proper motion.
        /// 
        /// Use of this function is appropriate when efficiency is important and
        /// where many star positions are to be transformed for one date.  The
        /// star-independent parameters can be obtained by calling one of the
        /// functions iauApci[13], iauApcg[13], wwaApco[13] or wwaApcs[13].
        /// 
        /// The corresponding function for the case of non-zero parallax and
        /// proper motion is wwaAtciq.
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
        /// <param name="rc">ICRS astrometric RA,Dec (radians)</param>
        /// <param name="dc">ICRS astrometric RA,Dec (radians)</param>
        /// <param name="astrom">star-independent astrometry parameters:</param>
        /// <param name="ri">CIRS RA,Dec (radians)</param>
        /// <param name="di">CIRS RA,Dec (radians)</param>
        public static void wwaAtciqz(double rc, double dc, ref wwaASTROM astrom, ref double ri, ref double di)
        {
            double[] pco = new double[3] { 0, 0, 0 };
            double[] pnat = new double[3] { 0, 0, 0 };
            double[] ppr = new double[3] { 0, 0, 0 };
            double[] pi = new double[3];
            double w = 0;

            /* BCRS coordinate direction (unit vector). */
            wwaS2c(rc, dc, pco);

            /* Light deflection by the Sun, giving BCRS natural direction. */
            wwaLdsun(pco, astrom.eh, astrom.em, pnat);

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