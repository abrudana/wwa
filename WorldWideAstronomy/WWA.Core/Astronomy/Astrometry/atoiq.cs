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
        /// Quick observed place to CIRS, given the star-independent astrometry parameters.
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
        /// <param name="type">type of coordinates: "R", "H" or "A" (Note 1)</param>
        /// <param name="ob1">observed Az, HA or RA (radians; Az is N=0,E=90)</param>
        /// <param name="ob2">observed ZD or Dec (radians)</param>
        /// <param name="astrom">star-independent astrometry parameters:</param>
        /// <param name="ri">CIRS right ascension (CIO-based, radians)</param>
        /// <param name="di">CIRS declination (radians)</param>
        public static void wwaAtoiq(ref char type, double ob1, double ob2, ref wwaASTROM astrom, ref double ri, ref double di)
        {
            int c;
            double c1, c2, sphi, cphi, ce, xaeo, yaeo, zaeo, xmhdo, ymhdo, zmhdo, az, sz, zdo, refa, refb, tz, dref,
                   zdt, xaet, yaet, zaet, xmhda, ymhda, zmhda, f, xhd, yhd, zhd, xpl, ypl, w, hma = 0;
            double[] v = new double[3];


            /* Coordinate type. */
            c = (int)type;

            /* Coordinates. */
            c1 = ob1;
            c2 = ob2;

            /* Sin, cos of latitude. */
            sphi = astrom.sphi;
            cphi = astrom.cphi;

            /* Standardize coordinate type. */
            if (c == 'r' || c == 'R')
            {
                c = 'R';
            }
            else if (c == 'h' || c == 'H')
            {
                c = 'H';
            }
            else
            {
                c = 'A';
            }

            /* If Az,ZD, convert to Cartesian (S=0,E=90). */
            if (c == 'A')
            {
                ce = Math.Sin(c2);
                xaeo = -Math.Cos(c1) * ce;
                yaeo = Math.Sin(c1) * ce;
                zaeo = Math.Cos(c2);
            }
            else
            {
                /* If RA,Dec, convert to HA,Dec. */
                if (c == 'R') c1 = astrom.eral - c1;

                /* To Cartesian -HA,Dec. */
                wwaS2c(-c1, c2, v);
                xmhdo = v[0];
                ymhdo = v[1];
                zmhdo = v[2];

                /* To Cartesian Az,El (S=0,E=90). */
                xaeo = sphi * xmhdo - cphi * zmhdo;
                yaeo = ymhdo;
                zaeo = cphi * xmhdo + sphi * zmhdo;
            }

            /* Azimuth (S=0,E=90). */
            az = (xaeo != 0.0 || yaeo != 0.0) ? Math.Atan2(yaeo, xaeo) : 0.0;

            /* Sine of observed ZD, and observed ZD. */
            sz = Math.Sqrt(xaeo * xaeo + yaeo * yaeo);
            zdo = Math.Atan2(sz, zaeo);

            /*
            ** Refraction
            ** ----------
            */

            /* Fast algorithm using two constant model. */
            refa = astrom.refa;
            refb = astrom.refb;
            tz = sz / zaeo;
            dref = (refa + refb * tz * tz) * tz;
            zdt = zdo + dref;

            /* To Cartesian Az,ZD. */
            ce = Math.Sin(zdt);
            xaet = Math.Cos(az) * ce;
            yaet = Math.Sin(az) * ce;
            zaet = Math.Cos(zdt);

            /* Cartesian Az,ZD to Cartesian -HA,Dec. */
            xmhda = sphi * xaet + cphi * zaet;
            ymhda = yaet;
            zmhda = -cphi * xaet + sphi * zaet;

            /* Diurnal aberration. */
            f = (1.0 + astrom.diurab * ymhda);
            xhd = f * xmhda;
            yhd = f * (ymhda - astrom.diurab);
            zhd = f * zmhda;

            /* Polar motion. */
            xpl = astrom.xpl;
            ypl = astrom.ypl;
            w = xpl * xhd - ypl * yhd + zhd;
            v[0] = xhd - xpl * w;
            v[1] = yhd + ypl * w;
            v[2] = w - (xpl * xpl + ypl * ypl) * zhd;

            /* To spherical -HA,Dec. */
            wwaC2s(v, ref hma, ref di);

            /* Right ascension. */
            ri = wwaAnp(astrom.eral + hma);
        }
    }
}