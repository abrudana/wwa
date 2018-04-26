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
        /// Convert star catalog coordinates to position+velocity vector.
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
        /// <param name="ra">right ascension (radians)</param>
        /// <param name="dec">declination (radians)</param>
        /// <param name="pmr">RA proper motion (radians/year)</param>
        /// <param name="pmd">Dec proper motion (radians/year)</param>
        /// <param name="px">parallax (arcseconds)</param>
        /// <param name="rv">radial velocity (km/s, positive = receding)</param>
        /// <param name="pv">pv-vector (AU, AU/day)</param>
        /// <returns>status:
        /// 0 = no warnings
        /// 1 = distance overridden (Note 6)
        /// 2 = excessive speed (Note 7)
        /// 4 = solution didn't converge (Note 8)
        /// else = binary logical OR of the above
        /// </returns>
        public static int wwaStarpv(double ra, double dec, double pmr, double pmd, double px, double rv, double[,] pv)
        {
            /* Smallest allowed parallax */
            const double PXMIN = 1e-7;

            /* Largest allowed speed (fraction of c) */
            const double VMAX = 0.5;

            /* Maximum number of iterations for relativistic solution */
            const int IMAX = 100;

            int i, iwarn;
            double w, r, rd, rad, decd, v;
            double[] x = new double[3];
            double[] usr = new double[3];
            double[] ust = new double[3];
            double vsr, vst, betst, betsr, bett, betr, dd, ddel;
            double[] ur = new double[3];
            double[] ut = new double[3];
            double d = 0.0, del = 0.0,       /* to prevent */
            odd = 0.0, oddel = 0.0,   /* compiler   */
            od = 0.0, odel = 0.0;     /* warnings   */
            double[] pv1 = new double[3];

            /* Distance (AU). */
            if (px >= PXMIN)
            {
                w = px;
                iwarn = 0;
            }
            else
            {
                w = PXMIN;
                iwarn = 1;
            }
            r = DR2AS / w;

            /* Radial velocity (AU/day). */
            rd = DAYSEC * rv * 1e3 / DAU;

            /* Proper motion (radian/day). */
            rad = pmr / DJY;
            decd = pmd / DJY;

            /* To pv-vector (AU,AU/day). */
            wwaS2pv(ra, dec, r, rad, decd, rd, pv);

            /* If excessive velocity, arbitrarily set it to zero. */
            v = wwaPm(CopyArray(pv, 1));
            if (v / DC > VMAX)
            {
                pv1 = CopyArray(pv, 1);
                wwaZp(pv1);
                AddArray2(ref pv, pv1, 1);
                iwarn += 2;
            }

            /* Isolate the radial component of the velocity (AU/day). */
            wwaPn(CopyArray(pv, 0), ref w, x);
            vsr = wwaPdp(x, CopyArray(pv, 1));
            wwaSxp(vsr, x, usr);

            /* Isolate the transverse component of the velocity (AU/day). */
            wwaPmp(CopyArray(pv, 1), usr, ust);
            vst = wwaPm(ust);

            /* Special-relativity dimensionless parameters. */
            betsr = vsr / DC;
            betst = vst / DC;

            /* Determine the inertial-to-observed relativistic correction terms. */
            bett = betst;
            betr = betsr;
            for (i = 0; i < IMAX; i++)
            {
                d = 1.0 + betr;
                w = betr * betr + bett * bett;
                w = betr * betr + bett * bett;
                del = -w / (Math.Sqrt(1.0 - w) + 1.0);
                betr = d * betsr + del;
                bett = d * betst;
                if (i > 0)
                {
                    dd = Math.Abs(d - od);
                    ddel = Math.Abs(del - odel);
                    if ((i > 1) && (dd >= odd) && (ddel >= oddel)) break;
                    odd = dd;
                    oddel = ddel;
                }
                od = d;
                odel = del;
            }
            if (i >= IMAX) iwarn += 4;

            /* Replace observed radial velocity with inertial value. */
            w = (betsr != 0.0) ? d + del / betsr : 1.0;
            wwaSxp(w, usr, ur);

            /* Replace observed tangential velocity with inertial value. */
            wwaSxp(d, ust, ut);

            /* Combine the two to obtain the inertial space velocity. */
            //wwaPpp(ur, ut, CopyArray(pv, 1));
            pv1 = CopyArray(pv, 1);
            wwaPpp(ur, ut, pv1);
            AddArray2(ref pv, pv1, 1);

            /* Return the status. */
            return iwarn;
        }
    }
}
