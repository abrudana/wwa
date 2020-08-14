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
            double y, x;

            /* Precession matrix via Fukushima-Williams angles. */
            wwaPmat06(date1, date2, r);

            /* Solve for z, choosing the +/- pi alternative. */
            y = r[1, 2];
            x = -r[0, 2];
            if (x < 0.0)
            {
                y = -y;
                x = -x;
            }
            bz = (x != 0.0 || y != 0.0) ? -Math.Atan2(y, x) : 0.0;

            /* Derotate it out of the matrix. */
            wwaRz(bz, r);

            /* Solve for the remaining two angles. */
            y = r[0, 2];
            x = r[2, 2];
            btheta = (x != 0.0 || y != 0.0) ? -Math.Atan2(y, x) : 0.0;

            y = -r[1, 0];
            x = r[1, 1];
            bzeta = (x != 0.0 || y != 0.0) ? -Math.Atan2(y, x) : 0.0;

            return;
        }
    }
}

