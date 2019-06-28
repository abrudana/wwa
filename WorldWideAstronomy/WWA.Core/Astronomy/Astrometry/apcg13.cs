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
        /// For a geocentric observer, prepare star-independent astrometry
        /// parameters for transformations between ICRS and GCRS coordinates.
        /// The caller supplies the date, and SOFA models are used to predict
        /// the Earth ephemeris.
        /// The parameters produced by this function are required in the
        /// parallax, light deflection and aberration parts of the astrometric
        /// transformation chain.
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
        /// <param name="date1">TDB as a 2-part...</param>
        /// <param name="date2">...Julian Date (Note 1)</param>
        /// <param name="astrom">star-independent astrometry parameters</param>
        public static void wwaApcg13(double date1, double date2, ref wwaASTROM astrom)
        {
            double[,] ehpv = new double[2, 3] { { 0, 0, 0 }, { 0, 0, 0 } };
            double[,] ebpv = new double[2, 3] { { 0, 0, 0 }, { 0, 0, 0 } };

            /* Earth barycentric & heliocentric position/velocity (au, au/d). */
            wwaEpv00(date1, date2, ehpv, ebpv);
            
            /* Compute the star-independent astrometry parameters. */
            //wwaApcg(date1, date2, ebpv, ehpv[0], ref astrom);
            wwaApcg(date1, date2, ebpv, CopyArray(ehpv, 0), ref astrom);
        }
    }
}