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
        /// Julian Epoch to Julian Date.
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
        /// Attila Abrud�n
        /// 
        /// Please read the ReadMe.1st text file for more information.
        /// </remarks>
        /// <param name="epj">Julian Epoch (e.g. 1996.8)</param>
        /// <param name="djm0">MJD zero-point: always 2400000.5</param>
        /// <param name="djm">Modified Julian Date</param>
        public static void wwaEpj2jd(double epj, ref double djm0, ref double djm)
        {
            djm0 = DJM0;
            djm = DJM00 + (epj - 2000.0) * 365.25;

            return;
        }
    }
}
