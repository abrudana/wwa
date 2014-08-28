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
        /// Copy a position/velocity vector.
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
        /// Given:
        /// <param name="pv">position/velocity vector to be copied</param>
        /// Returned:
        /// <param name="c">copy</param>
        public static void wwaCpv(double[,] pv, double[,] c)
        {
            Array.Copy(pv, c, pv.Length);

            //wwaCp(CopyArray(pv, 0), CopyArray(c, 0));
            //wwaCp(CopyArray(pv, 1), CopyArray(c, 1));

            return;
        }
    }
}
