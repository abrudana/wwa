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
        /// Multiply two r-matrices.
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
        /// <param name="a">first r-matrix</param>
        /// <param name="b">second r-matrix</param>
        /// Returned:
        /// <param name="atb">a * b</param>
        public static void wwaRxr(double[,] a, double[,] b, double[,] atb)
        {
            int i, j, k;
            double w;
            double[,] wm = new double[3, 3];

            for (i = 0; i < 3; i++)
            {
                for (j = 0; j < 3; j++)
                {
                    w = 0.0;
                    for (k = 0; k < 3; k++)
                    {
                        w += a[i, k] * b[k, j];
                    }
                    wm[i, j] = w;
                }
            }
            wwaCr(wm, atb);

            return;
        }
    }
}
