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
        /// Multiply a p-vector by a scalar.
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
        /// <param name="s">scalar</param>
        /// <param name="p">p-vector</param>
        /// <param name="sp">s * p</param>
        public static void wwaSxp(double s, double[] p, double[] sp)
        {
            if (sp == null)
                sp = new double[3];

            sp[0] = s * p[0];
            sp[1] = s * p[1];
            sp[2] = s * p[2];

            return;
        }
    }
}
