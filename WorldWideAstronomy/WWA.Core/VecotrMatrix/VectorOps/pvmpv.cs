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
        /// Subtract one pv-vector from another.
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
        /// <param name="amb">a - b</param>
        public static void wwaPvmpv(double[,] a, double[,] b, double[,] amb)
        {
            double[] amb1 = new double[3];

            //wwaPmp(CopyArray(a, 0), CopyArray(b, 0), CopyArray(amb, 0));
            amb1 = CopyArray(amb, 0);
            wwaPmp(CopyArray(a, 0), CopyArray(b, 0), amb1);
            AddArray2(ref amb, amb1, 0);

            //wwaPmp(CopyArray(a, 1), CopyArray(b, 1), CopyArray(amb, 1));
            amb1 = CopyArray(amb, 1);
            wwaPmp(CopyArray(a, 1), CopyArray(b, 1), amb1);
            AddArray2(ref amb, amb1, 1);

            return;
        }
    }
}
