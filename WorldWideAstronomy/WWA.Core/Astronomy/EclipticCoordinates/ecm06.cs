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
        /// ICRS equatorial to ecliptic rotation matrix, IAU 2006.
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
        /// <param name="date1">TT as a 2-part Julian date (Note 1)</param>
        /// <param name="date2">TT as a 2-part Julian date (Note 1)</param>
        /// <param name="rm">ICRS to ecliptic rotation matrix</param>
        public static void wwaEcm06(double date1, double date2, double[,] rm)
        {
            double ob;
            double[,] bp = new double[3, 3];
            double[,] e = new double[3, 3];


            /* Obliquity, IAU 2006. */
            ob = wwaObl06(date1, date2);

            /* Precession-bias matrix, IAU 2006. */
            wwaPmat06(date1, date2, bp);

            /* Equatorial of date to ecliptic matrix. */
            wwaIr(e);
            wwaRx(ob, e);

            /* ICRS to ecliptic coordinates rotation matrix, IAU 2006. */
            wwaRxr(e, bp, rm);
        }
    }
}
