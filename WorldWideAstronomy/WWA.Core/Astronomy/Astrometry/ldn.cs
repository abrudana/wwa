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
        /// For a star, apply light deflection by multiple solar-system bodies,
        /// as part of transforming coordinate direction into natural direction.
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
        /// <param name="n">number of bodies (note 1)</param>
        /// <param name="b">data for each of the n bodies (Notes 1,2):</param>
        /// <param name="ob">barycentric position of the observer (au)</param>
        /// <param name="sc">observer to star coord direction (unit vector)</param>
        /// <param name="sn">observer to deflected star (unit vector)</param>
        public static void wwaLdn(int n, wwaLDBODY[] b, double[] ob, double[] sc, double[] sn)
        {
            /* Light time for 1 AU (days) */
            const double CR = AULT / DAYSEC;

            int i;
            double[] v = new double[3] { 0, 0, 0 };
            double[] ev = new double[3] { 0, 0, 0 };
            double[] e = new double[3] { 0, 0, 0 };
            double em = 0, dt;

            /* Star direction prior to deflection. */
            wwaCp(sc, sn);

            /* Body by body. */
            for (i = 0; i < n; i++)
            {

                /* Body to observer vector at epoch of observation (au). */
                wwaPmp(ob, CopyArray(b[i].pv, 0), v);

                /* Minus the time since the light passed the body (days). */
                dt = wwaPdp(sn, v) * CR;

                /* Neutralize if the star is "behind" the observer. */
                dt = gmin(dt, 0.0);

                /* Backtrack the body to the time the light was passing the body. */
                wwaPpsp(v, -dt, CopyArray(b[i].pv, 1), ev);

                /* Body to observer vector as magnitude and direction. */
                wwaPn(ev, ref em, e);

                /* Apply light deflection for this body. */
                wwaLd(b[i].bm, sn, sn, e, em, b[i].dl, sn);

                /* Next body. */
            }
        }
    }
}