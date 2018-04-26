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
        /// Proper motion and parallax.
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
        /// <param name="rc">ICRS RA,Dec at catalog epoch (radians)</param>
        /// <param name="dc">ICRS RA,Dec at catalog epoch (radians)</param>
        /// <param name="pr">RA proper motion (radians/year; Note 1)</param>
        /// <param name="pd">Dec proper motion (radians/year)</param>
        /// <param name="px">parallax (arcsec)</param>
        /// <param name="rv">radial velocity (km/s, +ve if receding)</param>
        /// <param name="pmt">proper motion time interval (SSB, Julian years)</param>
        /// <param name="pob">SSB to observer vector (au)</param>
        /// <param name="pco">coordinate direction (BCRS unit vector)</param>
        public static void wwaPmpx(double rc, double dc, double pr, double pd, double px, double rv, double pmt, double[] pob, double[] pco)
        {
            if (pob == null)
                pob = new double[3];

            /* Km/s to au/year */
            const double VF = DAYSEC * DJM / DAU;

            /* Light time for 1 au, Julian years */
            const double AULTY = AULT / DAYSEC / DJY;

            int i;
            double sr, cr, sd, cd, x, y, z, dt, pxr, w, pdz;
            double[] p = new double[3] { 0, 0, 0 };
            double[] pm = new double[3];

            /* Spherical coordinates to unit vector (and useful functions). */
            sr = Math.Sin(rc);
            cr = Math.Cos(rc);
            sd = Math.Sin(dc);
            cd = Math.Cos(dc);
            p[0] = x = cr * cd;
            p[1] = y = sr * cd;
            p[2] = z = sd;

            /* Proper motion time interval (y) including Roemer effect. */
            dt = pmt + wwaPdp(p, pob) * AULTY;

            /* Space motion (radians per year). */
            pxr = px * DAS2R;
            w = VF * rv * pxr;
            pdz = pd * z;
            pm[0] = -pr * y - pdz * cr + w * x;
            pm[1] = pr * x - pdz * sr + w * y;
            pm[2] = pd * cd + w * z;

            /* Coordinate direction of star (unit vector, BCRS). */
            for (i = 0; i < 3; i++)
            {
                p[i] += dt * pm[i] - pxr * pob[i];
            }

            wwaPn(p, ref w, pco);
        }       
    }
}