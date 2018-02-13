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
        /// Apply light deflection by a solar-system body, as part of
        /// transforming coordinate direction into natural direction.
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
        /// <param name="bm">mass of the gravitating body (solar masses)</param>
        /// <param name="p">direction from observer to source (unit vector)</param>
        /// <param name="q">direction from body to source (unit vector)</param>
        /// <param name="e">direction from body to observer (unit vector)</param>
        /// <param name="em">distance from body to observer (au)</param>
        /// <param name="dlim">deflection limiter (Note 4)</param>
        /// <param name="p1">observer to deflected source (unit vector)</param>
        public static void wwaLd(double bm, double[] p, double[] q, double[] e, double em, double dlim, double[] p1)
        {
            if (e == null)
                e = new double[3];

            int i;
            double[] qpe = new double[3] { 0, 0, 0 };
            double[] eq = new double[3] { 0, 0, 0 };
            double[] peq = new double[3];
            double qdqpe, w;

            /* q . (q + e). */
            for (i = 0; i < 3; i++)
            {
                qpe[i] = q[i] + e[i];
            }
            qdqpe = wwaPdp(q, qpe);

            /* 2 x G x bm / ( em x c^2 x ( q . (q + e) ) ). */
            w = bm * SRS / em / gmax(qdqpe, dlim);

            /* p x (e x q). */
            wwaPxp(e, q, eq);
            wwaPxp(p, eq, peq);

            /* Apply the deflection. */
            for (i = 0; i < 3; i++)
            {
                p1[i] = p[i] + w * peq[i];
            }
        }
    }
}