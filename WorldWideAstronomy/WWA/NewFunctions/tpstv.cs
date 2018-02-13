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
        /// In the tangent plane projection, given the star's rectangular
        /// coordinates and the direction cosines of the tangent point, solve
        /// for the direction cosines of the star.
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
        /// <param name="xi">rectangular coordinates of star image (Note 2)</param>
        /// <param name="eta">rectangular coordinates of star image (Note 2)</param>
        /// <param name="v0">tangent point's direction cosines</param>
        /// <param name="v">star's direction cosines</param>
        public static void wwaTpstv(double xi, double eta, double[] v0, double[] v)
        {
            double x, y, z, f, r;

            /* Tangent point. */
            x = v0[0];
            y = v0[1];
            z = v0[2];

            /* Deal with polar case. */
            r = Math.Sqrt(x * x + y * y);
            if (r == 0.0)
            {
                r = 1e-20;
                x = r;
            }

            /* Star vector length to tangent plane. */
            f = Math.Sqrt(1.0 + xi * xi + eta * eta);

            /* Apply the transformation and normalize. */
            v[0] = (x - (xi * y + eta * x * z) / r) / f;
            v[1] = (y + (xi * x - eta * y * z) / r) / f;
            v[2] = (z + eta * r) / f;
        }
    }
}
