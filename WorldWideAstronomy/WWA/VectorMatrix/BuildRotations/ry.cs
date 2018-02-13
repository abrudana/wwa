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
        /// Rotate an r-matrix about the y-axis.
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
        /// <param name="theta"><angle (radians)/param>
        /// Given and returned:
        /// <param name="r">r-matrix, rotated</param>
        public static void wwaRy(double theta, double[,] r)
        {
            double s, c, a00, a01, a02, a20, a21, a22;

            if (r == null)
                r = new double[3, 3];

            s = Math.Sin(theta);
            c = Math.Cos(theta);

            a00 = c * r[0, 0] - s * r[2, 0];
            a01 = c * r[0, 1] - s * r[2, 1];
            a02 = c * r[0, 2] - s * r[2, 2];
            a20 = s * r[0, 0] + c * r[2, 0];
            a21 = s * r[0, 1] + c * r[2, 1];
            a22 = s * r[0, 2] + c * r[2, 2];

            r[0, 0] = a00;
            r[0, 1] = a01;
            r[0, 2] = a02;
            r[2, 0] = a20;
            r[2, 1] = a21;
            r[2, 2] = a22;

            return;
        }
    }
}
