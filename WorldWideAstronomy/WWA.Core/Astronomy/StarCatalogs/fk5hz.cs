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
        /// Transform an FK5 (J2000.0) star position into the system of the
        /// Hipparcos catalogue, assuming zero Hipparcos proper motion.
        /// </summary>
        /// 
        //// <remarks>
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
        /// <param name="r5">FK5 RA (radians), equinox J2000.0, at date</param>
        /// <param name="d5">FK5 Dec (radians), equinox J2000.0, at date</param>
        /// <param name="date1">TDB date (Notes 1,2)</param>
        /// <param name="date2">TDB date (Notes 1,2)</param>
        /// <param name="rh">Hipparcos RA (radians)</param>
        /// <param name="dh">Hipparcos Dec (radians)</param>
        public static void wwaFk5hz(double r5, double d5, double date1, double date2, ref double rh, ref double dh)
        {
            double t;
            double[] p5e = new double[3];
            double[,] r5h = new double[3, 3];
            double[] s5h = new double[3];
            double[] vst = new double[3];
            double[,] rst = new double[3, 3];
            double[] p5 = new double[3];
            double[] ph = new double[3];
            double w = 0;

            /* Interval from given date to fundamental epoch J2000.0 (JY). */
            t = -((date1 - DJ00) + date2) / DJY;

            /* FK5 barycentric position vector. */
            wwaS2c(r5, d5, p5e);

            /* FK5 to Hipparcos orientation matrix and spin vector. */
            wwaFk5hip(r5h, s5h);

            /* Accumulated Hipparcos wrt FK5 spin over that interval. */
            wwaSxp(t, s5h, vst);

            /* Express the accumulated spin as a rotation matrix. */
            wwaRv2m(vst, rst);

            /* Derotate the vector's FK5 axes back to date. */
            wwaTrxp(rst, p5e, p5);

            /* Rotate the vector into the Hipparcos system. */
            wwaRxp(r5h, p5, ph);

            /* Hipparcos vector to spherical. */
            wwaC2s(ph, ref w, ref dh);
            rh = wwaAnp(w);

            return;
        }
    }
}
