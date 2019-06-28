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
        /// Modulus of pv-vector.
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
        /// <param name="pv">pv-vector</param>
        /// <param name="r">modulus of position component</param>
        /// <param name="s">modulus of velocity component</param>
        public static void wwaPvm(double[,] pv, ref double r, ref double s)
        {
            /* Distance. */
            //r = wwaPm(pv[0]);
            r = wwaPm(CopyArray(pv, 0)); // by AA

            /* Speed. */
            //s = wwaPm(pv[1]);
            s = wwaPm(CopyArray(pv, 1));

            return;
        }
    }
}
