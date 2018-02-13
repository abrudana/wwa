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
        /// Extend a p-vector to a pv-vector by appending a zero velocity.
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
        /// <param name="p">p-vector</param>
        /// Returned:
        /// <param name="pv">pv-vector</param>
        public static void wwaP2pv(double[] p, double[,] pv)
        {
            double[] pv1 = new double[3];

            //wwaCp(p, CopyArray(pv, 0));
            pv1 = CopyArray(pv, 0);
            wwaCp(p, pv1);
            AddArray2(ref pv, pv1, 0);

            //wwaZp(CopyArray(pv, 1));
            pv1 = CopyArray(pv, 1);
            wwaZp(pv1);
            AddArray2(ref pv, pv1, 1);

            return;
        }
    }
}
