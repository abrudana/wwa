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
        /// Position-angle from spherical coordinates.
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
        /// <param name="al">longitude of point A (e.g. RA) in radians</param>
        /// <param name="ap">latitude of point A (e.g. Dec) in radians</param>
        /// <param name="bl">longitude of point B</param>
        /// <param name="bp">latitude of point B</param>
        /// <returns>position angle of B with respect to A</returns>
        public static double wwaPas(double al, double ap, double bl, double bp)
        {
            double dl, x, y, pa;

            dl = bl - al;
            y = Math.Sin(dl) * Math.Cos(bp);
            x = Math.Sin(bp) * Math.Cos(ap) - Math.Cos(bp) * Math.Sin(ap) * Math.Cos(dl);
            pa = ((x != 0.0) || (y != 0.0)) ? Math.Atan2(y, x) : 0.0;

            return pa;
        }
    }
}
