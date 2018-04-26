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
        /// coordinates.  The Earth ephemeris and CIP/CIO are supplied by the
        /// caller.
        /// 
        /// The parameters produced by this function are required in the
        /// parallax, light deflection, aberration, and bias-precession-nutation
        /// parts of the astrometric transformation chain.
        /// </summary>
        /// 
        /// /// <remarks>
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
        /// <param name="ebpv">Earth barycentric position/velocity (au, au/day)</param>
        /// <param name="ehp">Earth heliocentric position (au)</param>
        /// <param name="x">CIP X,Y (components of unit vector)</param>
        /// <param name="y">CIP X,Y (components of unit vector)</param>
        /// <param name="s"></param>
        /// <param name="astrom">the CIO locator s (radians)</param>
        public static void wwaApci(double date1, double date2, double[,] ebpv, double[] ehp, double x, double y, double s, ref wwaASTROM astrom)
        {
            /* Star-independent astrometry parameters for geocenter. */
            wwaApcg(date1, date2, ebpv, ehp, ref astrom);

            /* CIO based BPN matrix. */
            wwaC2ixys(x, y, s, astrom.bpn);
        }
    }
}