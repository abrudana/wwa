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
        /// Precession angles, IAU 2006 (Fukushima-Williams 4-angle formulation).
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
        /// <param name="gamb">F-W angle gamma_bar (radians)</param>
        /// <param name="phib">F-W angle phi_bar (radians)</param>
        /// <param name="psib">F-W angle psi_bar (radians)</param>
        /// <param name="epsa">F-W angle epsilon_A (radians)</param>
        public static void wwaPfw06(double date1, double date2, ref double gamb, ref double phib, ref double psib, ref double epsa)
        {
            double t;

            /* Interval between fundamental date J2000.0 and given date (JC). */
            t = ((date1 - DJ00) + date2) / DJC;

            /* P03 bias+precession angles. */
            gamb = (-0.052928 +
                    (10.556378 +
                    (0.4932044 +
                    (-0.00031238 +
                    (-0.000002788 +
                    (0.0000000260)
                    * t) * t) * t) * t) * t) * DAS2R;
            phib = (84381.412819 +
                    (-46.811016 +
                    (0.0511268 +
                    (0.00053289 +
                    (-0.000000440 +
                    (-0.0000000176)
                    * t) * t) * t) * t) * t) * DAS2R;
            psib = (-0.041775 +
                    (5038.481484 +
                    (1.5584175 +
                    (-0.00018522 +
                    (-0.000026452 +
                    (-0.0000000148)
                    * t) * t) * t) * t) * t) * DAS2R;
            epsa = wwaObl06(date1, date2);

            return;
        }
    }
}
