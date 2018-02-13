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
        /// Copy an r-matrix.
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
        /// <param name="r">r-matrix to be copied</param>
        /// Returned:
        /// <param name="c">copy</param>
        public static void wwaCr(double[,] r, double[,] c)
        {
            if (c == null)
                c = new double[3, 3];

            Array.Copy(r, c, r.Length);

            //wwaCp(CopyArray(r, 0), d);
            //wwaCp(CopyArray(r, 1), d);
            //wwaCp(CopyArray(r, 2), d);

            return;
        }
    }
}
