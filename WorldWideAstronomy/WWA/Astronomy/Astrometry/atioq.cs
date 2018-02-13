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
        /// Quick CIRS to observed place transformation.
        /// 
        /// Use of this function is appropriate when efficiency is important and
        /// where many star positions are all to be transformed for one date.
        /// The star-independent astrometry parameters can be obtained by
        /// calling wwaApio[13] or wwaApco[13].
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
        /// <param name="ri">CIRS right ascension</param>
        /// <param name="di">CIRS declination</param>
        /// <param name="astrom">star-independent astrometry parameters:</param>
        /// <param name="aob">observed azimuth (radians: N=0,E=90)</param>
        /// <param name="zob">observed zenith distance (radians)</param>
        /// <param name="hob">observed hour angle (radians)</param>
        /// <param name="dob">observed declination (radians)</param>
        /// <param name="rob">observed right ascension (CIO-based, radians)</param>
        public static void wwaAtioq(double ri, double di, ref wwaASTROM astrom, ref double aob, ref double zob, ref double hob, ref double dob, ref double rob)
        {
            /* Minimum cos(alt) and sin(alt) for refraction purposes */
            const double CELMIN = 1e-6;
            const double SELMIN = 0.05;

            double[] v = new double[3];
            double x, y, z, xhd, yhd, zhd, f, xhdt, yhdt, zhdt, xaet, yaet, zaet, azobs, r, tz, w, del, cosdel, xaeo, yaeo, zaeo, zdobs, hmobs = 0, dcobs = 0, raobs;

            /* CIRS RA,Dec to Cartesian -HA,Dec. */
            wwaS2c(ri - astrom.eral, di, v);
            x = v[0];
            y = v[1];
            z = v[2];

            /* Polar motion. */
            xhd = x + astrom.xpl * z;
            yhd = y - astrom.ypl * z;
            zhd = z - astrom.xpl * x + astrom.ypl * y;

            /* Diurnal aberration. */
            f = (1.0 - astrom.diurab * yhd);
            xhdt = f * xhd;
            yhdt = f * (yhd + astrom.diurab);
            zhdt = f * zhd;

            /* Cartesian -HA,Dec to Cartesian Az,El (S=0,E=90). */
            xaet = astrom.sphi * xhdt - astrom.cphi * zhdt;
            yaet = yhdt;
            zaet = astrom.cphi * xhdt + astrom.sphi * zhdt;

            /* Azimuth (N=0,E=90). */
            azobs = (xaet != 0.0 || yaet != 0.0) ? Math.Atan2(yaet, -xaet) : 0.0;

            /* ---------- */
            /* Refraction */
            /* ---------- */

            /* Cosine and sine of altitude, with precautions. */
            r = Math.Sqrt(xaet * xaet + yaet * yaet);
            r = r > CELMIN ? r : CELMIN;
            z = zaet > SELMIN ? zaet : SELMIN;

            /* A*tan(z)+B*tan^3(z) model, with Newton-Raphson correction. */
            tz = r / z;
            w = astrom.refb * tz * tz;
            del = (astrom.refa + w) * tz / (1.0 + (astrom.refa + 3.0 * w) / (z * z));

            /* Apply the change, giving observed vector. */
            cosdel = 1.0 - del * del / 2.0;
            f = cosdel - del * z / r;
            xaeo = xaet * f;
            yaeo = yaet * f;
            zaeo = cosdel * zaet + del * r;

            /* Observed ZD. */
            zdobs = Math.Atan2(Math.Sqrt(xaeo * xaeo + yaeo * yaeo), zaeo);

            /* Az/El vector to HA,Dec vector (both right-handed). */
            v[0] = astrom.sphi * xaeo + astrom.cphi * zaeo;
            v[1] = yaeo;
            v[2] = -astrom.cphi * xaeo + astrom.sphi * zaeo;

            /* To spherical -HA,Dec. */
            wwaC2s(v, ref hmobs, ref dcobs);

            /* Right ascension (with respect to CIO). */
            raobs = astrom.eral + hmobs;

            /* Return the results. */
            aob = wwaAnp(azobs);
            zob = zdobs;
            hob = -hmobs;
            dob = dcobs;
            rob = wwaAnp(raobs);
        }
    }
}