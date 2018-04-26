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
        /// Time scale transformation:  Universal Time, UT1, to Coordinated
        /// Universal Time, UTC.
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
        /// <param name="ut11">UT1 as a 2-part Julian Date (Note 1)</param>
        /// <param name="ut12">UT1 as a 2-part Julian Date (Note 1)</param>
        /// <param name="dut1">Delta UT1: UT1-UTC in seconds (Note 2)</param>
        /// Returned:
        /// <param name="utc1">UTC as a 2-part quasi Julian Date (Notes 3,4)</param>
        /// <param name="utc2">UTC as a 2-part quasi Julian Date (Notes 3,4)</param>
        /// <returns>status: 
        /// +1 = dubious year (Note 5)
        /// 0 = OK
        /// -1 = unacceptable date
        /// </returns>
        public static int wwaUt1utc(double ut11, double ut12, double dut1, ref double utc1, ref double utc2)
        {
            bool big1;
            int i, iy = 0, im = 0, id = 0, js = 0;
            double duts, u1, u2, d1, dats1, d2, fd = 0, dats2 = 0, ddats, us1, us2, du;

            /* UT1-UTC in seconds. */
            duts = dut1;

            /* Put the two parts of the UT1 into big-first order. */
            big1 = (ut11 >= ut12 ? true : false);
            if (big1)
            {
                u1 = ut11;
                u2 = ut12;
            }
            else
            {
                u1 = ut12;
                u2 = ut11;
            }

            /* See if the UT1 can possibly be in a leap-second day. */
            d1 = u1;
            dats1 = 0;
            for (i = -1; i <= 3; i++)
            {
                d2 = u2 + (double)i;
                if (wwaJd2cal(d1, d2, ref iy, ref im, ref id, ref fd) == 0 ? false : true) return -1;
                js = wwaDat(iy, im, id, 0.0, ref dats2);
                if (js < 0) return -1;
                if (i == -1) dats1 = dats2;
                ddats = dats2 - dats1;
                if (Math.Abs(ddats) >= 0.5)
                {
                    /* Yes, leap second nearby: ensure UT1-UTC is "before" value. */
                    if (ddats * duts >= 0) duts -= ddats;

                    /* UT1 for the start of the UTC day that ends in a leap. */
                    if (wwaCal2jd(iy, im, id, ref d1, ref d2) == 0 ? false : true) return -1;
                    us1 = d1;
                    us2 = d2 - 1.0 + duts / DAYSEC;

                    /* Is the UT1 after this point? */
                    du = u1 - us1;
                    du += u2 - us2;
                    if (du > 0)
                    {

                        /* Yes:  fraction of the current UTC day that has elapsed. */
                        fd = du * DAYSEC / (DAYSEC + ddats);

                        /* Ramp UT1-UTC to bring about SOFA's JD(UTC) convention. */
                        duts += ddats * (fd <= 1.0 ? fd : 1.0);
                    }

                    /* Done. */
                    break;
                }
                dats1 = dats2;
            }

            /* Subtract the (possibly adjusted) UT1-UTC from UT1 to give UTC. */
            u2 -= duts / DAYSEC;

            /* Result, safeguarding precision. */
            if (big1)
            {
                utc1 = u1;
                utc2 = u2;
            }
            else
            {
                utc1 = u2;
                utc2 = u1;
            }

            /* Status. */
            return js;
        }
    }
}
