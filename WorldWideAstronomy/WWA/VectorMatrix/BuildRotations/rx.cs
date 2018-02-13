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
        /// Rotate an r-matrix about the x-axis.
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
        /// <param name="phi">angle (radians)</param>
        /// Given and returned:
        /// <param name="r">r-matrix, rotated</param>
        public static void wwaRx(double phi, double[,] r)
        {
            double s, c, a10, a11, a12, a20, a21, a22;

            s = Math.Sin(phi);
            c = Math.Cos(phi);

            a10 = c * r[1, 0] + s * r[2, 0];
            a11 = c * r[1, 1] + s * r[2, 1];
            a12 = c * r[1, 2] + s * r[2, 2];
            a20 = -s * r[1, 0] + c * r[2, 0];
            a21 = -s * r[1, 1] + c * r[2, 1];
            a22 = -s * r[1, 2] + c * r[2, 2];

            r[1, 0] = a10;
            r[1, 1] = a11;
            r[1, 2] = a12;
            r[2, 0] = a20;
            r[2, 1] = a21;
            r[2, 2] = a22;

            return;
        }
    }
}
