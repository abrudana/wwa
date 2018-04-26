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
        /// Transform ICRS star data, epoch J2000.0, to CIRS.
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
        /// <param name="dc">ICRS declination at J2000.0 (radians, Note 1)</param>
        /// <param name="pr">RA proper motion (radians/year; Note 2)</param>
        /// <param name="pd">Dec proper motion (radians/year)</param>
        /// <param name="px">parallax (arcsec)</param>
        /// <param name="rv">radial velocity (km/s, +ve if receding)</param>
        /// <param name="date1">TDB as a 2-part...</param>
        /// <param name="date2">...Julian Date (Note 3)</param>
        /// <param name="ri">CIRS geocentric RA,Dec (radians)</param>
        /// <param name="di">CIRS geocentric RA,Dec (radians)</param>
        /// <param name="eo">equation of the origins (ERA-GST, Note 5)</param>
        public static void wwaAtci13(double rc, double dc, double pr, double pd, double px, double rv, double date1, double date2, ref double ri, ref double di, ref double eo)
        {
            /* Star-independent astrometry parameters */
            wwaASTROM astrom = new wwaASTROM();
            astrom.eb = new double[3];
            astrom.eh = new double[3];
            astrom.bpn = new double[3, 3];

            /* The transformation parameters. */
            wwaApci13(date1, date2, ref astrom, ref eo);

            /* ICRS (epoch J2000.0) to CIRS. */
            wwaAtciq(rc, dc, pr, pd, px, rv, ref astrom, ref ri, ref di);
        }
    }
}