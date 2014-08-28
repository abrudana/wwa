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
        /// Earth rotation angle, supplied by the caller explicitly.
        /// </summary>
        /// 
        /// <remarks>
        /// This function is derived from the International Astronomical Union's
        /// SOFA (Standards of Fundamental Astronomy) software collection.
        /// http://www.iausofa.org
        /// The code does not itself constitute software provided by and/or endorsed by SOFA.
        /// This version is intended to retain identical functionality to the SOFA library, but
        /// made distinct through different function names (prefixes) and C# language specific
        /// modifications in code.
        /// </remarks>
        /// <param name="theta">Earth rotation angle (radians, Note 2)</param>
        /// <param name="astrom">star-independent astrometry parameters:</param>
        public static void wwaAper(double theta, ref wwaASTROM astrom)
        {
            astrom.eral = theta + astrom.along;
        }
    }
}