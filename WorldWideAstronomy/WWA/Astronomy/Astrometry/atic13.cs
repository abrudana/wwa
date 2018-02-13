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
        /// Transform star RA,Dec from geocentric CIRS to ICRS astrometric.
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
        /// <param name="ri">CIRS geocentric RA,Dec (radians)</param>
        /// <param name="di">CIRS geocentric RA,Dec (radians)</param>
        /// <param name="date1">TDB as a 2-part...</param>
        /// <param name="date2">...Julian Date (Note 1)</param>
        /// <param name="rc">ICRS astrometric RA,Dec (radians)</param>
        /// <param name="dc">ICRS astrometric RA,Dec (radians)</param>
        /// <param name="eo">equation of the origins (ERA-GST, Note 4)</param>
        public static void wwaAtic13(double ri, double di, double date1, double date2, ref double rc, ref double dc, ref double eo)
        {
            /* Star-independent astrometry parameters */
            wwaASTROM astrom = new wwaASTROM();

            /* Star-independent astrometry parameters. */
            wwaApci13(date1, date2, ref astrom, ref eo);

            /* CIRS to ICRS astrometric. */
            wwaAticq(ri, di, ref astrom, ref rc, ref dc);
        }
    }
}