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
        /// Determine the constants A and B in the atmospheric refraction model
        /// dZ = A tan Z + B tan^3 Z.
        /// 
        /// Z is the "observed" zenith distance (i.e. affected by refraction)
        /// and dZ is what to add to Z to give the "topocentric" (i.e. in vacuo)
        /// zenith distance.
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
        /// <param name="phpa">pressure at the observer (hPa = millibar)</param>
        /// <param name="tc">ambient temperature at the observer (deg C)</param>
        /// <param name="rh">relative humidity at the observer (range 0-1)</param>
        /// <param name="wl">wavelength (micrometers)</param>
        /// <param name="refa">tan Z coefficient (radians)</param>
        /// <param name="refb">tan^3 Z coefficient (radians)</param>
        public static void wwaRefco(double phpa, double tc, double rh, double wl, ref double refa, ref double refb)
        {
            bool optic;
            double p, t, r, w, ps, pw, tk, wlsq, gamma, beta;

            /* Decide whether optical/IR or radio case:  switch at 100 microns. */
            optic = (wl <= 100.0);

            /* Restrict parameters to safe values. */
            t = gmax(tc, -150.0);
            t = gmin(t, 200.0);
            p = gmax(phpa, 0.0);
            p = gmin(p, 10000.0);
            r = gmax(rh, 0.0);
            r = gmin(r, 1.0);
            w = gmax(wl, 0.1);
            w = gmin(w, 1e6);

            /* Water vapour pressure at the observer. */
            if (p > 0.0)
            {
                ps = Math.Pow(10.0, (0.7859 + 0.03477 * t) /
                                    (1.0 + 0.00412 * t)) *
                           (1.0 + p * (4.5e-6 + 6e-10 * t * t));
                pw = r * ps / (1.0 - (1.0 - r) * ps / p);
            }
            else
            {
                pw = 0.0;
            }

            /* Refractive index minus 1 at the observer. */
            tk = t + 273.15;
            if (optic)
            {
                wlsq = w * w;
                gamma = ((77.53484e-6 +
                           (4.39108e-7 + 3.666e-9 / wlsq) / wlsq) * p
                              - 11.2684e-6 * pw) / tk;
            }
            else
            {
                gamma = (77.6890e-6 * p - (6.3938e-6 - 0.375463 / tk) * pw) / tk;
            }

            /* Formula for beta from Stone, with empirical adjustments. */
            beta = 4.4474e-6 * tk;
            if (!optic) beta -= 0.0074 * pw * beta;

            /* Refraction constants from Green. */
            refa = gamma * (1.0 - beta);
            refb = -gamma * (beta - gamma / 2.0);
        }
    }
}