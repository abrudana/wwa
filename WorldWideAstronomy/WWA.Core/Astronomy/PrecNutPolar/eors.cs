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
        /// Equation of the origins, given the classical NPB matrix and the
        /// quantity s.
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
        /// <param name="rnpb">classical nutation x precession x bias matrix</param>
        /// <param name="s">the quantity s (the CIO locator)</param>
        /// <returns>the equation of the origins in radians.</returns>
        public static double wwaEors(double[,] rnpb, double s)
        {
            double x, ax, xs, ys, zs, p, q, eo;

            /* Evaluate Wallace & Capitaine (2006) expression (16). */
            x = rnpb[2, 0];
            ax = x / (1.0 + rnpb[2, 2]);
            xs = 1.0 - ax * x;
            ys = -ax * rnpb[2, 1];
            zs = -x;
            p = rnpb[0, 0] * xs + rnpb[0, 1] * ys + rnpb[0, 2] * zs;
            q = rnpb[1, 0] * xs + rnpb[1, 1] * ys + rnpb[1, 2] * zs;
            eo = ((p != 0) || (q != 0)) ? s - Math.Atan2(q, p) : s;

            return eo;
        }
    }
}
