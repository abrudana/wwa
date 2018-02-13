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
        /// Transform geodetic coordinates to geocentric for a reference
        /// ellipsoid of specified form.
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
        /// <param name="a">equatorial radius (Notes 1,4)</param>
        /// <param name="f">flattening (Notes 2,4)</param>
        /// <param name="elong">longitude (radians, east +ve)</param>
        /// <param name="phi">latitude (geodetic, radians, Note 4)</param>
        /// <param name="height">height above ellipsoid (geodetic, Notes 3,4)</param>
        /// <param name="xyz">geocentric vector (Note 3)</param>
        /// <returns>
        /// status:  0 = OK
        /// -1 = illegal case (Note 4)
        /// </returns>
        public static int wwaGd2gce(double a, double f, double elong, double phi, double height, double[] xyz)
        {
            double sp, cp, w, d, ac, _as, r;

            /* Functions of geodetic latitude. */
            sp = Math.Sin(phi);
            cp = Math.Cos(phi);
            w = 1.0 - f;
            w = w * w;
            d = cp * cp + w * sp * sp;
            if (d <= 0.0) return -1;
            ac = a / Math.Sqrt(d);
            _as = w * ac;

            /* Geocentric vector. */
            r = (ac + height) * cp;
            xyz[0] = r * Math.Cos(elong);
            xyz[1] = r * Math.Sin(elong);
            xyz[2] = (_as + height) * sp;

            /* Success. */
            return 0;
        }
    }
}