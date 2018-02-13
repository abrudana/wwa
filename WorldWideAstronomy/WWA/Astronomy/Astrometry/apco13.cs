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
        /// coordinates.  The caller supplies UTC, site coordinates, ambient air
        /// conditions and observing wavelength, and SOFA models are used to
        /// obtain the Earth ephemeris, CIP/CIO and refraction constants.
        /// 
        /// The parameters produced by this function are required in the
        /// parallax, light deflection, aberration, and bias-precession-nutation
        /// parts of the ICRS/CIRS transformations.
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
        /// <param name="utc1">UTC as a 2-part...</param>
        /// <param name="utc2">...quasi Julian Date (Notes 1,2)</param>
        /// <param name="dut1">UT1-UTC (seconds, Note 3)</param>
        /// <param name="elong">longitude (radians, east +ve, Note 4)</param>
        /// <param name="phi">latitude (geodetic, radians, Note 4)</param>
        /// <param name="hm">height above ellipsoid (m, geodetic, Notes 4,6)</param>
        /// <param name="xp">polar motion coordinates (radians, Note 5)</param>
        /// <param name="yp">polar motion coordinates (radians, Note 5)</param>
        /// <param name="phpa">pressure at the observer (hPa = mB, Note 6)</param>
        /// <param name="tc">ambient temperature at the observer (deg C)</param>
        /// <param name="rh">relative humidity at the observer (range 0-1)</param>
        /// <param name="wl">wavelength (micrometers, Note 7)</param>
        /// <param name="astrom">star-independent astrometry parameters</param>
        /// <param name="eo">equation of the origins (ERA-GST)</param>
        /// <returns></returns>
        public static int wwaApco13(double utc1, double utc2, double dut1, double elong, double phi, double hm, double xp, double yp, double phpa, double tc, double rh, 
            double wl, ref wwaASTROM astrom, ref double eo)
        {
            int j;
            double tai1 = 0, tai2 = 0, tt1 = 0, tt2 = 0, ut11 = 0, ut12 = 0, x = 0, y = 0, s, theta, sp, refa = 0, refb = 0;
            double[,] ehpv = new double[2, 3] { { 0, 0, 0 }, { 0, 0, 0 } };
            double[,] ebpv = new double[2, 3] { { 0, 0, 0 }, { 0, 0, 0 } };
            double[,] r = new double[3,3];

            /* UTC to other time scales. */
            j = wwaUtctai(utc1, utc2, ref tai1, ref tai2);
            if ( j < 0 ) return -1;
            j = wwaTaitt(tai1, tai2, ref tt1, ref tt2);
            j = wwaUtcut1(utc1, utc2, dut1, ref ut11, ref ut12);
            if ( j < 0 ) return -1;

             /* Earth barycentric & heliocentric position/velocity (au, au/d). */
            wwaEpv00(tt1, tt2, ehpv, ebpv);

             /* Form the equinox based BPN matrix, IAU 2006/2000A. */
            wwaPnm06a(tt1, tt2, r);

             /* Extract CIP X,Y. */
            wwaBpn2xy(r, ref x, ref y);

             /* Obtain CIO locator s. */
            s = wwaS06(tt1, tt2, x, y);

             /* Earth rotation angle. */
            theta = wwaEra00(ut11, ut12);

             /* TIO locator s'. */
            sp = wwaSp00(tt1, tt2);

             /* Refraction constants A and B. */
            wwaRefco(phpa, tc, rh, wl, ref refa, ref refb);

             /* Compute the star-independent astrometry parameters. */
            wwaApco(tt1, tt2, ebpv, CopyArray(ehpv, 0), x, y, s, theta, elong, phi, hm, xp, yp, sp, refa, refb, ref astrom);

             /* Equation of the origins. */
            eo = wwaEors(r, s);

             /* Return any warning status. */
            return j;
        }
    }
}