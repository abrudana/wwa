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
        /// Julian Date to Julian Epoch.
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
        /// <param name="dj1">Julian Date (see note)</param>
        /// <param name="dj2">Julian Date (see note)</param>
        /// <returns>Julian Epoch</returns>
        public static double wwaEpj(double dj1, double dj2)
        {
            double epj;

            epj = 2000.0 + ((dj1 - DJ00) + dj2) / DJY;

            return epj;
        }
    }
}
