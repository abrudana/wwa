using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace SOFA.Astronomy.Astrometry
namespace WorldWideAstronomy
{
    public static partial class WWA
    {
        /// <summary>
        /// Apply aberration to transform natural direction into proper direction.
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
        /// <param name="pnat">natural direction to the source (unit vector)</param>
        /// <param name="v">observer barycentric velocity in units of c</param>
        /// <param name="s">distance between the Sun and the observer (au)</param>
        /// <param name="bm1">sqrt(1-|v|^2): reciprocal of Lorenz factor</param>
        /// <param name="ppr">proper direction to source (unit vector)</param>
        public static void wwaAb(double[] pnat, double[] v, double s, double bm1, ref double[] ppr)
        {
            int i;
            double pdv, w1, w2, r2, w, r;
            double[] p = new double[3];
            pdv = wwaPdp(pnat, v);
            w1 = 1.0 + pdv/(1.0 + bm1);
            w2 = SRS/s;
            r2 = 0.0;
            for (i = 0; i < 3; i++)
            {
                w = pnat[i]*bm1 + w1*v[i] + w2*(v[i] - pdv*pnat[i]);
                p[i] = w;
                r2 = r2 + w*w;
            }

            r = Math.Sqrt(r2);
            for (i = 0; i < 3; i++)
            {
                ppr[i] = p[i]/r;
            }
        }
    }
}