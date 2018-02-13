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
        /// Time scale transformation:  International Atomic Time, TAI, to
        /// Universal Time, UT1.
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
        /// <param name="tai1">TAI as a 2-part Julian Date</param>
        /// <param name="tai2">TAI as a 2-part Julian Date</param>
        /// <param name="dta">UT1-TAI in seconds</param>
        /// Returned:
        /// <param name="ut11">UT1 as a 2-part Julian Date</param>
        /// <param name="ut12">UT1 as a 2-part Julian Date</param>
        /// <returns>status:  0 = OK</returns>
        public static int wwaTaiut1(double tai1, double tai2, double dta, ref double ut11, ref double ut12)
        {
            double dtad;


            /* Result, safeguarding precision. */
            dtad = dta / DAYSEC;
            if (tai1 > tai2)
            {
                ut11 = tai1;
                ut12 = tai2 + dtad;
            }
            else
            {
                ut11 = tai1 + dtad;
                ut12 = tai2;
            }

            /* Status (always OK). */
            return 0;
        }
    }
}
