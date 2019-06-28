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
        /// Convert position/velocity from spherical to Cartesian coordinates.
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
        /// <param name="theta">longitude angle (radians)</param>
        /// <param name="phi">latitude angle (radians)</param>
        /// <param name="r">radial distance</param>
        /// <param name="td">rate of change of theta</param>
        /// <param name="pd">rate of change of phi</param>
        /// <param name="rd">rate of change of r</param>
        /// <param name="pv">pv-vector</param>
        public static void wwaS2pv(double theta, double phi, double r, double td, double pd, double rd, double[,] pv)
        {
            double st, ct, sp, cp, rcp, x, y, rpd, w;

            st = Math.Sin(theta);
            ct = Math.Cos(theta);
            sp = Math.Sin(phi);
            cp = Math.Cos(phi);
            rcp = r * cp;
            x = rcp * ct;
            y = rcp * st;
            rpd = r * pd;
            w = rpd * sp - cp * rd;

            pv[0, 0] = x;
            pv[0, 1] = y;
            pv[0, 2] = r * sp;
            pv[1, 0] = -y * td - w * ct;
            pv[1, 1] = x * td - w * st;
            pv[1, 2] = rpd * cp + sp * rd;

            return;
        }
    }
}
