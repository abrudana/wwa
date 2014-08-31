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
        /// Light deflection by the Sun.
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
        /// <param name="p">direction from observer to source (unit vector)</param>
        /// <param name="e">direction from Sun to observer (unit vector)</param>
        /// <param name="em">distance from Sun to observer (au)</param>
        /// <param name="p1">observer to deflected source (unit vector)</param>
        public static void wwaLdsun(double[] p, double[] e, double em, double[] p1)
        {
            wwaLd(1.0, p, p, e, em, 1e-9, p1);
        }
    }
}