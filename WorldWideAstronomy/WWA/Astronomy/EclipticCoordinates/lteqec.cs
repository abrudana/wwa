﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldWideAstronomy
{
    public static partial class WWA
    {
        /// <summary>
        /// Transformation from ICRS equatorial coordinates to ecliptic
        /// coordinates(mean equinox and ecliptic of date) using a long-term
        /// precession model.
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
        /// <param name="epj">Julian epoch (TT)</param>
        /// <param name="dr">ICRS right ascension and declination (radians)</param>
        /// <param name="dd">ICRS right ascension and declination (radians)</param>
        /// <param name="dl">ecliptic longitude and latitude (radians)</param>
        /// <param name="db">ecliptic longitude and latitude (radians)</param>
        public static void wwaLteqec(double epj, double dr, double dd, ref double dl, ref double db)
        {
            double[,] rm = new double[3, 3];
            double[] v1 = new double[3];
            double[] v2 = new double[3];
            double a = 0, b = 0;


            /* Spherical to Cartesian. */
            wwaS2c(dr, dd, v1);

            /* Rotation matrix, ICRS equatorial to ecliptic. */
            wwaLtecm(epj, rm);

            /* The transformation from ICRS to ecliptic. */
            wwaRxp(rm, v1, v2);

            /* Cartesian to spherical. */
            wwaC2s(v2, ref a, ref b);

            /* Express in conventional ranges. */
            dl = wwaAnp(a);
            db = wwaAnpm(b);
        }
    }
}
