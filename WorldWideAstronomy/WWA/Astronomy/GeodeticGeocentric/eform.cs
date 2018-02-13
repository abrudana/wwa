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
        /// Earth reference ellipsoids.
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
        /// <param name="n">ellipsoid identifier (Note 1)</param>
        /// <param name="a">equatorial radius (meters, Note 2)</param>
        /// <param name="f">flattening (Note 2)</param>
        /// <returns>
        /// status:  0 = OK
        /// -1 = illegal identifier (Note 3)
        /// </returns>
        public static int wwaEform(int n, ref double a, ref double f)
        {
            /* Look up a and f for the specified reference ellipsoid. */
            switch (n)
            {

                case WGS84:
                    a = 6378137.0;
                    f = 1.0 / 298.257223563;
                    break;

                case GRS80:
                    a = 6378137.0;
                    f = 1.0 / 298.257222101;
                    break;

                case WGS72:
                    a = 6378135.0;
                    f = 1.0 / 298.26;
                    break;

                default:

                    /* Invalid identifier. */
                    a = 0.0;
                    f = 0.0;
                    return -1;
            }

            /* OK status. */
            return 0;
        }
    }
}