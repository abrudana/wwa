using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldWideAstronomy
{
    public static partial class WWA
    {
        //     /* ----------------------------------------- */
        //     /* The series for the EE complementary terms */
        //     /* ----------------------------------------- */

        //typedef struct {
        //   int nfa[8];      /* coefficients of l,l',F,D,Om,LVe,LE,pA */
        //   double s, c;     /* sine and cosine coefficients */
        //} TERM;

        /// <summary>
        /// Equation of the equinoxes complementary terms, consistent with
        /// IAU 2000 resolutions.
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
        /// <returns>complementary terms (Note 2)</returns>
        public static double wwaEect00(double date1, double date2)
        {
            /* Time since J2000.0, in Julian centuries */
            double t;

            /* Miscellaneous */
            int i, j;
            double a, s0, s1;

            /* Fundamental arguments */
            double[] fa = new double[14];

            /* Returned value. */
            double eect;

            /* Terms of order t^0 */
            TERM[] e0 = new TERM[] {

   /* 1-10 */
      new TERM { nfa = new int[] { 0,  0,  0,  0,  1,  0,  0,  0}, s = 2640.96e-6, c = -0.39e-6 },
      new TERM { nfa = new int[] { 0,  0,  0,  0,  2,  0,  0,  0}, s = 63.52e-6, c = -0.02e-6 },
      new TERM { nfa = new int[] { 0,  0,  2, -2,  3,  0,  0,  0}, s = 11.75e-6, c = 0.01e-6 },
      new TERM { nfa = new int[] { 0,  0,  2, -2,  1,  0,  0,  0}, s = 11.21e-6, c = 0.01e-6 },
      new TERM { nfa = new int[] { 0,  0,  2, -2,  2,  0,  0,  0}, s = -4.55e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 0,  0,  2,  0,  3,  0,  0,  0}, s = 2.02e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 0,  0,  2,  0,  1,  0,  0,  0}, s = 1.98e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 0,  0,  0,  0,  3,  0,  0,  0}, s = -1.72e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 0,  1,  0,  0,  1,  0,  0,  0}, s = -1.41e-6, c = -0.01e-6 },
      new TERM { nfa = new int[] { 0,  1,  0,  0, -1,  0,  0,  0}, s = -1.26e-6, c = -0.01e-6 },

   /* 11-20 */
      new TERM { nfa = new int[] { 1,  0,  0,  0, -1,  0,  0,  0}, s = -0.63e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 1,  0,  0,  0,  1,  0,  0,  0}, s = -0.63e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 0,  1,  2, -2,  3,  0,  0,  0}, s = 0.46e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 0,  1,  2, -2,  1,  0,  0,  0}, s = 0.45e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 0,  0,  4, -4,  4,  0,  0,  0}, s = 0.36e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 0,  0,  1, -1,  1, -8, 12,  0}, s = -0.24e-6, c = -0.12e-6 },
      new TERM { nfa = new int[] { 0,  0,  2,  0,  0,  0,  0,  0}, s = 0.32e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 0,  0,  2,  0,  2,  0,  0,  0}, s = 0.28e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 1,  0,  2,  0,  3,  0,  0,  0}, s = 0.27e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 1,  0,  2,  0,  1,  0,  0,  0}, s = 0.26e-6, c = 0.00e-6 },

   /* 21-30 */
      new TERM { nfa = new int[] { 0,  0,  2, -2,  0,  0,  0,  0}, s = -0.21e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 0,  1, -2,  2, -3,  0,  0,  0}, s = 0.19e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 0,  1, -2,  2, -1,  0,  0,  0}, s = 0.18e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 0,  0,  0,  0,  0,  8,-13, -1}, s = -0.10e-6, c = 0.05e-6 },
      new TERM { nfa = new int[] { 0,  0,  0,  2,  0,  0,  0,  0}, s = 0.15e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 2,  0, -2,  0, -1,  0,  0,  0}, s = -0.14e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 1,  0,  0, -2,  1,  0,  0,  0}, s = 0.14e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 0,  1,  2, -2,  2,  0,  0,  0}, s = -0.14e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 1,  0,  0, -2, -1,  0,  0,  0}, s = 0.14e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 0,  0,  4, -2,  4,  0,  0,  0}, s = 0.13e-6, c = 0.00e-6 },

   /* 31-33 */
      new TERM { nfa = new int[] { 0,  0,  2, -2,  4,  0,  0,  0}, s = -0.11e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 1,  0, -2,  0, -3,  0,  0,  0}, s = 0.11e-6, c = 0.00e-6 },
      new TERM { nfa = new int[] { 1,  0, -2,  0, -1,  0,  0,  0}, s = 0.11e-6, c = 0.00e-6 }
   };

            /* Terms of order t^1 */
            TERM[] e1 = new TERM[] {
      new TERM { nfa = new int[] { 0,  0,  0,  0,  1,  0,  0,  0}, s = -0.87e-6, c = 0.00e-6 }
   };

            /* Number of terms in the series */
            //const int NE0 = (int) (sizeof e0 / sizeof (TERM));
            //const int NE1 = (int) (sizeof e1 / sizeof (TERM));
            int NE0 = e0.GetLength(0); // by AA
            int NE1 = e1.GetLength(0); // by AA

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

            /* Evaluate the EE complementary terms. */
            s0 = 0.0;
            s1 = 0.0;

            for (i = NE0 - 1; i >= 0; i--)
            {
                a = 0.0;
                for (j = 0; j < 8; j++)
                {
                    a += (double)(e0[i].nfa[j]) * fa[j];
                }
                s0 += e0[i].s * Math.Sin(a) + e0[i].c * Math.Cos(a);
            }

            for (i = NE1 - 1; i >= 0; i--)
            {
                a = 0.0;
                for (j = 0; j < 8; j++)
                {
                    a += (double)(e1[i].nfa[j]) * fa[j];
                }
                s1 += e1[i].s * Math.Sin(a) + e1[i].c * Math.Cos(a);
            }

            eect = (s0 + s1 * t) * DAS2R;

            return eect;
        }
    }
}
