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
        /// Multiply a pv-vector by two scalars.
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
        /// <param name="s1">scalar to multiply position component by</param>
        /// <param name="s2">scalar to multiply velocity component by</param>
        /// <param name="pv">pv-vector</param>
        /// <param name="spv">pv-vector: p scaled by s1, v scaled by s2</param>
        public static void wwaS2xpv(double s1, double s2, double[,] pv, double[,] spv)
        {
            double[] spv1 = new double[3];

            //wwaSxp(s1, CopyArray(pv, 0), CopyArray(spv, 0));
            spv1 = CopyArray(spv, 0);
            wwaSxp(s1, CopyArray(pv, 0), spv1);
            AddArray2(ref spv, spv1, 0);

            spv1 = CopyArray(spv, 1);
            wwaSxp(s2, CopyArray(pv, 1), spv1);
            AddArray2(ref spv, spv1, 1);

            return;
        }
    }
}
