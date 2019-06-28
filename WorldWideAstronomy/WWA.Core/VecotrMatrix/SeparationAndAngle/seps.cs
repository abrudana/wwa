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
        /// Angular separation between two sets of spherical coordinates.
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
        /// <param name="al">first longitude (radians)</param>
        /// <param name="ap">first latitude (radians)</param>
        /// <param name="bl">second longitude (radians)</param>
        /// <param name="bp">second latitude (radians)</param>
        /// <returns>angular separation (radians)</returns>
        public static double wwaSeps(double al, double ap, double bl, double bp)
        {
            double[] ac = new double[3];
            double[] bc = new double[3];
            double s;

            /* Spherical to Cartesian. */
            wwaS2c(al, ap, ac);
            wwaS2c(bl, bp, bc);

            /* Angle between the vectors. */
            s = wwaSepp(ac, bc);

            return s;
        }
    }
}
