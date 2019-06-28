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
        /// Transform FK5 (J2000.0) star data into the Hipparcos system.
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
        /// Given (all FK5, equinox J2000.0, epoch J2000.0)
        /// <param name="r5">RA (radians)</param>
        /// <param name="d5">Dec (radians)</param>
        /// <param name="dr5">proper motion in RA (dRA/dt, rad/Jyear)</param>
        /// <param name="dd5">proper motion in Dec (dDec/dt, rad/Jyear)</param>
        /// <param name="px5">parallax (arcsec)</param>
        /// <param name="rv5">radial velocity (km/s, positive = receding)</param>
        /// Returned (all Hipparcos, epoch J2000.0)
        /// <param name="rh">RA (radians)</param>
        /// <param name="dh">Dec (radians)</param>
        /// <param name="drh">proper motion in RA (dRA/dt, rad/Jyear)</param>
        /// <param name="ddh">proper motion in Dec (dDec/dt, rad/Jyear)</param>
        /// <param name="pxh">parallax (arcsec)</param>
        /// <param name="rvh">radial velocity (km/s, positive = receding)</param>
        public static void wwaFk52h(double r5, double d5,
              double dr5, double dd5, double px5, double rv5,
              ref double rh, ref double dh,
              ref double drh, ref double ddh, ref double pxh, ref double rvh)
        {
            int i;
            double[,] pv5 = new double[2, 3];
            double[,] r5h = new double[3, 3];
            double[] s5h = new double[3];
            double[] wxp = new double[3];
            double[] vv = new double[3];
            double[,] pvh = new double[2, 3];
            double[] pvh1 = new double[3];
            
            /* FK5 barycentric position/velocity pv-vector (normalized). */
            wwaStarpv(r5, d5, dr5, dd5, px5, rv5, pv5);

            /* FK5 to Hipparcos orientation matrix and spin vector. */
            wwaFk5hip(r5h, s5h);

            /* Make spin units per day instead of per year. */
            for (i = 0; i < 3; s5h[i++] /= 365.25) ;

            /* Orient the FK5 position into the Hipparcos system. */
            //wwaRxp(r5h, CopyArray(pv5, 0), CopyArray(pvh, 0));
            //pvh1 = CopyArray(pvh, 0);
            wwaRxp(r5h, CopyArray(pv5, 0), pvh1);
            pvh = CopyArray2(pvh1, 0);

            /* Apply spin to the position giving an extra space motion component. */
            wwaPxp(CopyArray(pv5, 0), s5h, wxp);

            /* Add this component to the FK5 space motion. */
            wwaPpp(wxp, CopyArray(pv5, 1), vv);

            /* Orient the FK5 space motion into the Hipparcos system. */
            //wwaRxp(r5h, vv, CopyArray(pvh, 1));
            pvh1 = CopyArray(pvh, 1);
            wwaRxp(r5h, vv, pvh1);
            AddArray2(ref pvh, pvh1, 1);

            /* Hipparcos pv-vector to spherical. */
            wwaPvstar(pvh, ref rh, ref dh, ref drh, ref ddh, ref pxh, ref rvh);

            return;
        }
    }
}
