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
        /// Position-angle from two p-vectors.
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
        /// Given:
        /// <param name="a">direction of reference point</param>
        /// <param name="b">direction of point whose PA is required</param>
        /// <returns>position angle of b with respect to a (radians)</returns>
        public static double wwaPap(double[] a, double[] b)
        {
            double am = 0;
            double[] au = new double[3];
            double bm, st, ct, xa, ya, za;
            double[] eta = new double[3];
            double[] xi = new double[3];
            double[] a2b = new double[3];
            double pa;

            /* Modulus and direction of the a vector. */
            wwaPn(a, ref am, au);

            /* Modulus of the b vector. */
            bm = wwaPm(b);

            /* Deal with the case of a null vector. */
            if ((am == 0.0) || (bm == 0.0))
            {
                st = 0.0;
                ct = 1.0;
            }
            else
            {
                /* The "north" axis tangential from a (arbitrary length). */
                xa = a[0];
                ya = a[1];
                za = a[2];
                eta[0] = -xa * za;
                eta[1] = -ya * za;
                eta[2] = xa * xa + ya * ya;

                /* The "east" axis tangential from a (same length). */
                wwaPxp(eta, au, xi);

                /* The vector from a to b. */
                wwaPmp(b, a, a2b);

                /* Resolve into components along the north and east axes. */
                st = wwaPdp(a2b, xi);
                ct = wwaPdp(a2b, eta);

                /* Deal with degenerate cases. */
                if ((st == 0.0) && (ct == 0.0)) ct = 1.0;
            }

            /* Position angle. */
            pa = Math.Atan2(st, ct);

            return pa;
        }
    }
}
