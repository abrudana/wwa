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
        /// P-vector to spherical polar coordinates.
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
        /// <param name="p">p-vector</param>
        /// <param name="theta">longitude angle (radians)</param>
        /// <param name="phi">latitude angle (radians)</param>
        /// <param name="r">radial distance</param>
        public static void wwaP2s(double[] p, ref double theta, ref double phi, ref double r)
        {
            wwaC2s(p, ref theta, ref phi);
            r = wwaPm(p);

            return;
        }
    }
}
