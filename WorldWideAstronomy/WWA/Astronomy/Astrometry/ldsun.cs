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
        /// 
        /// Notes:
        /// 
        /// 1) The source is presumed to be sufficiently distant that its
        /// directions seen from the Sun and the observer are essentially
        /// the same.
        /// 
        /// 2) The deflection is restrained when the angle between the star and
        /// the center of the Sun is less than a threshold value, falling to
        /// zero deflection for zero separation.The chosen threshold value
        /// is within the solar limb for all solar-system applications, and
        /// is about 5 arcminutes for the case of a terrestrial observer.
        /// 
        /// 3) The arguments p and p1 can be the same array.
        /// 
        /// Called:
        /// wwaLd        light deflection by a solar-system body
        public static void wwaLdsun(double[] p, double[] e, double em, double[] p1)
        {
            double em2, dlim;

            /* Deflection limiter (smaller for distant observers). */
            em2 = em * em;
            if (em2 < 1.0) em2 = 1.0;
            dlim = 1e-6 / (em2 > 1.0 ? em2 : 1.0);

            /* Apply the deflection. */
            //wwaLd(1.0, p, p, e, em, 1e-9, p1); // old
            wwaLd(1.0, p, p, e, em, dlim, p1);
        }
    }
}