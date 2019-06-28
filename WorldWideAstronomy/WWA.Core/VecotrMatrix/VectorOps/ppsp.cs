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
        /// P-vector plus scaled p-vector.
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
        /// <param name="a">first p-vector</param>
        /// <param name="s">scalar (multiplier for b)</param>
        /// <param name="b">second p-vector</param>
        /// <param name="apsb">a + s*b</param>
        public static void wwaPpsp(double[] a, double s, double[] b, double[] apsb)
        {
            double[] sb = new double[3];

            /* s*b. */
            wwaSxp(s, b, sb);

            /* a + s*b. */
            wwaPpp(a, sb, apsb);

            return;
        }
    }
}
