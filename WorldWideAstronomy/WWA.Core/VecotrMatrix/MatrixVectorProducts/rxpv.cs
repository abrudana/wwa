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
        /// Multiply a pv-vector by an r-matrix.
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
        /// Given:
        /// <param name="r">r-matrix</param>
        /// <param name="pv">pv-vector</param>
        /// Returned:
        /// <param name="rpv">r * pv</param>
        public static void wwaRxpv(double[,] r, double[,] pv, double[,] rpv)
        {
            double[] n = new double[rpv.GetLength(1)];

            wwaRxp(r, CopyArray(pv, 0), n);

            AddArray2(ref rpv, n, 0);

            wwaRxp(r, CopyArray(pv, 1), n);

            AddArray2(ref rpv, n, 1);

            return;
        }
    }
}
