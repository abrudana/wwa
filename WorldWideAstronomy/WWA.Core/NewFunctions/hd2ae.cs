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
        /// Equatorial to horizon coordinates:  transform hour angle and
        /// declination to azimuth and altitude.
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
        /// <param name="ha">hour angle (local)</param>
        /// <param name="dec">declination</param>
        /// <param name="phi">site latitude</param>
        /// <param name="az">azimuth</param>
        /// <param name="el">altitude (informally, elevation)</param>
        public static void wwaHd2ae(double ha, double dec, double phi, ref double az, ref double el)
        {
            double sh, ch, sd, cd, sp, cp, x, y, z, r, a;

            /* Useful trig functions. */
            sh = Math.Sin(ha);
            ch = Math.Cos(ha);
            sd = Math.Sin(dec);
            cd = Math.Cos(dec);
            sp = Math.Sin(phi);
            cp = Math.Cos(phi);

            /* Az,Alt unit vector. */
            x = -ch * cd * sp + sd * cp;
            y = -sh * cd;
            z = ch * cd * cp + sd * sp;

            /* To spherical. */
            r = Math.Sqrt(x * x + y * y);
            a = (r != 0.0) ? Math.Atan2(y, x) : 0.0;
            az = (a < 0.0) ? a + D2PI : a;
            el = Math.Atan2(z, r);
        }
    }
}
