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
        /// Multiply a pv-vector by a scalar.
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
        /// <param name="s">scalar</param>
        /// <param name="pv">pv-vector</param>
        /// <param name="spv">s * pv</param>
        public static void wwaSxpv(double s, double[,] pv, double[,] spv)
        {
            wwaS2xpv(s, s, pv, spv);

            return;
        }
    }
}
