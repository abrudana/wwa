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
        /// p-vector inner (=scalar=dot) product.
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
        /// <param name="b">second p-vector</param>
        /// <returns>a . b</returns>
        public static double wwaPdp(double[] a, double[] b)
        {
            double w;

            w = a[0] * b[0]
               + a[1] * b[1]
               + a[2] * b[2];

            return w;
        }
    }
}
