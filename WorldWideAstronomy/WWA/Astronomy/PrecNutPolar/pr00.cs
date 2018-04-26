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
        /// Precession-rate part of the IAU 2000 precession-nutation models
        /// (part of MHB2000).
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
        /// <param name="dpsipr">precession corrections (Notes 2,3)</param>
        /// <param name="depspr">precession corrections (Notes 2,3)</param>
        public static void wwaPr00(double date1, double date2, ref double dpsipr, ref double depspr)
        {
            double t;

            /* Precession and obliquity corrections (radians per century) */
            double PRECOR = -0.29965 * DAS2R;
            double OBLCOR = -0.02524 * DAS2R;

            //const double PRECOR = -0.29965 * (DAS2R * 10e5); // by AA !!!!!!!!!!!!!
            //const double OBLCOR = -0.02524 * (DAS2R * 10e6); // by AA !!!!!!!!!!!!!

            /* Interval between fundamental epoch J2000.0 and given date (JC). */
            t = ((date1 - DJ00) + date2) / DJC;
            
            /* Precession rate contributions with respect to IAU 1976/80. */
            dpsipr = PRECOR * t;
            depspr = OBLCOR * t;

            return;
        }
    }
}
