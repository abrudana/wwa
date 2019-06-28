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
        /// Normalize angle into the range -pi <= a < +pi.
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
        /// <param name="a">angle (radians)</param>
        /// <returns>angle in range +/-pi</returns>
        public static double wwaAnpm(double a)
        {
            double w;

            //w = fmod(a, D2PI);
            w = Math.IEEERemainder(a, D2PI);

            //if (fabs(w) >= DPI) w -= dsign(D2PI, a);
            if (Math.Abs(w) >= DPI) w -= dsign(D2PI, a);

            return w;
        }
    }
}
