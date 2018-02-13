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
        /// In the tangent plane projection, given the rectangular coordinates
        /// of a star and its spherical coordinates, determine the spherical
        /// coordinates of the tangent point.
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
        /// </summary>
        /// <param name="xi">rectangular coordinates of star image (Note 2)</param>
        /// <param name="eta">rectangular coordinates of star image (Note 2)</param>
        /// <param name="a">star's spherical coordinates (Note 3)</param>
        /// <param name="b">star's spherical coordinates (Note 3)</param>
        /// <param name="a01">tangent point's spherical coordinates, Soln. 1</param>
        /// <param name="b01">tangent point's spherical coordinates, Soln. 1</param>
        /// <param name="a02">tangent point's spherical coordinates, Soln. 2</param>
        /// <param name="b02">tangent point's spherical coordinates, Soln. 2</param>
        public static int wwaTpors(double xi, double eta, double a, double b,
             ref double a01, ref double b01, ref double a02, ref double b02)
        {
            double xi2, r, sb, cb, rsb, rcb, w2, w, s, c;

            xi2 = xi * xi;
            r = Math.Sqrt(1.0 + xi2 + eta * eta);
            sb = Math.Sin(b);
            cb = Math.Cos(b);
            rsb = r * sb;
            rcb = r * cb;
            w2 = rcb * rcb - xi2;
            if (w2 >= 0.0)
            {
                w = Math.Sqrt(w2);
                s = rsb - eta * w;
                c = rsb * eta + w;
                if (xi == 0.0 && w == 0.0) w = 1.0;
                a01 = wwaAnp(a - Math.Atan2(xi, w));
                b01 = Math.Atan2(s, c);
                w = -w;
                s = rsb - eta * w;
                c = rsb * eta + w;
                a02 = wwaAnp(a - Math.Atan2(xi, w));
                b02 = Math.Atan2(s, c);
                return (Math.Abs(rsb) < 1.0) ? 1 : 2;
            }
            else
            {
                return 0;
            }
        }
    }
}
