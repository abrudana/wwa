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
        /// ICRS RA,Dec to observed place.  The caller supplies UTC, site
        /// coordinates, ambient air conditions and observing wavelength.
        /// 
        /// SOFA models are used for the Earth ephemeris, bias-precession-
        /// nutation, Earth orientation and refraction.
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
        /// <param name="rc">ICRS right ascension at J2000.0 (radians, Note 1)</param>
        /// <param name="dc">ICRS right ascension at J2000.0 (radians, Note 1)</param>
        /// <param name="pr">RA proper motion (radians/year; Note 2)</param>
        /// <param name="pd">Dec proper motion (radians/year)</param>
        /// <param name="px">parallax (arcsec)</param>
        /// <param name="rv">radial velocity (km/s, +ve if receding)</param>
        /// <param name="utc1">UTC as a 2-part...</param>
        /// <param name="utc2">...quasi Julian Date (Notes 3-4)</param>
        /// <param name="dut1">UT1-UTC (seconds, Note 5)</param>
        /// <param name="elong">longitude (radians, east +ve, Note 6)</param>
        /// <param name="phi">latitude (geodetic, radians, Note 6)</param>
        /// <param name="hm">height above ellipsoid (m, geodetic, Notes 6,8)</param>
        /// <param name="xp">polar motion coordinates (radians, Note 7)</param>
        /// <param name="yp">polar motion coordinates (radians, Note 7)</param>
        /// <param name="phpa">pressure at the observer (hPa = mB, Note 8)</param>
        /// <param name="tc">ambient temperature at the observer (deg C)</param>
        /// <param name="rh">relative humidity at the observer (range 0-1)</param>
        /// <param name="wl">wavelength (micrometers, Note 9)</param>
        /// <param name="aob">observed azimuth (radians: N=0,E=90)</param>
        /// <param name="zob">observed zenith distance (radians)</param>
        /// <param name="hob">observed hour angle (radians)</param>
        /// <param name="dob">observed declination (radians)</param>
        /// <param name="rob">observed right ascension (CIO-based, radians)</param>
        /// <param name="eo">equation of the origins (ERA-GST)</param>
        /// <returns>status: 
        /// +1 = dubious year (Note 4)
        /// 0 = OK
        /// -1 = unacceptable date
        /// </returns>
        public static int wwaAtco13(double rc, double dc, double pr, double pd, double px, double rv, double utc1, double utc2, double dut1,
              double elong, double phi, double hm, double xp, double yp, double phpa, double tc, double rh, double wl,
              ref double aob, ref double zob, ref double hob, ref double dob, ref double rob, ref double eo)
        {
            int j;
            wwaASTROM astrom = new wwaASTROM();

            astrom.eb = new double[3];
            astrom.eh = new double[3];
            astrom.bpn = new double[3, 3];

            double ri = 0, di = 0;

            /* Star-independent astrometry parameters. */
            j = wwaApco13(utc1, utc2, dut1, elong, phi, hm, xp, yp, phpa, tc, rh, wl, ref astrom, ref eo);

            /* Abort if bad UTC. */
            if (j < 0) return j;

            /* Transform ICRS to CIRS. */
            wwaAtciq(rc, dc, pr, pd, px, rv, ref astrom, ref ri, ref di);

            /* Transform CIRS to observed. */
            wwaAtioq(ri, di, ref astrom, ref aob, ref zob, ref hob, ref dob, ref rob);

            /* Return OK/warning status. */
            return j;
        }
    }
}