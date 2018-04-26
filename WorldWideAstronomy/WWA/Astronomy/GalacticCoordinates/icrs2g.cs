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
        /// Transformation from ICRS to Galactic Coordinates.
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
        /// <param name="dr">ICRS right ascension (radians)</param>
        /// <param name="dd">ICRS declination (radians)</param>
        /// <param name="dl">galactic longitude (radians)</param>
        /// <param name="db">galactic latitude (radians)</param>
        public static void wwaIcrs2g(double dr, double dd, ref double dl, ref double db)
        {
            double[] v1 = new double[3];
            double[] v2 = new double[3];

            /*
            **  L2,B2 system of galactic coordinates in the form presented in the
            **  Hipparcos Catalogue.  In degrees:
            **
            **  P = 192.85948    right ascension of the Galactic north pole in ICRS
            **  Q =  27.12825    declination of the Galactic north pole in ICRS
            **  R =  32.93192    longitude of the ascending node of the Galactic
            **                   plane on the ICRS equator
            **
            **  ICRS to galactic rotation matrix, obtained by computing
            **  R_3(-R) R_1(pi/2-Q) R_3(pi/2+P) to the full precision shown:
            */
            //double r[3][3] = { { -0.054875560416215368492398900454,
            //                     -0.873437090234885048760383168409,
            //                     -0.483835015548713226831774175116 },
            //                   { +0.494109427875583673525222371358,
            //                     -0.444829629960011178146614061616,
            //                     +0.746982244497218890527388004556 },
            //                   { -0.867666149019004701181616534570,
            //                     -0.198076373431201528180486091412,
            //                     +0.455983776175066922272100478348 } };

            //double r[3][3] = { { -0.054875560416215368492398900454,
            //                     -0.873437090234885048760383168409,
            //                     -0.483835015548713226831774175116 },
            //                   { +0.494109427875583673525222371358,
            //                     -0.444829629960011178146614061616,
            //                     +0.746982244497218890527388004556 },
            //                   { -0.867666149019004701181616534570,
            //                     -0.198076373431201528180486091412,
            //                     +0.455983776175066922272100478348 } };

            double[,] r = new double[3, 3] { 
                      { -0.054875560416215368492398900454,
                        -0.873437090234885048760383168409,
                        -0.483835015548713226831774175116 },
                      { +0.494109427875583673525222371358,
                        -0.444829629960011178146614061616,
                        +0.746982244497218890527388004556 },
                      { -0.867666149019004701181616534570,
                        -0.198076373431201528180486091412,
                        +0.455983776175066922272100478348 } };

            /* Spherical to Cartesian. */
            WWA.wwaS2c(dr, dd, v1);

            /* ICRS to Galactic. */
            wwaRxp(r, v1, v2);

            /* Cartesian to spherical. */
            wwaC2s(v2, ref dl, ref db);

            /* Express in conventional ranges. */
            dl = wwaAnp(dl);
            db = wwaAnpm(db);
        }
    }
}
