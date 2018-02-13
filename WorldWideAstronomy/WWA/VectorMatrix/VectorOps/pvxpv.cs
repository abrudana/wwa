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
        /// Outer (=vector=cross) product of two pv-vectors.
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
        /// <param name="axb">a x b</param>
        public static void wwaPvxpv(double[,] a, double[,] b, double[,] axb)
        {
            double[,] wa = new double[2, 3];
            double[,] wb = new double[2, 3];
            double[] axbd = new double[3];
            double[] adxb = new double[3];
            double[] axb1 = new double[3];

            /* Make copies of the inputs. */
            wwaCpv(a, wa);
            wwaCpv(b, wb);

            /* a x b = position part of result. */
            //wwaPxp(CopyArray(wa, 0), CopyArray(wb, 0), CopyArray(axb, 0));
            axb1 = CopyArray(axb, 0);
            wwaPxp(CopyArray(wa, 0), CopyArray(wb, 0), axb1);
            AddArray2(ref axb, axb1, 0);

            /* a x bdot + adot x b = velocity part of result. */
            wwaPxp(CopyArray(wa, 0), CopyArray(wb, 1), axbd);
            wwaPxp(CopyArray(wa, 1), CopyArray(wb, 0), adxb);
            
            //wwaPpp(axbd, adxb, CopyArray(axb, 1));
            axb1 = CopyArray(axb, 1);
            wwaPpp(axbd, adxb, axb1);
            AddArray2(ref axb, axb1, 1);

            return;
        }
    }
}
