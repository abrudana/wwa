using System;
using System.Collections.Generic;
using System.Text;

namespace WorldWideAstronomy
{
    /// <summary>
    /// Macros used by SOFA library.
    /// </summary>
    public static partial class WWA
    {
        /// <summary>
        /// Star-independent astrometry parameters.
        /// (Vectors eb, eh, em and v are all with respect to BCRS axes.)
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
        public struct wwaASTROM
        {
            public double pmt;      /* PM time interval (SSB, Julian years) */
            public double[] eb;     /* SSB to observer (vector, au) - array[3] */
            public double[] eh;     /* Sun to observer (unit vector) - array[3] */
            public double em;       /* distance from Sun to observer (au) */
            public double[] v;      /* barycentric observer velocity (vector, c) - array[3] */
            public double bm1;      /* sqrt(1-|v|^2): reciprocal of Lorenz factor */
            public double[,] bpn;  /* bias-precession-nutation matrix - array[3][3] */
            public double along;    /* longitude + s' + dERA(DUT) (radians) */
            public double phi;      /* geodetic latitude (radians) */
            public double xpl;      /* polar motion xp wrt local meridian (radians) */
            public double ypl;      /* polar motion yp wrt local meridian (radians) */
            public double sphi;     /* sine of geodetic latitude */
            public double cphi;     /* cosine of geodetic latitude */
            public double diurab;   /* magnitude of diurnal aberration vector */
            public double eral;     /* "local" Earth rotation angle (radians) */
            public double refa;     /* refraction constant A (radians) */
            public double refb;     /* refraction constant B (radians) */
        }

        /// <summary>
        /// Body parameters for light deflection.
        /// </summary>
        public struct wwaLDBODY
        {
            public double bm;       /* mass of the body (solar masses) */
            public double dl;       /* deflection limiter (radians^2/2) */
            public double[,] pv;   /* barycentric PV of the body (au, au/day) - array[2][3] */
        }

        /// <summary>
        /// Pi
        /// </summary>
        public const double DPI = 3.141592653589793238462643;

        /// <summary>
        /// 2Pi
        /// </summary>
        public const double D2PI = 6.283185307179586476925287;

        /// <summary>
        /// Radians to degrees
        /// </summary>
        public const double DR2D = 57.29577951308232087679815;

        /// <summary>
        /// Degrees to radians
        /// </summary>
        public const double DD2R = 1.745329251994329576923691e-2;

        /* Radians to arcseconds */
        public const double DR2AS = 206264.8062470963551564734;

        /// <summary>
        /// Arcseconds to radians
        /// </summary>
        public const double DAS2R = 4.848136811095359935899141e-6;

        /// <summary>
        /// Seconds of time to radians
        /// </summary>
        public const double DS2R = 7.272205216643039903848712e-5;

        /// <summary>
        /// Arcseconds in a full circle
        /// </summary>
        public const double TURNAS = 1296000.0;

        /// <summary>
        /// Milliarcseconds to radians
        /// </summary>
        public const double DMAS2R = DAS2R / 1e3;

        /// <summary>
        /// Length of tropical year B1900 (days)
        /// </summary>
        public const double DTY = 365.242198781;

        /// <summary>
        /// Seconds per day
        /// </summary>
        public const double DAYSEC = 86400.0;

        /// <summary>
        /// Days per Julian year
        /// </summary>
        public const double DJY = 365.25;

        /// <summary>
        /// Days per Julian century
        /// </summary>
        public const double DJC = 36525.0;

        /// <summary>
        /// Days per Julian millennium
        /// </summary>
        public const double DJM = 365250.0;

        /// <summary>
        /// Reference epoch (J2000.0), Julian Date
        /// </summary>
        public const double DJ00 = 2451545.0;

        /// <summary>
        /// Julian Date of Modified Julian Date zero
        /// </summary>
        public const double DJM0 = 2400000.5;

        /// <summary>
        /// Reference epoch (J2000.0), Modified Julian Date
        /// </summary>
        public const double DJM00 = 51544.5;

        /// <summary>
        /// 1977 Jan 1.0 as MJD
        /// </summary>
        public const double DJM77 = 43144.0;

        /// <summary>
        /// TT minus TAI (s)
        /// </summary>
        public const double TTMTAI = 32.184;

        /// <summary>
        /// Astronomical unit (m, IAU 2012)
        /// </summary>
        //public const double DAU = 149597870e3;
        public const double DAU = 149597870.7e3;

