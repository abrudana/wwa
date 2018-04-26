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
        /// IAU 2000A nutation with adjustments to match the IAU 2006 precession.
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
        /// <param name="dpsi">nutation, luni-solar + planetary (Note 2)</param>
        /// <param name="deps">nutation, luni-solar + planetary (Note 2)</param>
        public static void wwaNut06a(double date1, double date2, ref double dpsi, ref double deps)
        {
            double t, fj2, dp = 0, de = 0;


            /* Interval between fundamental date J2000.0 and given date (JC). */
            t = ((date1 - DJ00) + date2) / DJC;

            /* Factor correcting for secular variation of J2. */
            fj2 = -2.7774e-6 * t;

            /* Obtain IAU 2000A nutation. */
            wwaNut00a(date1, date2, ref dp, ref de);

            /* Apply P03 adjustments (Wallace & Capitaine, 2006, Eqs.5). */
            dpsi = dp + dp * (0.4697e-6 + fj2);
            deps = de + de * fj2;

            return;
        }
    }
}
