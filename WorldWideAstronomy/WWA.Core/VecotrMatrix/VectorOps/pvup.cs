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
        /// Update a pv-vector, discarding the velocity component.
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
        /// <param name="p">p-vector</param>
        public static void wwaPvup(double dt, double[,] pv, double[] p)
        {
            p[0] = pv[0, 0] + dt * pv[1, 0];
            p[1] = pv[0, 1] + dt * pv[1, 1];
            p[2] = pv[0, 2] + dt * pv[1, 2];

            return;
        }
    }
}