        /// <summary>
        /// Speed of light (m/s)
        /// </summary>
        public const double CMPS = 299792458.0;

        /// <summary>
        /// Light time for 1 au (s)
        /// </summary>
        //public const double AULT = 499.004782;
        public const double AULT = (DAU / CMPS);

        /// <summary>
        /// Speed of light (AU per day)
        /// </summary>
        public const double DC = DAYSEC / AULT;

        /// <summary>
        /// L_G = 1 - d(TT)/d(TCG)
        /// </summary>
        public const double ELG = 6.969290134e-10;

        /// <summary>
        /// L_B = 1 - d(TDB)/d(TCB), and TDB (s) at TAI 1977/1/1.0
        /// </summary>
        public const double ELB = 1.550519768e-8;
        public const double TDB0 = -6.55e-5;

        /// <summary>
        /// Schwarzschild radius of the Sun (au)
        /// = 2 * 1.32712440041e20 / (2.99792458e8)^2 / 1.49597870700e11
        /// </summary>
        public const double SRS = 1.97412574336e-8;

        /// <summary>
        /// dint(A) - truncate to nearest whole number towards zero (double)
        /// </summary>
        /// <param name="A"></param>
        /// <returns></returns>
        public static double dint(double A)
        {
            //#define dint(A) ((A)<0.0?ceil(A):floor(A))
            return A < 0.0 ? Math.Ceiling(A) : Math.Floor(A);
        }

        /// <summary>
        /// dnint(A) - round to nearest whole number (double)
        /// </summary>
        /// <param name="A"></param>
        /// <returns></returns>
        public static double dnint(double A)
        {
            //return A < 0.0 ? Math.Ceiling((A)-0.5) : Math.Floor((A)+0.5);
            return A < 0.0 ? Math.Ceiling((A) - 0.5) : Math.Floor((A) + 0.5);
            //return A < 0.0 ? Math.Round(A) : Math.Round(A, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// dsign(A,B) - magnitude of A with sign of B (double)
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static double dsign(double A, double B)
        {
            return B < 0.0 ? -(Math.Abs(A)) : Math.Abs(A);
        }

        /// <summary>
        /// max(A,B) - larger (most +ve) of two numbers (generic)
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static double gmax(double A, double B)
        {
            return A > B ? A : B;
        }

        /// <summary>
        /// min(A,B) - smaller (least +ve) of two numbers (generic)
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static double gmin(double A, double B)
        {
            return A < B ? A : B;
        }

        /// <summary>
        /// Reference ellipsoids
        /// </summary>
        public const int WGS84 = 1;
        public const int GRS80 = 2;
        public const int WGS72 = 3;

        // ----------------------------------------------------------------------------------
        /// <summary>
        /// Copy 2 dimension array into 1 dimension
        /// 
        /// by Attila Abrudán
        /// </summary>
        /// <param name="arrayIn"></param>
        /// <param name="dimension"></param>
        /// <returns></returns>
        public static double[] CopyArray(double[,] arrayIn, int dimension)
        {
            if (arrayIn == null)
                return null;

            double[] res = new double[arrayIn.GetLength(1)];

            for (int i = 0; i < arrayIn.GetLength(1); i++)
            {
                res[i] = arrayIn[dimension, i];
            }

            return res;
        }

        /// <summary>
        /// Copy 1 dimensional array into a 2 dimensional array.
        /// </summary>
        /// <param name="arrayIn"></param>
        /// <param name="dimension"></param>
        /// <returns></returns>
        public static double[,] CopyArray2(double[] arrayIn, int dimension)
        {
            if (arrayIn == null)
                return null;

            double[,] res = new double[2, arrayIn.GetLength(0)];

            for (int i = 0; i < arrayIn.GetLength(0); i++)
            {
                res[dimension, i] = arrayIn[i];
            }

            return res;
        }

        /// <summary>
        /// Copy 1 dimensional array into a 2 dimensional array
        /// </summary>
        /// <param name="arrayDest">destination (2 dimensional) array</param>
        /// <param name="arraySource">source array (1 dimensional)</param>
        /// <param name="dimension">dimension</param>
        /// <returns></returns>
        public static bool AddArray2(ref double[,] arrayDest, double[] arraySource, int dimension)
        {
            bool res = false;

            if (arraySource == null)
                return res;

            for (int i = 0; i < arraySource.GetLength(0); i++)
            {
                arrayDest[dimension, i] = arraySource[i];
            }

            return res;
        }
    }
}
