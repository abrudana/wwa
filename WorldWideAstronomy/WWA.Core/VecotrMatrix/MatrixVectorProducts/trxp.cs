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
        /// Multiply a p-vector by the transpose of an r-matrix.
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
        /// Given:
        /// <param name="r">r-matrix</param>
        /// <param name="p">p-vector</param>
        /// Returned:
        /// <param name="trp">r * p</param>
        public static void wwaTrxp(double[,] r, double[] p, double[] trp)
        {
            double[,] tr = new double[3, 3];

            /* Transpose of matrix r. */
            wwaTr(r, tr);

            /* Matrix tr * vector p -> vector trp. */
            wwaRxp(tr, p, trp);

            return;
        }
    }
}
