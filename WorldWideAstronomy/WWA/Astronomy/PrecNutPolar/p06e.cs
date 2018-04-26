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
        /// Precession angles, IAU 2006, equinox based.
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
        /// <param name="eps0">epsilon_0</param>
        /// <param name="psia">psi_A</param>
        /// <param name="oma">omega_A</param>
        /// <param name="bpa">P_A</param>
        /// <param name="bqa">Q_A</param>
        /// <param name="pia">pi_A</param>
        /// <param name="bpia">Pi_A</param>
        /// <param name="epsa">obliquity epsilon_A</param>
        /// <param name="chia">chi_A</param>
        /// <param name="za">z_A</param>
        /// <param name="zetaa">zeta_A</param>
        /// <param name="thetaa">theta_A</param>
        /// <param name="pa">p_A</param>
        /// <param name="gam">F-W angle gamma_J2000</param>
        /// <param name="phi">F-W angle phi_J2000</param>
        /// <param name="psi">F-W angle psi_J2000</param>
        public static void wwaP06e(double date1, double date2, ref double eps0, ref double psia, ref double oma, ref double bpa,
             ref double bqa, ref double pia, ref double bpia, ref double epsa, ref double chia, ref double za, ref double zetaa,
             ref double thetaa, ref double pa,
             ref double gam, ref double phi, ref double psi)
        {
            double t;


            /* Interval between fundamental date J2000.0 and given date (JC). */
            t = ((date1 - DJ00) + date2) / DJC;

            /* Obliquity at J2000.0. */

            eps0 = 84381.406 * DAS2R;

            /* Luni-solar precession. */

            psia = (5038.481507 +
                   (-1.0790069 +
                   (-0.00114045 +
                   (0.000132851 +
                   (-0.0000000951)
                   * t) * t) * t) * t) * t * DAS2R;

            /* Inclination of mean equator with respect to the J2000.0 ecliptic. */

            oma = eps0 + (-0.025754 +
                           (0.0512623 +
                           (-0.00772503 +
                           (-0.000000467 +
                           (0.0000003337)
                           * t) * t) * t) * t) * t * DAS2R;

            /* Ecliptic pole x, J2000.0 ecliptic triad. */

            bpa = (4.199094 +
                   (0.1939873 +
                   (-0.00022466 +
                   (-0.000000912 +
                   (0.0000000120)
                   * t) * t) * t) * t) * t * DAS2R;

            /* Ecliptic pole -y, J2000.0 ecliptic triad. */

            bqa = (-46.811015 +
                   (0.0510283 +
                   (0.00052413 +
                   (-0.000000646 +
                   (-0.0000000172)
                   * t) * t) * t) * t) * t * DAS2R;

            /* Angle between moving and J2000.0 ecliptics. */

            pia = (46.998973 +
                   (-0.0334926 +
                   (-0.00012559 +
                   (0.000000113 +
                   (-0.0000000022)
                   * t) * t) * t) * t) * t * DAS2R;

            /* Longitude of ascending node of the moving ecliptic. */

            bpia = (629546.7936 +
                    (-867.95758 +
                    (0.157992 +
                    (-0.0005371 +
                    (-0.00004797 +
                    (0.000000072)
                    * t) * t) * t) * t) * t) * DAS2R;

            /* Mean obliquity of the ecliptic. */

            epsa = wwaObl06(date1, date2);

            /* Planetary precession. */

            chia = (10.556403 +
                    (-2.3814292 +
                    (-0.00121197 +
                    (0.000170663 +
                    (-0.0000000560)
                    * t) * t) * t) * t) * t * DAS2R;

            /* Equatorial precession: minus the third of the 323 Euler angles. */

            za = (-2.650545 +
                  (2306.077181 +
                  (1.0927348 +
                  (0.01826837 +
                  (-0.000028596 +
                  (-0.0000002904)
                  * t) * t) * t) * t) * t) * DAS2R;

            /* Equatorial precession: minus the first of the 323 Euler angles. */

            zetaa = (2.650545 +
                     (2306.083227 +
                     (0.2988499 +
                     (0.01801828 +
                     (-0.000005971 +
                     (-0.0000003173)
                     * t) * t) * t) * t) * t) * DAS2R;

            /* Equatorial precession: second of the 323 Euler angles. */

            thetaa = (2004.191903 +
                      (-0.4294934 +
                      (-0.04182264 +
                      (-0.000007089 +
                      (-0.0000001274)
                      * t) * t) * t) * t) * t * DAS2R;

            /* General precession. */

            pa = (5028.796195 +
                  (1.1054348 +
                  (0.00007964 +
                  (-0.000023857 +
                  (0.0000000383)
                  * t) * t) * t) * t) * t * DAS2R;

            /* Fukushima-Williams angles for precession. */

            gam = (10.556403 +
                   (0.4932044 +
                   (-0.00031238 +
                   (-0.000002788 +
                   (0.0000000260)
                   * t) * t) * t) * t) * t * DAS2R;

            phi = eps0 + (-46.811015 +
                           (0.0511269 +
                           (0.00053289 +
                           (-0.000000440 +
                           (-0.0000000176)
                           * t) * t) * t) * t) * t * DAS2R;

            psi = (5038.481507 +
                   (1.5584176 +
                   (-0.00018522 +
                   (-0.000026452 +
                   (-0.0000000148)
                   * t) * t) * t) * t) * t * DAS2R;

            return;
        }
    }
}
