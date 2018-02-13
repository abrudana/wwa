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
        /// Angular separation between two p-vectors.
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
        /// <param name="a">first p-vector (not necessarily unit length)</param>
        /// <param name="b">second p-vector (not necessarily unit length)</param>
        /// <returns>angular separation (radians, always positive)</returns>
        public static double wwaSepp(double[] a, double[] b)
        {
            double[] axb = new double[3];
            double ss, cs, s;

            /* Sine of angle between the vectors, multiplied by the two moduli. */
            wwaPxp(a, b, axb);
            ss = wwaPm(axb);

            /* Cosine of the angle, multiplied by the two moduli. */
            cs = wwaPdp(a, b);

            /* The angle. */
            s = ((ss != 0.0) || (cs != 0.0)) ? Math.Atan2(ss, cs) : 0.0;

            return s;
        }
    }
}
