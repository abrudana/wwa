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
        /// parameters for transformations between ICRS and geocentric CIRS
        /// coordinates.  The caller supplies the date, and SOFA models are used
        /// to predict the Earth ephemeris and CIP/CIO.
        /// 
        /// The parameters produced by this function are required in the
        /// parallax, light deflection, aberration, and bias-precession-nutation
        /// parts of the astrometric transformation chain.
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
        /// <param name="astrom">star-independent astrometry parameters</param>
        /// <param name="eo">equation of the origins (ERA-GST)</param>
        public static void wwaApci13(double date1, double date2, ref wwaASTROM astrom, ref double eo)
        {
            double[,] ehpv = new double[2, 3] { { 0, 0, 0 }, { 0, 0, 0 } };
            double[,] ebpv = new double[2, 3] { { 0, 0, 0 }, { 0, 0, 0 } };

            double[,] r = new double[3,3];
            double x = 0, y = 0, s;

            /* Earth barycentric & heliocentric position/velocity (au, au/d). */
            wwaEpv00(date1, date2, ehpv, ebpv);

            /* Form the equinox based BPN matrix, IAU 2006/2000A. */
            wwaPnm06a(date1, date2, r);

            /* Extract CIP X,Y. */
            wwaBpn2xy(r, ref x, ref y);

            /* Obtain CIO locator s. */
            s = wwaS06(date1, date2, x, y);

            /* Compute the star-independent astrometry parameters. */
            wwaApci(date1, date2, ebpv, CopyArray(ehpv, 0), x, y, s, ref astrom);

            /* Equation of the origins. */
            eo = wwaEors(r, s);
        }
    }
}