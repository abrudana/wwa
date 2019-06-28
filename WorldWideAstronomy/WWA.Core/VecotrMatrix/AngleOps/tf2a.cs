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
        /// Convert hours, minutes, seconds to radians.
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
        /// <param name="ihour">hours</param>
        /// <param name="imin">minutes</param>
        /// <param name="sec">seconds</param>
        /// Returned:
        /// <param name="rad">angle in radians</param>
        /// <returns>
        /// status:  0 = OK
        /// 1 = ihour outside range 0-23
        /// 2 = imin outside range 0-59
        /// 3 = sec outside range 0-59.999...
        /// </returns>
        public static int wwaTf2a(char s, int ihour, int imin, double sec, ref double rad)
        {
            /* Compute the interval. */
            rad = (s == '-' ? -1.0 : 1.0) *
                    (60.0 * (60.0 * ((double)Math.Abs(ihour)) +
                                      ((double)Math.Abs(imin))) +
                                                 Math.Abs(sec)) * DS2R;

            /* Validate arguments and return status. */
            if (ihour < 0 || ihour > 23) return 1;
            if (imin < 0 || imin > 59) return 2;
            if (sec < 0.0 || sec >= 60.0) return 3;
            return 0;
        }
    }
}
