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
        /// Convert position/velocity from Cartesian to spherical coordinates.
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
        /// <param name="pv">pv-vector</param>
        /// <param name="theta">longitude angle (radians)</param>
        /// <param name="phi">latitude angle (radians)</param>
        /// <param name="r">radial distance</param>
        /// <param name="td">rate of change of theta</param>
        /// <param name="pd">rate of change of phi</param>
        /// <param name="rd">rate of change of r</param>
        public static void wwaPv2s(double[,] pv, ref double theta, ref double phi, ref double r, ref double td, ref double pd, ref double rd)
        {
            double x, y, z, xd, yd, zd, rxy2, rxy, r2, rtrue, rw, xyp;

            /* Components of position/velocity vector. */
            x = pv[0, 0];
            y = pv[0, 1];
            z = pv[0, 2];
            xd = pv[1, 0];
            yd = pv[1, 1];
            zd = pv[1, 2];

            /* Component of r in XY plane squared. */
            rxy2 = x * x + y * y;

            /* Modulus squared. */
            r2 = rxy2 + z * z;

            /* Modulus. */
            rtrue = Math.Sqrt(r2);

            /* If null vector, move the origin along the direction of movement. */
            rw = rtrue;
            if (rtrue == 0.0)
            {
                x = xd;
                y = yd;
                z = zd;
                rxy2 = x * x + y * y;
                r2 = rxy2 + z * z;
                rw = Math.Sqrt(r2);
            }

            /* Position and velocity in spherical coordinates. */
            rxy = Math.Sqrt(rxy2);
            xyp = x * xd + y * yd;
            if (rxy2 != 0.0)
            {
                theta = Math.Atan2(y, x);
                phi = Math.Atan2(z, rxy);
                td = (x * yd - y * xd) / rxy2;
                pd = (zd * rxy2 - z * xyp) / (r2 * rxy);
            }
            else
            {
                theta = 0.0;
                phi = (z != 0.0) ? Math.Atan2(z, rxy) : 0.0;
                td = 0.0;
                pd = 0.0;
            }
            r = rtrue;
            rd = (rw != 0.0) ? (xyp + z * zd) / rw : 0.0;

            return;
        }
    }
}
