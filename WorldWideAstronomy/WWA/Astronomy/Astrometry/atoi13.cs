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
        /// Observed place to CIRS.  The caller supplies UTC, site coordinates,
        /// ambient air conditions and observing wavelength.
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
        /// <param name="hm">height above the ellipsoid (meters, Notes 6,8)</param>
        /// <param name="xp">polar motion coordinates (radians, Note 7)</param>
        /// <param name="yp">polar motion coordinates (radians, Note 7)</param>
        /// <param name="phpa">pressure at the observer (hPa = mB, Note 8)</param>
        /// <param name="tc">ambient temperature at the observer (deg C)</param>
        /// <param name="rh">relative humidity at the observer (range 0-1)</param>
        /// <param name="wl">wavelength (micrometers, Note 9)</param>
        /// <param name="ri">CIRS right ascension (CIO-based, radians)</param>
        /// <param name="di">CIRS declination (radians)</param>
        /// <returns>status:
        /// +1 = dubious year (Note 2)
        /// 0 = OK
        /// -1 = unacceptable date
        /// </returns>
        public static int wwaAtoi13(ref char type, double ob1, double ob2, double utc1, double utc2, double dut1,
              double elong, double phi, double hm, double xp, double yp, double phpa, double tc, double rh, double wl,
              ref double ri, ref double di)
        {
            int j;
            wwaASTROM astrom = new wwaASTROM();

            /* Star-independent astrometry parameters for CIRS->observed. */
            j = wwaApio13(utc1, utc2, dut1, elong, phi, hm, xp, yp, phpa, tc, rh, wl, ref astrom);

            /* Abort if bad UTC. */
            if (j < 0) return j;

            /* Transform observed to CIRS. */
            wwaAtoiq(ref type, ob1, ob2, ref astrom, ref ri, ref di);

            /* Return OK/warning status. */
            return j;
        }
    }
}