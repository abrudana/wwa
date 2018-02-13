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
        /// Multiply a p-vector by an r-matrix.
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
        /// <param name="p">p-vector</param>
        /// Returned:
        /// <param name="rp">r * p</param>
        public static void wwaRxp(double[,] r, double[] p, double[] rp)
        {
            if (r == null)
                r = new double[3, 3];

            double w;
            double[] wrp = new double[3];
            int i, j;

            /* Matrix r * vector p. */
            for (j = 0; j < 3; j++)
            {
                w = 0.0;
                for (i = 0; i < 3; i++)
                {
                    w += r[j, i] * p[i];
                }
                wrp[j] = w;
            }

            /* Return the result. */
            wwaCp(wrp, rp);

            return;
        }
    }
}
