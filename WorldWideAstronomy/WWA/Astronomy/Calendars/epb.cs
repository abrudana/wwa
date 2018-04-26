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
        /// Julian Date to Besselian Epoch.
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
        /// <param name="dj1">Julian Date (see note)</param>
        /// <param name="dj2">Julian Date (see note)</param>
        /// <returns>Besselian Epoch.</returns>
        public static double wwaEpb(double dj1, double dj2)
        {
            /* J2000.0-B1900.0 (2415019.81352) in days */
            const double D1900 = 36524.68648;

            return 1900.0 + ((dj1 - DJ00) + (dj2 + D1900)) / DTY;
        }
    }
}
