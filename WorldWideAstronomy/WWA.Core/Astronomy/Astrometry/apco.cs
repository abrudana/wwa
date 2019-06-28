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
        /// parameters for transformations between ICRS and observed
        /// coordinates.  The caller supplies the Earth ephemeris, the Earth
        /// rotation information and the refraction constants as well as the
        /// site coordinates.
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
        /// <param name="date1">TDB as a 2-part...</param>
        /// <param name="date2">...Julian Date (Note 1)</param>
        /// <param name="ebpv">Earth barycentric PV (au, au/day, Note 2)</param>
        /// <param name="ehp">Earth heliocentric P (au, Note 2)</param>
        /// <param name="x">CIP X,Y (components of unit vector)</param>
        /// <param name="y">CIP X,Y (components of unit vector)</param>
        /// <param name="s">the CIO locator s (radians)</param>
        /// <param name="theta">Earth rotation angle (radians)</param>
        /// <param name="elong">longitude (radians, east +ve, Note 3)</param>
        /// <param name="phi">latitude (geodetic, radians, Note 3)</param>
        /// <param name="hm">height above ellipsoid (m, geodetic, Note 3)</param>
        /// <param name="xp">polar motion coordinates (radians, Note 4)</param>
        /// <param name="yp">polar motion coordinates (radians, Note 4)</param>
        /// <param name="sp">the TIO locator s' (radians, Note 4)</param>
        /// <param name="refa">refraction constant A (radians, Note 5)</param>
        /// <param name="refb">refraction constant B (radians, Note 5)</param>
        /// <param name="astrom">star-independent astrometry parameters</param>
        public static void wwaApco(double date1, double date2, double[,] ebpv, double[] ehp, double x, double y, double s, double theta, 
            double elong, double phi, double hm, double xp, double yp, double sp, double refa, double refb, ref wwaASTROM astrom)
        {
            double sl, cl;
            double [,] r = new double[3, 3];
            double[,] pvc = new double[2, 3] { { 0, 0, 0 }, { 0, 0, 0 } };
            double[,] pv = new double[2, 3] { { 0, 0, 0 }, { 0, 0, 0 } };

            /* Longitude with adjustment for TIO locator s'. */
            astrom.along = elong + sp;

            /* Polar motion, rotated onto the local meridian. */
            sl = Math.Sin(astrom.along);
            cl = Math.Cos(astrom.along);
            astrom.xpl = xp*cl - yp*sl;
            astrom.ypl = xp*sl + yp*cl;

            /* Functions of latitude. */
            astrom.sphi = Math.Sin(phi);
            astrom.cphi = Math.Cos(phi);

            /* Refraction constants. */
            astrom.refa = refa;
            astrom.refb = refb;

            /* Local Earth rotation angle. */
            wwaAper(theta, ref astrom);

            /* Disable the (redundant) diurnal aberration step. */
            astrom.diurab = 0.0;

            /* CIO based BPN matrix. */
            wwaC2ixys(x, y, s, r);

            /* Observer's geocentric position and velocity (m, m/s, CIRS). */
            wwaPvtob(elong, phi, hm, xp, yp, sp, theta, pvc);

            /* Rotate into GCRS. */
            wwaTrxpv(r, pvc, pv);

            /* ICRS <-> GCRS parameters. */
            wwaApcs(date1, date2, pv, ebpv, ehp, ref astrom);

            /* Store the CIO based BPN matrix. */
            wwaCr(r, astrom.bpn);
        }
    }
}