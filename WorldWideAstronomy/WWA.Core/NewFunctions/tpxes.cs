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
        /// In the tangent plane projection, given celestial spherical
        /// coordinates for a star and the tangent point, solve for the star's
        /// rectangular coordinates in the tangent plane.
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
        /// <param name="a">star's spherical coordinates</param>
        /// <param name="b">star's spherical coordinates</param>
        /// <param name="a0">tangent point's spherical coordinates</param>
        /// <param name="b0">tangent point's spherical coordinates</param>
        /// <param name="xi">rectangular coordinates of star image (Note 2)</param>
        /// <param name="eta">rectangular coordinates of star image (Note 2)</param>
        /// <returns>status:  0 = OK, 
        ///                 1 = star too far from axis, 
        ///                 2 = antistar on tangent plane
        ///                 3 = antistar too far from axis
        /// </returns>
        public static int wwaTpxes(double a, double b, double a0, double b0, ref double xi, ref double eta)
        {
            const double TINY = 1e-6;
            int j;
            double sb0, sb, cb0, cb, da, sda, cda, d;


            /* Functions of the spherical coordinates. */
            sb0 = Math.Sin(b0);
            sb = Math.Sin(b);
            cb0 = Math.Cos(b0);
            cb = Math.Cos(b);
            da = a - a0;
            sda = Math.Sin(da);
            cda = Math.Cos(da);

            /* Reciprocal of star vector length to tangent plane. */
            d = sb * sb0 + cb * cb0 * cda;

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
            xi = cb * sda / d;
            eta = (sb * cb0 - cb * sb0 * cda) / d;

            /* Return the status. */
            return j;
        }
    }
}
