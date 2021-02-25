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
        /// For a terrestrial observer, prepare star-independent astrometry
        /// parameters for transformations between CIRS and observed
        /// coordinates.  The caller supplies the Earth orientation information
        /// and the refraction constants as well as the site coordinates.
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
        /// Attila Abrud�n
        /// 
        /// Please read the ReadMe.1st text file for more information.
        /// </remarks>
        /// <param name="sp">the TIO locator s' (radians, Note 1)</param>
        /// <param name="theta">Earth rotation angle (radians)</param>
        /// <param name="elong">longitude (radians, east +ve, Note 2)</param>
        /// <param name="phi">geodetic latitude (radians, Note 2)</param>
        /// <param name="hm">height above ellipsoid (m, geodetic Note 2)</param>
        /// <param name="xp">polar motion coordinates (radians, Note 3)</param>
        /// <param name="yp">polar motion coordinates (radians, Note 3)</param>
        /// <param name="refa">refraction constant A (radians, Note 4)</param>
        /// <param name="refb">refraction constant B (radians, Note 4)</param>
        /// <param name="astrom">star-independent astrometry parameters:</param>
        public static void wwaApio(double sp, double theta, double elong, double phi, double hm, double xp, double yp, double refa, double refb, ref wwaASTROM astrom)
        {
            double[,] r = new double[3, 3];
            double a, b, eral, c;
            double[,] pv = new double[2, 3];


            /* Form the rotation matrix, CIRS to apparent [HA,Dec]. */
            wwaIr(r);
            wwaRz(theta + sp, r);
            wwaRy(-xp, r);
            wwaRx(-yp, r);
            wwaRz(elong, r);

            /* Solve for local Earth rotation angle. */
            a = r[0, 0];
            b = r[0, 1];
            eral = (a != 0.0 || b != 0.0) ? Math.Atan2(b, a) : 0.0;
            astrom.eral = eral;

            /* Solve for polar motion [X,Y] with respect to local meridian. */
            a = r[0, 0];
            c = r[0, 2];
            astrom.xpl = Math.Atan2(c, Math.Sqrt(a * a + b * b));
            a = r[1, 2];
            b = r[2, 2];
            astrom.ypl = (a != 0.0 || b != 0.0) ? -Math.Atan2(a, b) : 0.0;

            /* Adjusted longitude. */
            astrom.along = wwaAnpm(eral - theta);

            /* Functions of latitude. */
            astrom.sphi = Math.Sin(phi);
            astrom.cphi = Math.Cos(phi);

            /* Observer's geocentric position and velocity (m, m/s, CIRS). */
            wwaPvtob(elong, phi, hm, xp, yp, sp, theta, pv);

            /* Magnitude of diurnal aberration vector. */
            astrom.diurab = Math.Sqrt(pv[1, 0] * pv[1, 0] + pv[1, 1] * pv[1, 1]) / CMPS;

            /* Refraction constants. */
            astrom.refa = refa;
            astrom.refb = refb;
        }   
    }
}