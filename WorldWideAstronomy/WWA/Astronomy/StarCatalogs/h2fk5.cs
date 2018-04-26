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
        /// Transform Hipparcos star data into the FK5 (J2000.0) system.
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
        /// Given (all Hipparcos, epoch J2000.0):
        /// <param name="rh">RA (radians)</param>
        /// <param name="dh">Dec (radians)</param>
        /// <param name="drh">proper motion in RA (dRA/dt, rad/Jyear)</param>
        /// <param name="ddh">proper motion in Dec (dDec/dt, rad/Jyear)</param>
        /// <param name="pxh">parallax (arcsec)</param>
        /// <param name="rvh">radial velocity (km/s, positive = receding)</param>
        /// Returned (all FK5, equinox J2000.0, epoch J2000.0):
        /// <param name="r5">RA (radians)</param>
        /// <param name="d5">Dec (radians)</param>
        /// <param name="dr5">proper motion in RA (dRA/dt, rad/Jyear)</param>
        /// <param name="dd5">proper motion in Dec (dDec/dt, rad/Jyear)</param>
        /// <param name="px5">parallax (arcsec)</param>
        /// <param name="rv5">radial velocity (km/s, positive = receding)</param>
        public static void wwaH2fk5(double rh, double dh,
              double drh, double ddh, double pxh, double rvh,
              ref double r5, ref double d5,
              ref double dr5, ref double dd5, ref double px5, ref double rv5)
        {
            int i;
            double[,] pvh = new double[2, 3];
            double[,] r5h = new double[3, 3];
            double[] s5h = new double[3];
            double[] sh = new double[3];
            double[] wxp = new double[3];
            double[] vv = new double[3];
            double[,] pv5 = new double[2, 3];
            double[] pvh1 = new double[3];
            double[] pv51 = new double[3];

            /* Hipparcos barycentric position/velocity pv-vector (normalized). */
            wwaStarpv(rh, dh, drh, ddh, pxh, rvh, pvh);

            /* FK5 to Hipparcos orientation matrix and spin vector. */
            wwaFk5hip(r5h, s5h);

            /* Make spin units per day instead of per year. */
            for (i = 0; i < 3; s5h[i++] /= 365.25) ;

            /* Orient the spin into the Hipparcos system. */
            wwaRxp(r5h, s5h, sh);

            /* De-orient the Hipparcos position into the FK5 system. */
            //wwaTrxp(r5h, CopyArray(pvh, 0), CopyArray(pv5, 0));
            pvh1 = CopyArray(pvh, 0);
            pv51 = CopyArray(pv5, 0);
            wwaTrxp(r5h, pvh1, pv51);
            AddArray2(ref pvh, pvh1, 0);
            AddArray2(ref pv5, pv51, 0);

            /* Apply spin to the position giving an extra space motion component. */
            wwaPxp(CopyArray(pvh, 0), sh, wxp);

            /* Subtract this component from the Hipparcos space motion. */
            wwaPmp(CopyArray(pvh, 1), wxp, vv);

            /* De-orient the Hipparcos space motion into the FK5 system. */
            pv51 = CopyArray(pv5, 1);
            wwaTrxp(r5h, vv, pv51);
            AddArray2(ref pv5, pv51, 1);

            /* FK5 pv-vector to spherical. */
            wwaPvstar(pv5, ref r5, ref d5, ref dr5, ref dd5, ref px5, ref rv5);

            return;
        }
    }
}
