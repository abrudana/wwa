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
        /// Form rotation matrix given the Fukushima-Williams angles.
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
        /// <param name="r">rotation matrix</param>
        public static void wwaFw2m(double gamb, double phib, double psi, double eps, double[,] r)
        {
            /* Construct the matrix. */
            wwaIr(r);
            wwaRz(gamb, r);
            wwaRx(phib, r);
            wwaRz(-psi, r);
            wwaRx(-eps, r);

            return;
        }
    }
}
