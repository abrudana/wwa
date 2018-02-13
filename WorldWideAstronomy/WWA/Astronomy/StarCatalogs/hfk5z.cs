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
        /// Transform a Hipparcos star position into FK5 J2000.0, assuming
        /// zero Hipparcos proper motion.
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
        /// Given:
        /// <param name="rh">Hipparcos RA (radians)</param>
        /// <param name="dh">Hipparcos Dec (radians)</param>
        /// <param name="date1">TDB date (Note 1)</param>
        /// <param name="date2">TDB date (Note 1)</param>
        /// Returned (all FK5, equinox J2000.0, date date1+date2):
        /// <param name="r5">RA (radians)</param>
        /// <param name="d5">Dec (radians)</param>
        /// <param name="dr5">FK5 RA proper motion (rad/year, Note 4)</param>
        /// <param name="dd5">Dec proper motion (rad/year, Note 4)</param>
        public static void wwaHfk5z(double rh, double dh, double date1, double date2, ref double r5, ref double d5, ref double dr5, ref double dd5)
        {
            double t;
            double[] ph = new double[3];
            double[,] r5h = new double[3, 3];
            double[] s5h = new double[3];
            double[] sh = new double[3];
            double[] vst = new double[3];
            double[,] rst = new double[3, 3];
            double[,] r5ht = new double[3, 3];
            double[,] pv5e = new double[2, 3];
            double[] vv = new double[3];
            double w = 0, r = 0, v = 0;
            double[] pv5e1 = new double[3];

            /* Time interval from fundamental epoch J2000.0 to given date (JY). */
            t = ((date1 - DJ00) + date2) / DJY;

            /* Hipparcos barycentric position vector (normalized). */
            wwaS2c(rh, dh, ph);

            /* FK5 to Hipparcos orientation matrix and spin vector. */
            wwaFk5hip(r5h, s5h);

            /* Rotate the spin into the Hipparcos system. */
            wwaRxp(r5h, s5h, sh);

            /* Accumulated Hipparcos wrt FK5 spin over that interval. */
            wwaSxp(t, s5h, vst);

            /* Express the accumulated spin as a rotation matrix. */
            wwaRv2m(vst, rst);

            /* Rotation matrix:  accumulated spin, then FK5 to Hipparcos. */
            wwaRxr(r5h, rst, r5ht);

            /* De-orient & de-spin the Hipparcos position into FK5 J2000.0. */
            pv5e1 = CopyArray(pv5e, 0);
            wwaTrxp(r5ht, ph, pv5e1);
            AddArray2(ref pv5e, pv5e1, 0);

            /* Apply spin to the position giving a space motion. */
            wwaPxp(sh, ph, vv);

            /* De-orient & de-spin the Hipparcos space motion into FK5 J2000.0. */
            //wwaTrxp(r5ht, vv, CopyArray(pv5e, 1));
            pv5e1 = CopyArray(pv5e, 1);
            wwaTrxp(r5ht, vv, pv5e1);
            AddArray2(ref pv5e, pv5e1, 1);

            /* FK5 position/velocity pv-vector to spherical. */
            wwaPv2s(pv5e, ref w, ref d5, ref r, ref dr5, ref dd5, ref v);
            r5 = wwaAnp(w);

            return;
        }
    }
}
