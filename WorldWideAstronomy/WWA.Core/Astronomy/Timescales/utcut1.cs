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
        /// Time scale transformation:  Coordinated Universal Time, UTC, to
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
        /// <param name="utc1">UTC as a 2-part quasi Julian Date (Notes 1-4)</param>
        /// <param name="utc2">UTC as a 2-part quasi Julian Date (Notes 1-4)</param>
        /// <param name="dut1">Delta UT1 = UT1-UTC in seconds (Note 5)</param>
        /// Returned:
        /// <param name="ut11">UT1 as a 2-part Julian Date (Note 6)</param>
        /// <param name="ut12">UT1 as a 2-part Julian Date (Note 6)</param>
        /// <returns>status: 
        /// +1 = dubious year (Note 3)
        /// 0 = OK
        /// -1 = unacceptable date
        /// </returns>
        public static int wwaUtcut1(double utc1, double utc2, double dut1, ref double ut11, ref double ut12)
        {
            int iy = 0, im = 0, id = 0, js, jw;
            double w = 0, dat = 0, dta, tai1 = 0, tai2 = 0;

            /* Look up TAI-UTC. */
            if (wwaJd2cal(utc1, utc2, ref iy, ref im, ref id, ref w) == 0 ? false : true) return -1;
            js = wwaDat(iy, im, id, 0.0, ref dat);
            if (js < 0) return -1;

            /* Form UT1-TAI. */
            dta = dut1 - dat;

            /* UTC to TAI to UT1. */
            jw = wwaUtctai(utc1, utc2, ref tai1, ref tai2);
            if (jw < 0)
            {
                return -1;
            }
            else if (jw > 0)
            {
                js = jw;
            }
            if (wwaTaiut1(tai1, tai2, dta, ref ut11, ref ut12) == 0 ? false : true) return -1;

            /* Status. */
            return js;
        }
    }
}
