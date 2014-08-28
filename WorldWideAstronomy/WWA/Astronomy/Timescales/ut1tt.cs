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
        /// Time scale transformation:  Universal Time, UT1, to Terrestrial
        /// Time, TT.
        /// </summary>
        /// 
        /// <remarks>
        /// This function is derived from the International Astronomical Union's
        /// SOFA (Standards of Fundamental Astronomy) software collection.
        /// http://www.iausofa.org
        /// The code does not itself constitute software provided by and/or endorsed by SOFA.
        /// This version is intended to retain identical functionality to the SOFA library, but
        /// made distinct through different function names (prefixes) and C# language specific
        /// modifications in code.
        /// </remarks>
        /// Given:
        /// <param name="ut11">UT1 as a 2-part Julian Date</param>
        /// <param name="ut12">UT1 as a 2-part Julian Date</param>
        /// <param name="dt">TT-UT1 in seconds</param>
        /// Returned:
        /// <param name="tt1">TT as a 2-part Julian Date</param>
        /// <param name="tt2">TT as a 2-part Julian Date</param>
        /// <returns>status:  0 = OK</returns>
        public static int wwaUt1tt(double ut11, double ut12, double dt, ref double tt1, ref double tt2)
        {
            double dtd;


            /* Result, safeguarding precision. */
            dtd = dt / DAYSEC;
            if (ut11 > ut12)
            {
                tt1 = ut11;
                tt2 = ut12 + dtd;
            }
            else
            {
                tt1 = ut11 + dtd;
                tt2 = ut12;
            }

            /* Status (always OK). */
            return 0;
        }
    }
}
