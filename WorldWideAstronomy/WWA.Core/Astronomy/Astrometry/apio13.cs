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
        /// coordinates.  The caller supplies UTC, site coordinates, ambient air
        /// conditions and observing wavelength.
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
        /// <param name="dut1">UT1-UTC (seconds)</param>
        /// <param name="elong">longitude (radians, east +ve, Note 3)</param>
        /// <param name="phi">geodetic latitude (radians, Note 3)</param>
        /// <param name="hm">height above ellipsoid (m, geodetic Notes 4,6)</param>
        /// <param name="xp">polar motion coordinates (radians, Note 5)</param>
        /// <param name="yp">polar motion coordinates (radians, Note 5)</param>
        /// <param name="phpa">pressure at the observer (hPa = mB, Note 6)</param>
        /// <param name="tc">ambient temperature at the observer (deg C)</param>
        /// <param name="rh">relative humidity at the observer (range 0-1)</param>
        /// <param name="wl">wavelength (micrometers, Note 7)</param>
        /// <param name="astrom">star-independent astrometry parameters:</param>
        /// <returns></returns>
        public static int wwaApio13(double utc1, double utc2, double dut1, double elong, double phi, double hm, double xp, double yp,
              double phpa, double tc, double rh, double wl, ref wwaASTROM astrom)
        {
            int j;
            double tai1 = 0, tai2 = 0, tt1 = 0, tt2 = 0, ut11 = 0, ut12 = 0, sp, theta, refa = 0, refb = 0;

            /* UTC to other time scales. */
            j = wwaUtctai(utc1, utc2, ref tai1, ref tai2);
            if (j < 0) return -1;
            j = wwaTaitt(tai1, tai2, ref tt1, ref tt2);
            j = wwaUtcut1(utc1, utc2, dut1, ref ut11, ref ut12);
            if (j < 0) return -1;

            /* TIO locator s'. */
            sp = wwaSp00(tt1, tt2);

            /* Earth rotation angle. */
            theta = wwaEra00(ut11, ut12);

            /* Refraction constants A and B. */
            wwaRefco(phpa, tc, rh, wl, ref refa, ref refb);

            /* CIRS <-> observed astrometry parameters. */
            wwaApio(sp, theta, elong, phi, hm, xp, yp, refa, refb, ref astrom);

            /* Return any warning status. */
            return j;
        }
    }
}