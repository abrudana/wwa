using System;

namespace WorldWideAstronomy
{
    public static partial class WWA
    {
        /// <summary>
        /// Convert a J2000.0 FK5 star position to B1950.0 FK4, assuming zero proper motion in FK5 and parallax.
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
        /// <param name="r1950"></param>
        /// <param name="d1950"></param>
        /// <param name="bepoch"></param>
        /// <param name="r2000"></param>
        /// <param name="d2000"></param>
        public static void wwaFk54z(double r2000, double d2000, double bepoch, 
              ref double r1950, ref double d1950,
              ref double dr1950, ref double dd1950)
        {
            double[] p = new double[3];
            double[] v = new double[3];
            double r = 0, d = 0, pr = 0, pd = 0, px = 0, rv = 0, w;
            int i;


            /* FK5 equinox J2000.0 to FK4 equinox B1950.0. */
            wwaFk524(r2000, d2000, 0.0, 0.0, 0.0, 0.0,
                     ref r, ref d, ref pr, ref pd, ref px, ref rv);

            /* Spherical to Cartesian. */
            wwaS2c(r, d, p);

            /* Fictitious proper motion (radians per year). */
            v[0] = -pr * p[1] - pd * Math.Cos(r) * Math.Sin(d);
            v[1] = pr * p[0] - pd * Math.Sin(r) * Math.Sin(d);
            v[2] = pd * Math.Cos(d);

            /* Apply the motion. */
            w = bepoch - 1950.0;
            for (i = 0; i < 3; i++)
            {
                p[i] += w * v[i];
            }

            /* Cartesian to spherical. */
            wwaC2s(p, ref w, ref d1950);
            r1950 = wwaAnp(w);

            /* Fictitious proper motion. */
            dr1950 = pr;
            dd1950 = pd;

            /* Finished. */
        }
    }
}
