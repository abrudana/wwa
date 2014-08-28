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
        /// Initialize an r-matrix to the null matrix.
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
        /// Returned:
        /// <param name="r">r-matrix</param>
        public static void wwaZr(double[,] r)
        {
            r[0, 0] = 0.0;
            r[0, 1] = 0.0;
            r[0, 2] = 0.0;
            r[1, 0] = 0.0;
            r[1, 1] = 0.0;
            r[1, 2] = 0.0;
            r[2, 0] = 0.0;
            r[2, 1] = 0.0;
            r[2, 2] = 0.0;

            return;
        }
    }
}
