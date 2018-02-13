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
        /// Time scale transformation:  Geocentric Coordinate Time, TCG, to
        /// Terrestrial Time, TT.
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
        /// <param name="tcg1">TCG as a 2-part Julian Date</param>
        /// <param name="tcg2">TCG as a 2-part Julian Date</param>
        /// Returned:
        /// <param name="tt1">TT as a 2-part Julian Date</param>
        /// <param name="tt2">TT as a 2-part Julian Date</param>
        /// <returns>status:  0 = OK</returns>
        public static int wwaTcgtt(double tcg1, double tcg2, ref double tt1, ref double tt2)
        {

            /* 1977 Jan 1 00:00:32.184 TT, as MJD */
            const double t77t = DJM77 + TTMTAI / DAYSEC;


            /* Result, safeguarding precision. */
            if (tcg1 > tcg2)
            {
                tt1 = tcg1;
                tt2 = tcg2 - ((tcg1 - DJM0) + (tcg2 - t77t)) * ELG;
            }
            else
            {
                tt1 = tcg1 - ((tcg2 - DJM0) + (tcg1 - t77t)) * ELG;
                tt2 = tcg2;
            }

            /* OK status. */
            return 0;
        }
    }
}
