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
        /// Time scale transformation:  Terrestrial Time, TT, to Geocentric
        /// Coordinate Time, TCG.
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
        /// <param name="tt1">TT as a 2-part Julian Date</param>
        /// <param name="tt2">TT as a 2-part Julian Date</param>
        /// Returned:
        /// <param name="tcg1">TCG as a 2-part Julian Date</param>
        /// <param name="tcg2">TCG as a 2-part Julian Date</param>
        /// <returns>status:  0 = OK</returns>
        public static int wwaTttcg(double tt1, double tt2, ref double tcg1, ref double tcg2)
        {

            /* 1977 Jan 1 00:00:32.184 TT, as MJD */
            const double t77t = DJM77 + TTMTAI / DAYSEC;

            /* TT to TCG rate */
            const double elgg = ELG / (1.0 - ELG);


            /* Result, safeguarding precision. */
            if (tt1 > tt2)
            {
                tcg1 = tt1;
                tcg2 = tt2 + ((tt1 - DJM0) + (tt2 - t77t)) * elgg;
            }
            else
            {
                tcg1 = tt1 + ((tt2 - DJM0) + (tt1 - t77t)) * elgg;
                tcg2 = tt2;
            }

            /* Status (always OK). */
            return 0;
        }
    }
}
