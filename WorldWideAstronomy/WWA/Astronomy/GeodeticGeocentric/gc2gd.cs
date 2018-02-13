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
        /// Transform geocentric coordinates to geodetic using the specified
        /// reference ellipsoid.
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
        /// <param name="xyz">geocentric vector (Note 2)</param>
        /// <param name="elong">longitude (radians, east +ve, Note 3)</param>
        /// <param name="phi">latitude (geodetic, radians, Note 3)</param>
        /// <param name="height">height above ellipsoid (geodetic, Notes 2,3)</param>
        /// <returns>
        /// status:  0 = OK
        /// -1 = illegal identifier (Note 3)
        /// -2 = internal error (Note 3)
        /// </returns>
        public static int wwaGc2gd(int n, double[] xyz, ref double elong, ref double phi, ref double height)
        {
            int j;
            double a = 0, f = 0;

            /* Obtain reference ellipsoid parameters. */
            j = wwaEform(n, ref a, ref f);

            /* If OK, transform x,y,z to longitude, geodetic latitude, height. */
            if (j == 0)
            {
                j = wwaGc2gde(a, f, xyz, ref elong, ref phi, ref height);
                if (j < 0) j = -2;
            }

            /* Deal with any errors. */
            if (j < 0)
            {
                elong = -1e9;
                phi = -1e9;
                height = -1e9;
            }

            /* Return the status. */
            return j;
        }
    }
}