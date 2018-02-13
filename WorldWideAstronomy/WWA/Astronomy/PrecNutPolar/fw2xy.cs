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
        /// CIP X,Y given Fukushima-Williams bias-precession-nutation angles.
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
        /// <param name="gamb">F-W angle gamma_bar (radians)</param>
        /// <param name="phib">F-W angle phi_bar (radians)</param>
        /// <param name="psi">F-W angle psi (radians)</param>
        /// <param name="eps">F-W angle epsilon (radians)</param>
        /// <param name="x">CIP unit vector X,Y</param>
        /// <param name="y">CIP unit vector X,Y</param>
        public static void wwaFw2xy(double gamb, double phib, double psi, double eps, ref double x, ref double y)
        {
            double[,] r = new double[3, 3];

            /* Form NxPxB matrix. */
            wwaFw2m(gamb, phib, psi, eps, r);

            /* Extract CIP X,Y. */
            wwaBpn2xy(r, ref x, ref y);

            return;
        }
    }
}
