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
        /// Initialize an r-matrix to the identity matrix.
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
        /// Returned:
        /// <param name="r">r-matrix</param>
        public static void wwaIr(double[,] r)
        {
            if (r == null)
                r = new double[3, 3];

            r[0, 0] = 1.0;
            r[0, 1] = 0.0;
            r[0, 2] = 0.0;
            r[1, 0] = 0.0;
            r[1, 1] = 1.0;
            r[1, 2] = 0.0;
            r[2, 0] = 0.0;
            r[2, 1] = 0.0;
            r[2, 2] = 1.0;

            return;
        }
    }
}
