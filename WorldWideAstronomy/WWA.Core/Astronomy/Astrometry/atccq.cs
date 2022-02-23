using System;
using System.Collections.Generic;
using System.Text;

namespace WorldWideAstronomy
{
    public static partial class WWA
    {
        /// <summary>
        /// For a geocentric observer, prepare star-independent astrometry
        /// parameters for transformations between ICRS and GCRS coordinates.
        /// The caller supplies the date, and SOFA models are used to predict
        /// the Earth ephemeris.
        /// The parameters produced by this function are required in the
        /// parallax, light deflection and aberration parts of the astrometric
        /// transformation chain.
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
        /// <param name="astrom"></param>
        /// <param name="ra"></param>
        /// <param name="da"></param>
        public static void wwaAtccq(double rc, double dc,
              double pr, double pd, double px, double rv,
              ref wwaASTROM astrom, ref double ra, ref double da)
        {
            double[] p = new double[3];
            double w = 0;

            /* Proper motion and parallax, giving BCRS coordinate direction. */
            wwaPmpx(rc, dc, pr, pd, px, rv, astrom.pmt, astrom.eb, p);

            /* ICRS astrometric RA,Dec. */
            wwaC2s(p, ref w, ref da);
            ra = wwaAnp(w);

            /* Finished. */
        }
    }
}
