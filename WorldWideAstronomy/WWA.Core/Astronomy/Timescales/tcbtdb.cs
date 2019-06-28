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
        /// Time scale transformation:  Barycentric Coordinate Time, TCB, to
        /// Barycentric Dynamical Time, TDB.
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
        /// <param name="tcb1">TCB as a 2-part Julian Date</param>
        /// <param name="tcb2">TCB as a 2-part Julian Date</param>
        /// Returned:
        /// <param name="tdb1">TDB as a 2-part Julian Date</param>
        /// <param name="tdb2">TDB as a 2-part Julian Date</param>
        /// <returns>status:  0 = OK</returns>
        public static int wwaTcbtdb(double tcb1, double tcb2, ref double tdb1, ref double tdb2)
        {

            /* 1977 Jan 1 00:00:32.184 TT, as two-part JD */
            const double t77td = DJM0 + DJM77;
            const double t77tf = TTMTAI / DAYSEC;

            /* TDB (days) at TAI 1977 Jan 1.0 */
            const double tdb0 = TDB0 / DAYSEC;

            double d;


            /* Result, safeguarding precision. */
            if (tcb1 > tcb2)
            {
                d = tcb1 - t77td;
                tdb1 = tcb1;
                tdb2 = tcb2 + tdb0 - (d + (tcb2 - t77tf)) * ELB;
            }
            else
            {
                d = tcb2 - t77td;
                tdb1 = tcb1 + tdb0 - (d + (tcb1 - t77tf)) * ELB;
                tdb2 = tcb2;
            }

            /* Status (always OK). */
            return 0;
        }
    }
}
