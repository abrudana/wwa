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
        /// This function is derived from the International Astronomical Union's
        /// SOFA (Standards of Fundamental Astronomy) software collection.
        /// http://www.iausofa.org
        /// The code does not itself constitute software provided by and/or endorsed by SOFA.
        /// This version is intended to retain identical functionality to the SOFA library, but
        /// made distinct through different function names (prefixes) and C# language specific
        /// modifications in code.
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
