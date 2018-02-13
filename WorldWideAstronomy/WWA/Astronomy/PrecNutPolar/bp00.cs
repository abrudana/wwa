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
        /// Frame bias and precession, IAU 2006.
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
        /// <param name="date1">TT as a 2-part Julian Date (Note 1)</param>
        /// <param name="date2">TT as a 2-part Julian Date (Note 1)</param>
        /// <param name="rb">frame bias matrix (Note 2)</param>
        /// <param name="rp">precession matrix (Note 3)</param>
        /// <param name="rbp">bias-precession matrix (Note 4)</param>
        public static void wwaBp00(double date1, double date2, double[,] rb, double[,] rp, double[,] rbp)
        {
            /* J2000.0 obliquity (Lieske et al. 1977) */
            const double EPS0 = 84381.448 * WWA.DAS2R;

            double t, dpsibi = 0, depsbi = 0, dra0 = 0, psia77, oma77, chia, dpsipr = 0, depspr = 0, psia, oma;
            double[,] rbw = new double[3, 3];


            /* Interval between fundamental epoch J2000.0 and current date (JC). */
            t = ((date1 - DJ00) + date2) / DJC;

            /* Frame bias. */
            wwaBi00(ref dpsibi, ref depsbi, ref dra0);

            /* Precession angles (Lieske et al. 1977) */
            psia77 = (5038.7784 + (-1.07259 + (-0.001147) * t) * t) * t * DAS2R;
            oma77 = EPS0 + ((0.05127 + (-0.007726) * t) * t) * t * DAS2R;
            chia = (10.5526 + (-2.38064 + (-0.001125) * t) * t) * t * DAS2R;

            /* Apply IAU 2000 precession corrections. */
            wwaPr00(date1, date2, ref dpsipr, ref depspr);
            psia = psia77 + dpsipr;
            oma = oma77 + depspr;

            /* Frame bias matrix: GCRS to J2000.0. */
            wwaIr(rbw);
            wwaRz(dra0, rbw);
            wwaRy(dpsibi * Math.Sin(EPS0), rbw);
            wwaRx(-depsbi, rbw);
            wwaCr(rbw, rb);

            /* Precession matrix: J2000.0 to mean of date. */
            wwaIr(rp);
            wwaRx(EPS0, rp);
            wwaRz(-psia, rp);
            wwaRx(-oma, rp);
            wwaRz(chia, rp);

            /* Bias-precession matrix: GCRS to mean of date. */
            wwaRxr(rp, rbw, rbp);

            return;
        }
    }
}
