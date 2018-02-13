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
        /// coordinates and the spherical coordinates of the tangent point,
        /// solve for the spherical coordinates of the star.
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
        /// <param name="a0">tangent point's spherical coordinates</param>
        /// <param name="b0">tangent point's spherical coordinates</param>
        /// <param name="a">star's spherical coordinates</param>
        /// <param name="b">star's spherical coordinates</param>
        public static void wwaTpsts(double xi, double eta, double a0, double b0, ref double a, ref double b)
        {
            double sb0, cb0, d;

            sb0 = Math.Sin(b0);
            cb0 = Math.Cos(b0);
            d = cb0 - eta * sb0;
            a = wwaAnp(Math.Atan2(xi, d) + a0);
            b = Math.Atan2(sb0 + eta * cb0, Math.Sqrt(xi * xi + d * d));
        }
    }
}
