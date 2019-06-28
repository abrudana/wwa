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
        /// Frame bias components of IAU 2000 precession-nutation models (part
        /// of MHB2000 with additions).
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
        /// <param name="dpsibi">longitude and obliquity corrections</param>
        /// <param name="depsbi">longitude and obliquity corrections</param>
        /// <param name="dra">the ICRS RA of the J2000.0 mean equinox</param>
        public static void wwaBi00(ref double dpsibi, ref double depsbi, ref double dra)
        {
            /* The frame bias corrections in longitude and obliquity */
            const double DPBIAS = -0.041775 * DAS2R;
            const double DEBIAS = -0.0068192 * DAS2R;

            /* The ICRS RA of the J2000.0 equinox (Chapront et al., 2002) */
            const double DRA0 = -0.0146 * DAS2R;


            /* Return the results (which are fixed). */
            dpsibi = DPBIAS;
            depsbi = DEBIAS;
            dra = DRA0;

            return;
        }
    }
}
