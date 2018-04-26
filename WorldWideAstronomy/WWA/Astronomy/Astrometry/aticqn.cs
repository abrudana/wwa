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
        /// Quick CIRS to ICRS astrometric place transformation, given the star-
        /// independent astrometry parameters plus a list of light-deflecting bodies.
        /// 
        /// Use of this function is appropriate when efficiency is important and
        /// where many star positions are all to be transformed for one date.
        /// The star-independent astrometry parameters can be obtained by
        /// calling one of the functions iauApci[13], iauApcg[13], wwaApco[13]
        /// or wwaApcs[13].
        /// 
        /// If the only light-deflecting body to be taken into account is the
        /// Sun, the wwaAticq function can be used instead.
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
        /// <param name="ri">CIRS RA,Dec (radians)</param>
        /// <param name="di">CIRS RA,Dec (radians)</param>
        /// <param name="astrom">star-independent astrometry parameters:</param>
        /// <param name="n">number of bodies (Note 3)</param>
        /// <param name="b">data for each of the n bodies (Notes 3,4):</param>
        /// <param name="rc">ICRS astrometric RA,Dec (radians)</param>
        /// <param name="dc">ICRS astrometric RA,Dec (radians)</param>
        public static void wwaAticqn(double ri, double di, ref wwaASTROM astrom, int n, wwaLDBODY[] b, ref double rc, ref double dc)
        {
            int j, i;
            double[] pi = new double[3] { 0, 0, 0 };
            double[] ppr = new double[3] { 0, 0, 0 };
            double[] pnat = new double[3] { 0, 0, 0 };
            double[] pco = new double[3] { 0, 0, 0 };
            double[] d = new double[3] { 0, 0, 0 };
            double[] before = new double[3] { 0, 0, 0 };
            double[] after = new double[3] { 0, 0, 0 };
            double r2, r, w = 0;

            /* CIRS RA,Dec to Cartesian. */
            wwaS2c(ri, di, pi);

            /* Bias-precession-nutation, giving GCRS proper direction. */
            wwaTrxp(astrom.bpn, pi, ppr);

            /* Aberration, giving GCRS natural direction. */
            wwaZp(d);
            for (j = 0; j < 2; j++)
            {
                r2 = 0.0;
                for (i = 0; i < 3; i++)
                {
                    w = ppr[i] - d[i];
                    before[i] = w;
                    r2 += w * w;
                }
                r = Math.Sqrt(r2);
                for (i = 0; i < 3; i++)
                {
                    before[i] /= r;
                }
                wwaAb(before, astrom.v, astrom.em, astrom.bm1, ref after);
                r2 = 0.0;
                for (i = 0; i < 3; i++)
                {
                    d[i] = after[i] - before[i];
                    w = ppr[i] - d[i];
                    pnat[i] = w;
                    r2 += w * w;
                }
                r = Math.Sqrt(r2);
                for (i = 0; i < 3; i++)
                {
                    pnat[i] /= r;
                }
            }

            /* Light deflection, giving BCRS coordinate direction. */
            wwaZp(d);
            for (j = 0; j < 5; j++)
            {
                r2 = 0.0;
                for (i = 0; i < 3; i++)
                {
                    w = pnat[i] - d[i];
                    before[i] = w;
                    r2 += w * w;
                }
                r = Math.Sqrt(r2);
                for (i = 0; i < 3; i++)
                {
                    before[i] /= r;
                }
                wwaLdn(n, b, astrom.eb, before, after);
                r2 = 0.0;
                for (i = 0; i < 3; i++)
                {
                    d[i] = after[i] - before[i];
                    w = pnat[i] - d[i];
                    pco[i] = w;
                    r2 += w * w;
                }
                r = Math.Sqrt(r2);
                for (i = 0; i < 3; i++)
                {
                    pco[i] /= r;
                }
            }

            /* ICRS astrometric RA,Dec. */
            wwaC2s(pco, ref w, ref dc);
            rc = wwaAnp(w);
        }        
    }
}