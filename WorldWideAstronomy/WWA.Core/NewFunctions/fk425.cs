using System;

namespace WorldWideAstronomy
{
    public static partial class WWA
    {
        /// <summary>
        /// Convert B1950.0 FK4 star catalog data to J2000.0 FK5.
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
        public static void wwaFk425(double r1950, double d1950,
              double dr1950, double dd1950,
              double p1950, double v1950,
              ref double r2000, ref double d2000,
              ref double dr2000, ref double dd2000,
              ref double p2000, ref double v2000)
        {
            /* Radians per year to arcsec per century */
            const double PMF = 100.0 * DR2AS;

            /* Small number to avoid arithmetic problems */
            const double TINY = 1e-30;

            /* Miscellaneous */
            double r, d, ur, ud, px, rv, pxvf, w, rd = 0;
            int i, j, k, l;

            /* Pv-vectors */
            double[,] r0 = new double[2, 3];
            double[,] pv1 = new double[2, 3];
            double[,] pv2 = new double[2, 3];

            /*
            ** CANONICAL CONSTANTS (Seidelmann 1992)
            */

            /* Km per sec to AU per tropical century */
            /* = 86400 * 36524.2198782 / 149597870.7 */
            const double VF = 21.095;

            /* Constant pv-vector (cf. Seidelmann 3.591-2, vectors A and Adot) */
            double[,] a = new double[2, 3] {
                { -1.62557e-6, -0.31919e-6, -0.13843e-6 },
                { +1.245e-3,   -1.580e-3,   -0.659e-3   }
            };

            /* 3x2 matrix of pv-vectors (cf. Seidelmann 3.591-4, matrix M) */
            double[,,,] em = new double[2, 3, 2, 3]
            {
                {
                    {
                        { +0.9999256782,     -0.0111820611,     -0.0048579477     },
                        { +0.00000242395018, -0.00000002710663, -0.00000001177656 }
                    },
                    {
                        { +0.0111820610,     +0.9999374784,     -0.0000271765     },
                        { +0.00000002710663, +0.00000242397878, -0.00000000006587 }
                    },
                    {
                        { +0.0048579479,     -0.0000271474,     +0.9999881997,    },
                        { +0.00000001177656, -0.00000000006582, +0.00000242410173 }
                    }
                },
                {
                    {
                        { -0.000551,         -0.238565,         +0.435739        },
                        { +0.99994704,       -0.01118251,       -0.00485767       }
                    },
                    {
                        { +0.238514,         -0.002667,         -0.008541        },
                        { +0.01118251,       +0.99995883,       -0.00002718       }
                    },
                    {
                        { -0.435623,         +0.012254,         +0.002117         },
                        { +0.00485767,       -0.00002714,       +1.00000956       }
                    }
                }
            };

            /*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

            /* The FK4 data (units radians and arcsec per tropical century). */
            r = r1950;
            d = d1950;
            ur = dr1950 * PMF;
            ud = dd1950 * PMF;
            px = p1950;
            rv = v1950;

            /* Express as a pv-vector. */
            pxvf = px * VF;
            w = rv * pxvf;
            wwaS2pv(r, d, 1.0, ur, ud, w, r0);

            /* Allow for E-terms (cf. Seidelmann 3.591-2). */
            wwaPvmpv(r0, a, pv1);
            wwaSxp(wwaPdp(CopyArray(r0, 0), CopyArray(a, 0)), CopyArray(r0, 0), CopyArray(pv2, 0));
            wwaSxp(wwaPdp(CopyArray(r0, 0), CopyArray(a, 1)), CopyArray(r0, 0), CopyArray(pv2, 1));
            wwaPvppv(pv1, pv2, pv1);

            /* Convert pv-vector to Fricke system (cf. Seidelmann 3.591-3). */
            for (i = 0; i < 2; i++)
            {
                for (j = 0; j < 3; j++)
                {
                    w = 0.0;
                    for (k = 0; k < 2; k++)
                    {
                        for (l = 0; l < 3; l++)
                        {
                            w += em[i, j, k, l] * pv1[k, l];
                        }
                    }
                    pv2[i, j] = w;
                }
            }

            /* Revert to catalog form. */
            wwaPv2s(pv2, ref r, ref d, ref w, ref ur, ref ud, ref rd);
            if (px > TINY)
            {
                rv = rd / pxvf;
                px = px / w;
            }

            /* Return the results. */
            r2000 = wwaAnp(r);
            d2000 = d;
            dr2000 = ur / PMF;
            dd2000 = ud / PMF;
            v2000 = rv;
            p2000 = px;

            /* Finished. */
        }
    }
}
