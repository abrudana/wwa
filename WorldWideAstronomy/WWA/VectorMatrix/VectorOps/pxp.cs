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
        /// p-vector outer (=vector=cross) product.
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
        /// <param name="axb">a x b</param>
        public static void wwaPxp(double[] a, double[] b, double[] axb)
        {
            double xa, ya, za, xb, yb, zb;

            xa = a[0];
            ya = a[1];
            za = a[2];
            xb = b[0];
            yb = b[1];
            zb = b[2];
            axb[0] = ya * zb - za * yb;
            axb[1] = za * xb - xa * zb;
            axb[2] = xa * yb - ya * xb;

            return;
        }
    }
}
