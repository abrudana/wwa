using System;

namespace WorldWideAstronomy
{
    public static partial class WWA
    {
        /// <summary>
        /// Convert a B1950.0 FK4 star position to J2000.0 FK5, assuming zero proper motion in the FK5 system.
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
        public static void wwaFk45z(double r1950, double d1950, double bepoch, ref double r2000, ref double d2000)
        {
            /* Radians per year to arcsec per century */
            const double PMF = 100.0 * DR2AS;

            /* Position and position+velocity vectors */
            double[] r0 = new double[3];
            double[] p = new double[3];
            double[,] pv = new double[2, 3];

            /* Miscellaneous */
            double w, djm0 = 0, djm = 0;
            int i, j, k;

            /*
            ** CANONICAL CONSTANTS (Seidelmann 1992)
            */

            /* Vectors A and Adot (Seidelmann 3.591-2) */
            double[] a = new double[3] { -1.62557e-6, -0.31919e-6, -0.13843e-6 };
            double[] ad = new double[3] { +1.245e-3, -1.580e-3, -0.659e-3 };

            /* 3x2 matrix of p-vectors (cf. Seidelmann 3.591-4, matrix M) */
            double[,,] em = new double[2, 3, 3]
            {
                {
                    { +0.9999256782, -0.0111820611, -0.0048579477 },
                    { +0.0111820610, +0.9999374784, -0.0000271765 },
                    { +0.0048579479, -0.0000271474, +0.9999881997 }
                },
                {
                    { -0.000551,     -0.238565,     +0.435739     },
                    { +0.238514,     -0.002667,     -0.008541     },
                    { -0.435623,     +0.012254,     +0.002117     }
                }
            };

            /*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

            /* Spherical coordinates to p-vector. */
            wwaS2c(r1950, d1950, r0);

            /* Adjust p-vector A to give zero proper motion in FK5. */
            w = (bepoch - 1950) / PMF;
            wwaPpsp(a, w, ad, p);

            /* Remove E-terms. */
            wwaPpsp(p, -wwaPdp(r0, p), r0, p);
            wwaPmp(r0, p, p);

            /* Convert to Fricke system pv-vector (cf. Seidelmann 3.591-3). */
            for (i = 0; i < 2; i++)
            {
                for (j = 0; j < 3; j++)
                {
                    w = 0.0;
                    for (k = 0; k < 3; k++)
                    {
                        w += em[i, j, k] * p[k];
                    }
                    pv[i, j] = w;
                }
            }

            /* Allow for fictitious proper motion. */
            wwaEpb2jd(bepoch, ref djm0, ref djm);
            w = (wwaEpj(djm0, djm) - 2000.0) / PMF;
            wwaPvu(w, pv, pv);

            /* Revert to spherical coordinates. */
            wwaC2s(CopyArray(pv, 0), ref w, ref d2000);
            r2000 = wwaAnp(w);

            /* Finished. */

        }
    }
}
