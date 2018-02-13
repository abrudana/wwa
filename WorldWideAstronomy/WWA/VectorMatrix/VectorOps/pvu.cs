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
        /// Update a pv-vector.
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
        /// <param name="dt">time interval</param>
        /// <param name="pv">pv-vector</param>
        /// <param name="upv">p updated, v unchanged</param>
        public static void wwaPvu(double dt, double[,] pv, double[,] upv)
        {
            double[] upv1 = new double[3];

            //wwaPpsp(CopyArray(pv, 0), dt, CopyArray(pv, 1), CopyArray(upv, 0));
            upv1 = CopyArray(upv, 0);
            wwaPpsp(CopyArray(pv, 0), dt, CopyArray(pv, 1), upv1);
            AddArray2(ref upv, upv1, 0);

            //wwaCp(CopyArray(pv, 1), CopyArray(upv, 1));
            upv1 = CopyArray(upv, 1);
            wwaCp(CopyArray(pv, 1), upv1);
            AddArray2(ref upv, upv1, 1);

            return;
        }
    }
}
