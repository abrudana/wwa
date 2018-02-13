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
        /// This function forms three Euler angles which implement general
        /// precession from epoch J2000.0, using the IAU 2006 model.  Frame
        /// bias (the offset between ICRS and mean J2000.0) is included.
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
        /// <param name="bzeta">1st rotation: radians cw around z</param>
        /// <param name="bz">3rd rotation: radians cw around z</param>
        /// <param name="btheta">2nd rotation: radians ccw around y</param>
        public static void wwaPb06(double date1, double date2, ref double bzeta, ref double bz, ref double btheta)
        {
            double[,] r = new double[3, 3];
            double r31, r32;


            /* Precession matrix via Fukushima-Williams angles. */
            wwaPmat06(date1, date2, r);

            /* Solve for z. */
            bz = Math.Atan2(r[1, 2], r[0, 2]);

            /* Remove it from the matrix. */
            wwaRz(bz, r);

            /* Solve for the remaining two angles. */
            bzeta = Math.Atan2(r[1, 0], r[1, 1]);
            r31 = r[2, 0];
            r32 = r[2, 1];
            btheta = Math.Atan2(-dsign(Math.Sqrt(r31 * r31 + r32 * r32), r[0, 2]), r[2, 2]);

            return;
        }
    }
}

