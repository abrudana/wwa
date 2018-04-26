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
        /// Convert star position+velocity vector to catalog coordinates.
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
        /// <param name="pv">pv-vector (au, au/day)</param>
        /// <param name="ra">right ascension (radians)</param>
        /// <param name="dec">declination (radians)</param>
        /// <param name="pmr">RA proper motion (radians/year)</param>
        /// <param name="pmd">Dec proper motion (radians/year)</param>
        /// <param name="px">parallax (arcsec)</param>
        /// <param name="rv">radial velocity (km/s, positive = receding)</param>
        /// <returns>status:
        /// 0 = OK
        /// -1 = superluminal speed (Note 5)
        /// -2 = null position vector
        /// </returns>
        public static int wwaPvstar(double[,] pv, ref double ra, ref double dec, ref double pmr, ref double pmd, ref double px, ref double rv)
        {
            double r = 0;
            double[] x = new double[3];
            double vr;
            double[] ur = new double[3];
            double vt;
            double[] ut = new double[3];
            double bett, betr, d, w, del;
            double[] usr = new double[3];
            double[] ust = new double[3];
            double a = 0, rad = 0, decd = 0, rd = 0;
            double[] pv1 = new double[3];


            /* Isolate the radial component of the velocity (au/day, inertial). */
            wwaPn(CopyArray(pv, 0), ref r, x);
            vr = wwaPdp(x, CopyArray(pv, 1));
            wwaSxp(vr, x, ur);

            /* Isolate the transverse component of the velocity (au/day, inertial). */
            wwaPmp(CopyArray(pv, 1), ur, ut);
            vt = wwaPm(ut);

            /* Special-relativity dimensionless parameters. */
            bett = vt / DC;
            betr = vr / DC;

            /* The inertial-to-observed correction terms. */
            d = 1.0 + betr;
            w = betr * betr + bett * bett;
            if (d == 0.0 || w > 1.0) return -1;
            del = -w / (Math.Sqrt(1.0 - w) + 1.0);

            /* Apply relativistic correction factor to radial velocity component. */
            w = (betr != 0) ? (betr - del) / (betr * d) : 1.0;
            wwaSxp(w, ur, usr);

            /* Apply relativistic correction factor to tangential velocity */
            /* component.                                                  */
            wwaSxp(1.0 / d, ut, ust);

            /* Combine the two to obtain the observed velocity vector (au/day). */
            //wwaPpp(usr, ust, CopyArray(pv, 1));
            pv1 = CopyArray(pv, 1);
            wwaPpp(usr, ust, pv1);
            AddArray2(ref pv, pv1, 1);

            /* Cartesian to spherical. */
            wwaPv2s(pv, ref a, ref dec, ref r, ref rad, ref decd, ref rd);
            if (r == 0.0) return -2;

            /* Return RA in range 0 to 2pi. */
            ra = wwaAnp(a);

            /* Return proper motions in radians per year. */
            pmr = rad * DJY;
            pmd = decd * DJY;

            /* Return parallax in arcsec. */
            px = DR2AS / r;

            /* Return radial velocity in km/s. */
            rv = 1e-3 * rd * DAU / DAYSEC;

            /* OK status. */
            return 0;
        }
    }
}
