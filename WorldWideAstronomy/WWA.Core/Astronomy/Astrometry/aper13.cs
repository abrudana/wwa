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
        /// In the star-independent astrometry parameters, update only the
        /// Earth rotation angle.  The caller provides UT1, (n.b. not UTC).
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
        /// <param name="ut11">UT1 as a 2-part...</param>
        /// <param name="ut12">...Julian Date (Note 1)</param>
        /// <param name="astrom">star-independent astrometry parameters</param>
        public static void wwaAper13(double ut11, double ut12, ref wwaASTROM astrom)
        {
            wwaAper(wwaEra00(ut11, ut12), ref astrom);
        }
    }
}