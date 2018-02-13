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
        /// Transform geocentric coordinates to geodetic for a reference
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
        /// <param name="a">equatorial radius (Notes 2,4)</param>
        /// <param name="f">flattening (Note 3)</param>
        /// <param name="xyz">geocentric vector (Note 4)</param>
        /// <param name="elong">longitude (radians, east +ve)</param>
        /// <param name="phi">latitude (geodetic, radians)</param>
        /// <param name="height">height above ellipsoid (geodetic, Note 4)</param>
        /// <returns>
        /// status:  0 = OK
        /// -1 = illegal f
        /// -2 = illegal a
        /// </returns>
        public static int wwaGc2gde(double a, double f, double[] xyz, ref double elong, ref double phi, ref double height)
        {
            double aeps2, e2, e4t, ec2, ec, b, x, y, z, p2, absz, p, s0, pn, zc,
                          c0, c02, c03, s02, s03, a02, a0, a03, d0, f0, b0, s1,
                          cc, s12, cc2;

            /* ------------- */
            /* Preliminaries */
            /* ------------- */

            /* Validate ellipsoid parameters. */
            if (f < 0.0 || f >= 1.0) return -1;
            if (a <= 0.0) return -2;

            /* Functions of ellipsoid parameters (with further validation of f). */
            aeps2 = a * a * 1e-32;
            e2 = (2.0 - f) * f;
            e4t = e2 * e2 * 1.5;
            ec2 = 1.0 - e2;
            if (ec2 <= 0.0) return -1;
            ec = Math.Sqrt(ec2);
            b = a * ec;

            /* Cartesian components. */
            x = xyz[0];
            y = xyz[1];
            z = xyz[2];

            /* Distance from polar axis squared. */
            p2 = x * x + y * y;

            /* Longitude. */
            //elong = p2 != 0.0 ? Math.Atan2(y, x) : 0.0;
            //*elong = p2 > 0.0 ? atan2(y, x) : 0.0;
            elong = p2 > 0.0 ? Math.Atan2(y, x) : 0.0;

            /* Unsigned z-coordinate. */
            absz = Math.Abs(z);

            /* Proceed unless polar case. */
            if (p2 > aeps2)
            {
                /* Distance from polar axis. */
                p = Math.Sqrt(p2);

                /* Normalization. */
                s0 = absz / a;
                pn = p / a;
                zc = ec * s0;

                /* Prepare Newton correction factors. */
                c0 = ec * pn;
                c02 = c0 * c0;
                c03 = c02 * c0;
                s02 = s0 * s0;
                s03 = s02 * s0;
                a02 = c02 + s02;
                a0 = Math.Sqrt(a02);
                a03 = a02 * a0;
                d0 = zc * a03 + e2 * s03;
                f0 = pn * a03 - e2 * c03;

                /* Prepare Halley correction factor. */
                b0 = e4t * s02 * c02 * pn * (a0 - ec);
                s1 = d0 * f0 - b0 * s0;
                cc = ec * (f0 * f0 - b0 * c0);

                /* Evaluate latitude and height. */
                phi = Math.Atan(s1 / cc);
                s12 = s1 * s1;
                cc2 = cc * cc;
                height = (p * cc + absz * s1 - a * Math.Sqrt(ec2 * s12 + cc2)) / Math.Sqrt(s12 + cc2);
            }
            else
            {
                /* Exception: pole. */
                phi = DPI / 2.0;
                height = absz - b;
            }

            /* Restore sign of latitude. */
            if (z < 0) phi = -phi;

            /* OK status. */
            return 0;
        }
    }
}