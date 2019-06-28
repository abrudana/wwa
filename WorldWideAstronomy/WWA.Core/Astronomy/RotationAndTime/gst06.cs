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
        /// Greenwich apparent sidereal time, IAU 2006, given the NPB matrix.
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
        /// <param name="uta">UT1 as a 2-part Julian Date (Notes 1,2)</param>
        /// <param name="utb">UT1 as a 2-part Julian Date (Notes 1,2)</param>
        /// <param name="tta">TT as a 2-part Julian Date (Notes 1,2)</param>
        /// <param name="ttb">TT as a 2-part Julian Date (Notes 1,2)</param>
        /// <param name="rnpb">nutation x precession x bias matrix</param>
        /// <returns>Greenwich apparent sidereal time (radians)</returns>
        public static double wwaGst06(double uta, double utb, double tta, double ttb, double[,] rnpb)
        {
            double x = 0, y = 0, s, era, eors, gst;


            /* Extract CIP coordinates. */
            wwaBpn2xy(rnpb, ref x, ref y);

            /* The CIO locator, s. */
            s = wwaS06(tta, ttb, x, y);

            /* Greenwich apparent sidereal time. */
            era = wwaEra00(uta, utb);
            eors = wwaEors(rnpb, s);
            gst = wwaAnp(era - eors);

            return gst;
        }
    }
}
