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
        /// P-vector to spherical coordinates.
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
        /// <param name="p">p-vector</param>
        /// <param name="theta">longitude angle (radians)</param>
        /// <param name="phi">latitude angle (radians)</param>
        public static void wwaC2s(double[] p, ref double theta, ref double phi)
        {
            double x, y, z, d2;

            x = p[0];
            y = p[1];
            z = p[2];
            d2 = x * x + y * y;

            theta = (d2 == 0.0) ? 0.0 : Math.Atan2(y, x);
            phi = (z == 0.0) ? 0.0 : Math.Atan2(z, Math.Sqrt(d2));

            return;
        }
    }
}
