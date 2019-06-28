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
        /// Form the r-matrix corresponding to a given r-vector.
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
        /// <param name="w">rotation vector (Note 1)</param>
        /// Returned:
        /// <param name="r">rotation matrix</param>
        public static void wwaRv2m(double[] w, double[,] r)
        {
            double x, y, z, phi, s, c, f;

            /* Euler angle (magnitude of rotation vector) and functions. */
            x = w[0];
            y = w[1];
            z = w[2];
            phi = Math.Sqrt(x * x + y * y + z * z);
            s = Math.Sin(phi);
            c = Math.Cos(phi);
            f = 1.0 - c;

            /* Euler axis (direction of rotation vector), perhaps null. */
            //if (phi != 0.0)
            if (phi > 0.0)
            {
                x /= phi;
                y /= phi;
                z /= phi;
            }

            /* Form the rotation matrix. */
            r[0, 0] = x * x * f + c;
            r[0, 1] = x * y * f + z * s;
            r[0, 2] = x * z * f - y * s;
            r[1, 0] = y * x * f - z * s;
            r[1, 1] = y * y * f + c;
            r[1, 2] = y * z * f + x * s;
            r[2, 0] = z * x * f + y * s;
            r[2, 1] = z * y * f - x * s;
            r[2, 2] = z * z * f + c;

            return;
        }
    }
}
