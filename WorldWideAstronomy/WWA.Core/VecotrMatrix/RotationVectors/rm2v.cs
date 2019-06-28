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
        /// Express an r-matrix as an r-vector.
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
        /// <param name="r">rotation matrix</param>
        /// Returned:
        /// <param name="w"rotation vector (Note 1)></param>
        public static void wwaRm2v(double[,] r, double[] w)
        {
            double x, y, z, s2, c2, phi, f;

            x = r[1, 2] - r[2, 1];
            y = r[2, 0] - r[0, 2];
            z = r[0, 1] - r[1, 0];
            s2 = Math.Sqrt(x * x + y * y + z * z);
            //if (s2 != 0)
            if (s2 > 0)
            {
                c2 = r[0, 0] + r[1, 1] + r[2, 2] - 1.0;
                phi = Math.Atan2(s2, c2);
                f = phi / s2;
                w[0] = x * f;
                w[1] = y * f;
                w[2] = z * f;
            }
            else
            {
                w[0] = 0.0;
                w[1] = 0.0;
                w[2] = 0.0;
            }

            return;
        }
    }
}
