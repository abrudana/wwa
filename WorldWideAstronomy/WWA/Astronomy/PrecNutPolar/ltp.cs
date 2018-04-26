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
        /// Long-term precession matrix.
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
        /// <param name="epj">Julian epoch (TT)</param>
        /// <param name="rp">precession matrix, J2000.0 to date</param>
        public static void wwaLtp(double epj, double[,] rp)
        {
            int i;
            double[] peqr = new double[3];
            double[] pecl = new double[3];
            double[] v = new double[3];
            double w = 0;
            double[] eqx = new double[3];


            /* Equator pole (bottom row of matrix). */
            wwaLtpequ(epj, peqr);

            /* Ecliptic pole. */
            wwaLtpecl(epj, pecl);

            /* Equinox (top row of matrix). */
            wwaPxp(peqr, pecl, v);
            wwaPn(v, ref w, eqx);

            /* Middle row of matrix. */
            wwaPxp(peqr, eqx, v);

            /* Assemble the matrix. */
            for (i = 0; i < 3; i++)
            {
                rp[0, i] = eqx[i];
                rp[1, i] = v[i];
                rp[2, i] = peqr[i];
            }
        }
    }
}
