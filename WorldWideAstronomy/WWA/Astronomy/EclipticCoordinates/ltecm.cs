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
        /// ICRS equatorial to ecliptic rotation matrix, long-term.
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
        /// <param name="rm">ICRS to ecliptic rotation matrix</param>
        public static void wwaLtecm(double epj, double[,] rm)
        {
            /* Frame bias (IERS Conventions 2010, Eqs. 5.21 and 5.33) */
            const double dx = -0.016617 * DAS2R,
                         de = -0.0068192 * DAS2R,
                         dr = -0.0146 * DAS2R;

            double[] p = new double[3];
            double[] z = new double[3];
            double[] w = new double[3];
            double s = 0;
            double[] x = new double[3];
            double[] y = new double[3];


            /* Equator pole. */
            wwaLtpequ(epj, p);

            /* Ecliptic pole (bottom row of equatorial to ecliptic matrix). */
            wwaLtpecl(epj, z);

            /* Equinox (top row of matrix). */
            wwaPxp(p, z, w);
            wwaPn(w, ref s, x);

            /* Middle row of matrix. */
            wwaPxp(z, x, y);

            /* Combine with frame bias. */
            rm[0, 0] = x[0] - x[1] * dr + x[2] * dx;
            rm[0, 1] = x[0] * dr + x[1] + x[2] * de;
            rm[0, 2] = -x[0] * dx - x[1] * de + x[2];
            rm[1, 0] = y[0] - y[1] * dr + y[2] * dx;
            rm[1, 1] = y[0] * dr + y[1] + y[2] * de;
            rm[1, 2] = -y[0] * dx - y[1] * de + y[2];
            rm[2, 0] = z[0] - z[1] * dr + z[2] * dx;
            rm[2, 1] = z[0] * dr + z[1] + z[2] * de;
            rm[2, 2] = -z[0] * dx - z[1] * de + z[2];
        }
    }
}
