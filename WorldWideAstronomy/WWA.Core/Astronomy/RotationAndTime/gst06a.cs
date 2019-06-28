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
        /// Greenwich apparent sidereal time (consistent with IAU 2000 and 2006
        /// resolutions).
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
        /// <returns>Greenwich apparent sidereal time (radians)</returns>
        public static double wwaGst06a(double uta, double utb, double tta, double ttb)
        {
            double[,] rnpb = new double[3, 3];
            double gst;


            /* Classical nutation x precession x bias matrix, IAU 2000A. */
            wwaPnm06a(tta, ttb, rnpb);

            /* Greenwich apparent sidereal time. */
            gst = wwaGst06(uta, utb, tta, ttb, rnpb);

            return gst;
        }
    }
}
