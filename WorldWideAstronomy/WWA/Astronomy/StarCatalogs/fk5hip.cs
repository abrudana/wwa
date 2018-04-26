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
        /// FK5 to Hipparcos rotation and spin.
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
        /// <param name="r5h">r-matrix: FK5 rotation wrt Hipparcos (Note 2)</param>
        /// <param name="s5h">r-vector: FK5 spin wrt Hipparcos (Note 3)</param>
        public static void wwaFk5hip(double[,] r5h, double[] s5h)
        {
            double[] v = new double[3];

            /* FK5 wrt Hipparcos orientation and spin (radians, radians/year) */
            double epx, epy, epz;
            double omx, omy, omz;


            epx = -19.9e-3 * DAS2R;
            epy = -9.1e-3 * DAS2R;
            epz = 22.9e-3 * DAS2R;

            omx = -0.30e-3 * DAS2R;
            omy = 0.60e-3 * DAS2R;
            omz = 0.70e-3 * DAS2R;

            /* FK5 to Hipparcos orientation expressed as an r-vector. */
            v[0] = epx;
            v[1] = epy;
            v[2] = epz;

            /* Re-express as an r-matrix. */
            wwaRv2m(v, r5h);

            /* Hipparcos wrt FK5 spin expressed as an r-vector. */
            s5h[0] = omx;
            s5h[1] = omy;
            s5h[2] = omz;

            return;
        }
    }
}
