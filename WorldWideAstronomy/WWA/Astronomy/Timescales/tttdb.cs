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
        /// Time scale transformation:  Terrestrial Time, TT, to Barycentric
        /// Dynamical Time, TDB.
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
        /// <param name="dtr">TDB-TT in seconds</param>
        /// Returned:
        /// <param name="tdb1">TDB as a 2-part Julian Date</param>
        /// <param name="tdb2">TDB as a 2-part Julian Date</param>
        /// <returns>status:  0 = OK</returns>
        public static int wwaTttdb(double tt1, double tt2, double dtr, ref double tdb1, ref double tdb2)
        {
            double dtrd;


            /* Result, safeguarding precision. */
            dtrd = dtr / DAYSEC;
            if (tt1 > tt2)
            {
                tdb1 = tt1;
                tdb2 = tt2 + dtrd;
            }
            else
            {
                tdb1 = tt1 + dtrd;
                tdb2 = tt2;
            }

            /* Status (always OK). */
            return 0;
        }
    }
}
