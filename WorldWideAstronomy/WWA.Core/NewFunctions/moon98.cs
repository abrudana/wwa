using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldWideAstronomy
{
    public static partial class WWA
    {
        /*
        ** Coefficients for Moon longitude and distance series
        */
        struct termlr
        {
            public int nd;           /* multiple of D  in argument           */
            public int nem;          /*     "    "  M   "    "               */
            public int nemp;         /*     "    "  M'  "    "               */
            public int nf;           /*     "    "  F   "    "               */
            public double coefl;     /* coefficient of L sine argument (deg) */
            public double coefr;     /* coefficient of R cosine argument (m) */
        };

        /*
        ** Coefficients for Moon latitude series
        */
        struct termb
        {
            public int nd;           /* multiple of D  in argument           */
            public int nem;          /*     "    "  M   "    "               */
            public int nemp;         /*     "    "  M'  "    "               */
            public int nf;           /*     "    "  F   "    "               */
            public double coefb;     /* coefficient of B sine argument (deg) */
        };


        public static void wwaMoon98(double date1, double date2, double[,] pv)
        {
            /* Moon's mean longitude (wrt mean equinox and ecliptic of date) */
            double elp0 = 218.31665436,        /* Simon et al. (1994). */
                          elp1 = 481267.88123421,
                          elp2 = -0.0015786,
                          elp3 = 1.0 / 538841.0,
                          elp4 = -1.0 / 65194000.0;
            double elp, delp;

            /* Moon's mean elongation */
            double d0 = 297.8501921,
                          d1 = 445267.1114034,
                          d2 = -0.0018819,
                          d3 = 1.0 / 545868.0,
                          d4 = 1.0 / 113065000.0;
            double d, dd;

            /* Sun's mean anomaly */
            double em0 = 357.5291092,
                          em1 = 35999.0502909,
                          em2 = -0.0001536,
                          em3 = 1.0 / 24490000.0,
                          em4 = 0.0;
            double em, dem;

            /* Moon's mean anomaly */
            double emp0 = 134.9633964,
                          emp1 = 477198.8675055,
                          emp2 = 0.0087414,
                          emp3 = 1.0 / 69699.0,
                          emp4 = -1.0 / 14712000.0;
            double emp, demp;

            /* Mean distance of the Moon from its ascending node */
            double f0 = 93.2720950,
                          f1 = 483202.0175233,
                          f2 = -0.0036539,
                          f3 = 1.0 / 3526000.0,
                          f4 = 1.0 / 863310000.0;
            double f, df;

            /*
            ** Other arguments
            */

            /* Meeus A_1, due to Venus (deg) */
            double a10 = 119.75,
                          a11 = 131.849;
            double a1, da1;

            /* Meeus A_2, due to Jupiter (deg) */
            double a20 = 53.09,
                          a21 = 479264.290;
            double a2, da2;

            /* Meeus A_3, due to sidereal motion of the Moon in longitude (deg) */
            double a30 = 313.45,
                          a31 = 481266.484;
            double a3, da3;

            /* Coefficients for Meeus "additive terms" (deg) */
            double al1 = 0.003958,
                          al2 = 0.001962,
                          al3 = 0.000318;
            double ab1 = -0.002235,
                          ab2 = 0.000382,
                          ab3 = 0.000175,
                          ab4 = 0.000175,
                          ab5 = 0.000127,
                          ab6 = -0.000115;

            /* Fixed term in distance (m) */
            double r0 = 385000560.0;

            /* Coefficients for (dimensionless) E factor */
            double e1 = -0.002516,
                          e2 = -0.0000074;
            double e, de, esq, desq;

            termlr[] tlr = new termlr[] {
            new termlr() {nd = 0, nem = 0, nemp =  1, nf = 0, coefl = 6.288774, coefr = -20905355.0},
            new termlr() {nd = 2, nem = 0, nemp = -1, nf = 0, coefl = 1.274027, coefr = -3699111.0},
            new termlr() {nd = 2, nem = 0, nemp =  0, nf = 0, coefl = 0.658314, coefr = -2955968.0},
            new termlr() {nd = 0, nem = 0, nemp =  2, nf = 0, coefl = 0.213618, coefr =  -569925.0},
            new termlr() {nd = 0, nem = 1, nemp =  0, nf = 0, coefl =-0.185116, coefr =    48888.0},
            new termlr() {nd = 0, nem = 0, nemp =  0, nf = 2, coefl =-0.114332, coefr =    -3149.0},
            new termlr() {nd = 2, nem = 0, nemp = -2, nf = 0, coefl = 0.058793, coefr =   246158.0},
            new termlr() {nd = 2, nem =-1, nemp = -1, nf = 0, coefl = 0.057066, coefr =  -152138.0},
            new termlr() {nd = 2, nem = 0, nemp =  1, nf = 0, coefl = 0.053322, coefr =  -170733.0},
            new termlr() {nd = 2, nem =-1, nemp =  0, nf = 0, coefl = 0.045758, coefr =  -204586.0},
            new termlr() {nd = 0, nem = 1, nemp = -1, nf = 0, coefl =-0.040923, coefr =  -129620.0},
            new termlr() {nd = 1, nem = 0, nemp =  0, nf = 0, coefl =-0.034720, coefr =   108743.0},
            new termlr() {nd = 0, nem = 1, nemp =  1, nf = 0, coefl =-0.030383, coefr =   104755.0},
            new termlr() {nd = 2, nem = 0, nemp =  0, nf =-2, coefl = 0.015327, coefr =    10321.0},
            new termlr() {nd = 0, nem = 0, nemp =  1, nf = 2, coefl =-0.012528, coefr =        0.0},
            new termlr() {nd = 0, nem = 0, nemp =  1, nf =-2, coefl = 0.010980, coefr =    79661.0},
            new termlr() {nd = 4, nem = 0, nemp = -1, nf = 0, coefl = 0.010675, coefr =   -34782.0},
            new termlr() {nd = 0, nem = 0, nemp =  3, nf = 0, coefl = 0.010034, coefr =   -23210.0},
            new termlr() {nd = 4, nem = 0, nemp = -2, nf = 0, coefl = 0.008548, coefr =   -21636.0},
            new termlr() {nd = 2, nem = 1, nemp = -1, nf = 0, coefl =-0.007888, coefr =    24208.0},
            new termlr() {nd = 2, nem = 1, nemp =  0, nf = 0, coefl =-0.006766, coefr =    30824.0},
            new termlr() {nd = 1, nem = 0, nemp = -1, nf = 0, coefl =-0.005163, coefr =    -8379.0},
            new termlr() {nd = 1, nem = 1, nemp =  0, nf = 0, coefl = 0.004987, coefr =   -16675.0},
            new termlr() {nd = 2, nem =-1, nemp =  1, nf = 0, coefl = 0.004036, coefr =   -12831.0},
            new termlr() {nd = 2, nem = 0, nemp =  2, nf = 0, coefl = 0.003994, coefr =   -10445.0},
            new termlr() {nd = 4, nem = 0, nemp =  0, nf = 0, coefl = 0.003861, coefr =   -11650.0},
            new termlr() {nd = 2, nem = 0, nemp = -3, nf = 0, coefl = 0.003665, coefr =    14403.0},
            new termlr() {nd = 0, nem = 1, nemp = -2, nf = 0, coefl =-0.002689, coefr =    -7003.0},
            new termlr() {nd = 2, nem = 0, nemp = -1, nf = 2, coefl =-0.002602, coefr =        0.0},
            new termlr() {nd = 2, nem =-1, nemp = -2, nf = 0, coefl = 0.002390, coefr =    10056.0},
            new termlr() {nd = 1, nem = 0, nemp =  1, nf = 0, coefl =-0.002348, coefr =     6322.0},
            new termlr() {nd = 2, nem =-2, nemp =  0, nf = 0, coefl = 0.002236, coefr =    -9884.0},
            new termlr() {nd = 0, nem = 1, nemp =  2, nf = 0, coefl =-0.002120, coefr =     5751.0},
            new termlr() {nd = 0, nem = 2, nemp =  0, nf = 0, coefl =-0.002069, coefr =        0.0},
            new termlr() {nd = 2, nem =-2, nemp = -1, nf = 0, coefl = 0.002048, coefr =    -4950.0},
            new termlr() {nd = 2, nem = 0, nemp =  1, nf =-2, coefl =-0.001773, coefr =     4130.0},
            new termlr() {nd = 2, nem = 0, nemp =  0, nf = 2, coefl =-0.001595, coefr =        0.0},
            new termlr() {nd = 4, nem =-1, nemp = -1, nf = 0, coefl = 0.001215, coefr =    -3958.0},
            new termlr() {nd = 0, nem = 0, nemp =  2, nf = 2, coefl =-0.001110, coefr =        0.0},
            new termlr() {nd = 3, nem = 0, nemp = -1, nf = 0, coefl =-0.000892, coefr =     3258.0},
            new termlr() {nd = 2, nem = 1, nemp =  1, nf = 0, coefl =-0.000810, coefr =     2616.0},
            new termlr() {nd = 4, nem =-1, nemp = -2, nf = 0, coefl = 0.000759, coefr =    -1897.0},
            new termlr() {nd = 0, nem = 2, nemp = -1, nf = 0, coefl =-0.000713, coefr =    -2117.0},
            new termlr() {nd = 2, nem = 2, nemp = -1, nf = 0, coefl =-0.000700, coefr =     2354.0},
            new termlr() {nd = 2, nem = 1, nemp = -2, nf = 0, coefl = 0.000691, coefr =        0.0},
            new termlr() {nd = 2, nem =-1, nemp =  0, nf =-2, coefl = 0.000596, coefr =        0.0},
            new termlr() {nd = 4, nem = 0, nemp =  1, nf = 0, coefl = 0.000549, coefr =    -1423.0},
            new termlr() {nd = 0, nem = 0, nemp =  4, nf = 0, coefl = 0.000537, coefr =    -1117.0},
            new termlr() {nd = 4, nem =-1, nemp =  0, nf = 0, coefl = 0.000520, coefr =    -1571.0},
            new termlr() {nd = 1, nem = 0, nemp = -2, nf = 0, coefl =-0.000487, coefr =    -1739.0},
            new termlr() {nd = 2, nem = 1, nemp =  0, nf =-2, coefl =-0.000399, coefr =        0.0},
            new termlr() {nd = 0, nem = 0, nemp =  2, nf =-2, coefl =-0.000381, coefr =    -4421.0},
            new termlr() {nd = 1, nem = 1, nemp =  1, nf = 0, coefl = 0.000351, coefr =        0.0},
            new termlr() {nd = 3, nem = 0, nemp = -2, nf = 0, coefl =-0.000340, coefr =        0.0},
            new termlr() {nd = 4, nem = 0, nemp = -3, nf = 0, coefl = 0.000330, coefr =        0.0},
            new termlr() {nd = 2, nem =-1, nemp =  2, nf = 0, coefl = 0.000327, coefr =        0.0},
            new termlr() {nd = 0, nem = 2, nemp =  1, nf = 0, coefl =-0.000323, coefr =     1165.0},
            new termlr() {nd = 1, nem = 1, nemp = -1, nf = 0, coefl = 0.000299, coefr =        0.0},
            new termlr() {nd = 2, nem = 0, nemp =  3, nf = 0, coefl = 0.000294, coefr =        0.0},
            new termlr() {nd = 2, nem = 0, nemp = -1, nf =-2, coefl = 0.000000, coefr =     8752.0}
        };

            //int NLR = (sizeof(tlr) / sizeof(termlr));
            int NLR = tlr.GetLength(0);

            termb[] tb = new termb[] {
            new termb() {nd = 0, nem = 0, nemp = 0, nf = 1, coefb = 5.128122},
            new termb() {nd = 0, nem = 0, nemp = 1, nf = 1, coefb = 0.280602},
            new termb() {nd = 0, nem = 0, nemp = 1, nf =-1, coefb = 0.277693},
            new termb() {nd = 2, nem = 0, nemp = 0, nf =-1, coefb = 0.173237},
            new termb() {nd = 2, nem = 0, nemp =-1, nf = 1, coefb = 0.055413},
            new termb() {nd = 2, nem = 0, nemp =-1, nf =-1, coefb = 0.046271},
            new termb() {nd = 2, nem = 0, nemp = 0, nf = 1, coefb = 0.032573},
            new termb() {nd = 0, nem = 0, nemp = 2, nf = 1, coefb = 0.017198},
            new termb() {nd = 2, nem = 0, nemp = 1, nf =-1, coefb = 0.009266},
            new termb() {nd = 0, nem = 0, nemp = 2, nf =-1, coefb = 0.008822},
            new termb() {nd = 2, nem =-1, nemp = 0, nf =-1, coefb = 0.008216},
            new termb() {nd = 2, nem = 0, nemp =-2, nf =-1, coefb = 0.004324},
            new termb() {nd = 2, nem = 0, nemp = 1, nf = 1, coefb = 0.004200},
            new termb() {nd = 2, nem = 1, nemp = 0, nf =-1, coefb =-0.003359},
            new termb() {nd = 2, nem =-1, nemp =-1, nf = 1, coefb = 0.002463},
            new termb() {nd = 2, nem =-1, nemp = 0, nf = 1, coefb = 0.002211},
            new termb() {nd = 2, nem =-1, nemp =-1, nf =-1, coefb = 0.002065},
            new termb() {nd = 0, nem = 1, nemp =-1, nf =-1, coefb =-0.001870},
            new termb() {nd = 4, nem = 0, nemp =-1, nf =-1, coefb = 0.001828},
            new termb() {nd = 0, nem = 1, nemp = 0, nf = 1, coefb =-0.001794},
            new termb() {nd = 0, nem = 0, nemp = 0, nf = 3, coefb =-0.001749},
            new termb() {nd = 0, nem = 1, nemp =-1, nf = 1, coefb =-0.001565},
            new termb() {nd = 1, nem = 0, nemp = 0, nf = 1, coefb =-0.001491},
            new termb() {nd = 0, nem = 1, nemp = 1, nf = 1, coefb =-0.001475},
            new termb() {nd = 0, nem = 1, nemp = 1, nf =-1, coefb =-0.001410},
            new termb() {nd = 0, nem = 1, nemp = 0, nf =-1, coefb =-0.001344},
            new termb() {nd = 1, nem = 0, nemp = 0, nf =-1, coefb =-0.001335},
            new termb() {nd = 0, nem = 0, nemp = 3, nf = 1, coefb = 0.001107},
            new termb() {nd = 4, nem = 0, nemp = 0, nf =-1, coefb = 0.001021},
            new termb() {nd = 4, nem = 0, nemp =-1, nf = 1, coefb = 0.000833},
            new termb() {nd = 0, nem = 0, nemp = 1, nf =-3, coefb = 0.000777},
            new termb() {nd = 4, nem = 0, nemp =-2, nf = 1, coefb = 0.000671},
            new termb() {nd = 2, nem = 0, nemp = 0, nf =-3, coefb = 0.000607},
            new termb() {nd = 2, nem = 0, nemp = 2, nf =-1, coefb = 0.000596},
            new termb() {nd = 2, nem =-1, nemp = 1, nf =-1, coefb = 0.000491},
            new termb() {nd = 2, nem = 0, nemp =-2, nf = 1, coefb =-0.000451},
            new termb() {nd = 0, nem = 0, nemp = 3, nf =-1, coefb = 0.000439},
            new termb() {nd = 2, nem = 0, nemp = 2, nf = 1, coefb = 0.000422},
            new termb() {nd = 2, nem = 0, nemp =-3, nf =-1, coefb = 0.000421},
            new termb() {nd = 2, nem = 1, nemp =-1, nf = 1, coefb =-0.000366},
            new termb() {nd = 2, nem = 1, nemp = 0, nf = 1, coefb =-0.000351},
            new termb() {nd = 4, nem = 0, nemp = 0, nf = 1, coefb = 0.000331},
            new termb() {nd = 2, nem =-1, nemp = 1, nf = 1, coefb = 0.000315},
            new termb() {nd = 2, nem =-2, nemp = 0, nf =-1, coefb = 0.000302},
            new termb() {nd = 0, nem = 0, nemp = 1, nf = 3, coefb =-0.000283},
            new termb() {nd = 2, nem = 1, nemp = 1, nf =-1, coefb =-0.000229},
            new termb() {nd = 1, nem = 1, nemp = 0, nf =-1, coefb = 0.000223},
            new termb() {nd = 1, nem = 1, nemp = 0, nf = 1, coefb = 0.000223},
            new termb() {nd = 0, nem = 1, nemp =-2, nf =-1, coefb =-0.000220},
            new termb() {nd = 2, nem = 1, nemp =-1, nf =-1, coefb =-0.000220},
            new termb() {nd = 1, nem = 0, nemp = 1, nf = 1, coefb =-0.000185},
            new termb() {nd = 2, nem =-1, nemp =-2, nf =-1, coefb = 0.000181},
            new termb() {nd = 0, nem = 1, nemp = 2, nf = 1, coefb =-0.000177},
            new termb() {nd = 4, nem = 0, nemp =-2, nf =-1, coefb = 0.000176},
            new termb() {nd = 4, nem =-1, nemp =-1, nf =-1, coefb = 0.000166},
            new termb() {nd = 1, nem = 0, nemp = 1, nf =-1, coefb =-0.000164},
            new termb() {nd = 4, nem = 0, nemp = 1, nf =-1, coefb = 0.000132},
            new termb() {nd = 1, nem = 0, nemp =-1, nf =-1, coefb =-0.000119},
            new termb() {nd = 4, nem =-1, nemp = 0, nf =-1, coefb = 0.000115},
            new termb() {nd = 2, nem =-2, nemp = 0, nf = 1, coefb = 0.000107}
            };


            //int NB = (sizeof tb / sizeof( struct termb ) );
            int NB = tb.GetLength(0);

            /* Miscellaneous */
            int n, i;
            double t, elpmf, delpmf, vel, vdel, vr, vdr, a1mf, da1mf, a1pf,
                   da1pf, dlpmp, slpmp, vb, vdb, v, dv, emn, empn, dn, fn, en,
                   den, arg, darg, farg, coeff, el, del, r, dr, b, db,
                   gamb = 0, phib = 0, psib = 0, epsa = 0;
            double[,] rm = new double[3, 3];

            /* ------------------------------------------------------------------ */

            /* Centuries since J2000.0 */
            t = ((date1 - DJ00) + date2) / DJC;

            /* --------------------- */
            /* Fundamental arguments */
            /* --------------------- */

            /* Arguments (radians) and derivatives (radians per Julian century)
               for the current date. */

            /* Moon's mean longitude. */
            elp = DD2R * Math.IEEERemainder(elp0
                            + (elp1
                            + (elp2
                            + (elp3
                            + elp4 * t) * t) * t) * t, 360.0);
            delp = DD2R * (elp1
                            + (elp2 * 2.0
                            + (elp3 * 3.0
                            + elp4 * 4.0 * t) * t) * t);

            /* Moon's mean elongation. */
            d = DD2R * Math.IEEERemainder(d0
                          + (d1
                          + (d2
                          + (d3
                          + d4 * t) * t) * t) * t, 360.0);
            dd = DD2R * (d1
                          + (d2 * 2.0
                          + (d3 * 3.0
                          + d4 * 4.0 * t) * t) * t);

            /* Sun's mean anomaly. */
            em = DD2R * Math.IEEERemainder(em0
                           + (em1
                           + (em2
                           + (em3
                           + em4 * t) * t) * t) * t, 360.0);
            dem = DD2R * (em1
                           + (em2 * 2.0
                           + (em3 * 3.0
                           + em4 * 4.0 * t) * t) * t);

            /* Moon's mean anomaly. */
            emp = DD2R * Math.IEEERemainder(emp0
                            + (emp1
                            + (emp2
                            + (emp3
                            + emp4 * t) * t) * t) * t, 360.0);
            demp = DD2R * (emp1
                            + (emp2 * 2.0
                            + (emp3 * 3.0
                            + emp4 * 4.0 * t) * t) * t);

            /* Mean distance of the Moon from its ascending node. */
            f = DD2R * Math.IEEERemainder(f0
                          + (f1
                          + (f2
                          + (f3
                          + f4 * t) * t) * t) * t, 360.0);
            df = DD2R * (f1
                          + (f2 * 2.0
                          + (f3 * 3.0
                          + f4 * 4.0 * t) * t) * t);

            /* Meeus further arguments. */
            a1 = DD2R * (a10 + a11 * t);
            da1 = DD2R * al1;
            a2 = DD2R * (a20 + a21 * t);
            da2 = DD2R * a21;
            a3 = DD2R * (a30 + a31 * t);
            da3 = DD2R * a31;

            /* E-factor, and square. */
            e = 1.0 + (e1 + e2 * t) * t;
            de = e1 + 2.0 * e2 * t;
            esq = e * e;
            desq = 2.0 * e * de;

            /* Use the Meeus additive terms (deg) to start off the summations. */
            elpmf = elp - f;
            delpmf = delp - df;
            vel = al1 * Math.Sin(a1)
                + al2 * Math.Sin(elpmf)
                + al3 * Math.Sin(a2);
            vdel = al1 * Math.Cos(a1) * da1
            + al2 * Math.Cos(elpmf) * delpmf
            + al3 * Math.Cos(a2) * da2;

            vr = 0.0;
            vdr = 0.0;

            a1mf = a1 - f;
            da1mf = da1 - df;
            a1pf = a1 + f;
            da1pf = da1 + df;
            dlpmp = elp - emp;
            slpmp = elp + emp;
            vb = ab1 * Math.Sin(elp)
               + ab2 * Math.Sin(a3)
               + ab3 * Math.Sin(a1mf)
               + ab4 * Math.Sin(a1pf)
               + ab5 * Math.Sin(dlpmp)
               + ab6 * Math.Sin(slpmp);
            vdb = ab1 * Math.Cos(elp) * delp
           + ab2 * Math.Cos(a3) * da3
           + ab3 * Math.Cos(a1mf) * da1mf
           + ab4 * Math.Cos(a1pf) * da1pf
           + ab5 * Math.Cos(dlpmp) * (delp - demp)
           + ab6 * Math.Cos(slpmp) * (delp + demp);

            /* ----------------- */
            /* Series expansions */
            /* ----------------- */

            /* Longitude and distance plus derivatives. */
            for (n = NLR - 1; n >= 0; n--)
            {
                dn = (double)tlr[n].nd;
                emn = (double)(i = tlr[n].nem);
                empn = (double)tlr[n].nemp;
                fn = (double)tlr[n].nf;
                switch (Math.Abs(i))
                {
                    case 1:
                        en = e;
                        den = de;
                        break;
                    case 2:
                        en = esq;
                        den = desq;
                        break;
                    default:
                        en = 1.0;
                        den = 0.0;
                        break;
                }
                arg = dn * d + emn * em + empn * emp + fn * f;
                darg = dn * dd + emn * dem + empn * demp + fn * df;
                farg = Math.Sin(arg);
                v = farg * en;
                dv = Math.Cos(arg) * darg * en + farg * den;
                coeff = tlr[n].coefl;
                vel += coeff * v;
                vdel += coeff * dv;
                farg = Math.Cos(arg);
                v = farg * en;
                dv = -Math.Sin(arg) * darg * en + farg * den;
                coeff = tlr[n].coefr;
                vr += coeff * v;
                vdr += coeff * dv;
            }
            el = elp + DD2R * vel;
            del = (delp + DD2R * vdel) / DJC;
            r = (vr + r0) / DAU;
            dr = vdr / DAU / DJC;

            /* Latitude plus derivative. */
            for (n = NB - 1; n >= 0; n--)
            {
                dn = (double)tb[n].nd;
                emn = (double)(i = tb[n].nem);
                empn = (double)tb[n].nemp;
                fn = (double)tb[n].nf;
                switch (Math.Abs(i))
                {
                    case 1:
                        en = e;
                        den = de;
                        break;
                    case 2:
                        en = esq;
                        den = desq;
                        break;
                    default:
                        en = 1.0;
                        den = 0.0;
                        break;
                }
                arg = dn * d + emn * em + empn * emp + fn * f;
                darg = dn * dd + emn * dem + empn * demp + fn * df;
                farg = Math.Sin(arg);
                v = farg * en;
                dv = Math.Cos(arg) * darg * en + farg * den;
                coeff = tb[n].coefb;
                vb += coeff * v;
                vdb += coeff * dv;
            }
            b = vb * DD2R;
            db = vdb * DD2R / DJC;

            /* ------------------------------ */
            /* Transformation into final form */
            /* ------------------------------ */

            /* Longitude, latitude to x, y, z (AU). */
            wwaS2pv(el, b, r, del, db, dr, pv);

            /* IAU 2006 Fukushima-Williams bias+precession angles. */
            wwaPfw06(date1, date2, ref gamb, ref phib, ref psib, ref epsa);

            /* Mean ecliptic coordinates to GCRS rotation matrix. */
            wwaIr(rm);
            wwaRz(psib, rm);
            wwaRx(-phib, rm);
            wwaRz(-gamb, rm);

            /* Rotate the Moon position and velocity into GCRS (Note 6). */
            wwaRxpv(rm, pv, pv);

            /* Finished. */
        }
    }
}
