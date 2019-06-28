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
        /// Inner (=scalar=dot) product of two pv-vectors.
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
        /// <param name="a">first pv-vector</param>
        /// <param name="b">second pv-vector</param>
        /// <param name="adb"> a . b (see note)</param>
        public static void wwaPvdpv(double[,] a, double[,] b, double[] adb)
        {
            double adbd, addb;

            /* a . b = constant part of result. */
            //adb[0] = wwaPdp(a[0], b[0]);
            adb[0] = wwaPdp(CopyArray(a, 0), CopyArray(b, 0)); // by AA

            /* a . bdot */
            //adbd = wwaPdp(a[0], b[1]);
            adbd = wwaPdp(CopyArray(a, 0), CopyArray(b, 1));

            /* adot . b */
            //addb = wwaPdp(a[1], b[0]);
            addb = wwaPdp(CopyArray(a, 1), CopyArray(b, 0));

            /* Velocity part of result. */
            adb[1] = adbd + addb;

            return;
        }
    }
}