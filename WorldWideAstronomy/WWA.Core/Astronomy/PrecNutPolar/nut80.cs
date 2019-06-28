using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldWideAstronomy
{
    public static partial class WWA
    {
        /* ------------------------------------------------ */
        /* Table of multiples of arguments and coefficients */
        /* ------------------------------------------------ */
        struct X1980
        {
            public int nl, nlp, nf, nd, nom; /* coefficients of l,l',F,D,Om */
            public double sp, spt;        /* longitude sine, 1 and t coefficients */
            public double ce, cet;        /* obliquity cosine, 1 and t coefficients */
        }

        /// <summary>
        /// Nutation, IAU 1980 model.
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
        /// <param name="dpsi">nutation in longitude (radians)</param>
        /// <param name="deps">nutation in obliquity (radians)</param>
        public static void wwaNut80(double date1, double date2, ref double dpsi, ref double deps)
        {
            double t, el, elp, f, d, om, dp, de, arg, s, c;
            int j;

            /* Units of 0.1 milliarcsecond to radians */
            const double U2R = DAS2R / 1e4;

            /* ------------------------------------------------ */
            /* Table of multiples of arguments and coefficients */
            /* ------------------------------------------------ */

            /* The units for the sine and cosine coefficients are 0.1 mas and */
            /* the same per Julian century */

            X1980[] x = new X1980[] {

   /* 1-10 */
      new X1980 { nl =  0, nlp =  0, nf = 0, nd = 0, nom = 1, sp = -171996.0, spt = -174.2, ce = 92025.0, cet =   8.9 },
      new X1980 { nl =  0, nlp =  0, nf = 0, nd = 0, nom = 2, sp =    2062.0, spt =    0.2, ce =  -895.0, cet =   0.5 },
      new X1980 { nl = -2, nlp =  0, nf = 2, nd = 0, nom = 1, sp =      46.0, spt =    0.0, ce =   -24.0, cet =   0.0 },
      new X1980 { nl =  2, nlp =  0, nf =-2, nd = 0, nom = 0, sp =      11.0, spt =    0.0, ce =     0.0, cet =   0.0 },
      new X1980 { nl = -2, nlp =  0, nf = 2, nd = 0, nom = 2, sp =      -3.0, spt =    0.0, ce =     1.0, cet =   0.0 },
      new X1980 { nl =  1, nlp = -1, nf = 0, nd =-1, nom = 0, sp =      -3.0, spt =    0.0, ce =     0.0, cet =   0.0 },
      new X1980 { nl =  0, nlp = -2, nf = 2, nd =-2, nom = 1, sp =      -2.0, spt =    0.0, ce =     1.0, cet =   0.0 },
      new X1980 { nl =  2, nlp =  0, nf =-2, nd = 0, nom = 1, sp =       1.0, spt =    0.0, ce =     0.0, cet =   0.0 },
      new X1980 { nl =  0, nlp =  0, nf = 2, nd =-2, nom = 2, sp =  -13187.0, spt =   -1.6, ce =  5736.0, cet =  -3.1 },
      new X1980 { nl =  0, nlp =  1, nf = 0, nd = 0, nom = 0, sp =    1426.0, spt =   -3.4, ce =    54.0, cet =  -0.1 },

   /* 11-20 */
      new X1980 { nl =  0, nlp =  1, nf = 2, nd =-2, nom = 2, sp =    -517.0, spt =    1.2, ce =   224.0, cet =  -0.6 },
      new X1980 { nl =  0, nlp = -1, nf = 2, nd =-2, nom = 2, sp =     217.0, spt =   -0.5, ce =   -95.0, cet =   0.3 },
      new X1980 { nl =  0, nlp =  0, nf = 2, nd =-2, nom = 1, sp =     129.0, spt =    0.1, ce =   -70.0, cet =   0.0 },
      new X1980 { nl =  2, nlp =  0, nf = 0, nd =-2, nom = 0, sp =      48.0, spt =    0.0, ce =     1.0, cet =   0.0 },
      new X1980 { nl =  0, nlp =  0, nf = 2, nd =-2, nom = 0, sp =     -22.0, spt =    0.0, ce =     0.0, cet =   0.0 },
      new X1980 { nl =  0, nlp =  2, nf = 0, nd = 0, nom = 0, sp =      17.0, spt =   -0.1, ce =     0.0, cet =   0.0 },
      new X1980 { nl =  0, nlp =  1, nf = 0, nd = 0, nom = 1, sp =     -15.0, spt =    0.0, ce =     9.0, cet =   0.0 },
      new X1980 { nl =  0, nlp =  2, nf = 2, nd =-2, nom = 2, sp =     -16.0, spt =    0.1, ce =     7.0, cet =   0.0 },
      new X1980 { nl =  0, nlp = -1, nf = 0, nd = 0, nom = 1, sp =     -12.0, spt =    0.0, ce =     6.0, cet =   0.0 },
      new X1980 { nl = -2, nlp =  0, nf = 0, nd = 2, nom = 1, sp =      -6.0, spt =    0.0, ce =     3.0, cet =   0.0 },

   /* 21-30 */
      new X1980 { nl =  0, nlp = -1, nf = 2, nd =-2, nom = 1, sp =      -5.0, spt =    0.0, ce =     3.0, cet =   0.0 },
      new X1980 { nl =  2, nlp =  0, nf = 0, nd =-2, nom = 1, sp =       4.0, spt =    0.0, ce =    -2.0, cet =   0.0 },
      new X1980 { nl =  0, nlp =  1, nf = 2, nd =-2, nom = 1, sp =       4.0, spt =    0.0, ce =    -2.0, cet =   0.0 },
      new X1980 { nl =  1, nlp =  0, nf = 0, nd =-1, nom = 0, sp =      -4.0, spt =    0.0, ce =     0.0, cet =   0.0 },
      new X1980 { nl =  2, nlp =  1, nf = 0, nd =-2, nom = 0, sp =       1.0, spt =    0.0, ce =     0.0, cet =   0.0 },
      new X1980 { nl =  0, nlp =  0, nf =-2, nd = 2, nom = 1, sp =       1.0, spt =    0.0, ce =     0.0, cet =   0.0 },
      new X1980 { nl =  0, nlp =  1, nf =-2, nd = 2, nom = 0, sp =      -1.0, spt =    0.0, ce =     0.0, cet =   0.0 },
      new X1980 { nl =  0, nlp =  1, nf = 0, nd = 0, nom = 2, sp =       1.0, spt =    0.0, ce =     0.0, cet =   0.0 },
      new X1980 { nl = -1, nlp =  0, nf = 0, nd = 1, nom = 1, sp =       1.0, spt =    0.0, ce =     0.0, cet =   0.0 },
      new X1980 { nl =  0, nlp =  1, nf = 2, nd =-2, nom = 0, sp =      -1.0, spt =    0.0, ce =     0.0, cet =   0.0 },

   /* 31-40 */
      new X1980 { nl =  0, nlp =  0, nf = 2, nd = 0, nom = 2, sp =   -2274.0, spt =   -0.2, ce =   977.0, cet =  -0.5 },
      new X1980 { nl =  1, nlp =  0, nf = 0, nd = 0, nom = 0, sp =     712.0, spt =    0.1, ce =    -7.0, cet =   0.0 },
      new X1980 { nl =  0, nlp =  0, nf = 2, nd = 0, nom = 1, sp =    -386.0, spt =   -0.4, ce =   200.0, cet =   0.0 },
      new X1980 { nl =  1, nlp =  0, nf = 2, nd = 0, nom = 2, sp =    -301.0, spt =    0.0, ce =   129.0, cet =  -0.1 },
      new X1980 { nl =  1, nlp =  0, nf = 0, nd =-2, nom = 0, sp =    -158.0, spt =    0.0, ce =    -1.0, cet =   0.0 },
      new X1980 { nl = -1, nlp =  0, nf = 2, nd = 0, nom = 2, sp =     123.0, spt =    0.0, ce =   -53.0, cet =   0.0 },
      new X1980 { nl =  0, nlp =  0, nf = 0, nd = 2, nom = 0, sp =      63.0, spt =    0.0, ce =    -2.0, cet =   0.0 },
      new X1980 { nl =  1, nlp =  0, nf = 0, nd = 0, nom = 1, sp =      63.0, spt =    0.1, ce =   -33.0, cet =   0.0 },
      new X1980 { nl = -1, nlp =  0, nf = 0, nd = 0, nom = 1, sp =     -58.0, spt =   -0.1, ce =    32.0, cet =   0.0 },
      new X1980 { nl = -1, nlp =  0, nf = 2, nd = 2, nom = 2, sp =     -59.0, spt =    0.0, ce =    26.0, cet =   0.0 },

   /* 41-50 */
      new X1980 { nl =  1, nlp =  0, nf = 2, nd = 0, nom = 1, sp =     -51.0, spt =    0.0, ce =    27.0, cet =   0.0 },
      new X1980 { nl =  0, nlp =  0, nf = 2, nd = 2, nom = 2, sp =     -38.0, spt =    0.0, ce =    16.0, cet =   0.0 },
      new X1980 { nl =  2, nlp =  0, nf = 0, nd = 0, nom = 0, sp =      29.0, spt =    0.0, ce =    -1.0, cet =   0.0 },
      new X1980 { nl =  1, nlp =  0, nf = 2, nd =-2, nom = 2, sp =      29.0, spt =    0.0, ce =   -12.0, cet =   0.0 },
      new X1980 { nl =  2, nlp =  0, nf = 2, nd = 0, nom = 2, sp =     -31.0, spt =    0.0, ce =    13.0, cet =   0.0 },
      new X1980 { nl =  0, nlp =  0, nf = 2, nd = 0, nom = 0, sp =      26.0, spt =    0.0, ce =    -1.0, cet =   0.0 },
      new X1980 { nl = -1, nlp =  0, nf = 2, nd = 0, nom = 1, sp =      21.0, spt =    0.0, ce =   -10.0, cet =   0.0 },
      new X1980 { nl = -1, nlp =  0, nf = 0, nd = 2, nom = 1, sp =      16.0, spt =    0.0, ce =    -8.0, cet =   0.0 },
      new X1980 { nl =  1, nlp =  0, nf = 0, nd =-2, nom = 1, sp =     -13.0, spt =    0.0, ce =     7.0, cet =   0.0 },
      new X1980 { nl = -1, nlp =  0, nf = 2, nd = 2, nom = 1, sp =     -10.0, spt =    0.0, ce =     5.0, cet =   0.0 },

   /* 51-60 */
      new X1980 { nl =  1, nlp =  1, nf = 0, nd =-2, nom = 0, sp =      -7.0, spt =    0.0, ce =     0.0, cet =   0.0 },
      new X1980 { nl =  0, nlp =  1, nf = 2, nd = 0, nom = 2, sp =       7.0, spt =    0.0, ce =    -3.0, cet =   0.0 },
      new X1980 { nl =  0, nlp = -1, nf = 2, nd = 0, nom = 2, sp =      -7.0, spt =    0.0, ce =     3.0, cet =   0.0 },
      new X1980 { nl =  1, nlp =  0, nf = 2, nd = 2, nom = 2, sp =      -8.0, spt =    0.0, ce =     3.0, cet =   0.0 },
      new X1980 { nl =  1, nlp =  0, nf = 0, nd = 2, nom = 0, sp =       6.0, spt =    0.0, ce =     0.0, cet =   0.0 },
      new X1980 { nl =  2, nlp =  0, nf = 2, nd =-2, nom = 2, sp =       6.0, spt =    0.0, ce =    -3.0, cet =   0.0 },
      new X1980 { nl =  0, nlp =  0, nf = 0, nd = 2, nom = 1, sp =      -6.0, spt =    0.0, ce =     3.0, cet =   0.0 },
      new X1980 { nl =  0, nlp =  0, nf = 2, nd = 2, nom = 1, sp =      -7.0, spt =    0.0, ce =     3.0, cet =   0.0 },
      new X1980 { nl =  1, nlp =  0, nf = 2, nd =-2, nom = 1, sp =       6.0, spt =    0.0, ce =    -3.0, cet =   0.0 },
      new X1980 { nl =  0, nlp =  0, nf = 0, nd =-2, nom = 1, sp =      -5.0, spt =    0.0, ce =     3.0, cet =   0.0 },

   /* 61-70 */
      new X1980 { nl =  1, nlp = -1, nf = 0, nd = 0, nom = 0, sp =       5.0, spt =    0.0, ce =     0.0, cet =   0.0 },
      new X1980 { nl =  2, nlp =  0, nf = 2, nd = 0, nom = 1, sp =      -5.0, spt =    0.0, ce =     3.0, cet =   0.0 },
      new X1980 { nl =  0, nlp =  1, nf = 0, nd =-2, nom = 0, sp =      -4.0, spt =    0.0, ce =     0.0, cet =   0.0 },
      new X1980 { nl =  1, nlp =  0, nf =-2, nd = 0, nom = 0, sp =       4.0, spt =    0.0, ce =     0.0, cet =   0.0 },
      new X1980 { nl =  0, nlp =  0, nf = 0, nd = 1, nom = 0, sp =      -4.0, spt =    0.0, ce =     0.0, cet =   0.0 },
      new X1980 { nl =  1, nlp =  1, nf = 0, nd = 0, nom = 0, sp =      -3.0, spt =    0.0, ce =     0.0, cet =   0.0 },
      new X1980 { nl =  1, nlp =  0, nf = 2, nd = 0, nom = 0, sp =       3.0, spt =    0.0, ce =     0.0, cet =   0.0 },
      new X1980 { nl =  1, nlp = -1, nf = 2, nd = 0, nom = 2, sp =      -3.0, spt =    0.0, ce =     1.0, cet =   0.0 },
      new X1980 { nl = -1, nlp = -1, nf = 2, nd = 2, nom = 2, sp =      -3.0, spt =    0.0, ce =     1.0, cet =   0.0 },
      new X1980 { nl = -2, nlp =  0, nf = 0, nd = 0, nom = 1, sp =      -2.0, spt =    0.0, ce =     1.0, cet =   0.0 },

   /* 71-80 */
      new X1980 { nl =  3, nlp =  0, nf = 2, nd = 0, nom = 2, sp =      -3.0, spt =    0.0, ce =     1.0, cet =   0.0 },
      new X1980 { nl =  0, nlp = -1, nf = 2, nd = 2, nom = 2, sp =      -3.0, spt =    0.0, ce =     1.0, cet =   0.0 },
      new X1980 { nl =  1, nlp =  1, nf = 2, nd = 0, nom = 2, sp =       2.0, spt =    0.0, ce =    -1.0, cet =   0.0 },
      new X1980 { nl = -1, nlp =  0, nf = 2, nd =-2, nom = 1, sp =      -2.0, spt =    0.0, ce =     1.0, cet =   0.0 },
      new X1980 { nl =  2, nlp =  0, nf = 0, nd = 0, nom = 1, sp =       2.0, spt =    0.0, ce =    -1.0, cet =   0.0 },
      new X1980 { nl =  1, nlp =  0, nf = 0, nd = 0, nom = 2, sp =      -2.0, spt =    0.0, ce =     1.0, cet =   0.0 },
      new X1980 { nl =  3, nlp =  0, nf = 0, nd = 0, nom = 0, sp =       2.0, spt =    0.0, ce =     0.0, cet =   0.0 },
      new X1980 { nl =  0, nlp =  0, nf = 2, nd = 1, nom = 2, sp =       2.0, spt =    0.0, ce =    -1.0, cet =   0.0 },
      new X1980 { nl = -1, nlp =  0, nf = 0, nd = 0, nom = 2, sp =       1.0, spt =    0.0, ce =    -1.0, cet =   0.0 },
      new X1980 { nl =  1, nlp =  0, nf = 0, nd =-4, nom = 0, sp =      -1.0, spt =    0.0, ce =     0.0, cet =   0.0 },

   /* 81-90 */
      new X1980 { nl = -2, nlp =  0, nf = 2, nd = 2, nom = 2, sp =       1.0, spt =    0.0, ce =    -1.0, cet =   0.0 },
      new X1980 { nl = -1, nlp =  0, nf = 2, nd = 4, nom = 2, sp =      -2.0, spt =    0.0, ce =     1.0, cet =   0.0 },
      new X1980 { nl =  2, nlp =  0, nf = 0, nd =-4, nom = 0, sp =      -1.0, spt =    0.0, ce =     0.0, cet =   0.0 },
      new X1980 { nl =  1, nlp =  1, nf = 2, nd =-2, nom = 2, sp =       1.0, spt =    0.0, ce =    -1.0, cet =   0.0 },
      new X1980 { nl =  1, nlp =  0, nf = 2, nd = 2, nom = 1, sp =      -1.0, spt =    0.0, ce =     1.0, cet =   0.0 },
      new X1980 { nl = -2, nlp =  0, nf = 2, nd = 4, nom = 2, sp =      -1.0, spt =    0.0, ce =     1.0, cet =   0.0 },
      new X1980 { nl = -1, nlp =  0, nf = 4, nd = 0, nom = 2, sp =       1.0, spt =    0.0, ce =     0.0, cet =   0.0 },
      new X1980 { nl =  1, nlp = -1, nf = 0, nd =-2, nom = 0, sp =       1.0, spt =    0.0, ce =     0.0, cet =   0.0 },
      new X1980 { nl =  2, nlp =  0, nf = 2, nd =-2, nom = 1, sp =       1.0, spt =    0.0, ce =    -1.0, cet =   0.0 },
      new X1980 { nl =  2, nlp =  0, nf = 2, nd = 2, nom = 2, sp =      -1.0, spt =    0.0, ce =     0.0, cet =   0.0 },

   /* 91-100 */
      new X1980 { nl =  1, nlp =  0, nf = 0, nd = 2, nom = 1, sp =      -1.0, spt =    0.0, ce =     0.0, cet =   0.0 },
      new X1980 { nl =  0, nlp =  0, nf = 4, nd =-2, nom = 2, sp =       1.0, spt =    0.0, ce =     0.0, cet =   0.0 },
      new X1980 { nl =  3, nlp =  0, nf = 2, nd =-2, nom = 2, sp =       1.0, spt =    0.0, ce =     0.0, cet =   0.0 },
      new X1980 { nl =  1, nlp =  0, nf = 2, nd =-2, nom = 0, sp =      -1.0, spt =    0.0, ce =     0.0, cet =   0.0 },
      new X1980 { nl =  0, nlp =  1, nf = 2, nd = 0, nom = 1, sp =       1.0, spt =    0.0, ce =     0.0, cet =   0.0 },
      new X1980 { nl = -1, nlp = -1, nf = 0, nd = 2, nom = 1, sp =       1.0, spt =    0.0, ce =     0.0, cet =   0.0 },
      new X1980 { nl =  0, nlp =  0, nf =-2, nd = 0, nom = 1, sp =      -1.0, spt =    0.0, ce =     0.0, cet =   0.0 },
      new X1980 { nl =  0, nlp =  0, nf = 2, nd =-1, nom = 2, sp =      -1.0, spt =    0.0, ce =     0.0, cet =   0.0 },
      new X1980 { nl =  0, nlp =  1, nf = 0, nd = 2, nom = 0, sp =      -1.0, spt =    0.0, ce =     0.0, cet =   0.0 },
      new X1980 { nl =  1, nlp =  0, nf =-2, nd =-2, nom = 0, sp =      -1.0, spt =    0.0, ce =     0.0, cet =   0.0 },

   /* 101-106 */
      new X1980 { nl =  0, nlp = -1, nf = 2, nd = 0, nom = 1, sp =      -1.0, spt =    0.0, ce =     0.0, cet =   0.0 },
      new X1980 { nl =  1, nlp =  1, nf = 0, nd =-2, nom = 1, sp =      -1.0, spt =    0.0, ce =     0.0, cet =   0.0 },
      new X1980 { nl =  1, nlp =  0, nf =-2, nd = 2, nom = 0, sp =      -1.0, spt =    0.0, ce =     0.0, cet =   0.0 },
      new X1980 { nl =  2, nlp =  0, nf = 0, nd = 2, nom = 0, sp =       1.0, spt =    0.0, ce =     0.0, cet =   0.0 },
      new X1980 { nl =  0, nlp =  0, nf = 2, nd = 4, nom = 2, sp =      -1.0, spt =    0.0, ce =     0.0, cet =   0.0 },
      new X1980 { nl =  0, nlp =  1, nf = 0, nd = 1, nom = 0, sp =       1.0, spt =    0.0, ce =     0.0, cet =   0.0 }
   };                                                                                                     
                                                                                                          
                                                                                                                                                                                                 

            /* Number of terms in the series */
            //const int NT = (int) (sizeof x / sizeof x[0]);
            int NT = x.GetLength(0);

            /*--------------------------------------------------------------------*/

            /* Interval between fundamental epoch J2000.0 and given date (JC). */
            t = ((date1 - DJ00) + date2) / DJC;

            /* --------------------- */
            /* Fundamental arguments */
            /* --------------------- */

            /* Mean longitude of Moon minus mean longitude of Moon's perigee. */
            el = wwaAnpm(
                 (485866.733 + (715922.633 + (31.310 + 0.064 * t) * t) * t)
                 * DAS2R + Math.IEEERemainder(1325.0 * t, 1.0) * D2PI);

            /* Mean longitude of Sun minus mean longitude of Sun's perigee. */
            elp = wwaAnpm(
                  (1287099.804 + (1292581.224 + (-0.577 - 0.012 * t) * t) * t)
                  * DAS2R + Math.IEEERemainder(99.0 * t, 1.0) * D2PI);

            /* Mean longitude of Moon minus mean longitude of Moon's node. */
            f = wwaAnpm(
                (335778.877 + (295263.137 + (-13.257 + 0.011 * t) * t) * t)
                * DAS2R + Math.IEEERemainder(1342.0 * t, 1.0) * D2PI);

            /* Mean elongation of Moon from Sun. */
            d = wwaAnpm(
                (1072261.307 + (1105601.328 + (-6.891 + 0.019 * t) * t) * t)
                * DAS2R + Math.IEEERemainder(1236.0 * t, 1.0) * D2PI);

            /* Longitude of the mean ascending node of the lunar orbit on the */
            /* ecliptic, measured from the mean equinox of date. */
            om = wwaAnpm(
                 (450160.280 + (-482890.539 + (7.455 + 0.008 * t) * t) * t)
                 * DAS2R + Math.IEEERemainder(-5.0 * t, 1.0) * D2PI);

            /* --------------- */
            /* Nutation series */
            /* --------------- */

            /* Initialize nutation components. */
            dp = 0.0;
            de = 0.0;

            /* Sum the nutation terms, ending with the biggest. */
            for (j = NT - 1; j >= 0; j--)
            {

                /* Form argument for current term. */
                arg = (double)x[j].nl * el
                    + (double)x[j].nlp * elp
                    + (double)x[j].nf * f
                    + (double)x[j].nd * d
                    + (double)x[j].nom * om;

                /* Accumulate current nutation term. */
                s = x[j].sp + x[j].spt * t;
                c = x[j].ce + x[j].cet * t;
                if (s != 0.0) dp += s * Math.Sin(arg);
                if (c != 0.0) de += c * Math.Cos(arg);
            }

            /* Convert results from 0.1 mas units to radians. */
            dpsi = dp * U2R;
            deps = de * U2R;

            return;
        }
    }
}
