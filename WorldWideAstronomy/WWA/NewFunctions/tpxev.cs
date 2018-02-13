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
        /// In the tangent plane projection, given celestial direction cosines
        /// for a star and the tangent point, solve for the star's rectangular
        /// coordinates in the tangent plane.
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
        /// <param name="v">direction cosines of star (Note 4)</param>
        /// <param name="v0">direction cosines of tangent point (Note 4)</param>
        /// <param name="xi">tangent plane coordinates of star</param>
        /// <param name="eta">tangent plane coordinates of star</param>
        public static int wwaTpxev(double[] v, double[] v0, ref double xi, ref double eta)
        {
            const double TINY = 1e-6;
            int j;
            double x, y, z, x0, y0, z0, r2, r, w, d;

            /* Star and tangent point. */
            x = v[0];
            y = v[1];
            z = v[2];
            x0 = v0[0];
            y0 = v0[1];
            z0 = v0[2];

            /* Deal with polar case. */
            r2 = x0 * x0 + y0 * y0;
            r = Math.Sqrt(r2);
            if (r == 0.0)
            {
                r = 1e-20;
                x0 = r;
            }

            /* Reciprocal of star vector length to tangent plane. */
            w = x * x0 + y * y0;
            d = w + z * z0;

            /* Check for error cases. */
            if (d > TINY)
            {
                j = 0;
            }
            else if (d >= 0.0)
            {
                j = 1;
                d = TINY;
            }
            else if (d > -TINY)
            {
                j = 2;
                d = -TINY;
            }
            else
            {
                j = 3;
            }

            /* Return the tangent plane coordinates (even in dubious cases). */
            d *= r;
            xi = (y * x0 - x * y0) / d;
            eta = (z * r2 - z0 * w) / d;

            /* Return the status. */
            return j;
        }
    }
}
