using System;

namespace WorldWideAstronomy
{
    public static partial class WWA
    {
        /// <summary>
        /// Convert J2000.0 FK5 star catalog data to B1950.0 FK4.
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
        /// <param name="r1950"></param>
        /// <param name="d1950"></param>
        /// <param name="bepoch"></param>
        /// <param name="r2000"></param>
        /// <param name="d2000"></param>
        public static void wwaFk524(double r2000, double d2000,
              double dr2000, double dd2000,
              double p2000, double v2000,
              ref double r1950, ref double d1950,
              ref double dr1950, ref double dd1950,
              ref double p1950, ref double v1950)
        {
            /* Radians per year to arcsec per century */
            const double PMF = 100.0 * DR2AS;

            /* Small number to avoid arithmetic problems */
            const double TINY = 1e-30;

            /* Miscellaneous */
            double r, d, ur, ud, px, rv, pxvf, w, rd = 0;
            int i, j, k, l;

            /* Vectors, p and pv */
            double[,] r0 = new double[2, 3];
            double[,] r1 = new double[2, 3];
            double[] p1 = new double[3];
            double[] p2 = new double[3];
            double[,] pv = new double[2, 3];

            /*
            ** CANONICAL CONSTANTS (Seidelmann 1992)
            */

            /* Km per sec to AU per tropical century */
            /* = 86400 * 36524.2198782 / 149597870.7 */
            const double VF = 21.095;

            /* Constant pv-vector (cf. Seidelmann 3.591-2, vectors A and Adot) */
            double[,] a = new double[2, 3]
            {
                { -1.62557e-6, -0.31919e-6, -0.13843e-6 },
                { +1.245e-3,   -1.580e-3,   -0.659e-3   }
            };

            /* 3x2 matrix of pv-vectors (cf. Seidelmann 3.592-1, matrix M^-1) */
            double[,,,] em = new double[2, 3, 2, 3]
            {
                {
                    {
                        { +0.9999256795,     +0.0111814828,     +0.0048590039,    },
                        { -0.00000242389840, -0.00000002710544, -0.00000001177742 }
                    },
                    {
                        { -0.0111814828,     +0.9999374849,     -0.0000271771,    },
                        { +0.00000002710544, -0.00000242392702, +0.00000000006585 }
                    },
                    {
                        { -0.0048590040,     -0.0000271557,     +0.9999881946,    },
                        { +0.00000001177742, +0.00000000006585, -0.00000242404995 }
                    }
                },
                {
                    {
                        { -0.000551,         +0.238509,         -0.435614,        },
                        { +0.99990432,       +0.01118145,       +0.00485852       }
                    },
                    {
                        { -0.238560,         -0.002667,         +0.012254,        },
                        { -0.01118145,       +0.99991613,       -0.00002717       }
                    },
                    {
                        { +0.435730,         -0.008541,         +0.002117,        },
                        { -0.00485852,       -0.00002716,       +0.99996684       }
                    }
                }
            };
            /*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

            /* The FK5 data (units radians and arcsec per Julian century). */
            r = r2000;
            d = d2000;
            ur = dr2000 * PMF;
            ud = dd2000 * PMF;
            px = p2000;
            rv = v2000;

            /* Express as a pv-vector. */
            pxvf = px * VF;
            w = rv * pxvf;
            wwaS2pv(r, d, 1.0, ur, ud, w, r0);

            /* Convert pv-vector to Bessel-Newcomb system (cf. Seidelmann 3.592-1). */
            for (i = 0; i < 2; i++)
            {
                for (j = 0; j < 3; j++)
                {
                    w = 0.0;
                    for (k = 0; k < 2; k++)
                    {
                        for (l = 0; l < 3; l++)
                        {
                            w += em[i, j, k, l] * r0[k, l];
                        }
                    }
                    r1[i, j] = w;
                }
            }

            /* Apply E-terms (equivalent to Seidelmann 3.592-3, one iteration). */

            /* Direction. */
            w = wwaPm(CopyArray(r1, 0));
            wwaSxp(wwaPdp(CopyArray(r1, 0), CopyArray(a, 0)), CopyArray(r1, 0), p1);
            wwaSxp(w, CopyArray(a, 0), p2);
            wwaPmp(p2, p1, p1);
            wwaPpp(CopyArray(r1, 0), p1, p1);

            /* Recompute length. */
            w = wwaPm(p1);

            /* Direction. */
            wwaSxp(wwaPdp(CopyArray(r1, 0), CopyArray(a, 0)), CopyArray(r1, 0), p1);
            wwaSxp(w, CopyArray(a, 0), p2);
            wwaPmp(p2, p1, p1);
            wwaPpp(CopyArray(r1, 0), p1, CopyArray(pv, 0));

            /* Derivative. */
            wwaSxp(wwaPdp(CopyArray(r1, 0), CopyArray(a, 1)), CopyArray(pv, 0), p1);
            wwaSxp(w, CopyArray(a, 1), p2);
            wwaPmp(p2, p1, p1);
            wwaPpp(CopyArray(r1, 1), p1, CopyArray(pv, 1));

            /* Revert to catalog form. */
            wwaPv2s(pv, ref r, ref d, ref w, ref ur, ref ud, ref rd);
            if (px > TINY)
            {
                rv = rd / pxvf;
                px = px / w;
            }

            /* Return the results. */
            r1950 = wwaAnp(r);
            d1950 = d;
            dr1950 = ur / PMF;
            dd1950 = ud / PMF;
            p1950 = px;
            v1950 = rv;

            /* Finished. */

        }
    }
}
