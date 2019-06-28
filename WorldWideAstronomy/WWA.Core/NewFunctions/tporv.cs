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
        /// of a star and its direction cosines, determine the direction
        /// cosines of the tangent point.
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
        /// <param name="v">star's direction cosines (Note 3)</param>
        /// <param name="v01">tangent point's direction cosines, Solution 1</param>
        /// <param name="v02">tangent point's direction cosines, Solution 1</param>
        public static int wwaTporv(double xi, double eta, double[] v, double[] v01, double[] v02)
        {
            double x, y, z, rxy2, xi2, eta2p1, r, rsb, rcb, w2, w, c;

            x = v[0];
            y = v[1];
            z = v[2];
            rxy2 = x * x + y * y;
            xi2 = xi * xi;
            eta2p1 = eta * eta + 1.0;
            r = Math.Sqrt(xi2 + eta2p1);
            rsb = r * z;
            rcb = r * Math.Sqrt(x * x + y * y);
            w2 = rcb * rcb - xi2;
            if (w2 > 0.0)
            {
                w = Math.Sqrt(w2);
                c = (rsb * eta + w) / (eta2p1 * Math.Sqrt(rxy2 * (w2 + xi2)));
                v01[0] = c * (x * w + y * xi);
                v01[1] = c * (y * w - x * xi);
                v01[2] = (rsb - eta * w) / eta2p1;
                w = -w;
                c = (rsb * eta + w) / (eta2p1 * Math.Sqrt(rxy2 * (w2 + xi2)));
                v02[0] = c * (x * w + y * xi);
                v02[1] = c * (y * w - x * xi);
                v02[2] = (rsb - eta * w) / eta2p1;
                return (Math.Abs(rsb) < 1.0) ? 1 : 2;
            }
            else
            {
                return 0;
            }
        }
    }
}
