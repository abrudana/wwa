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
        /// Observed place at a groundbased site to to ICRS astrometric RA,Dec.
        /// The caller supplies UTC, site coordinates, ambient air conditions
        /// and observing wavelength.
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
        /// <param name="type">type of coordinates - "R", "H" or "A" (Notes 1,2)</param>
        /// <param name="ob1">observed Az, HA or RA (radians; Az is N=0,E=90)</param>
        /// <param name="ob2">observed ZD or Dec (radians)</param>
        /// <param name="utc1">UTC as a 2-part...</param>
        /// <param name="utc2">...quasi Julian Date (Notes 3,4)</param>
        /// <param name="dut1">UT1-UTC (seconds, Note 5)</param>
        /// <param name="elong">longitude (radians, east +ve, Note 6)</param>
        /// <param name="phi">geodetic latitude (radians, Note 6)</param>
        /// <param name="hm">height above ellipsoid (m, geodetic Notes 6,8)</param>
        /// <param name="xp">polar motion coordinates (radians, Note 7)</param>
        /// <param name="yp">polar motion coordinates (radians, Note 7)</param>
        /// <param name="phpa">pressure at the observer (hPa = mB, Note 8)</param>
        /// <param name="tc">ambient temperature at the observer (deg C)</param>
        /// <param name="rh">relative humidity at the observer (range 0-1)</param>
        /// <param name="wl">wavelength (micrometers, Note 9)</param>
        /// <param name="rc">ICRS astrometric RA,Dec (radians)</param>
        /// <param name="dc">ICRS astrometric RA,Dec (radians)</param>
        /// <returns></returns>
        public static int wwaAtoc13(ref char type, double ob1, double ob2, double utc1, double utc2, double dut1, double elong, double phi, double hm, double xp, double yp,
              double phpa, double tc, double rh, double wl, ref double rc, ref double dc)
        {
            int j;
            wwaASTROM astrom = new wwaASTROM();
            double eo = 0, ri = 0, di = 0;

            /* Star-independent astrometry parameters. */
            j = wwaApco13(utc1, utc2, dut1, elong, phi, hm, xp, yp, phpa, tc, rh, wl, ref astrom, ref eo);

            /* Abort if bad UTC. */
            if (j < 0) return j;

            /* Transform observed to CIRS. */
            wwaAtoiq(ref type, ob1, ob2, ref astrom, ref ri, ref di);

            /* Transform CIRS to ICRS. */
            wwaAticq(ri, di, ref astrom, ref rc, ref dc);

            /* Return OK/warning status. */
            return j;
        }   
    }
}