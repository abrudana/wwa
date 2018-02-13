using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldWideAstronomy
{
    //public static partial class SOFA
    //{
    //    /// <summary>
    //    /// Position and velocity of a terrestrial observing station.
    //    /// </summary>
    //    /// 
    //    /// <remarks>
    //    /// World Wide Astronomy - WWA
    //    /// Set of C# algorithms and procedures that implement standard models used in fundamental astronomy.
    //    /// 
    //    /// This program is derived from the International Astronomical Union's
    //    /// SOFA (Standards of Fundamental Astronomy) software collection.
    //    /// http://www.iausofa.org
    //    /// 
    //    /// The WWA code does not itself constitute software provided by and/or endorsed by SOFA.
    //    /// This version is intended to retain identical functionality to the SOFA library, but
    //    /// made distinct through different function names (prefixes) and C# language specific
    //    /// modifications in code.
    //    /// 
    //    /// Contributor
    //    /// Attila Abrudán
    //    /// 
    //    /// Please read the ReadMe.1st text file for more information.
    //    /// </remarks>
    //    /// <param name="elong">longitude (radians, east +ve, Note 1)</param>
    //    /// <param name="phi">latitude (geodetic, radians, Note 1)</param>
    //    /// <param name="hm">height above ref. ellipsoid (geodetic, m)</param>
    //    /// <param name="xp">coordinates of the pole (radians, Note 2)</param>
    //    /// <param name="yp">coordinates of the pole (radians, Note 2)</param>
    //    /// <param name="sp">the TIO locator s' (radians, Note 2)</param>
    //    /// <param name="theta">Earth rotation angle (radians, Note 3)</param>
    //    /// <param name="pv">position/velocity vector (m, m/s, CIRS)</param>
    //    public static void wwaPvtob(double elong, double phi, double hm, double xp, double yp, double sp, double theta, double[,] pv)
    //    {
    //        /* Earth rotation rate in radians per UT1 second */
    //        const double OM = 1.00273781191135448 * D2PI / DAYSEC;

    //        double[] xyzm = new double[3];
    //        double[] xyz = new double[3];
    //        double[,] rpm = new double[3, 3];
    //        double x, y, z, s, c;

    //        /* Geodetic to geocentric transformation (WGS84). */
    //        wwaGd2gc(1, elong, phi, hm, xyzm);

    //        /* Polar motion and TIO position. */
    //        wwaPom00(xp, yp, sp, rpm);
    //        wwaTrxp(rpm, xyzm, xyz);
    //        x = xyz[0];
    //        y = xyz[1];
    //        z = xyz[2];

    //        /* Functions of ERA. */
    //        s = Math.Sin(theta);
    //        c = Math.Cos(theta);

    //        /* Position. */
    //        pv[0, 0] = c * x - s * y;
    //        pv[0, 1] = s * x + c * y;
    //        pv[0, 2] = z;

    //        /* Velocity. */
    //        pv[1, 0] = OM * (-s * x - c * y);
    //        pv[1, 1] = OM * (c * x - s * y);
    //        pv[1, 2] = 0.0;
    //    }
    //}
}