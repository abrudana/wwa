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
        /// Parallactic angle for a given hour angle and declination.
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
        /// <param name="ha">hour angle</param>
        /// <param name="dec">declination</param>
        /// <param name="phi">site latitude</param>
        public static double wwaHd2pa(double ha, double dec, double phi)
        {
            double cp, cqsz, sqsz;

            cp = Math.Cos(phi);
            sqsz = cp * Math.Sin(ha);
            cqsz = Math.Sin(phi) * Math.Cos(dec) - cp * Math.Sin(dec) * Math.Cos(ha);
            return ((sqsz != 0.0 || cqsz != 0.0) ? Math.Atan2(sqsz, cqsz) : 0.0);
        }
    }
}
