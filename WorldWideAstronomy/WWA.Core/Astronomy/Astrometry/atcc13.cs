using System;
using System.Collections.Generic;
using System.Text;

namespace WorldWideAstronomy
{
    public static partial class WWA
    {
        /// <summary>
        /// For a terrestrial observer, prepare star-independent astrometry
        /// parameters for transformations between CIRS and observed
        /// coordinates.  The caller supplies the Earth orientation information
        /// and the refraction constants as well as the site coordinates.
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
        /// <param name="rc"></param>
        /// <param name="dc"></param>
        /// <param name="pr"></param>
        /// <param name="pd"></param>
        /// <param name="px"></param>
        /// <param name="rv"></param>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="ra"></param>
        /// <param name="da"></param>
        public static void wwaAtcc13(double rc, double dc,
               double pr, double pd, double px, double rv,
               double date1, double date2,
               ref double ra, ref double da)
        {
            /* Star-independent astrometry parameters */
            wwaASTROM astrom = new wwaASTROM();

            double w = 0;

            /* The transformation parameters. */
            wwaApci13(date1, date2, ref astrom, ref w);

            /* Catalog ICRS (epoch J2000.0) to astrometric. */
            wwaAtccq(rc, dc, pr, pd, px, rv, ref astrom, ref ra, ref da);

            /* Finished. */
        }
    }
}
