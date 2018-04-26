using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldWideAstronomy
{
    //public static partial class SOFA
    //{
    //    /// <summary>
    //    /// Star proper motion:  update star catalog data for space motion, with
    //    /// special handling to handle the zero parallax case.
    //    /// </summary>
    //    /// 
    //    /// <remarks>
    //    /// World Wide Astronomy - WWA
    //    /// Set of C# algorithms and procedures that implement standard models used in fundamental astronomy.
    //    /// 
    //    /// This program is derived from the International Astronomical Union's
    //    /// SOFA (Standards of Fundamental Astronomy) software collection.
    //    /// http://www.iausofa.org
    //    /// 
    //    /// The WWA code does not itself constitute software provided by and/or endorsed by SOFA.
    //    /// This version is intended to retain identical functionality to the SOFA library, but
    //    /// made distinct through different function names (prefixes) and C# language specific
    //    /// modifications in code.
    //    /// 
    //    /// Contributor
    //    /// Attila Abrudán
    //    /// 
    //    /// Please read the ReadMe.1st text file for more information.
    //    /// </remarks>
    //    /// <param name="ra1">right ascension (radians), before</param>
    //    /// <param name="dec1">declination (radians), before</param>
    //    /// <param name="pmr1">RA proper motion (radians/year), before</param>
    //    /// <param name="pmd1">Dec proper motion (radians/year), before</param>
    //    /// <param name="px1">parallax (arcseconds), before</param>
    //    /// <param name="rv1">radial velocity (km/s, +ve = receding), before</param>
    //    /// <param name="ep1a">"before" epoch, part A (Note 1)</param>
    //    /// <param name="ep1b">"before" epoch, part B (Note 1)</param>
    //    /// <param name="ep2a">"after" epoch, part A (Note 1)</param>
    //    /// <param name="ep2b">"after" epoch, part B (Note 1)</param>
    //    /// <param name="ra2">right ascension (radians), after</param>
    //    /// <param name="dec2">declination (radians), after</param>
    //    /// <param name="pmr2">RA proper motion (radians/year), after</param>
    //    /// <param name="pmd2">Dec proper motion (radians/year), after</param>
    //    /// <param name="px2">parallax (arcseconds), after</param>
    //    /// <param name="rv2">radial velocity (km/s, +ve = receding), after</param>
    //    /// <returns>status:
    //    /// -1 = system error (should not occur)
    //    /// 0 = no warnings or errors
    //    /// 1 = distance overridden (Note 6)
    //    /// 2 = excessive velocity (Note 7)
    //    /// 4 = solution didn't converge (Note 8)
    //    /// else = binary logical OR of the above warnings
    //    /// </returns>
    //    public static int iauPmsafe(double ra1, double dec1, double pmr1, double pmd1,
    //          double px1, double rv1,
    //          double ep1a, double ep1b, double ep2a, double ep2b,
    //          ref double ra2, ref double dec2, ref double pmr2, ref double pmd2,
    //          ref double px2, ref double rv2)
    //    {

    //        /* Minimum allowed parallax (arcsec) */
    //        const double PXMIN = 5e-7;

    //        /* Factor giving maximum allowed transverse speed of about 1% c */
    //        const double F = 326.0;

    //        int jpx, j;
    //        double pm, px1a;


    //        /* Proper motion in one year (radians). */
    //        pm = wwaSeps(ra1, dec1, ra1 + pmr1, dec1 + pmd1);

    //        /* Override the parallax to reduce the chances of a warning status. */
    //        jpx = 0;
    //        px1a = px1;
    //        pm *= F;
    //        if (px1a < pm) { jpx = 1; px1a = pm; }
    //        if (px1a < PXMIN) { jpx = 1; px1a = PXMIN; }

    //        /* Carry out the transformation using the modified parallax. */
    //        j = wwaStarpm(ra1, dec1, pmr1, pmd1, px1a, rv1,
    //                      ep1a, ep1b, ep2a, ep2b,
    //                      ra2, dec2, pmr2, pmd2, px2, rv2);

    //        /* Revise and return the status. */
    //        if (!j % 2) j += jpx;
    //        return j;
    //    }
    //}
}
