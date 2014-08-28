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
        /// This function is derived from the International Astronomical Union's
        /// SOFA (Standards of Fundamental Astronomy) software collection.
        /// http://www.iausofa.org
        /// The code does not itself constitute software provided by and/or endorsed by SOFA.
        /// This version is intended to retain identical functionality to the SOFA library, but
        /// made distinct through different function names (prefixes) and C# language specific
        /// modifications in code.
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