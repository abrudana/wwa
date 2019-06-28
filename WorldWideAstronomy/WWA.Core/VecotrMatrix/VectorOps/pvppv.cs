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
        /// Add one pv-vector to another.
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
        /// <param name="apb">a + b</param>
        public static void wwaPvppv(double[,] a, double[,] b, double[,] apb)
        {
            double[] apb1 = new double[3];

            //wwaPpp(CopyArray(a, 0), CopyArray(b, 0), CopyArray(apb, 0));
            apb1 = CopyArray(apb, 0);
            wwaPpp(CopyArray(a, 0), CopyArray(b, 0), apb1);
            AddArray2(ref apb, apb1, 0);

            //wwaPpp(CopyArray(a, 1), CopyArray(b, 1), CopyArray(apb, 1));
            apb1 = CopyArray(apb, 1);
            wwaPpp(CopyArray(a, 1), CopyArray(b, 1), apb1);
            AddArray2(ref apb, apb1, 1);

            return;
        }
    }
}