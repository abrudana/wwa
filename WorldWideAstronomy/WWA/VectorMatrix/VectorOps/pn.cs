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
        /// Convert a p-vector into modulus and unit vector.
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
        /// <param name="p">p-vector</param>
        /// <param name="r">modulus</param>
        /// <param name="u">unit vector</param>
        public static void wwaPn(double[] p, ref double r, double[] u)
        {
            double w;

            /* Obtain the modulus and test for zero. */
            w = wwaPm(p);
            if (w == 0.0)
            {
                /* Null vector. */
                wwaZp(u);
            }
            else
            {
                /* Unit vector. */
                wwaSxp(1.0 / w, p, u);
            }

            /* Return the modulus. */
            r = w;

            return;
        }
    }
}
