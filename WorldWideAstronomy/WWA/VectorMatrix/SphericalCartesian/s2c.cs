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
        /// Convert spherical coordinates to Cartesian.
        /// </summary>
        /// 
        /// <remarks>
        /// This function is derived from the International Astronomical Union's
        /// SOFA (Standards of Fundamental Astronomy) software collection.
        /// http://www.iausofa.org
        /// The code does not itself constitute software provided by and/or endorsed by SOFA.
        /// This version is intended to retain identical functionality to the SOFA library, but
        /// made distinct through different function names (prefixes) and C# language specific
        /// modifications in code.
        /// </remarks>
        /// <param name="theta">longitude angle (radians)</param>
        /// <param name="phi">latitude angle (radians)</param>
        /// <param name="c">direction cosines</param>
        public static void wwaS2c(double theta, double phi, double[] c)
        {
            double cp;

            cp = Math.Cos(phi);
            c[0] = Math.Cos(theta) * cp;
            c[1] = Math.Sin(theta) * cp;
            c[2] = Math.Sin(phi);

            return;
        }
    }
}
