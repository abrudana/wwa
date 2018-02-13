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
        /// Convert degrees, arcminutes, arcseconds to radians.
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
        /// Given:
        /// <param name="s">sign:  '-' = negative, otherwise positive</param>
        /// <param name="ideg">degrees</param>
        /// <param name="iamin">arcminutes</param>
        /// <param name="asec">arcseconds</param>
        /// Returned
        /// <param name="rad">angle in radians</param>
        /// <returns>status:  
        /// 0 = OK
        /// 1 = ideg outside range 0-359
        /// 2 = iamin outside range 0-59
        /// 3 = asec outside range 0-59.999...
        /// </returns>
        public static int wwaAf2a(char s, int ideg, int iamin, double asec, ref double rad)
        {
            /* Compute the interval. */
            rad = (s == '-' ? -1.0 : 1.0) *
                    (60.0 * (60.0 * ((double)Math.Abs(ideg)) +
                                      ((double)Math.Abs(iamin))) +
                                                 Math.Abs(asec)) * DAS2R;

            /* Validate arguments and return status. */
            if (ideg < 0 || ideg > 359) return 1;
            if (iamin < 0 || iamin > 59) return 2;
            if (asec < 0.0 || asec >= 60.0) return 3;
            return 0;
        }
    }
}
