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
        /// Long-term precession matrix, including ICRS frame bias.
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
        /// <param name="rpb">precession-bias matrix, J2000.0 to date</param>
        public static void wwaLtpb(double epj, double[,] rpb)
        {
            /* Frame bias (IERS Conventions 2010, Eqs. 5.21 and 5.33) */
            const double dx = -0.016617 * DAS2R,
                         de = -0.0068192 * DAS2R,
                         dr = -0.0146 * DAS2R;

            int i;
            double[,] rp = new double[3, 3];


            /* Precession matrix. */
            wwaLtp(epj, rp);

            /* Apply the bias. */
            for (i = 0; i < 3; i++)
            {
                rpb[i, 0] = rp[i, 0] - rp[i, 1] * dr + rp[i, 2] * dx;
                rpb[i, 1] = rp[i, 0] * dr + rp[i, 1] + rp[i, 2] * de;
                rpb[i, 2] = -rp[i, 0] * dx - rp[i, 1] * de + rp[i, 2];
            }
        }

    }
}
