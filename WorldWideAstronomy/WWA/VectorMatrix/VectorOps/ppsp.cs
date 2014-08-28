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
        /// This function is derived from the International Astronomical Union's
        /// SOFA (Standards of Fundamental Astronomy) software collection.
        /// http://www.iausofa.org
        /// The code does not itself constitute software provided by and/or endorsed by SOFA.
        /// This version is intended to retain identical functionality to the SOFA library, but
        /// made distinct through different function names (prefixes) and C# language specific
        /// modifications in code.
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
