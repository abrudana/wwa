using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldWideAstronomy
{
    public static partial class WWA
    {
        /* --------------------- */
        /* The series for s+XY/2 */
        /* --------------------- */
        struct TERM
        {
            //public int[] nfa = new int[8];  /* coefficients of l,l',F,D,Om,LVe,LE,pA */
            public int[] nfa;
            public double s, c;             /* sine and cosine coefficients */
        }

        /// <summary>
        /// The CIO locator s, positioning the Celestial Intermediate Origin on
        /// the equator of the Celestial Intermediate Pole, given the CIP's X,Y
        /// coordinates.  Compatible with IAU 2000A precession-nutation.
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
        /// <param name="date1">TT as a 2-part Julian Date (Note 1)</param>
        /// <param name="date2">TT as a 2-part Julian Date (Note 1)</param>
        /// <param name="x">CIP coordinates (Note 3)</param>
        /// <param name="y">CIP coordinates (Note 3)</param>
        /// <returns></returns>
        public static double wwaS00(double date1, double date2, double x, double y)
        {
            /* Time since J2000.0, in Julian centuries */
            double t;

            /* Miscellaneous */
            int i, j;
            double a, w0, w1, w2, w3, w4, w5;

            /* Fundamental arguments */
            double[] fa = new double[8];

            /* Returned value */
            double s;

            /* Polynomial coefficients */
            double[] sp = new double[] {

   /* 1-6 */
          94.00E-6,
        3808.35E-6,
        -119.94E-6,
      -72574.09E-6,
          27.70E-6,
          15.61E-6
   };

            /* Terms of order t^0 */
            TERM[] s0 = new TERM[] {

   /* 1-10 */
      new TERM { nfa = new int[] { 0,  0,  0,  0,  1,  0,  0,  0}, s = -2640.73e-6, c = 0.39e-6 },
      new TERM { nfa = new int[] { 0,  0,  0,  0,  2,  0,  0,  0}, s = -63.53e-6, c = 0.02e-6 },
      new TERM { nfa = new int[] { 0,  0,  2, -2,  3,  0,  0,  0}, s = -11.75e-6, c = -0.01e-6 },
      new TERM { nfa = new int[] { 0,  0,  2, -2,  1,  0,  0,  0}, s = -11.21e-6, c = -0.01e-6 },
      new TERM { nfa = new int[] { 0,  0,  2, -2,  2,  0,  0,  0}, s = 4.57e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 0,  0,  2,  0,  3,  0,  0,  0}, s = -2.02e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 0,  0,  2,  0,  1,  0,  0,  0}, s = -1.98e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 0,  0,  0,  0,  3,  0,  0,  0}, s = 1.72e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 0,  1,  0,  0,  1,  0,  0,  0}, s = 1.41e-6, c = 0.01e-6 },
      new TERM { nfa = new int[] { 0,  1,  0,  0, -1,  0,  0,  0}, s = 1.26e-6, c = 0.01e-6 },

   /* 11-20 */
      new TERM { nfa = new int[] { 1,  0,  0,  0, -1,  0,  0,  0}, s = 0.63e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 1,  0,  0,  0,  1,  0,  0,  0}, s = 0.63e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 0,  1,  2, -2,  3,  0,  0,  0}, s = -0.46e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 0,  1,  2, -2,  1,  0,  0,  0}, s = -0.45e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 0,  0,  4, -4,  4,  0,  0,  0}, s = -0.36e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 0,  0,  1, -1,  1, -8, 12,  0}, s = 0.24e-6, c = 0.12e-6 },
      new TERM { nfa = new int[] { 0,  0,  2,  0,  0,  0,  0,  0}, s = -0.32e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 0,  0,  2,  0,  2,  0,  0,  0}, s = -0.28e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 1,  0,  2,  0,  3,  0,  0,  0}, s = -0.27e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 1,  0,  2,  0,  1,  0,  0,  0}, s = -0.26e-6, c = 0.00e-6 },

   /* 21-30 */
      new TERM { nfa = new int[] { 0,  0,  2, -2,  0,  0,  0,  0}, s = 0.21e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 0,  1, -2,  2, -3,  0,  0,  0}, s = -0.19e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 0,  1, -2,  2, -1,  0,  0,  0}, s = -0.18e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 0,  0,  0,  0,  0,  8,-13, -1}, s = 0.10e-6, c = -0.05e-6 },
      new TERM { nfa = new int[] { 0,  0,  0,  2,  0,  0,  0,  0}, s = -0.15e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 2,  0, -2,  0, -1,  0,  0,  0}, s = 0.14e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 0,  1,  2, -2,  2,  0,  0,  0}, s = 0.14e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 1,  0,  0, -2,  1,  0,  0,  0}, s = -0.14e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 1,  0,  0, -2, -1,  0,  0,  0}, s = -0.14e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 0,  0,  4, -2,  4,  0,  0,  0}, s = -0.13e-6, c = 0.00e-6 },

   /* 31-33 */
      new TERM { nfa = new int[] { 0,  0,  2, -2,  4,  0,  0,  0}, s = 0.11e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 1,  0, -2,  0, -3,  0,  0,  0}, s = -0.11e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 1,  0, -2,  0, -1,  0,  0,  0}, s = -0.11e-6, c = 0.00e-6 }
   };

            /* Terms of order t^1 */
            TERM[] s1 = new TERM[] {
   /* 1-3 */
      new TERM { nfa = new int[] { 0,  0,  0,  0,  2,  0,  0,  0}, s = -0.07e-6, c = 3.57e-6 },
      new TERM { nfa = new int[] { 0,  0,  0,  0,  1,  0,  0,  0}, s = 1.71e-6, c = -0.03e-6 },
      new TERM { nfa = new int[] { 0,  0,  2, -2,  3,  0,  0,  0}, s = 0.00e-6, c = 0.48e-6 }
   };

            /* Terms of order t^2 */
            TERM[] s2 = new TERM[] {

   /* 1-10 */
      new TERM { nfa = new int[] { 0,  0,  0,  0,  1,  0,  0,  0}, s = 743.53e-6, c = -0.17e-6 },
      new TERM { nfa = new int[] { 0,  0,  2, -2,  2,  0,  0,  0}, s = 56.91e-6, c = 0.06e-6 },
      new TERM { nfa = new int[] { 0,  0,  2,  0,  2,  0,  0,  0}, s = 9.84e-6, c = -0.01e-6 },
      new TERM { nfa = new int[] { 0,  0,  0,  0,  2,  0,  0,  0}, s = -8.85e-6, c = 0.01e-6 },
      new TERM { nfa = new int[] { 0,  1,  0,  0,  0,  0,  0,  0}, s = -6.38e-6, c = -0.05e-6 },
      new TERM { nfa = new int[] { 1,  0,  0,  0,  0,  0,  0,  0}, s = -3.07e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 0,  1,  2, -2,  2,  0,  0,  0}, s = 2.23e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 0,  0,  2,  0,  1,  0,  0,  0}, s = 1.67e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 1,  0,  2,  0,  2,  0,  0,  0}, s = 1.30e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 0,  1, -2,  2, -2,  0,  0,  0}, s = 0.93e-6, c = 0.00e-6 },

   /* 11-20 */
      new TERM { nfa = new int[] { 1,  0,  0, -2,  0,  0,  0,  0}, s = 0.68e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 0,  0,  2, -2,  1,  0,  0,  0}, s = -0.55e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 1,  0, -2,  0, -2,  0,  0,  0}, s = 0.53e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 0,  0,  0,  2,  0,  0,  0,  0}, s = -0.27e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 1,  0,  0,  0,  1,  0,  0,  0}, s = -0.27e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 1,  0, -2, -2, -2,  0,  0,  0}, s = -0.26e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 1,  0,  0,  0, -1,  0,  0,  0}, s = -0.25e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 1,  0,  2,  0,  1,  0,  0,  0}, s = 0.22e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 2,  0,  0, -2,  0,  0,  0,  0}, s = -0.21e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 2,  0, -2,  0, -1,  0,  0,  0}, s = 0.20e-6, c = 0.00e-6 },

   /* 21-25 */
      new TERM { nfa = new int[] { 0,  0,  2,  2,  2,  0,  0,  0}, s = 0.17e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 2,  0,  2,  0,  2,  0,  0,  0}, s = 0.13e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 2,  0,  0,  0,  0,  0,  0,  0}, s = -0.13e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 1,  0,  2, -2,  2,  0,  0,  0}, s = -0.12e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 0,  0,  2,  0,  0,  0,  0,  0}, s = -0.11e-6, c = 0.00e-6 }
   };

            /* Terms of order t^3 */
            TERM[] s3 = new TERM[] {

   /* 1-4 */
      new TERM { nfa = new int[] { 0,  0,  0,  0,  1,  0,  0,  0}, s = 0.30e-6, c = -23.51e-6 },
      new TERM { nfa = new int[] { 0,  0,  2, -2,  2,  0,  0,  0}, s = -0.03e-6, c = -1.39e-6 },
      new TERM { nfa = new int[] { 0,  0,  2,  0,  2,  0,  0,  0}, s = -0.01e-6, c = -0.24e-6 },
      new TERM { nfa = new int[] { 0,  0,  0,  0,  2,  0,  0,  0}, s = 0.00e-6, c = 0.22e-6 }
   };

            /* Terms of order t^4 */
            TERM[] s4 = new TERM[] {

   /* 1-1 */
      new TERM { nfa = new int[] { 0,  0,  0,  0,  1,  0,  0,  0}, s = -0.26e-6, c = -0.01e-6 }
   };

            /* Number of terms in the series */
            //const int NS0 = (int)(s0.GetLength(1) / sizeof(TERM[0]));
            int NS0 = s0.GetLength(0); // by AA
            int NS1 = s1.GetLength(0);
            int NS2 = s2.GetLength(0);
            int NS3 = s3.GetLength(0);
            int NS4 = s4.GetLength(0);
            /*--------------------------------------------------------------------*/

            /* Interval between fundamental epoch J2000.0 and current date (JC). */
            t = ((date1 - DJ00) + date2) / DJC;

            /* Fundamental Arguments (from IERS Conventions 2003) */

            /* Mean anomaly of the Moon. */
            fa[0] = wwaFal03(t);

            /* Mean anomaly of the Sun. */
            fa[1] = wwaFalp03(t);

            /* Mean longitude of the Moon minus that of the ascending node. */
            fa[2] = wwaFaf03(t);

            /* Mean elongation of the Moon from the Sun. */
            fa[3] = wwaFad03(t);

            /* Mean longitude of the ascending node of the Moon. */
            fa[4] = wwaFaom03(t);

            /* Mean longitude of Venus. */
            fa[5] = wwaFave03(t);

            /* Mean longitude of Earth. */
            fa[6] = wwaFae03(t);

            /* General precession in longitude. */
            fa[7] = wwaFapa03(t);

            /* Evaluate s. */
            w0 = sp[0];
            w1 = sp[1];
            w2 = sp[2];
            w3 = sp[3];
            w4 = sp[4];
            w5 = sp[5];

            for (i = NS0 - 1; i >= 0; i--)
            {
                a = 0.0;
                for (j = 0; j < 8; j++)
                {
                    a += (double)s0[i].nfa[j] * fa[j];
                }
                w0 += s0[i].s * Math.Sin(a) + s0[i].c * Math.Cos(a);
            }

            for (i = NS1 - 1; i >= 0; i--)
            {
                a = 0.0;
                for (j = 0; j < 8; j++)
                {
                    a += (double)s1[i].nfa[j] * fa[j];
                }
                w1 += s1[i].s * Math.Sin(a) + s1[i].c * Math.Cos(a);
            }

            for (i = NS2 - 1; i >= 0; i--)
            {
                a = 0.0;
                for (j = 0; j < 8; j++)
                {
                    a += (double)s2[i].nfa[j] * fa[j];
                }
                w2 += s2[i].s * Math.Sin(a) + s2[i].c * Math.Cos(a);
            }

            for (i = NS3 - 1; i >= 0; i--)
            {
                a = 0.0;
                for (j = 0; j < 8; j++)
                {
                    a += (double)s3[i].nfa[j] * fa[j];
                }
                w3 += s3[i].s * Math.Sin(a) + s3[i].c * Math.Cos(a);
            }

            for (i = NS4 - 1; i >= 0; i--)
            {
                a = 0.0;
                for (j = 0; j < 8; j++)
                {
                    a += (double)s4[i].nfa[j] * fa[j];
                }
                w4 += s4[i].s * Math.Sin(a) + s4[i].c * Math.Cos(a);
            }

            s = (w0 +
                (w1 +
                (w2 +
                (w3 +
                (w4 +
                 w5 * t) * t) * t) * t) * t) * DAS2R - x * y / 2.0;

            return s;
        }
    }
}
