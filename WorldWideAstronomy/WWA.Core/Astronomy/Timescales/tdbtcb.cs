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
        /// Time scale transformation:  Barycentric Dynamical Time, TDB, to
        /// Barycentric Coordinate Time, TCB.
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
        /// <param name="tdb1">TDB as a 2-part Julian Date</param>
        /// <param name="tdb2">TDB as a 2-part Julian Date</param>
        /// Returned:
        /// <param name="tcb1">TCB as a 2-part Julian Date</param>
        /// <param name="tcb2">TCB as a 2-part Julian Date</param>
        /// <returns>status:  0 = OK</returns>
        public static int wwaTdbtcb(double tdb1, double tdb2, ref double tcb1, ref double tcb2)
        {

            /* 1977 Jan 1 00:00:32.184 TT, as two-part JD */
            const double t77td = DJM0 + DJM77;
            const double t77tf = TTMTAI / DAYSEC;

            /* TDB (days) at TAI 1977 Jan 1.0 */
            const double tdb0 = TDB0 / DAYSEC;

            /* TDB to TCB rate */
            const double elbb = ELB / (1.0 - ELB);

            double d, f;


            /* Result, preserving date format but safeguarding precision. */
            if (tdb1 > tdb2)
            {
                d = t77td - tdb1;
                f = tdb2 - tdb0;
                tcb1 = tdb1;
                tcb2 = f - (d - (f - t77tf)) * elbb;
            }
            else
            {
                d = t77td - tdb2;
                f = tdb1 - tdb0;
                tcb1 = f + (d - (f - t77tf)) * elbb;
                tcb2 = tdb2;
            }

            /* Status (always OK). */
            return 0;
        }
    }
}
