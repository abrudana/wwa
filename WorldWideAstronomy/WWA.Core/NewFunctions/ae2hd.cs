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
        /// Horizon to equatorial coordinates:  transform azimuth and altitude
        /// to hour angle and declination.
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
        /// <param name="az">azimuth</param>
        /// <param name="el">altitude (informally, elevation)</param>
        /// <param name="phi">site latitude</param>
        /// <param name="ha">hour angle (local)</param>
        /// <param name="dec">declination</param>
        public static void wwaAe2hd(double az, double el, double phi, ref double ha, ref double dec)
        {
            double sa, ca, se, ce, sp, cp, x, y, z, r;

            /* Useful trig functions. */
            sa = Math.Sin(az);
            ca = Math.Cos(az);
            se = Math.Sin(el);
            ce = Math.Cos(el);
            sp = Math.Sin(phi);
            cp = Math.Cos(phi);

            /* HA,Dec unit vector. */
            x = -ca * ce * sp + se * cp;
            y = -sa * ce;
            z = ca * ce * cp + se * sp;

            /* To spherical. */
            r = Math.Sqrt(x * x + y * y);
            ha = (r != 0.0) ? Math.Atan2(y, x) : 0.0;
            dec = Math.Atan2(z, r);
        }
    }
}
