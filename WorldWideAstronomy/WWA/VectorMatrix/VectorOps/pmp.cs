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
        /// P-vector subtraction.
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
        /// Attila Abrud�n
        /// 
        /// Please read the ReadMe.1st text file for more information.
        /// </remarks>
        /// <param name="a">first p-vector</param>
        /// <param name="b">second p-vector</param>
        /// <param name="amb">a - b</param>
        public static void wwaPmp(double[] a, double[] b, double[] amb)
        {
            if (a == null)
                a = new double[3];

            amb[0] = a[0] - b[0];
            amb[1] = a[1] - b[1];
            amb[2] = a[2] - b[2];

            return;
        }
    }
}
