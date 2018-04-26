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
        /// Gregorian Calendar to Julian Date.
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
        /// <param name="iy">year, month, day in Gregorian calendar (Note 1)</param>
        /// <param name="im">year, month, day in Gregorian calendar (Note 1)</param>
        /// <param name="id">year, month, day in Gregorian calendar (Note 1)</param>
        /// <param name="djm0">MJD zero-point: always 2400000.5</param>
        /// <param name="djm">Modified Julian Date for 0 hrs</param>
        /// <returns></returns>
        public static int wwaCal2jd(int iy, int im, int id, ref double djm0, ref double djm)
        {
            int j, ly, my;
            long iypmy;

            /* Earliest year allowed (4800BC) */
            const int IYMIN = -4799;

            /* Month lengths in days */
            int[] mtab = new int[12] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };


            /* Preset status. */
            j = 0;

            /* Validate year and month. */
            if (iy < IYMIN) return -1;
            if (im < 1 || im > 12) return -2;

            /* If February in a leap year, 1, otherwise 0. */
            //ly = ((im == 2) && !(iy % 4) && (iy % 100 || !(iy % 400)));
            //ly = ((im == 2) && ~(bool)(iy % 4) && (iy % 100 || ~(iy % 400)));
            ly = DateTime.IsLeapYear(iy) ? 1 : 0;

            /* Validate day, taking into account leap years. */
            if ((id < 1) || (id > (mtab[im - 1] + ly))) j = -3;

            /* Return result. */
            my = (im - 14) / 12;
            iypmy = (long)(iy + my);
            djm0 = DJM0;
            djm = (double)((1461L * (iypmy + 4800L)) / 4L
                          + (367L * (long)(im - 2 - 12 * my)) / 12L
                          - (3L * ((iypmy + 4900L) / 100L)) / 4L
                          + (long)id - 2432076L);

            /* Return status. */
            return j;
        }
    }
}
