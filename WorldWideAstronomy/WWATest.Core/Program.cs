using System;
using WorldWideAstronomy;

namespace WWATest.Core
{
    // Validate the WWA C functions.
    //
    // Each WWA function is at least called and a usually quite basic test
    // is performed.  Successful completion is signalled by a confirming
    // message.  Failure of a given function or group of functions results
    // in error messages.
    //
    // All messages go to stdout.
    //
    // This revision:  2016 April 21
    //
    // SOFA release 2016-05-03
    //
    // Copyright (C) 2016 IAU SOFA Board.
    //
    //<remarks>
    //This program is derived from the International Astronomical Union's
    //SOFA (Standards of Fundamental Astronomy) software collection.
    //http://www.iausofa.org
    //The code does not itself constitute software provided by and/or endorsed by SOFA.
    //This version is intended to retain identical functionality to the SOFA library, but
    //made distinct through different function names (prefixes) and C# language specific
    //modifications in code.
    //</remarks>
    class Program
    {
        static int verbose = 0;

        /// <summary>
        /// Validate an integer result.
        /// Internal function used by wwa_Test program.
        /// </summary>
        /// <param name="ival">value computed by function under test</param>
        /// <param name="ivalok">correct value</param>
        /// <param name="func">name of function under test</param>
        /// <param name="test">name of individual test</param>
        /// <param name="status">set to TRUE if test fails</param>
        static void viv(int ival, int ivalok, string func, string test, ref int status)
        {
            if (ival != ivalok)
            {
                status = 1;
                Console.WriteLine("{0} failed: {1} want {2} got {3}\n", func, test, ivalok, ival);
            }
            else if (verbose == 0 ? false : true)
            {
                Console.WriteLine("{0} passed: {1} want {2} got {3}\n", func, test, ivalok, ival);
            }
        }

        /// <summary>
        /// Validate a double result.
        /// Internal function used by wwa_Test program.
        /// </summary>
        /// <param name="val">value computed by function under test</param>
        /// <param name="valok">expected value</param>
        /// <param name="dval">maximum allowable error</param>
        /// <param name="func">name of function under test</param>
        /// <param name="test">name of individual test</param>
        /// <param name="status">set to TRUE if test fails</param>
        static void vvd(double val, double valok, double dval, string func, string test, ref int status)
        {
            double a, f;   /* absolute and fractional error */


            a = val - valok;
            //if (Math.Abs(a) > dval)
            if (a != 0.0 && Math.Abs(a) > Math.Abs(dval))
            {
                f = Math.Abs(valok / a);
                status = 1;
                Console.WriteLine(string.Format("{0} failed: {1} want {2:G20} got {3:G20} (1/{4:G3})\n", func, test, valok, val, f));
            }
            else if (verbose == 0 ? false : true)
            {
                Console.WriteLine(string.Format("{0} passed: {1} want {2:G20} got {3:G20}\n", func, test, valok, val));
            }
        }

        static void t_a2af(ref int status)
        /*
        **  - - - - - - -
        **   t _ a 2 a f
        **  - - - - - - -
        **
        **  Test wwaA2af function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaA2af, viv
        **
        **  This revision:  2013 August 7
        */
        {
            int[] idmsf = new int[4];
            char s = '\0';

            WWA.wwaA2af(4, 2.345, ref s, idmsf);

            viv((int)s, (int)'+', "wwaA2af", "s", ref status);

            viv(idmsf[0], 134, "wwaA2af", "0", ref status);
            viv(idmsf[1], 21, "wwaA2af", "1", ref status);
            viv(idmsf[2], 30, "wwaA2af", "2", ref status);
            viv(idmsf[3], 9706, "wwaA2af", "3", ref status);

        }

        static void t_a2tf(ref int status)
        /*
        **  - - - - - - -
        **   t _ a 2 t f
        **  - - - - - - -
        **
        **  Test wwaA2tf function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaA2tf, viv
        **
        **  This revision:  2013 August 7
        */
        {
            int[] ihmsf = new int[4];
            char s = '\0';

            WWA.wwaA2tf(4, -3.01234, ref s, ihmsf);

            viv((int)s, (int)'-', "wwaA2tf", "s", ref status);

            viv(ihmsf[0], 11, "wwaA2tf", "0", ref status);
            viv(ihmsf[1], 30, "wwaA2tf", "1", ref status);
            viv(ihmsf[2], 22, "wwaA2tf", "2", ref status);
            viv(ihmsf[3], 6484, "wwaA2tf", "3", ref status);
        }

        static void t_ab(ref int status)
        /*
        **  - - - - -
        **   t _ a b
        **  - - - - -
        **
        **  Test wwaAb function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaAb, vvd
        **
        **  This revision:  2013 October 1
        */
        {
            double[] pnat = new double[3];
            double[] v = new double[3];
            double[] ppr = new double[3];
            double s, bm1;

            pnat[0] = -0.76321968546737951;
            pnat[1] = -0.60869453983060384;
            pnat[2] = -0.21676408580639883;
            v[0] = 2.1044018893653786e-5;
            v[1] = -8.9108923304429319e-5;
            v[2] = -3.8633714797716569e-5;
            s = 0.99980921395708788;
            bm1 = 0.99999999506209258;

            WWA.wwaAb(pnat, v, s, bm1, ref ppr);

            vvd(ppr[0], -0.7631631094219556269, 1e-12, "wwaAb", "1", ref status);
            vvd(ppr[1], -0.6087553082505590832, 1e-12, "wwaAb", "2", ref status);
            vvd(ppr[2], -0.2167926269368471279, 1e-12, "wwaAb", "3", ref status);

        }

        static void t_ae2hd(ref int status)
        /*
        **  - - - - - - - -
        **   t _ a e 2 h d
        **  - - - - - - - -
        **
        **  Test iauAe2hd function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaAe2hd and vvd
        **
        **  This revision:  2017 October 21
        */
        {
            double a, e, p, h = 0, d = 0;

            a = 5.5;
            e = 1.1;
            p = 0.7;

            WWA.wwaAe2hd(a, e, p, ref h, ref d);

            vvd(h, 0.5933291115507309663, 1e-14, "wwaAe2hd", "h", ref status);
            vvd(d, 0.9613934761647817620, 1e-14, "wwaAe2hd", "d", ref status);

        }

        static void t_af2a(ref int status)
        /*
        **  - - - - - - -
        **   t _ a f 2 a
        **  - - - - - - -
        **
        **  Test wwaAf2a function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaAf2a, viv
        **
        **  This revision:  2013 August 7
        */
        {
            double a = 0;
            int j;


            j = WWA.wwaAf2a('-', 45, 13, 27.2, ref a);

            vvd(a, -0.7893115794313644842, 1e-12, "wwaAf2a", "a", ref status);
            viv(j, 0, "wwaAf2a", "j", ref status);

        }

        static void t_anp(ref int status)
        /*
        **  - - - - - -
        **   t _ a n p
        **  - - - - - -
        **
        **  Test wwaAnp function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaAnp, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            vvd(WWA.wwaAnp(-0.1), 6.183185307179586477, 1e-12, "wwaAnp", "", ref status);
        }

        static void t_anpm(ref int status)
        /*
        **  - - - - - - -
        **   t _ a n p m
        **  - - - - - - -
        **
        **  Test wwaAnpm function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaAnpm, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            vvd(WWA.wwaAnpm(-4.0), 2.283185307179586477, 1e-12, "wwaAnpm", "", ref status);
        }

        static void t_apcg(ref int status)
        /*
        **  - - - - - - -
        **   t _ a p c g
        **  - - - - - - -
        **
        **  Test wwaApcg function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaApcg, vvd
        **
        **  This revision:  2013 October 3
        */
        {
            double date1, date2;
            double[,] ebpv = new double[2, 3];
            double[] ehp = new double[3];
            WWA.wwaASTROM astrom = new WWA.wwaASTROM();

            astrom.eb = new double[3];
            astrom.eh = new double[3];
            astrom.bpn = new double[3, 3];

            date1 = 2456165.5;
            date2 = 0.401182685;
            ebpv[0, 0] = 0.901310875;
            ebpv[0, 1] = -0.417402664;
            ebpv[0, 2] = -0.180982288;
            ebpv[1, 0] = 0.00742727954;
            ebpv[1, 1] = 0.0140507459;
            ebpv[1, 2] = 0.00609045792;
            ehp[0] = 0.903358544;
            ehp[1] = -0.415395237;
            ehp[2] = -0.180084014;

            WWA.wwaApcg(date1, date2, ebpv, ehp, ref astrom);

            vvd(astrom.pmt, 12.65133794027378508, 1e-11,
                            "wwaApcg", "pmt", ref status);
            vvd(astrom.eb[0], 0.901310875, 1e-12,
                              "wwaApcg", "eb(1)", ref status);
            vvd(astrom.eb[1], -0.417402664, 1e-12,
                              "wwaApcg", "eb(2)", ref status);
            vvd(astrom.eb[2], -0.180982288, 1e-12,
                              "wwaApcg", "eb(3)", ref status);
            vvd(astrom.eh[0], 0.8940025429324143045, 1e-12,
                              "wwaApcg", "eh(1)", ref status);
            vvd(astrom.eh[1], -0.4110930268679817955, 1e-12,
                              "wwaApcg", "eh(2)", ref status);
            vvd(astrom.eh[2], -0.1782189004872870264, 1e-12,
                              "wwaApcg", "eh(3)", ref status);
            vvd(astrom.em, 1.010465295811013146, 1e-12,
                           "wwaApcg", "em", ref status);
            //vvd(astrom.v[0], 0.4289638897813379954e-4, 1e-16,
            //                 "wwaApcg", "v(1_", ref status);
            vvd(astrom.v[0], 0.4289638913597693554e-4, 1e-16,
                    "wwaApcg", "v(1)", ref status);
            //vvd(astrom.v[1], 0.8115034021720941898e-4, 1e-16,
            //                 "wwaApcg", "v(2)", ref status);
            vvd(astrom.v[1], 0.8115034051581320575e-4, 1e-16,
                    "wwaApcg", "v(2)", ref status);
            //vvd(astrom.v[2], 0.3517555123437237778e-4, 1e-16,
            //                 "wwaApcg", "v(3)", ref status);
            vvd(astrom.v[2], 0.3517555136380563427e-4, 1e-16,
                    "wwaApcg", "v(3)", ref status);
            //vvd(astrom.bm1, 0.9999999951686013336, 1e-12,
            //                "wwaApcg", "bm1", ref status);
            vvd(astrom.bm1, 0.9999999951686012981, 1e-12,
                   "wwaApcg", "bm1", ref status);
            vvd(astrom.bpn[0, 0], 1.0, 0.0,
                                  "wwaApcg", "bpn(1,1)", ref status);
            vvd(astrom.bpn[1, 0], 0.0, 0.0,
                                  "wwaApcg", "bpn(2,1)", ref status);
            vvd(astrom.bpn[2, 0], 0.0, 0.0,
                                  "wwaApcg", "bpn(3,1)", ref status);
            vvd(astrom.bpn[0, 1], 0.0, 0.0,
                                  "wwaApcg", "bpn(1,2)", ref status);
            vvd(astrom.bpn[1, 1], 1.0, 0.0,
                                  "wwaApcg", "bpn(2,2)", ref status);
            vvd(astrom.bpn[2, 1], 0.0, 0.0,
                                  "wwaApcg", "bpn(3,2)", ref status);
            vvd(astrom.bpn[0, 2], 0.0, 0.0,
                                  "wwaApcg", "bpn(1,3)", ref status);
            vvd(astrom.bpn[1, 2], 0.0, 0.0,
                                  "wwaApcg", "bpn(2,3)", ref status);
            vvd(astrom.bpn[2, 2], 1.0, 0.0,
                                  "wwaApcg", "bpn(3,3)", ref status);

        }

        static void t_apcg13(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ a p c g 1 3
        **  - - - - - - - - -
        **
        **  Test wwaApcg13 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaApcg13, vvd
        **
        **  This revision:  2013 October 3
        */
        {
            double date1, date2;
            WWA.wwaASTROM astrom = new WWA.wwaASTROM();

            astrom.eb = new double[3];
            astrom.eh = new double[3];
            astrom.bpn = new double[3, 3];

            date1 = 2456165.5;
            date2 = 0.401182685;

            WWA.wwaApcg13(date1, date2, ref astrom);

            vvd(astrom.pmt, 12.65133794027378508, 1e-11,
                            "wwaApcg13", "pmt", ref status);
            vvd(astrom.eb[0], 0.9013108747340644755, 1e-12,
                            "wwaApcg13", "eb(1)", ref status);
            vvd(astrom.eb[1], -0.4174026640406119957, 1e-12,
                            "wwaApcg13", "eb(2)", ref status);
            vvd(astrom.eb[2], -0.1809822877867817771, 1e-12,
                            "wwaApcg13", "eb(3)", ref status);
            vvd(astrom.eh[0], 0.8940025429255499549, 1e-12,
                            "wwaApcg13", "eh(1)", ref status);
            vvd(astrom.eh[1], -0.4110930268331896318, 1e-12,
                            "wwaApcg13", "eh(2)", ref status);
            vvd(astrom.eh[2], -0.1782189006019749850, 1e-12,
                            "wwaApcg13", "eh(3)", ref status);
            vvd(astrom.em, 1.010465295964664178, 1e-12,
                            "wwaApcg13", "em", ref status);
            //vvd(astrom.v[0], 0.4289638897157027528e-4, 1e-16,
            //                "wwaApcg13", "v(1)", ref status);
            vvd(astrom.v[0], 0.4289638912941341125e-4, 1e-16,
                   "wwaApcg13", "v(1)", ref status);
            //vvd(astrom.v[1], 0.8115034002544663526e-4, 1e-16,
            //                "wwaApcg13", "v(2)", ref status);
            vvd(astrom.v[1], 0.8115034032405042132e-4, 1e-16,
                  "wwaApcg13", "v(2)", ref status);
            //vvd(astrom.v[2], 0.3517555122593144633e-4, 1e-16,
            //                "wwaApcg13", "v(3)", ref status);
            vvd(astrom.v[2], 0.3517555135536470279e-4, 1e-16,
                   "wwaApcg13", "v(3)", ref status);
            //vvd(astrom.bm1, 0.9999999951686013498, 1e-12,
            //                "wwaApcg13", "bm1", ref status);
            vvd(astrom.bm1, 0.9999999951686013142, 1e-12,
                   "wwaApcg13", "bm1", ref status);
            vvd(astrom.bpn[0, 0], 1.0, 0.0,
                                  "wwaApcg13", "bpn(1,1)", ref status);
            vvd(astrom.bpn[1, 0], 0.0, 0.0,
                                  "wwaApcg13", "bpn(2,1)", ref status);
            vvd(astrom.bpn[2, 0], 0.0, 0.0,
                                  "wwaApcg13", "bpn(3,1)", ref status);
            vvd(astrom.bpn[0, 1], 0.0, 0.0,
                                  "wwaApcg13", "bpn(1,2)", ref status);
            vvd(astrom.bpn[1, 1], 1.0, 0.0,
                                  "wwaApcg13", "bpn(2,2)", ref status);
            vvd(astrom.bpn[2, 1], 0.0, 0.0,
                                  "wwaApcg13", "bpn(3,2)", ref status);
            vvd(astrom.bpn[0, 2], 0.0, 0.0,
                                  "wwaApcg13", "bpn(1,3)", ref status);
            vvd(astrom.bpn[1, 2], 0.0, 0.0,
                                  "wwaApcg13", "bpn(2,3)", ref status);
            vvd(astrom.bpn[2, 2], 1.0, 0.0,
                                  "wwaApcg13", "bpn(3,3)", ref status);

        }

        static void t_apci(ref int status)
        /*
        **  - - - - - - -
        **   t _ a p c i
        **  - - - - - - -
        **
        **  Test wwaApci function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaApci, vvd
        **
        **  This revision:  2013 October 3
        */
        {
            double date1, date2, x, y, s;
            double[] ehp = new double[3];
            double[,] ebpv = new double[2, 3];
            WWA.wwaASTROM astrom = new WWA.wwaASTROM();

            astrom.eb = new double[3];
            astrom.eh = new double[3];
            astrom.bpn = new double[3, 3];

            date1 = 2456165.5;
            date2 = 0.401182685;
            ebpv[0, 0] = 0.901310875;
            ebpv[0, 1] = -0.417402664;
            ebpv[0, 2] = -0.180982288;
            ebpv[1, 0] = 0.00742727954;
            ebpv[1, 1] = 0.0140507459;
            ebpv[1, 2] = 0.00609045792;
            ehp[0] = 0.903358544;
            ehp[1] = -0.415395237;
            ehp[2] = -0.180084014;
            x = 0.0013122272;
            y = -2.92808623e-5;
            s = 3.05749468e-8;

            WWA.wwaApci(date1, date2, ebpv, ehp, x, y, s, ref astrom);

            vvd(astrom.pmt, 12.65133794027378508, 1e-11,
                            "wwaApci", "pmt", ref status);
            vvd(astrom.eb[0], 0.901310875, 1e-12,
                              "wwaApci", "eb(1)", ref status);
            vvd(astrom.eb[1], -0.417402664, 1e-12,
                              "wwaApci", "eb(2)", ref status);
            vvd(astrom.eb[2], -0.180982288, 1e-12,
                              "wwaApci", "eb(3)", ref status);
            vvd(astrom.eh[0], 0.8940025429324143045, 1e-12,
                              "wwaApci", "eh(1)", ref status);
            vvd(astrom.eh[1], -0.4110930268679817955, 1e-12,
                              "wwaApci", "eh(2)", ref status);
            vvd(astrom.eh[2], -0.1782189004872870264, 1e-12,
                              "wwaApci", "eh(3)", ref status);
            vvd(astrom.em, 1.010465295811013146, 1e-12,
                           "wwaApci", "em", ref status);
            //vvd(astrom.v[0], 0.4289638897813379954e-4, 1e-16,
            //                 "wwaApci", "v(1)", ref status);
            vvd(astrom.v[0], 0.4289638913597693554e-4, 1e-16,
                   "wwaApci", "v(1)", ref status);
            //vvd(astrom.v[1], 0.8115034021720941898e-4, 1e-16,
            //                 "wwaApci", "v(2)", ref status);
            vvd(astrom.v[1], 0.8115034051581320575e-4, 1e-16,
                    "wwaApci", "v(2)", ref status);
            //vvd(astrom.v[2], 0.3517555123437237778e-4, 1e-16,
            //                 "wwaApci", "v(3)", ref status);
            vvd(astrom.v[2], 0.3517555136380563427e-4, 1e-16,
                    "wwaApci", "v(3)", ref status);
            //vvd(astrom.bm1, 0.9999999951686013336, 1e-12,
            //                "wwaApci", "bm1", ref status);
            vvd(astrom.bm1, 0.9999999951686012981, 1e-12,
                   "wwaApci", "bm1", ref status);
            vvd(astrom.bpn[0, 0], 0.9999991390295159156, 1e-12,
                                  "wwaApci", "bpn(1,1)", ref status);
            vvd(astrom.bpn[1, 0], 0.4978650072505016932e-7, 1e-12,
                                  "wwaApci", "bpn(2,1)", ref status);
            vvd(astrom.bpn[2, 0], 0.1312227200000000000e-2, 1e-12,
                                  "wwaApci", "bpn(3,1)", ref status);
            vvd(astrom.bpn[0, 1], -0.1136336653771609630e-7, 1e-12,
                                  "wwaApci", "bpn(1,2)", ref status);
            vvd(astrom.bpn[1, 1], 0.9999999995713154868, 1e-12,
                                  "wwaApci", "bpn(2,2)", ref status);
            vvd(astrom.bpn[2, 1], -0.2928086230000000000e-4, 1e-12,
                                  "wwaApci", "bpn(3,2)", ref status);
            vvd(astrom.bpn[0, 2], -0.1312227200895260194e-2, 1e-12,
                                  "wwaApci", "bpn(1,3)", ref status);
            vvd(astrom.bpn[1, 2], 0.2928082217872315680e-4, 1e-12,
                                  "wwaApci", "bpn(2,3)", ref status);
            vvd(astrom.bpn[2, 2], 0.9999991386008323373, 1e-12,
                                  "wwaApci", "bpn(3,3)", ref status);

        }

        static void t_apci13(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ a p c i 1 3
        **  - - - - - - - - -
        **
        **  Test wwaApci13 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaApci13, vvd
        **
        **  This revision:  2013 October 3
        */
        {
            double date1, date2, eo = 0;
            WWA.wwaASTROM astrom = new WWA.wwaASTROM();

            astrom.eb = new double[3];
            astrom.eh = new double[3];
            astrom.bpn = new double[3, 3];

            date1 = 2456165.5;
            date2 = 0.401182685;

            WWA.wwaApci13(date1, date2, ref astrom, ref eo);

            vvd(astrom.pmt, 12.65133794027378508, 1e-11,
                            "wwaApci13", "pmt", ref status);
            vvd(astrom.eb[0], 0.9013108747340644755, 1e-12,
                              "wwaApci13", "eb(1)", ref status);
            vvd(astrom.eb[1], -0.4174026640406119957, 1e-12,
                              "wwaApci13", "eb(2)", ref status);
            vvd(astrom.eb[2], -0.1809822877867817771, 1e-12,
                              "wwaApci13", "eb(3)", ref status);
            vvd(astrom.eh[0], 0.8940025429255499549, 1e-12,
                              "wwaApci13", "eh(1)", ref status);
            vvd(astrom.eh[1], -0.4110930268331896318, 1e-12,
                              "wwaApci13", "eh(2)", ref status);
            vvd(astrom.eh[2], -0.1782189006019749850, 1e-12,
                              "wwaApci13", "eh(3)", ref status);
            vvd(astrom.em, 1.010465295964664178, 1e-12,
                           "wwaApci13", "em", ref status);
            //vvd(astrom.v[0], 0.4289638897157027528e-4, 1e-16,
            //                 "wwaApci13", "v(1)", ref status);
            //vvd(astrom.v[1], 0.8115034002544663526e-4, 1e-16,
            //                 "wwaApci13", "v(2)", ref status);
            //vvd(astrom.v[2], 0.3517555122593144633e-4, 1e-16,
            //                 "wwaApci13", "v(3)", ref status);
            //vvd(astrom.bm1, 0.9999999951686013498, 1e-12,
            //                "wwaApci13", "bm1", ref status);
            vvd(astrom.v[0], 0.4289638912941341125e-4, 1e-16,
                            "wwaApci13", "v(1)", ref status);
            vvd(astrom.v[1], 0.8115034032405042132e-4, 1e-16,
                            "wwaApci13", "v(2)", ref status);
            vvd(astrom.v[2], 0.3517555135536470279e-4, 1e-16,
                            "wwaApci13", "v(3)", ref status);
            vvd(astrom.bm1, 0.9999999951686013142, 1e-12,
                            "wwaApci13", "bm1", ref status);

            vvd(astrom.bpn[0, 0], 0.9999992060376761710, 1e-12,
                                  "wwaApci13", "bpn(1,1)", ref status);
            vvd(astrom.bpn[1, 0], 0.4124244860106037157e-7, 1e-12,
                                  "wwaApci13", "bpn(2,1)", ref status);
            vvd(astrom.bpn[2, 0], 0.1260128571051709670e-2, 1e-12,
                                  "wwaApci13", "bpn(3,1)", ref status);
            vvd(astrom.bpn[0, 1], -0.1282291987222130690e-7, 1e-12,
                                  "wwaApci13", "bpn(1,2)", ref status);
            vvd(astrom.bpn[1, 1], 0.9999999997456835325, 1e-12,
                                  "wwaApci13", "bpn(2,2)", ref status);
            vvd(astrom.bpn[2, 1], -0.2255288829420524935e-4, 1e-12,
                                  "wwaApci13", "bpn(3,2)", ref status);
            vvd(astrom.bpn[0, 2], -0.1260128571661374559e-2, 1e-12,
                                  "wwaApci13", "bpn(1,3)", ref status);
            vvd(astrom.bpn[1, 2], 0.2255285422953395494e-4, 1e-12,
                                  "wwaApci13", "bpn(2,3)", ref status);
            vvd(astrom.bpn[2, 2], 0.9999992057833604343, 1e-12,
                                  "wwaApci13", "bpn(3,3)", ref status);
            vvd(eo, -0.2900618712657375647e-2, 1e-12,
                    "wwaApci13", "eo", ref status);

        }

        static void t_apco(ref int status)
        /*
        **  - - - - - - -
        **   t _ a p c o
        **  - - - - - - -
        **
        **  Test wwaApco function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaApco, vvd
        **
        **  This revision:  2013 October 3
        */
        {
            double date1, date2, x, y, s,
                   theta, elong, phi, hm, xp, yp, sp, refa, refb;
            double[,] ebpv = new double[2, 3];
            double[] ehp = new double[3];
            WWA.wwaASTROM astrom = new WWA.wwaASTROM();

            astrom.eb = new double[3];
            astrom.eh = new double[3];
            astrom.bpn = new double[3, 3];

            date1 = 2456384.5;
            date2 = 0.970031644;
            ebpv[0, 0] = -0.974170438;
            ebpv[0, 1] = -0.211520082;
            ebpv[0, 2] = -0.0917583024;
            ebpv[1, 0] = 0.00364365824;
            ebpv[1, 1] = -0.0154287319;
            ebpv[1, 2] = -0.00668922024;
            ehp[0] = -0.973458265;
            ehp[1] = -0.209215307;
            ehp[2] = -0.0906996477;
            x = 0.0013122272;
            y = -2.92808623e-5;
            s = 3.05749468e-8;
            theta = 3.14540971;
            elong = -0.527800806;
            phi = -1.2345856;
            hm = 2738.0;
            xp = 2.47230737e-7;
            yp = 1.82640464e-6;
            sp = -3.01974337e-11;
            refa = 0.000201418779;
            refb = -2.36140831e-7;

            WWA.wwaApco(date1, date2, ebpv, ehp, x, y, s,
                    theta, elong, phi, hm, xp, yp, sp,
                    refa, refb, ref astrom);

            vvd(astrom.pmt, 13.25248468622587269, 1e-11,
                            "wwaApco", "pmt", ref status);
            //vvd(astrom.eb[0], -0.9741827110630897003, 1e-12,
            //                  "wwaApco", "eb(1)", ref status);
            //vvd(astrom.eb[1], -0.2115130190135014340, 1e-12,
            //                  "wwaApco", "eb(2)", ref status);
            //vvd(astrom.eb[2], -0.09179840186968295686, 1e-12,
            //                  "wwaApco", "eb(3)", ref status);
            //vvd(astrom.eh[0], -0.9736425571689670428, 1e-12,
            //                  "wwaApco", "eh(1)", ref status);
            //vvd(astrom.eh[1], -0.2092452125848862201, 1e-12,
            //                  "wwaApco", "eh(2)", ref status);
            //vvd(astrom.eh[2], -0.09075578152261439954, 1e-12,
            //                  "wwaApco", "eh(3)", ref status);
            //vvd(astrom.em, 0.9998233241710617934, 1e-12,
            //               "wwaApco", "em", ref status);
            //vvd(astrom.v[0], 0.2078704985147609823e-4, 1e-16,
            //                 "wwaApco", "v(1)", ref status);
            //vvd(astrom.v[1], -0.8955360074407552709e-4, 1e-16,
            //                 "wwaApco", "v(2)", ref status);
            //vvd(astrom.v[2], -0.3863338980073114703e-4, 1e-16,
            //                 "wwaApco", "v(3)", ref status);
            //vvd(astrom.bm1, 0.9999999950277561600, 1e-12,
            //                "wwaApco", "bm1", ref status);
            vvd(astrom.eb[0], -0.9741827110630322720, 1e-12,
                            "wwaApco", "eb(1)", ref status);
            vvd(astrom.eb[1], -0.2115130190135344832, 1e-12,
                              "wwaApco", "eb(2)", ref status);
            vvd(astrom.eb[2], -0.09179840186949532298, 1e-12,
                              "wwaApco", "eb(3)", ref status);
            vvd(astrom.eh[0], -0.9736425571689739035, 1e-12,
                              "wwaApco", "eh(1)", ref status);
            vvd(astrom.eh[1], -0.2092452125849330936, 1e-12,
                              "wwaApco", "eh(2)", ref status);
            vvd(astrom.eh[2], -0.09075578152243272599, 1e-12,
                              "wwaApco", "eh(3)", ref status);
            vvd(astrom.em, 0.9998233241709957653, 1e-12,
                           "wwaApco", "em", ref status);
            vvd(astrom.v[0], 0.2078704992916728762e-4, 1e-16,
                             "wwaApco", "v(1)", ref status);
            vvd(astrom.v[1], -0.8955360107151952319e-4, 1e-16,
                             "wwaApco", "v(2)", ref status);
            vvd(astrom.v[2], -0.3863338994288951082e-4, 1e-16,
                             "wwaApco", "v(3)", ref status);
            vvd(astrom.bm1, 0.9999999950277561236, 1e-12,
                            "wwaApco", "bm1", ref status);

            vvd(astrom.bpn[0, 0], 0.9999991390295159156, 1e-12,
                                  "wwaApco", "bpn(1,1)", ref status);
            vvd(astrom.bpn[1, 0], 0.4978650072505016932e-7, 1e-12,
                                  "wwaApco", "bpn(2,1)", ref status);
            vvd(astrom.bpn[2, 0], 0.1312227200000000000e-2, 1e-12,
                                  "wwaApco", "bpn(3,1)", ref status);
            vvd(astrom.bpn[0, 1], -0.1136336653771609630e-7, 1e-12,
                                  "wwaApco", "bpn(1,2)", ref status);
            vvd(astrom.bpn[1, 1], 0.9999999995713154868, 1e-12,
                                  "wwaApco", "bpn(2,2)", ref status);
            vvd(astrom.bpn[2, 1], -0.2928086230000000000e-4, 1e-12,
                                  "wwaApco", "bpn(3,2)", ref status);
            vvd(astrom.bpn[0, 2], -0.1312227200895260194e-2, 1e-12,
                                  "wwaApco", "bpn(1,3)", ref status);
            vvd(astrom.bpn[1, 2], 0.2928082217872315680e-4, 1e-12,
                                  "wwaApco", "bpn(2,3)", ref status);
            vvd(astrom.bpn[2, 2], 0.9999991386008323373, 1e-12,
                                  "wwaApco", "bpn(3,3)", ref status);
            vvd(astrom.along, -0.5278008060301974337, 1e-12,
                              "wwaApco", "along", ref status);
            vvd(astrom.xpl, 0.1133427418174939329e-5, 1e-17,
                            "wwaApco", "xpl", ref status);
            vvd(astrom.ypl, 0.1453347595745898629e-5, 1e-17,
                            "wwaApco", "ypl", ref status);
            vvd(astrom.sphi, -0.9440115679003211329, 1e-12,
                             "wwaApco", "sphi", ref status);
            vvd(astrom.cphi, 0.3299123514971474711, 1e-12,
                             "wwaApco", "cphi", ref status);
            vvd(astrom.diurab, 0, 0,
                               "wwaApco", "diurab", ref status);
            vvd(astrom.eral, 2.617608903969802566, 1e-12,
                             "wwaApco", "eral", ref status);
            vvd(astrom.refa, 0.2014187790000000000e-3, 1e-15,
                             "wwaApco", "refa", ref status);
            vvd(astrom.refb, -0.2361408310000000000e-6, 1e-18,
                             "wwaApco", "refb", ref status);

        }

        static void t_apco13(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ a p c o 1 3
        **  - - - - - - - - -
        **
        **  Test wwaApco13 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaApco13, vvd, viv
        **
        **  This revision:  2013 October 4
        */
        {
            double utc1, utc2, dut1, elong, phi, hm, xp, yp,
                   phpa, tc, rh, wl, eo = 0;
            WWA.wwaASTROM astrom = new WWA.wwaASTROM();

            astrom.eb = new double[3];
            astrom.eh = new double[3];
            astrom.bpn = new double[3, 3];

            int j;

            utc1 = 2456384.5;
            utc2 = 0.969254051;
            dut1 = 0.1550675;
            elong = -0.527800806;
            phi = -1.2345856;
            hm = 2738.0;
            xp = 2.47230737e-7;
            yp = 1.82640464e-6;
            phpa = 731.0;
            tc = 12.8;
            rh = 0.59;
            wl = 0.55;

            j = WWA.wwaApco13(utc1, utc2, dut1, elong, phi, hm, xp, yp,
                          phpa, tc, rh, wl, ref astrom, ref eo);

            vvd(astrom.pmt, 13.25248468622475727, 1e-11,
                            "wwaApco13", "pmt", ref status);
            //vvd(astrom.eb[0], -0.9741827107321449445, 1e-12,
            //                "wwaApco13", "eb(1)", ref status);
            //vvd(astrom.eb[1], -0.2115130190489386190, 1e-12,
            //                  "wwaApco13", "eb(2)", ref status);
            //vvd(astrom.eb[2], -0.09179840189515518726, 1e-12,
            //                  "wwaApco13", "eb(3)", ref status);
            //vvd(astrom.eh[0], -0.9736425572586866640, 1e-12,
            //                  "wwaApco13", "eh(1)", ref status);
            //vvd(astrom.eh[1], -0.2092452121602867431, 1e-12,
            //                  "wwaApco13", "eh(2)", ref status);
            //vvd(astrom.eh[2], -0.09075578153903832650, 1e-12,
            //                  "wwaApco13", "eh(3)", ref status);
            //vvd(astrom.em, 0.9998233240914558422, 1e-12,
            //               "wwaApco13", "em", ref status);
            //vvd(astrom.v[0], 0.2078704986751370303e-4, 1e-16,
            //                 "wwaApco13", "v(1)", ref status);
            //vvd(astrom.v[1], -0.8955360100494469232e-4, 1e-16,
            //                 "wwaApco13", "v(2)", ref status);
            //vvd(astrom.v[2], -0.3863338978840051024e-4, 1e-16,
            //                 "wwaApco13", "v(3)", ref status);
            //vvd(astrom.bm1, 0.9999999950277561368, 1e-12,
            //                "wwaApco13", "bm1", ref status);
            vvd(astrom.eb[0], -0.9741827107320875162, 1e-12,
                   "wwaApco13", "eb(1)", ref status);
            vvd(astrom.eb[1], -0.2115130190489716682, 1e-12,
                              "wwaApco13", "eb(2)", ref status);
            vvd(astrom.eb[2], -0.09179840189496755339, 1e-12,
                              "wwaApco13", "eb(3)", ref status);
            vvd(astrom.eh[0], -0.9736425572586935247, 1e-12,
                              "wwaApco13", "eh(1)", ref status);
            vvd(astrom.eh[1], -0.2092452121603336166, 1e-12,
                              "wwaApco13", "eh(2)", ref status);
            vvd(astrom.eh[2], -0.09075578153885665295, 1e-12,
                              "wwaApco13", "eh(3)", ref status);
            vvd(astrom.em, 0.9998233240913898141, 1e-12,
                           "wwaApco13", "em", ref status);
            vvd(astrom.v[0], 0.2078704994520489246e-4, 1e-16,
                             "wwaApco13", "v(1)", ref status);
            vvd(astrom.v[1], -0.8955360133238868938e-4, 1e-16,
                             "wwaApco13", "v(2)", ref status);
            vvd(astrom.v[2], -0.3863338993055887398e-4, 1e-16,
                             "wwaApco13", "v(3)", ref status);
            vvd(astrom.bm1, 0.9999999950277561004, 1e-12,
                            "wwaApco13", "bm1", ref status);

            vvd(astrom.bpn[0, 0], 0.9999991390295147999, 1e-12,
                                  "wwaApco13", "bpn(1,1)", ref status);
            vvd(astrom.bpn[1, 0], 0.4978650075315529277e-7, 1e-12,
                                  "wwaApco13", "bpn(2,1)", ref status);
            vvd(astrom.bpn[2, 0], 0.001312227200850293372, 1e-12,
                                  "wwaApco13", "bpn(3,1)", ref status);
            vvd(astrom.bpn[0, 1], -0.1136336652812486604e-7, 1e-12,
                                  "wwaApco13", "bpn(1,2)", ref status);
            vvd(astrom.bpn[1, 1], 0.9999999995713154865, 1e-12,
                                  "wwaApco13", "bpn(2,2)", ref status);
            vvd(astrom.bpn[2, 1], -0.2928086230975367296e-4, 1e-12,
                                  "wwaApco13", "bpn(3,2)", ref status);
            vvd(astrom.bpn[0, 2], -0.001312227201745553566, 1e-12,
                                  "wwaApco13", "bpn(1,3)", ref status);
            vvd(astrom.bpn[1, 2], 0.2928082218847679162e-4, 1e-12,
                                  "wwaApco13", "bpn(2,3)", ref status);
            vvd(astrom.bpn[2, 2], 0.9999991386008312212, 1e-12,
                                  "wwaApco13", "bpn(3,3)", ref status);
            vvd(astrom.along, -0.5278008060301974337, 1e-12,
                              "wwaApco13", "along", ref status);
            vvd(astrom.xpl, 0.1133427418174939329e-5, 1e-17,
                            "wwaApco13", "xpl", ref status);
            vvd(astrom.ypl, 0.1453347595745898629e-5, 1e-17,
                            "wwaApco13", "ypl", ref status);
            vvd(astrom.sphi, -0.9440115679003211329, 1e-12,
                             "wwaApco13", "sphi", ref status);
            vvd(astrom.cphi, 0.3299123514971474711, 1e-12,
                             "wwaApco13", "cphi", ref status);
            vvd(astrom.diurab, 0, 0,
                               "wwaApco13", "diurab", ref status);
            vvd(astrom.eral, 2.617608909189066140, 1e-12,
                             "wwaApco13", "eral", ref status);
            vvd(astrom.refa, 0.2014187785940396921e-3, 1e-15,
                             "wwaApco13", "refa", ref status);
            vvd(astrom.refb, -0.2361408314943696227e-6, 1e-18,
                             "wwaApco13", "refb", ref status);
            vvd(eo, -0.003020548354802412839, 1e-14,
                    "wwaApco13", "eo", ref status);
            viv(j, 0, "wwaApco13", "j", ref status);

        }

        static void t_apcs(ref int status)
        /*
        **  - - - - - - -
        **   t _ a p c s
        **  - - - - - - -
        **
        **  Test wwaApcs function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaApcs, vvd
        **
        **  This revision:  2013 October 3
        */
        {
            double date1, date2;
            double[,] pv = new double[2, 3];
            double[,] ebpv = new double[2, 3];
            double[] ehp = new double[3];
            WWA.wwaASTROM astrom = new WWA.wwaASTROM();

            astrom.eb = new double[3];
            astrom.eh = new double[3];
            astrom.bpn = new double[3, 3];

            date1 = 2456384.5;
            date2 = 0.970031644;
            pv[0, 0] = -1836024.09;
            pv[0, 1] = 1056607.72;
            pv[0, 2] = -5998795.26;
            pv[1, 0] = -77.0361767;
            pv[1, 1] = -133.310856;
            pv[1, 2] = 0.0971855934;
            ebpv[0, 0] = -0.974170438;
            ebpv[0, 1] = -0.211520082;
            ebpv[0, 2] = -0.0917583024;
            ebpv[1, 0] = 0.00364365824;
            ebpv[1, 1] = -0.0154287319;
            ebpv[1, 2] = -0.00668922024;
            ehp[0] = -0.973458265;
            ehp[1] = -0.209215307;
            ehp[2] = -0.0906996477;

            WWA.wwaApcs(date1, date2, pv, ebpv, ehp, ref astrom);

            vvd(astrom.pmt, 13.25248468622587269, 1e-11,
                            "wwaApcs", "pmt", ref status);
            //vvd(astrom.eb[0], -0.9741827110630456169, 1e-12,
            //                  "wwaApcs", "eb(1)", ref status);
            //vvd(astrom.eb[1], -0.2115130190136085494, 1e-12,
            //                  "wwaApcs", "eb(2)", ref status);
            //vvd(astrom.eb[2], -0.09179840186973175487, 1e-12,
            //                  "wwaApcs", "eb(3)", ref status);
            //vvd(astrom.eh[0], -0.9736425571689386099, 1e-12,
            //                  "wwaApcs", "eh(1)", ref status);
            //vvd(astrom.eh[1], -0.2092452125849967195, 1e-12,
            //                  "wwaApcs", "eh(2)", ref status);
            //vvd(astrom.eh[2], -0.09075578152266466572, 1e-12,
            //                  "wwaApcs", "eh(3)", ref status);
            //vvd(astrom.em, 0.9998233241710457140, 1e-12,
            //               "wwaApcs", "em", ref status);
            //vvd(astrom.v[0], 0.2078704985513566571e-4, 1e-16,
            //                 "wwaApcs", "v(1)", ref status);
            //vvd(astrom.v[1], -0.8955360074245006073e-4, 1e-16,
            //                 "wwaApcs", "v(2)", ref status);
            //vvd(astrom.v[2], -0.3863338980073572719e-4, 1e-16,
            //                 "wwaApcs", "v(3)", ref status);
            //vvd(astrom.bm1, 0.9999999950277561601, 1e-12,
            //                "wwaApcs", "bm1", ref status);
            vvd(astrom.eb[0], -0.9741827110629881886, 1e-12,
                     "wwaApcs", "eb(1)", ref status);
            vvd(astrom.eb[1], -0.2115130190136415986, 1e-12,
                              "wwaApcs", "eb(2)", ref status);
            vvd(astrom.eb[2], -0.09179840186954412099, 1e-12,
                              "wwaApcs", "eb(3)", ref status);
            vvd(astrom.eh[0], -0.9736425571689454706, 1e-12,
                              "wwaApcs", "eh(1)", ref status);
            vvd(astrom.eh[1], -0.2092452125850435930, 1e-12,
                              "wwaApcs", "eh(2)", ref status);
            vvd(astrom.eh[2], -0.09075578152248299218, 1e-12,
                              "wwaApcs", "eh(3)", ref status);
            vvd(astrom.em, 0.9998233241709796859, 1e-12,
                           "wwaApcs", "em", ref status);
            vvd(astrom.v[0], 0.2078704993282685510e-4, 1e-16,
                             "wwaApcs", "v(1)", ref status);
            vvd(astrom.v[1], -0.8955360106989405683e-4, 1e-16,
                             "wwaApcs", "v(2)", ref status);
            vvd(astrom.v[2], -0.3863338994289409097e-4, 1e-16,
                             "wwaApcs", "v(3)", ref status);
            vvd(astrom.bm1, 0.9999999950277561237, 1e-12,
                            "wwaApcs", "bm1", ref status);

            vvd(astrom.bpn[0, 0], 1, 0,
                                  "wwaApcs", "bpn(1,1)", ref status);
            vvd(astrom.bpn[1, 0], 0, 0,
                                  "wwaApcs", "bpn(2,1)", ref status);
            vvd(astrom.bpn[2, 0], 0, 0,
                                  "wwaApcs", "bpn(3,1)", ref status);
            vvd(astrom.bpn[0, 1], 0, 0,
                                  "wwaApcs", "bpn(1,2)", ref status);
            vvd(astrom.bpn[1, 1], 1, 0,
                                  "wwaApcs", "bpn(2,2)", ref status);
            vvd(astrom.bpn[2, 1], 0, 0,
                                  "wwaApcs", "bpn(3,2)", ref status);
            vvd(astrom.bpn[0, 2], 0, 0,
                                  "wwaApcs", "bpn(1,3)", ref status);
            vvd(astrom.bpn[1, 2], 0, 0,
                                  "wwaApcs", "bpn(2,3)", ref status);
            vvd(astrom.bpn[2, 2], 1, 0,
                                  "wwaApcs", "bpn(3,3)", ref status);

        }

        static void t_apcs13(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ a p c s 1 3
        **  - - - - - - - - -
        **
        **  Test wwaApcs13 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaApcs13, vvd
        **
        **  This revision:  2013 October 3
        */
        {
            double date1, date2;
            double[,] pv = new double[2, 3];
            WWA.wwaASTROM astrom = new WWA.wwaASTROM();

            astrom.eb = new double[3];
            astrom.eh = new double[3];
            astrom.bpn = new double[3, 3];

            date1 = 2456165.5;
            date2 = 0.401182685;
            pv[0, 0] = -6241497.16;
            pv[0, 1] = 401346.896;
            pv[0, 2] = -1251136.04;
            pv[1, 0] = -29.264597;
            pv[1, 1] = -455.021831;
            pv[1, 2] = 0.0266151194;

            WWA.wwaApcs13(date1, date2, pv, ref astrom);

            vvd(astrom.pmt, 12.65133794027378508, 1e-11,
                            "wwaApcs13", "pmt", ref status);
            //vvd(astrom.eb[0], 0.9012691529023298391, 1e-12,
            //                  "wwaApcs13", "eb(1)", ref status);
            //vvd(astrom.eb[1], -0.4173999812023068781, 1e-12,
            //                  "wwaApcs13", "eb(2)", ref status);
            //vvd(astrom.eb[2], -0.1809906511146821008, 1e-12,
            //                  "wwaApcs13", "eb(3)", ref status);
            //vvd(astrom.eh[0], 0.8939939101759726824, 1e-12,
            //                  "wwaApcs13", "eh(1)", ref status);
            //vvd(astrom.eh[1], -0.4111053891734599955, 1e-12,
            //                  "wwaApcs13", "eh(2)", ref status);
            //vvd(astrom.eh[2], -0.1782336880637689334, 1e-12,
            //                  "wwaApcs13", "eh(3)", ref status);
            //vvd(astrom.em, 1.010428384373318379, 1e-12,
            //               "wwaApcs13", "em", ref status);
            //vvd(astrom.v[0], 0.4279877278327626511e-4, 1e-16,
            //                 "wwaApcs13", "v(1)", ref status);
            //vvd(astrom.v[1], 0.7963255057040027770e-4, 1e-16,
            //                 "wwaApcs13", "v(2)", ref status);
            //vvd(astrom.v[2], 0.3517564000441374759e-4, 1e-16,
            //                 "wwaApcs13", "v(3)", ref status);
            //vvd(astrom.bm1, 0.9999999952947981330, 1e-12,
            //                "wwaApcs13", "bm1", ref status);
            vvd(astrom.eb[0], 0.9012691529025250644, 1e-12,
                     "wwaApcs13", "eb(1)", ref status);
            vvd(astrom.eb[1], -0.4173999812023194317, 1e-12,
                              "wwaApcs13", "eb(2)", ref status);
            vvd(astrom.eb[2], -0.1809906511146429670, 1e-12,
                              "wwaApcs13", "eb(3)", ref status);
            vvd(astrom.eh[0], 0.8939939101760130792, 1e-12,
                              "wwaApcs13", "eh(1)", ref status);
            vvd(astrom.eh[1], -0.4111053891734021478, 1e-12,
                              "wwaApcs13", "eh(2)", ref status);
            vvd(astrom.eh[2], -0.1782336880636997374, 1e-12,
                              "wwaApcs13", "eh(3)", ref status);
            vvd(astrom.em, 1.010428384373491095, 1e-12,
                           "wwaApcs13", "em", ref status);
            vvd(astrom.v[0], 0.4279877294121697570e-4, 1e-16,
                             "wwaApcs13", "v(1)", ref status);
            vvd(astrom.v[1], 0.7963255087052120678e-4, 1e-16,
                             "wwaApcs13", "v(2)", ref status);
            vvd(astrom.v[2], 0.3517564013384691531e-4, 1e-16,
                             "wwaApcs13", "v(3)", ref status);
            vvd(astrom.bm1, 0.9999999952947980978, 1e-12,
                            "wwaApcs13", "bm1", ref status);

            vvd(astrom.bpn[0, 0], 1, 0,
                                  "wwaApcs13", "bpn(1,1)", ref status);
            vvd(astrom.bpn[1, 0], 0, 0,
                                  "wwaApcs13", "bpn(2,1)", ref status);
            vvd(astrom.bpn[2, 0], 0, 0,
                                  "wwaApcs13", "bpn(3,1)", ref status);
            vvd(astrom.bpn[0, 1], 0, 0,
                                  "wwaApcs13", "bpn(1,2)", ref status);
            vvd(astrom.bpn[1, 1], 1, 0,
                                  "wwaApcs13", "bpn(2,2)", ref status);
            vvd(astrom.bpn[2, 1], 0, 0,
                                  "wwaApcs13", "bpn(3,2)", ref status);
            vvd(astrom.bpn[0, 2], 0, 0,
                                  "wwaApcs13", "bpn(1,3)", ref status);
            vvd(astrom.bpn[1, 2], 0, 0,
                                  "wwaApcs13", "bpn(2,3)", ref status);
            vvd(astrom.bpn[2, 2], 1, 0,
                                  "wwaApcs13", "bpn(3,3)", ref status);

        }

        static void t_aper(ref int status)
        /*
        **  - - - - - - -
        **   t _ a p e r
        **  - - - - - - -
        *
        **  Test wwaAper function.
        *
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        *
        **  Called:  wwaAper, vvd
        *
        **  This revision:  2013 October 3
        */
        {
            double theta;
            WWA.wwaASTROM astrom = new WWA.wwaASTROM();


            astrom.along = 1.234;
            theta = 5.678;

            WWA.wwaAper(theta, ref astrom);

            vvd(astrom.eral, 6.912000000000000000, 1e-12,
                             "wwaAper", "pmt", ref status);

        }

        static void t_aper13(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ a p e r 1 3
        **  - - - - - - - - -
        **
        **  Test wwaAper13 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaAper13, vvd
        **
        **  This revision:  2013 October 3
        */
        {
            double ut11, ut12;
            WWA.wwaASTROM astrom = new WWA.wwaASTROM();


            astrom.along = 1.234;
            ut11 = 2456165.5;
            ut12 = 0.401182685;

            WWA.wwaAper13(ut11, ut12, ref astrom);

            vvd(astrom.eral, 3.316236661789694933, 1e-12,
                             "wwaAper13", "pmt", ref status);

        }

        static void t_apio(ref int status)
        /*
        **  - - - - - - -
        **   t _ a p i o
        **  - - - - - - -
        **
        **  Test wwaApio function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaApio, vvd
        **
        **  This revision:  2013 October 3
        */
        {
            double sp, theta, elong, phi, hm, xp, yp, refa, refb;
            WWA.wwaASTROM astrom = new WWA.wwaASTROM();


            sp = -3.01974337e-11;
            theta = 3.14540971;
            elong = -0.527800806;
            phi = -1.2345856;
            hm = 2738.0;
            xp = 2.47230737e-7;
            yp = 1.82640464e-6;
            refa = 0.000201418779;
            refb = -2.36140831e-7;

            WWA.wwaApio(sp, theta, elong, phi, hm, xp, yp, refa, refb, ref astrom);

            vvd(astrom.along, -0.5278008060301974337, 1e-12,
                              "wwaApio", "along", ref status);
            vvd(astrom.xpl, 0.1133427418174939329e-5, 1e-17,
                            "wwaApio", "xpl", ref status);
            vvd(astrom.ypl, 0.1453347595745898629e-5, 1e-17,
                            "wwaApio", "ypl", ref status);
            vvd(astrom.sphi, -0.9440115679003211329, 1e-12,
                             "wwaApio", "sphi", ref status);
            vvd(astrom.cphi, 0.3299123514971474711, 1e-12,
                             "wwaApio", "cphi", ref status);
            vvd(astrom.diurab, 0.5135843661699913529e-6, 1e-12,
                               "wwaApio", "diurab", ref status);
            vvd(astrom.eral, 2.617608903969802566, 1e-12,
                             "wwaApio", "eral", ref status);
            vvd(astrom.refa, 0.2014187790000000000e-3, 1e-15,
                             "wwaApio", "refa", ref status);
            vvd(astrom.refb, -0.2361408310000000000e-6, 1e-18,
                             "wwaApio", "refb", ref status);

        }

        static void t_apio13(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ a p i o 1 3
        **  - - - - - - - - -
        **
        **  Test wwaApio13 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaApio13, vvd, viv
        **
        **  This revision:  2013 October 4
        */
        {
            double utc1, utc2, dut1, elong, phi, hm, xp, yp, phpa, tc, rh, wl;
            int j;
            WWA.wwaASTROM astrom = new WWA.wwaASTROM();


            utc1 = 2456384.5;
            utc2 = 0.969254051;
            dut1 = 0.1550675;
            elong = -0.527800806;
            phi = -1.2345856;
            hm = 2738.0;
            xp = 2.47230737e-7;
            yp = 1.82640464e-6;
            phpa = 731.0;
            tc = 12.8;
            rh = 0.59;
            wl = 0.55;

            j = WWA.wwaApio13(utc1, utc2, dut1, elong, phi, hm, xp, yp, phpa, tc, rh, wl, ref astrom);

            vvd(astrom.along, -0.5278008060301974337, 1e-12,
                              "wwaApio13", "along", ref status);
            vvd(astrom.xpl, 0.1133427418174939329e-5, 1e-17,
                            "wwaApio13", "xpl", ref status);
            vvd(astrom.ypl, 0.1453347595745898629e-5, 1e-17,
                            "wwaApio13", "ypl", ref status);
            vvd(astrom.sphi, -0.9440115679003211329, 1e-12,
                             "wwaApio13", "sphi", ref status);
            vvd(astrom.cphi, 0.3299123514971474711, 1e-12,
                             "wwaApio13", "cphi", ref status);
            vvd(astrom.diurab, 0.5135843661699913529e-6, 1e-12,
                               "wwaApio13", "diurab", ref status);
            vvd(astrom.eral, 2.617608909189066140, 1e-12,
                             "wwaApio13", "eral", ref status);
            vvd(astrom.refa, 0.2014187785940396921e-3, 1e-15,
                             "wwaApio13", "refa", ref status);
            vvd(astrom.refb, -0.2361408314943696227e-6, 1e-18,
                             "wwaApio13", "refb", ref status);
            viv(j, 0, "wwaApio13", "j", ref status);

        }

        static void t_atci13(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ a t c i 1 3
        **  - - - - - - - - -
        **
        **  Test wwaAtci13 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaAtci13, vvd
        **
        **  This revision:  2013 October 3
        */
        {
            double rc, dc, pr, pd, px, rv, date1, date2, ri = 0, di = 0, eo = 0;


            rc = 2.71;
            dc = 0.174;
            pr = 1e-5;
            pd = 5e-6;
            px = 0.1;
            rv = 55.0;
            date1 = 2456165.5;
            date2 = 0.401182685;

            WWA.wwaAtci13(rc, dc, pr, pd, px, rv, date1, date2, ref ri, ref di, ref eo);

            //vvd(ri, 2.710121572969038991, 1e-12,
            //        "wwaAtci13", "ri", ref status);
            //vvd(di, 0.1729371367218230438, 1e-12,
            //        "wwaAtci13", "di", ref status);
            vvd(ri, 2.710121572968696744, 1e-12,
                    "wwaAtci13", "ri", ref status);
            vvd(di, 0.1729371367219539137, 1e-12,
                    "wwaAtci13", "di", ref status);
            vvd(eo, -0.002900618712657375647, 1e-14,
                    "wwaAtci13", "eo", ref status);

        }

        static void t_atciq(ref int status)
        /*
        **  - - - - - - - -
        **   t _ a t c i q
        **  - - - - - - - -
        **
        **  Test wwaAtciq function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaApci13, wwaAtciq, vvd
        **
        **  This revision:  2013 October 3
        */
        {
            double date1, date2, eo = 0, rc, dc, pr, pd, px, rv, ri = 0, di = 0;
            WWA.wwaASTROM astrom = new WWA.wwaASTROM();

            astrom.eb = new double[3];
            astrom.eh = new double[3];
            astrom.bpn = new double[3, 3];

            date1 = 2456165.5;
            date2 = 0.401182685;
            WWA.wwaApci13(date1, date2, ref astrom, ref eo);
            rc = 2.71;
            dc = 0.174;
            pr = 1e-5;
            pd = 5e-6;
            px = 0.1;
            rv = 55.0;

            WWA.wwaAtciq(rc, dc, pr, pd, px, rv, ref astrom, ref ri, ref di);

            //vvd(ri, 2.710121572969038991, 1e-12, "wwaAtciq", "ri", ref status);
            //vvd(di, 0.1729371367218230438, 1e-12, "wwaAtciq", "di", ref status);
            vvd(ri, 2.710121572968696744, 1e-12, "wwaAtciq", "ri", ref status);
            vvd(di, 0.1729371367219539137, 1e-12, "wwaAtciq", "di", ref status);
        }

        static void t_atciqn(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ a t c i q n
        **  - - - - - - - - -
        **
        **  Test wwaAtciqn function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaApci13, wwaAtciqn, vvd
        **
        **  This revision:  2013 October 3
        */
        {
            WWA.wwaLDBODY[] b = new WWA.wwaLDBODY[3];

            double date1, date2, eo = 0, rc, dc, pr, pd, px, rv, ri = 0, di = 0;
            WWA.wwaASTROM astrom = new WWA.wwaASTROM();

            astrom.eb = new double[3];
            astrom.eh = new double[3];
            astrom.bpn = new double[3, 3];

            date1 = 2456165.5;
            date2 = 0.401182685;
            WWA.wwaApci13(date1, date2, ref astrom, ref eo);
            rc = 2.71;
            dc = 0.174;
            pr = 1e-5;
            pd = 5e-6;
            px = 0.1;
            rv = 55.0;
            b[0].bm = 0.00028574;
            b[0].dl = 3e-10;

            b[0].pv = new double[3, 3];
            b[0].pv[0, 0] = -7.81014427;
            b[0].pv[0, 1] = -5.60956681;
            b[0].pv[0, 2] = -1.98079819;
            b[0].pv[1, 0] = 0.0030723249;
            b[0].pv[1, 1] = -0.00406995477;
            b[0].pv[1, 2] = -0.00181335842;
            b[1].bm = 0.00095435;
            b[1].dl = 3e-9;

            b[1].pv = new double[3, 3];
            b[1].pv[0, 0] = 0.738098796;
            b[1].pv[0, 1] = 4.63658692;
            b[1].pv[0, 2] = 1.9693136;
            b[1].pv[1, 0] = -0.00755816922;
            b[1].pv[1, 1] = 0.00126913722;
            b[1].pv[1, 2] = 0.000727999001;
            b[2].bm = 1.0;
            b[2].dl = 6e-6;

            b[2].pv = new double[3, 3];
            b[2].pv[0, 0] = -0.000712174377;
            b[2].pv[0, 1] = -0.00230478303;
            b[2].pv[0, 2] = -0.00105865966;
            b[2].pv[1, 0] = 6.29235213e-6;
            b[2].pv[1, 1] = -3.30888387e-7;
            b[2].pv[1, 2] = -2.96486623e-7;

            WWA.wwaAtciqn(rc, dc, pr, pd, px, rv, ref astrom, 3, b, ref ri, ref di);

            //vvd(ri, 2.710122008105325582, 1e-12, "wwaAtciqn", "ri", ref status);
            //vvd(di, 0.1729371916491459122, 1e-12, "wwaAtciqn", "di", ref status);
            vvd(ri, 2.710122008104983335, 1e-12, "wwaAtciqn", "ri", ref status);
            vvd(di, 0.1729371916492767821, 1e-12, "wwaAtciqn", "di", ref status);
        }

        static void t_atciqz(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ a t c i q z
        **  - - - - - - - - -
        **
        **  Test wwaAtciqz function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaApci13, wwaAtciqz, vvd
        **
        **  This revision:  2013 October 3
        */
        {
            double date1, date2, eo = 0, rc, dc, ri = 0, di = 0;
            WWA.wwaASTROM astrom = new WWA.wwaASTROM();

            astrom.eb = new double[3];
            astrom.eh = new double[3];
            astrom.bpn = new double[3, 3];

            date1 = 2456165.5;
            date2 = 0.401182685;
            WWA.wwaApci13(date1, date2, ref astrom, ref eo);
            rc = 2.71;
            dc = 0.174;

            WWA.wwaAtciqz(rc, dc, ref astrom, ref ri, ref di);

            //vvd(ri, 2.709994899247599271, 1e-12, "wwaAtciqz", "ri", ref status);
            //vvd(di, 0.1728740720983623469, 1e-12, "wwaAtciqz", "di", ref status);
            vvd(ri, 2.709994899247256984, 1e-12, "wwaAtciqz", "ri", ref status);
            vvd(di, 0.1728740720984931891, 1e-12, "wwaAtciqz", "di", ref status);
        }

        static void t_atco13(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ a t c o 1 3
        **  - - - - - - - - -
        **
        **  Test wwaAtco13 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaAtco13, vvd, viv
        **
        **  This revision:  2013 October 4
        */
        {
            double rc, dc, pr, pd, px, rv, utc1, utc2, dut1,
                   elong, phi, hm, xp, yp, phpa, tc, rh, wl,
                   aob = 0, zob = 0, hob = 0, dob = 0, rob = 0, eo = 0;
            int j;

            rc = 2.71;
            dc = 0.174;
            pr = 1e-5;
            pd = 5e-6;
            px = 0.1;
            rv = 55.0;
            utc1 = 2456384.5;
            utc2 = 0.969254051;
            dut1 = 0.1550675;
            elong = -0.527800806;
            phi = -1.2345856;
            hm = 2738.0;
            xp = 2.47230737e-7;
            yp = 1.82640464e-6;
            phpa = 731.0;
            tc = 12.8;
            rh = 0.59;
            wl = 0.55;

            j = WWA.wwaAtco13(rc, dc, pr, pd, px, rv,
                          utc1, utc2, dut1, elong, phi, hm, xp, yp,
                          phpa, tc, rh, wl,
                          ref aob, ref zob, ref hob, ref dob, ref rob, ref eo);

            //vvd(aob, 0.09251774485358230653, 1e-12, "wwaAtco13", "aob", ref status);
            //vvd(zob, 1.407661405256767021, 1e-12, "wwaAtco13", "zob", ref status);
            //vvd(hob, -0.09265154431403157925, 1e-12, "wwaAtco13", "hob", ref status);
            //vvd(dob, 0.1716626560075591655, 1e-12, "wwaAtco13", "dob", ref status);
            //vvd(rob, 2.710260453503097719, 1e-12, "wwaAtco13", "rob", ref status);
            vvd(aob, 0.09251774485385390973, 1e-12, "wwaAtco13", "aob", ref status);
            vvd(zob, 1.407661405256671703, 1e-12, "wwaAtco13", "zob", ref status);
            vvd(hob, -0.09265154431430045141, 1e-12, "wwaAtco13", "hob", ref status);
            vvd(dob, 0.1716626560074556029, 1e-12, "wwaAtco13", "dob", ref status);
            vvd(rob, 2.710260453503366591, 1e-12, "wwaAtco13", "rob", ref status);

            vvd(eo, -0.003020548354802412839, 1e-14, "wwaAtco13", "eo", ref status);
            viv(j, 0, "wwaAtco13", "j", ref status);

        }

        static void t_atic13(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ a t i c 1 3
        **  - - - - - - - - -
        **
        **  Test wwaAtic13 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaAtic13, vvd
        **
        **  This revision:  2013 October 3
        */
        {
            double ri, di, date1, date2, rc = 0, dc = 0, eo = 0;


            ri = 2.710121572969038991;
            di = 0.1729371367218230438;
            date1 = 2456165.5;
            date2 = 0.401182685;

            WWA.wwaAtic13(ri, di, date1, date2, ref rc, ref dc, ref eo);

            //vvd(rc, 2.710126504531374930, 1e-12, "wwaAtic13", "rc", ref status);
            //vvd(dc, 0.1740632537628342320, 1e-12, "wwaAtic13", "dc", ref status);
            vvd(rc, 2.710126504531716819, 1e-12, "wwaAtic13", "rc", ref status);
            vvd(dc, 0.1740632537627034482, 1e-12, "wwaAtic13", "dc", ref status);
            vvd(eo, -0.002900618712657375647, 1e-14, "wwaAtic13", "eo", ref status);

        }

        static void t_aticq(ref int status)
        /*
        **  - - - - - - - -
        **   t _ a t i c q
        **  - - - - - - - -
        **
        **  Test wwaAticq function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaApci13, wwaAticq, vvd
        **
        **  This revision:  2013 October 3
        */
        {
            double date1, date2, eo = 0, ri, di, rc = 0, dc = 0;
            WWA.wwaASTROM astrom = new WWA.wwaASTROM();


            date1 = 2456165.5;
            date2 = 0.401182685;
            WWA.wwaApci13(date1, date2, ref astrom, ref eo);
            ri = 2.710121572969038991;
            di = 0.1729371367218230438;

            WWA.wwaAticq(ri, di, ref astrom, ref rc, ref dc);

            //vvd(rc, 2.710126504531374930, 1e-12, "wwaAticq", "rc", ref status);
            //vvd(dc, 0.1740632537628342320, 1e-12, "wwaAticq", "dc", ref status);
            vvd(rc, 2.710126504531716819, 1e-12, "wwaAticq", "rc", ref status);
            vvd(dc, 0.1740632537627034482, 1e-12, "wwaAticq", "dc", ref status);

        }

        static void t_aticqn(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ a t i c q n
        **  - - - - - - - - -
        **
        **  Test wwaAticqn function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaApci13, wwaAticqn, vvd
        **
        **  This revision:  2013 October 3
        */
        {
            double date1, date2, eo = 0, ri, di, rc = 0, dc = 0;
            WWA.wwaLDBODY[] b = new WWA.wwaLDBODY[3];
            WWA.wwaASTROM astrom = new WWA.wwaASTROM();

            date1 = 2456165.5;
            date2 = 0.401182685;
            WWA.wwaApci13(date1, date2, ref astrom, ref eo);
            ri = 2.709994899247599271;
            di = 0.1728740720983623469;
            b[0].bm = 0.00028574;
            b[0].dl = 3e-10;

            b[0].pv = new double[3, 3];
            b[0].pv[0, 0] = -7.81014427;
            b[0].pv[0, 1] = -5.60956681;
            b[0].pv[0, 2] = -1.98079819;
            b[0].pv[1, 0] = 0.0030723249;
            b[0].pv[1, 1] = -0.00406995477;
            b[0].pv[1, 2] = -0.00181335842;
            b[1].bm = 0.00095435;
            b[1].dl = 3e-9;

            b[1].pv = new double[3, 3];
            b[1].pv[0, 0] = 0.738098796;
            b[1].pv[0, 1] = 4.63658692;
            b[1].pv[0, 2] = 1.9693136;
            b[1].pv[1, 0] = -0.00755816922;
            b[1].pv[1, 1] = 0.00126913722;
            b[1].pv[1, 2] = 0.000727999001;
            b[2].bm = 1.0;
            b[2].dl = 6e-6;

            b[2].pv = new double[3, 3];
            b[2].pv[0, 0] = -0.000712174377;
            b[2].pv[0, 1] = -0.00230478303;
            b[2].pv[0, 2] = -0.00105865966;
            b[2].pv[1, 0] = 6.29235213e-6;
            b[2].pv[1, 1] = -3.30888387e-7;
            b[2].pv[1, 2] = -2.96486623e-7;

            WWA.wwaAticqn(ri, di, ref astrom, 3, b, ref rc, ref dc);

            //vvd(rc, 2.709999575032685412, 1e-12, "wwaAtciqn", "rc", ref status);
            //vvd(dc, 0.1739999656317778034, 1e-12, "wwaAtciqn", "dc", ref status);
            vvd(rc, 2.709999575033027333, 1e-12, "wwaAtciqn", "rc", ref status);
            vvd(dc, 0.1739999656316469990, 1e-12, "wwaAtciqn", "dc", ref status);
        }

        static void t_atio13(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ a t i o 1 3
        **  - - - - - - - - -
        **
        **  Test wwaAtio13 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaAtio13, vvd, viv
        **
        **  This revision:  2013 October 3
        */
        {
            double ri, di, utc1, utc2, dut1, elong, phi, hm, xp, yp,
                   phpa, tc, rh, wl, aob = 0, zob = 0, hob = 0, dob = 0, rob = 0;
            int j;


            ri = 2.710121572969038991;
            di = 0.1729371367218230438;
            utc1 = 2456384.5;
            utc2 = 0.969254051;
            dut1 = 0.1550675;
            elong = -0.527800806;
            phi = -1.2345856;
            hm = 2738.0;
            xp = 2.47230737e-7;
            yp = 1.82640464e-6;
            phpa = 731.0;
            tc = 12.8;
            rh = 0.59;
            wl = 0.55;

            j = WWA.wwaAtio13(ri, di, utc1, utc2, dut1, elong, phi, hm,
                          xp, yp, phpa, tc, rh, wl,
                          ref aob, ref zob, ref hob, ref dob, ref rob);

            vvd(aob, 0.09233952224794989993, 1e-12, "wwaAtio13", "aob", ref status);
            vvd(zob, 1.407758704513722461, 1e-12, "wwaAtio13", "zob", ref status);
            vvd(hob, -0.09247619879782006106, 1e-12, "wwaAtio13", "hob", ref status);
            vvd(dob, 0.1717653435758265198, 1e-12, "wwaAtio13", "dob", ref status);
            vvd(rob, 2.710085107986886201, 1e-12, "wwaAtio13", "rob", ref status);
            viv(j, 0, "wwaAtio13", "j", ref status);
        }

        static void t_atioq(ref int status)
        /*
        **  - - - - - - - -
        **   t _ a t i o q
        **  - - - - - - - -
        **
        **  Test wwaAtioq function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaApio13, wwaAtioq, vvd, viv
        **
        **  This revision:  2013 October 4
        */
        {
            double utc1, utc2, dut1, elong, phi, hm, xp, yp,
                   phpa, tc, rh, wl, ri, di, aob = 0, zob = 0, hob = 0, dob = 0, rob = 0;
            WWA.wwaASTROM astrom = new WWA.wwaASTROM();


            utc1 = 2456384.5;
            utc2 = 0.969254051;
            dut1 = 0.1550675;
            elong = -0.527800806;
            phi = -1.2345856;
            hm = 2738.0;
            xp = 2.47230737e-7;
            yp = 1.82640464e-6;
            phpa = 731.0;
            tc = 12.8;
            rh = 0.59;
            wl = 0.55;
            WWA.wwaApio13(utc1, utc2, dut1, elong, phi, hm, xp, yp, phpa, tc, rh, wl, ref astrom);
            ri = 2.710121572969038991;
            di = 0.1729371367218230438;

            WWA.wwaAtioq(ri, di, ref astrom, ref aob, ref zob, ref hob, ref dob, ref rob);

            vvd(aob, 0.09233952224794989993, 1e-12, "wwaAtioq", "aob", ref status);
            vvd(zob, 1.407758704513722461, 1e-12, "wwaAtioq", "zob", ref status);
            vvd(hob, -0.09247619879782006106, 1e-12, "wwaAtioq", "hob", ref status);
            vvd(dob, 0.1717653435758265198, 1e-12, "wwaAtioq", "dob", ref status);
            vvd(rob, 2.710085107986886201, 1e-12, "wwaAtioq", "rob", ref status);

        }

        static void t_atoc13(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ a t o c 1 3
        **  - - - - - - - - -
        **
        **  Test wwaAtoc13 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaAtoc13, vvd, viv
        **
        **  This revision:  2013 October 3
        */
        {
            double utc1, utc2, dut1,
                   elong, phi, hm, xp, yp, phpa, tc, rh, wl,
                   ob1, ob2, rc = 0, dc = 0;
            int j;


            utc1 = 2456384.5;
            utc2 = 0.969254051;
            dut1 = 0.1550675;
            elong = -0.527800806;
            phi = -1.2345856;
            hm = 2738.0;
            xp = 2.47230737e-7;
            yp = 1.82640464e-6;
            phpa = 731.0;
            tc = 12.8;
            rh = 0.59;
            wl = 0.55;

            ob1 = 2.710085107986886201;
            ob2 = 0.1717653435758265198;
            char refR = 'R';
            j = WWA.wwaAtoc13(ref refR, ob1, ob2, utc1, utc2, dut1,
                            elong, phi, hm, xp, yp, phpa, tc, rh, wl,
                            ref rc, ref dc);
            //vvd(rc, 2.709956744661000609, 1e-12, "wwaAtoc13", "R/rc", ref status);
            //vvd(dc, 0.1741696500895398562, 1e-12, "wwaAtoc13", "R/dc", ref status);
            vvd(rc, 2.709956744660731630, 1e-12, "wwaAtoc13", "R/rc", ref status);
            vvd(dc, 0.1741696500896438967, 1e-12, "wwaAtoc13", "R/dc", ref status);
            viv(j, 0, "wwaAtoc13", "R/j", ref status);

            ob1 = -0.09247619879782006106;
            ob2 = 0.1717653435758265198;
            char refH = 'H';
            j = WWA.wwaAtoc13(ref refH, ob1, ob2, utc1, utc2, dut1,
                            elong, phi, hm, xp, yp, phpa, tc, rh, wl,
                            ref rc, ref dc);
            //vvd(rc, 2.709956744661000609, 1e-12, "wwaAtoc13", "H/rc", ref status);
            //vvd(dc, 0.1741696500895398562, 1e-12, "wwaAtoc13", "H/dc", ref status);
            vvd(rc, 2.709956744660731630, 1e-12, "wwaAtoc13", "H/rc", ref status);
            vvd(dc, 0.1741696500896438967, 1e-12, "wwaAtoc13", "H/dc", ref status);
            viv(j, 0, "wwaAtoc13", "H/j", ref status);

            ob1 = 0.09233952224794989993;
            ob2 = 1.407758704513722461;
            char refA = 'A';
            j = WWA.wwaAtoc13(ref refA, ob1, ob2, utc1, utc2, dut1,
                            elong, phi, hm, xp, yp, phpa, tc, rh, wl,
                            ref rc, ref dc);
            //vvd(rc, 2.709956744661000609, 1e-12, "wwaAtoc13", "A/rc", ref status);
            //vvd(dc, 0.1741696500895398565, 1e-12, "wwaAtoc13", "A/dc", ref status);
            vvd(rc, 2.709956744660731630, 1e-12, "wwaAtoc13", "A/rc", ref status);
            vvd(dc, 0.1741696500896438970, 1e-12, "wwaAtoc13", "A/dc", ref status);
            viv(j, 0, "wwaAtoc13", "A/j", ref status);

        }

        static void t_atoi13(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ a t o i 1 3
        **  - - - - - - - - -
        **
        **  Test wwaAtoi13 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaAtoi13, vvd, viv
        **
        **  This revision:  2013 October 3
        */
        {
            double utc1, utc2, dut1, elong, phi, hm, xp, yp, phpa, tc, rh, wl,
                   ob1, ob2, ri = 0, di = 0;
            int j;


            utc1 = 2456384.5;
            utc2 = 0.969254051;
            dut1 = 0.1550675;
            elong = -0.527800806;
            phi = -1.2345856;
            hm = 2738.0;
            xp = 2.47230737e-7;
            yp = 1.82640464e-6;
            phpa = 731.0;
            tc = 12.8;
            rh = 0.59;
            wl = 0.55;

            ob1 = 2.710085107986886201;
            ob2 = 0.1717653435758265198;
            char refR = 'R';
            j = WWA.wwaAtoi13(ref refR, ob1, ob2, utc1, utc2, dut1,
                            elong, phi, hm, xp, yp, phpa, tc, rh, wl,
                            ref ri, ref di);
            vvd(ri, 2.710121574449135955, 1e-12, "wwaAtoi13", "R/ri", ref status);
            vvd(di, 0.1729371839114567725, 1e-12, "wwaAtoi13", "R/di", ref status);
            viv(j, 0, "wwaAtoi13", "R/J", ref status);

            ob1 = -0.09247619879782006106;
            ob2 = 0.1717653435758265198;
            char refH = 'H';
            j = WWA.wwaAtoi13(ref refH, ob1, ob2, utc1, utc2, dut1,
                            elong, phi, hm, xp, yp, phpa, tc, rh, wl,
                            ref ri, ref di);
            vvd(ri, 2.710121574449135955, 1e-12, "wwaAtoi13", "H/ri", ref status);
            vvd(di, 0.1729371839114567725, 1e-12, "wwaAtoi13", "H/di", ref status);
            viv(j, 0, "wwaAtoi13", "H/J", ref status);

            ob1 = 0.09233952224794989993;
            ob2 = 1.407758704513722461;
            char refA = 'A';
            j = WWA.wwaAtoi13(ref refA, ob1, ob2, utc1, utc2, dut1,
                            elong, phi, hm, xp, yp, phpa, tc, rh, wl,
                            ref ri, ref di);
            vvd(ri, 2.710121574449135955, 1e-12, "wwaAtoi13", "A/ri", ref status);
            vvd(di, 0.1729371839114567728, 1e-12, "wwaAtoi13", "A/di", ref status);
            viv(j, 0, "wwaAtoi13", "A/J", ref status);

        }

        static void t_atoiq(ref int status)
        /*
        **  - - - - - - - -
        **   t _ a t o i q
        **  - - - - - - - -
        *
        **  Test wwaAtoiq function.
        *
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        *
        **  Called:  wwaApio13, wwaAtoiq, vvd
        *
        **  This revision:  2013 October 4
        */
        {
            double utc1, utc2, dut1, elong, phi, hm, xp, yp, phpa, tc, rh, wl,
                   ob1, ob2, ri = 0, di = 0;
            WWA.wwaASTROM astrom = new WWA.wwaASTROM();


            utc1 = 2456384.5;
            utc2 = 0.969254051;
            dut1 = 0.1550675;
            elong = -0.527800806;
            phi = -1.2345856;
            hm = 2738.0;
            xp = 2.47230737e-7;
            yp = 1.82640464e-6;
            phpa = 731.0;
            tc = 12.8;
            rh = 0.59;
            wl = 0.55;
            WWA.wwaApio13(utc1, utc2, dut1, elong, phi, hm, xp, yp, phpa, tc, rh, wl, ref astrom);

            ob1 = 2.710085107986886201;
            ob2 = 0.1717653435758265198;
            char refR = 'R';
            WWA.wwaAtoiq(ref refR, ob1, ob2, ref astrom, ref ri, ref di);
            vvd(ri, 2.710121574449135955, 1e-12,
                    "wwaAtoiq", "R/ri", ref status);
            vvd(di, 0.1729371839114567725, 1e-12,
                    "wwaAtoiq", "R/di", ref status);

            ob1 = -0.09247619879782006106;
            ob2 = 0.1717653435758265198;
            char refH = 'H';
            WWA.wwaAtoiq(ref refH, ob1, ob2, ref astrom, ref ri, ref di);
            vvd(ri, 2.710121574449135955, 1e-12,
                    "wwaAtoiq", "H/ri", ref status);
            vvd(di, 0.1729371839114567725, 1e-12,
                    "wwaAtoiq", "H/di", ref status);

            ob1 = 0.09233952224794989993;
            ob2 = 1.407758704513722461;
            char refA = 'A';
            WWA.wwaAtoiq(ref refA, ob1, ob2, ref astrom, ref ri, ref di);
            vvd(ri, 2.710121574449135955, 1e-12,
                    "wwaAtoiq", "A/ri", ref status);
            vvd(di, 0.1729371839114567728, 1e-12,
                    "wwaAtoiq", "A/di", ref status);

        }

        static void t_bi00(ref int status)
        /*
        **  - - - - - - -
        **   t _ b i 0 0
        **  - - - - - - -
        **
        **  Test wwaBi00 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaBi00, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double dpsibi = 0, depsbi = 0, dra = 0;

            WWA.wwaBi00(ref dpsibi, ref depsbi, ref dra);

            vvd(dpsibi, -0.2025309152835086613e-6, 1e-12,
               "wwaBi00", "dpsibi", ref status);
            vvd(depsbi, -0.3306041454222147847e-7, 1e-12,
               "wwaBi00", "depsbi", ref status);
            vvd(dra, -0.7078279744199225506e-7, 1e-12,
               "wwaBi00", "dra", ref status);
        }

        static void t_bp00(ref int status)
        /*
        **  - - - - - - -
        **   t _ b p 0 0
        **  - - - - - - -
        **
        **  Test wwaBp00 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaBp00, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] rb = new double[3, 3];
            double[,] rp = new double[3, 3];
            double[,] rbp = new double[3, 3];

            WWA.wwaBp00(2400000.5, 50123.9999, rb, rp, rbp);

            vvd(rb[0, 0], 0.9999999999999942498, 1e-12,
                "wwaBp00", "rb11", ref status);
            vvd(rb[0, 1], -0.7078279744199196626e-7, 1e-16,
                "wwaBp00", "rb12", ref status);
            vvd(rb[0, 2], 0.8056217146976134152e-7, 1e-16,
                "wwaBp00", "rb13", ref status);
            vvd(rb[1, 0], 0.7078279477857337206e-7, 1e-16,
                "wwaBp00", "rb21", ref status);
            vvd(rb[1, 1], 0.9999999999999969484, 1e-12,
                "wwaBp00", "rb22", ref status);
            vvd(rb[1, 2], 0.3306041454222136517e-7, 1e-16,
                "wwaBp00", "rb23", ref status);
            vvd(rb[2, 0], -0.8056217380986972157e-7, 1e-16,
                "wwaBp00", "rb31", ref status);
            vvd(rb[2, 1], -0.3306040883980552500e-7, 1e-16,
                "wwaBp00", "rb32", ref status);
            vvd(rb[2, 2], 0.9999999999999962084, 1e-12,
                "wwaBp00", "rb33", ref status);

            vvd(rp[0, 0], 0.9999995504864048241, 1e-12,
                "wwaBp00", "rp11", ref status);
            vvd(rp[0, 1], 0.8696113836207084411e-3, 1e-14,
                "wwaBp00", "rp12", ref status);
            vvd(rp[0, 2], 0.3778928813389333402e-3, 1e-14,
                "wwaBp00", "rp13", ref status);
            vvd(rp[1, 0], -0.8696113818227265968e-3, 1e-14,
                "wwaBp00", "rp21", ref status);
            vvd(rp[1, 1], 0.9999996218879365258, 1e-12,
                "wwaBp00", "rp22", ref status);
            vvd(rp[1, 2], -0.1690679263009242066e-6, 1e-14,
                "wwaBp00", "rp23", ref status);
            vvd(rp[2, 0], -0.3778928854764695214e-3, 1e-14,
                "wwaBp00", "rp31", ref status);
            vvd(rp[2, 1], -0.1595521004195286491e-6, 1e-14,
                "wwaBp00", "rp32", ref status);
            vvd(rp[2, 2], 0.9999999285984682756, 1e-12,
                "wwaBp00", "rp33", ref status);

            vvd(rbp[0, 0], 0.9999995505175087260, 1e-12,
                "wwaBp00", "rbp11", ref status);
            vvd(rbp[0, 1], 0.8695405883617884705e-3, 1e-14,
                "wwaBp00", "rbp12", ref status);
            vvd(rbp[0, 2], 0.3779734722239007105e-3, 1e-14,
                "wwaBp00", "rbp13", ref status);
            vvd(rbp[1, 0], -0.8695405990410863719e-3, 1e-14,
                "wwaBp00", "rbp21", ref status);
            vvd(rbp[1, 1], 0.9999996219494925900, 1e-12,
                "wwaBp00", "rbp22", ref status);
            vvd(rbp[1, 2], -0.1360775820404982209e-6, 1e-14,
                "wwaBp00", "rbp23", ref status);
            vvd(rbp[2, 0], -0.3779734476558184991e-3, 1e-14,
                "wwaBp00", "rbp31", ref status);
            vvd(rbp[2, 1], -0.1925857585832024058e-6, 1e-14,
                "wwaBp00", "rbp32", ref status);
            vvd(rbp[2, 2], 0.9999999285680153377, 1e-12,
                "wwaBp00", "rbp33", ref status);
        }

        static void t_bp06(ref int status)
        /*
        **  - - - - - - -
        **   t _ b p 0 6
        **  - - - - - - -
        **
        **  Test wwaBp06 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaBp06, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] rb = new double[3, 3];
            double[,] rp = new double[3, 3];
            double[,] rbp = new double[3, 3];

            WWA.wwaBp06(2400000.5, 50123.9999, rb, rp, rbp);

            vvd(rb[0, 0], 0.9999999999999942497, 1e-12,
                "wwaBp06", "rb11", ref status);
            vvd(rb[0, 1], -0.7078368960971557145e-7, 1e-14,
                "wwaBp06", "rb12", ref status);
            vvd(rb[0, 2], 0.8056213977613185606e-7, 1e-14,
                "wwaBp06", "rb13", ref status);
            vvd(rb[1, 0], 0.7078368694637674333e-7, 1e-14,
                "wwaBp06", "rb21", ref status);
            vvd(rb[1, 1], 0.9999999999999969484, 1e-12,
                "wwaBp06", "rb22", ref status);
            vvd(rb[1, 2], 0.3305943742989134124e-7, 1e-14,
                "wwaBp06", "rb23", ref status);
            vvd(rb[2, 0], -0.8056214211620056792e-7, 1e-14,
                "wwaBp06", "rb31", ref status);
            vvd(rb[2, 1], -0.3305943172740586950e-7, 1e-14,
                "wwaBp06", "rb32", ref status);
            vvd(rb[2, 2], 0.9999999999999962084, 1e-12,
                "wwaBp06", "rb33", ref status);

            vvd(rp[0, 0], 0.9999995504864960278, 1e-12,
                "wwaBp06", "rp11", ref status);
            vvd(rp[0, 1], 0.8696112578855404832e-3, 1e-14,
                "wwaBp06", "rp12", ref status);
            vvd(rp[0, 2], 0.3778929293341390127e-3, 1e-14,
                "wwaBp06", "rp13", ref status);
            vvd(rp[1, 0], -0.8696112560510186244e-3, 1e-14,
                "wwaBp06", "rp21", ref status);
            vvd(rp[1, 1], 0.9999996218880458820, 1e-12,
                "wwaBp06", "rp22", ref status);
            vvd(rp[1, 2], -0.1691646168941896285e-6, 1e-14,
                "wwaBp06", "rp23", ref status);
            vvd(rp[2, 0], -0.3778929335557603418e-3, 1e-14,
                "wwaBp06", "rp31", ref status);
            vvd(rp[2, 1], -0.1594554040786495076e-6, 1e-14,
                "wwaBp06", "rp32", ref status);
            vvd(rp[2, 2], 0.9999999285984501222, 1e-12,
                "wwaBp06", "rp33", ref status);

            vvd(rbp[0, 0], 0.9999995505176007047, 1e-12,
                "wwaBp06", "rbp11", ref status);
            vvd(rbp[0, 1], 0.8695404617348208406e-3, 1e-14,
                "wwaBp06", "rbp12", ref status);
            vvd(rbp[0, 2], 0.3779735201865589104e-3, 1e-14,
                "wwaBp06", "rbp13", ref status);
            vvd(rbp[1, 0], -0.8695404723772031414e-3, 1e-14,
                "wwaBp06", "rbp21", ref status);
            vvd(rbp[1, 1], 0.9999996219496027161, 1e-12,
                "wwaBp06", "rbp22", ref status);
            vvd(rbp[1, 2], -0.1361752497080270143e-6, 1e-14,
                "wwaBp06", "rbp23", ref status);
            vvd(rbp[2, 0], -0.3779734957034089490e-3, 1e-14,
                "wwaBp06", "rbp31", ref status);
            vvd(rbp[2, 1], -0.1924880847894457113e-6, 1e-14,
                "wwaBp06", "rbp32", ref status);
            vvd(rbp[2, 2], 0.9999999285679971958, 1e-12,
                "wwaBp06", "rbp33", ref status);
        }

        static void t_bpn2xy(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ b p n 2 x y
        **  - - - - - - - - -
        **
        **  Test wwaBpn2xy function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaBpn2xy, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] rbpn = new double[3, 3];
            double x = 0, y = 0;

            rbpn[0, 0] = 9.999962358680738e-1;
            rbpn[0, 1] = -2.516417057665452e-3;
            rbpn[0, 2] = -1.093569785342370e-3;

            rbpn[1, 0] = 2.516462370370876e-3;
            rbpn[1, 1] = 9.999968329010883e-1;
            rbpn[1, 2] = 4.006159587358310e-5;

            rbpn[2, 0] = 1.093465510215479e-3;
            rbpn[2, 1] = -4.281337229063151e-5;
            rbpn[2, 2] = 9.999994012499173e-1;

            WWA.wwaBpn2xy(rbpn, ref x, ref y);

            vvd(x, 1.093465510215479e-3, 1e-12, "wwaBpn2xy", "x", ref status);
            vvd(y, -4.281337229063151e-5, 1e-12, "wwaBpn2xy", "y", ref status);

        }

        static void t_c2i00a(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ c 2 i 0 0 a
        **  - - - - - - - - -
        **
        **  Test wwaC2i00a function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaC2i00a, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] rc2i = new double[3, 3];


            WWA.wwaC2i00a(2400000.5, 53736.0, rc2i);

            vvd(rc2i[0, 0], 0.9999998323037165557, 1e-12,
                "wwaC2i00a", "11", ref status);
            vvd(rc2i[0, 1], 0.5581526348992140183e-9, 1e-12,
                "wwaC2i00a", "12", ref status);
            vvd(rc2i[0, 2], -0.5791308477073443415e-3, 1e-12,
                "wwaC2i00a", "13", ref status);

            vvd(rc2i[1, 0], -0.2384266227870752452e-7, 1e-12,
                "wwaC2i00a", "21", ref status);
            vvd(rc2i[1, 1], 0.9999999991917405258, 1e-12,
                "wwaC2i00a", "22", ref status);
            vvd(rc2i[1, 2], -0.4020594955028209745e-4, 1e-12,
                "wwaC2i00a", "23", ref status);

            vvd(rc2i[2, 0], 0.5791308472168152904e-3, 1e-12,
                "wwaC2i00a", "31", ref status);
            vvd(rc2i[2, 1], 0.4020595661591500259e-4, 1e-12,
                "wwaC2i00a", "32", ref status);
            vvd(rc2i[2, 2], 0.9999998314954572304, 1e-12,
                "wwaC2i00a", "33", ref status);

        }

        static void t_c2i00b(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ c 2 i 0 0 b
        **  - - - - - - - - -
        **
        **  Test wwaC2i00b function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaC2i00b, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] rc2i = new double[3, 3];


            WWA.wwaC2i00b(2400000.5, 53736.0, rc2i);

            vvd(rc2i[0, 0], 0.9999998323040954356, 1e-12,
                "wwaC2i00b", "11", ref status);
            vvd(rc2i[0, 1], 0.5581526349131823372e-9, 1e-12,
                "wwaC2i00b", "12", ref status);
            vvd(rc2i[0, 2], -0.5791301934855394005e-3, 1e-12,
                "wwaC2i00b", "13", ref status);

            vvd(rc2i[1, 0], -0.2384239285499175543e-7, 1e-12,
                "wwaC2i00b", "21", ref status);
            vvd(rc2i[1, 1], 0.9999999991917574043, 1e-12,
                "wwaC2i00b", "22", ref status);
            vvd(rc2i[1, 2], -0.4020552974819030066e-4, 1e-12,
                "wwaC2i00b", "23", ref status);

            vvd(rc2i[2, 0], 0.5791301929950208873e-3, 1e-12,
                "wwaC2i00b", "31", ref status);
            vvd(rc2i[2, 1], 0.4020553681373720832e-4, 1e-12,
                "wwaC2i00b", "32", ref status);
            vvd(rc2i[2, 2], 0.9999998314958529887, 1e-12,
                "wwaC2i00b", "33", ref status);

        }

        static void t_c2i06a(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ c 2 i 0 6 a
        **  - - - - - - - - -
        **
        **  Test wwaC2i06a function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaC2i06a, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] rc2i = new double[3, 3];


            WWA.wwaC2i06a(2400000.5, 53736.0, rc2i);

            vvd(rc2i[0, 0], 0.9999998323037159379, 1e-12,
                "wwaC2i06a", "11", ref status);
            vvd(rc2i[0, 1], 0.5581121329587613787e-9, 1e-12,
                "wwaC2i06a", "12", ref status);
            vvd(rc2i[0, 2], -0.5791308487740529749e-3, 1e-12,
                "wwaC2i06a", "13", ref status);

            vvd(rc2i[1, 0], -0.2384253169452306581e-7, 1e-12,
                "wwaC2i06a", "21", ref status);
            vvd(rc2i[1, 1], 0.9999999991917467827, 1e-12,
                "wwaC2i06a", "22", ref status);
            vvd(rc2i[1, 2], -0.4020579392895682558e-4, 1e-12,
                "wwaC2i06a", "23", ref status);

            vvd(rc2i[2, 0], 0.5791308482835292617e-3, 1e-12,
                "wwaC2i06a", "31", ref status);
            vvd(rc2i[2, 1], 0.4020580099454020310e-4, 1e-12,
                "wwaC2i06a", "32", ref status);
            vvd(rc2i[2, 2], 0.9999998314954628695, 1e-12,
                "wwaC2i06a", "33", ref status);

        }

        static void t_c2ibpn(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ c 2 i b p n
        **  - - - - - - - - -
        **
        **  Test wwaC2ibpn function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaC2ibpn, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] rbpn = new double[3, 3];
            double[,] rc2i = new double[3, 3];

            rbpn[0, 0] = 9.999962358680738e-1;
            rbpn[0, 1] = -2.516417057665452e-3;
            rbpn[0, 2] = -1.093569785342370e-3;

            rbpn[1, 0] = 2.516462370370876e-3;
            rbpn[1, 1] = 9.999968329010883e-1;
            rbpn[1, 2] = 4.006159587358310e-5;

            rbpn[2, 0] = 1.093465510215479e-3;
            rbpn[2, 1] = -4.281337229063151e-5;
            rbpn[2, 2] = 9.999994012499173e-1;

            WWA.wwaC2ibpn(2400000.5, 50123.9999, rbpn, rc2i);

            vvd(rc2i[0, 0], 0.9999994021664089977, 1e-12,
                "wwaC2ibpn", "11", ref status);
            vvd(rc2i[0, 1], -0.3869195948017503664e-8, 1e-12,
                "wwaC2ibpn", "12", ref status);
            vvd(rc2i[0, 2], -0.1093465511383285076e-2, 1e-12,
                "wwaC2ibpn", "13", ref status);

            vvd(rc2i[1, 0], 0.5068413965715446111e-7, 1e-12,
                "wwaC2ibpn", "21", ref status);
            vvd(rc2i[1, 1], 0.9999999990835075686, 1e-12,
                "wwaC2ibpn", "22", ref status);
            vvd(rc2i[1, 2], 0.4281334246452708915e-4, 1e-12,
                "wwaC2ibpn", "23", ref status);

            vvd(rc2i[2, 0], 0.1093465510215479000e-2, 1e-12,
                "wwaC2ibpn", "31", ref status);
            vvd(rc2i[2, 1], -0.4281337229063151000e-4, 1e-12,
                "wwaC2ibpn", "32", ref status);
            vvd(rc2i[2, 2], 0.9999994012499173103, 1e-12,
                "wwaC2ibpn", "33", ref status);

        }

        static void t_c2ixy(ref int status)
        /*
        **  - - - - - - - -
        **   t _ c 2 i x y
        **  - - - - - - - -
        **
        **  Test wwaC2ixy function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaC2ixy, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double x, y;
            double[,] rc2i = new double[3, 3];


            x = 0.5791308486706011000e-3;
            y = 0.4020579816732961219e-4;

            WWA.wwaC2ixy(2400000.5, 53736, x, y, rc2i);

            vvd(rc2i[0, 0], 0.9999998323037157138, 1e-12,
                "wwaC2ixy", "11", ref status);
            vvd(rc2i[0, 1], 0.5581526349032241205e-9, 1e-12,
                "wwaC2ixy", "12", ref status);
            vvd(rc2i[0, 2], -0.5791308491611263745e-3, 1e-12,
                "wwaC2ixy", "13", ref status);

            vvd(rc2i[1, 0], -0.2384257057469842953e-7, 1e-12,
                "wwaC2ixy", "21", ref status);
            vvd(rc2i[1, 1], 0.9999999991917468964, 1e-12,
                "wwaC2ixy", "22", ref status);
            vvd(rc2i[1, 2], -0.4020579110172324363e-4, 1e-12,
                "wwaC2ixy", "23", ref status);

            vvd(rc2i[2, 0], 0.5791308486706011000e-3, 1e-12,
                "wwaC2ixy", "31", ref status);
            vvd(rc2i[2, 1], 0.4020579816732961219e-4, 1e-12,
                "wwaC2ixy", "32", ref status);
            vvd(rc2i[2, 2], 0.9999998314954627590, 1e-12,
                "wwaC2ixy", "33", ref status);

        }

        static void t_c2ixys(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ c 2 i x y s
        **  - - - - - - - - -
        **
        **  Test wwaC2ixys function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaC2ixys, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double x, y, s;
            double[,] rc2i = new double[3, 3];

            x = 0.5791308486706011000e-3;
            y = 0.4020579816732961219e-4;
            s = -0.1220040848472271978e-7;

            WWA.wwaC2ixys(x, y, s, rc2i);

            vvd(rc2i[0, 0], 0.9999998323037157138, 1e-12,
                "wwaC2ixys", "11", ref status);
            vvd(rc2i[0, 1], 0.5581984869168499149e-9, 1e-12,
                "wwaC2ixys", "12", ref status);
            vvd(rc2i[0, 2], -0.5791308491611282180e-3, 1e-12,
                "wwaC2ixys", "13", ref status);

            vvd(rc2i[1, 0], -0.2384261642670440317e-7, 1e-12,
                "wwaC2ixys", "21", ref status);
            vvd(rc2i[1, 1], 0.9999999991917468964, 1e-12,
                "wwaC2ixys", "22", ref status);
            vvd(rc2i[1, 2], -0.4020579110169668931e-4, 1e-12,
                "wwaC2ixys", "23", ref status);

            vvd(rc2i[2, 0], 0.5791308486706011000e-3, 1e-12,
                "wwaC2ixys", "31", ref status);
            vvd(rc2i[2, 1], 0.4020579816732961219e-4, 1e-12,
                "wwaC2ixys", "32", ref status);
            vvd(rc2i[2, 2], 0.9999998314954627590, 1e-12,
                "wwaC2ixys", "33", ref status);

        }

        static void t_c2s(ref int status)
        /*
        **  - - - - - -
        **   t _ c 2 s
        **  - - - - - -
        **
        **  Test wwaC2s function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaC2s, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[] p = new double[3];
            double theta = 0, phi = 0;


            p[0] = 100.0;
            p[1] = -50.0;
            p[2] = 25.0;

            WWA.wwaC2s(p, ref theta, ref phi);

            vvd(theta, -0.4636476090008061162, 1e-14, "wwaC2s", "theta", ref status);
            vvd(phi, 0.2199879773954594463, 1e-14, "wwaC2s", "phi", ref status);

        }

        static void t_c2t00a(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ c 2 t 0 0 a
        **  - - - - - - - - -
        **
        **  Test wwaC2t00a function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaC2t00a, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double tta, ttb, uta, utb, xp, yp;
            double[,] rc2t = new double[3, 3];

            tta = 2400000.5;
            uta = 2400000.5;
            ttb = 53736.0;
            utb = 53736.0;
            xp = 2.55060238e-7;
            yp = 1.860359247e-6;

            WWA.wwaC2t00a(tta, ttb, uta, utb, xp, yp, rc2t);

            vvd(rc2t[0, 0], -0.1810332128307182668, 1e-12,
                "wwaC2t00a", "11", ref status);
            vvd(rc2t[0, 1], 0.9834769806938457836, 1e-12,
                "wwaC2t00a", "12", ref status);
            vvd(rc2t[0, 2], 0.6555535638688341725e-4, 1e-12,
                "wwaC2t00a", "13", ref status);

            vvd(rc2t[1, 0], -0.9834768134135984552, 1e-12,
                "wwaC2t00a", "21", ref status);
            vvd(rc2t[1, 1], -0.1810332203649520727, 1e-12,
                "wwaC2t00a", "22", ref status);
            vvd(rc2t[1, 2], 0.5749801116141056317e-3, 1e-12,
                "wwaC2t00a", "23", ref status);

            vvd(rc2t[2, 0], 0.5773474014081406921e-3, 1e-12,
                "wwaC2t00a", "31", ref status);
            vvd(rc2t[2, 1], 0.3961832391770163647e-4, 1e-12,
                "wwaC2t00a", "32", ref status);
            vvd(rc2t[2, 2], 0.9999998325501692289, 1e-12,
                "wwaC2t00a", "33", ref status);

        }

        static void t_c2t00b(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ c 2 t 0 0 b
        **  - - - - - - - - -
        **
        **  Test wwaC2t00b function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaC2t00b, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double tta, ttb, uta, utb, xp, yp;
            double[,] rc2t = new double[3, 3];

            tta = 2400000.5;
            uta = 2400000.5;
            ttb = 53736.0;
            utb = 53736.0;
            xp = 2.55060238e-7;
            yp = 1.860359247e-6;

            WWA.wwaC2t00b(tta, ttb, uta, utb, xp, yp, rc2t);

            vvd(rc2t[0, 0], -0.1810332128439678965, 1e-12,
                "wwaC2t00b", "11", ref status);
            vvd(rc2t[0, 1], 0.9834769806913872359, 1e-12,
                "wwaC2t00b", "12", ref status);
            vvd(rc2t[0, 2], 0.6555565082458415611e-4, 1e-12,
                "wwaC2t00b", "13", ref status);

            vvd(rc2t[1, 0], -0.9834768134115435923, 1e-12,
                "wwaC2t00b", "21", ref status);
            vvd(rc2t[1, 1], -0.1810332203784001946, 1e-12,
                "wwaC2t00b", "22", ref status);
            vvd(rc2t[1, 2], 0.5749793922030017230e-3, 1e-12,
                "wwaC2t00b", "23", ref status);

            vvd(rc2t[2, 0], 0.5773467471863534901e-3, 1e-12,
                "wwaC2t00b", "31", ref status);
            vvd(rc2t[2, 1], 0.3961790411549945020e-4, 1e-12,
                "wwaC2t00b", "32", ref status);
            vvd(rc2t[2, 2], 0.9999998325505635738, 1e-12,
                "wwaC2t00b", "33", ref status);

        }

        static void t_c2t06a(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ c 2 t 0 6 a
        **  - - - - - - - - -
        **
        **  Test wwaC2t06a function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaC2t06a, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double tta, ttb, uta, utb, xp, yp;
            double[,] rc2t = new double[3, 3];


            tta = 2400000.5;
            uta = 2400000.5;
            ttb = 53736.0;
            utb = 53736.0;
            xp = 2.55060238e-7;
            yp = 1.860359247e-6;

            WWA.wwaC2t06a(tta, ttb, uta, utb, xp, yp, rc2t);

            vvd(rc2t[0, 0], -0.1810332128305897282, 1e-12,
                "wwaC2t06a", "11", ref status);
            vvd(rc2t[0, 1], 0.9834769806938592296, 1e-12,
                "wwaC2t06a", "12", ref status);
            vvd(rc2t[0, 2], 0.6555550962998436505e-4, 1e-12,
                "wwaC2t06a", "13", ref status);

            vvd(rc2t[1, 0], -0.9834768134136214897, 1e-12,
                "wwaC2t06a", "21", ref status);
            vvd(rc2t[1, 1], -0.1810332203649130832, 1e-12,
                "wwaC2t06a", "22", ref status);
            vvd(rc2t[1, 2], 0.5749800844905594110e-3, 1e-12,
                "wwaC2t06a", "23", ref status);

            vvd(rc2t[2, 0], 0.5773474024748545878e-3, 1e-12,
                "wwaC2t06a", "31", ref status);
            vvd(rc2t[2, 1], 0.3961816829632690581e-4, 1e-12,
                "wwaC2t06a", "32", ref status);
            vvd(rc2t[2, 2], 0.9999998325501747785, 1e-12,
                "wwaC2t06a", "33", ref status);

        }

        static void t_c2tcio(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ c 2 t c i o
        **  - - - - - - - - -
        **
        **  Test wwaC2tcio function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaC2tcio, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] rc2i = new double[3, 3];
            double era;
            double[,] rpom = new double[3, 3];
            double[,] rc2t = new double[3, 3];


            rc2i[0, 0] = 0.9999998323037164738;
            rc2i[0, 1] = 0.5581526271714303683e-9;
            rc2i[0, 2] = -0.5791308477073443903e-3;

            rc2i[1, 0] = -0.2384266227524722273e-7;
            rc2i[1, 1] = 0.9999999991917404296;
            rc2i[1, 2] = -0.4020594955030704125e-4;

            rc2i[2, 0] = 0.5791308472168153320e-3;
            rc2i[2, 1] = 0.4020595661593994396e-4;
            rc2i[2, 2] = 0.9999998314954572365;

            era = 1.75283325530307;

            rpom[0, 0] = 0.9999999999999674705;
            rpom[0, 1] = -0.1367174580728847031e-10;
            rpom[0, 2] = 0.2550602379999972723e-6;

            rpom[1, 0] = 0.1414624947957029721e-10;
            rpom[1, 1] = 0.9999999999982694954;
            rpom[1, 2] = -0.1860359246998866338e-5;

            rpom[2, 0] = -0.2550602379741215275e-6;
            rpom[2, 1] = 0.1860359247002413923e-5;
            rpom[2, 2] = 0.9999999999982369658;


            WWA.wwaC2tcio(rc2i, era, rpom, rc2t);

            vvd(rc2t[0, 0], -0.1810332128307110439, 1e-12,
                "wwaC2tcio", "11", ref status);
            vvd(rc2t[0, 1], 0.9834769806938470149, 1e-12,
                "wwaC2tcio", "12", ref status);
            vvd(rc2t[0, 2], 0.6555535638685466874e-4, 1e-12,
                "wwaC2tcio", "13", ref status);

            vvd(rc2t[1, 0], -0.9834768134135996657, 1e-12,
                "wwaC2tcio", "21", ref status);
            vvd(rc2t[1, 1], -0.1810332203649448367, 1e-12,
                "wwaC2tcio", "22", ref status);
            vvd(rc2t[1, 2], 0.5749801116141106528e-3, 1e-12,
                "wwaC2tcio", "23", ref status);

            vvd(rc2t[2, 0], 0.5773474014081407076e-3, 1e-12,
                "wwaC2tcio", "31", ref status);
            vvd(rc2t[2, 1], 0.3961832391772658944e-4, 1e-12,
                "wwaC2tcio", "32", ref status);
            vvd(rc2t[2, 2], 0.9999998325501691969, 1e-12,
                "wwaC2tcio", "33", ref status);

        }

        static void t_c2teqx(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ c 2 t e q x
        **  - - - - - - - - -
        **
        **  Test wwaC2teqx function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaC2teqx, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] rbpn = new double[3, 3];
            double gst;
            double[,] rpom = new double[3, 3];
            double[,] rc2t = new double[3, 3];


            rbpn[0, 0] = 0.9999989440476103608;
            rbpn[0, 1] = -0.1332881761240011518e-2;
            rbpn[0, 2] = -0.5790767434730085097e-3;

            rbpn[1, 0] = 0.1332858254308954453e-2;
            rbpn[1, 1] = 0.9999991109044505944;
            rbpn[1, 2] = -0.4097782710401555759e-4;

            rbpn[2, 0] = 0.5791308472168153320e-3;
            rbpn[2, 1] = 0.4020595661593994396e-4;
            rbpn[2, 2] = 0.9999998314954572365;

            gst = 1.754166138040730516;

            rpom[0, 0] = 0.9999999999999674705;
            rpom[0, 1] = -0.1367174580728847031e-10;
            rpom[0, 2] = 0.2550602379999972723e-6;

            rpom[1, 0] = 0.1414624947957029721e-10;
            rpom[1, 1] = 0.9999999999982694954;
            rpom[1, 2] = -0.1860359246998866338e-5;

            rpom[2, 0] = -0.2550602379741215275e-6;
            rpom[2, 1] = 0.1860359247002413923e-5;
            rpom[2, 2] = 0.9999999999982369658;

            WWA.wwaC2teqx(rbpn, gst, rpom, rc2t);

            vvd(rc2t[0, 0], -0.1810332128528685730, 1e-12,
                "wwaC2teqx", "11", ref status);
            vvd(rc2t[0, 1], 0.9834769806897685071, 1e-12,
                "wwaC2teqx", "12", ref status);
            vvd(rc2t[0, 2], 0.6555535639982634449e-4, 1e-12,
                "wwaC2teqx", "13", ref status);

            vvd(rc2t[1, 0], -0.9834768134095211257, 1e-12,
                "wwaC2teqx", "21", ref status);
            vvd(rc2t[1, 1], -0.1810332203871023800, 1e-12,
                "wwaC2teqx", "22", ref status);
            vvd(rc2t[1, 2], 0.5749801116126438962e-3, 1e-12,
                "wwaC2teqx", "23", ref status);

            vvd(rc2t[2, 0], 0.5773474014081539467e-3, 1e-12,
                "wwaC2teqx", "31", ref status);
            vvd(rc2t[2, 1], 0.3961832391768640871e-4, 1e-12,
                "wwaC2teqx", "32", ref status);
            vvd(rc2t[2, 2], 0.9999998325501691969, 1e-12,
                "wwaC2teqx", "33", ref status);

        }

        static void t_c2tpe(ref int status)
        /*
        **  - - - - - - - -
        **   t _ c 2 t p e
        **  - - - - - - - -
        **
        **  Test wwaC2tpe function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaC2tpe, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double tta, ttb, uta, utb, dpsi, deps, xp, yp;
            double[,] rc2t = new double[3, 3];

            tta = 2400000.5;
            uta = 2400000.5;
            ttb = 53736.0;
            utb = 53736.0;
            deps = 0.4090789763356509900;
            dpsi = -0.9630909107115582393e-5;
            xp = 2.55060238e-7;
            yp = 1.860359247e-6;

            WWA.wwaC2tpe(tta, ttb, uta, utb, dpsi, deps, xp, yp, rc2t);

            vvd(rc2t[0, 0], -0.1813677995763029394, 1e-12,
                "wwaC2tpe", "11", ref status);
            vvd(rc2t[0, 1], 0.9023482206891683275, 1e-12,
                "wwaC2tpe", "12", ref status);
            vvd(rc2t[0, 2], -0.3909902938641085751, 1e-12,
                "wwaC2tpe", "13", ref status);

            vvd(rc2t[1, 0], -0.9834147641476804807, 1e-12,
                "wwaC2tpe", "21", ref status);
            vvd(rc2t[1, 1], -0.1659883635434995121, 1e-12,
                "wwaC2tpe", "22", ref status);
            vvd(rc2t[1, 2], 0.7309763898042819705e-1, 1e-12,
                "wwaC2tpe", "23", ref status);

            vvd(rc2t[2, 0], 0.1059685430673215247e-2, 1e-12,
                "wwaC2tpe", "31", ref status);
            vvd(rc2t[2, 1], 0.3977631855605078674, 1e-12,
                "wwaC2tpe", "32", ref status);
            vvd(rc2t[2, 2], 0.9174875068792735362, 1e-12,
                "wwaC2tpe", "33", ref status);

        }

        static void t_c2txy(ref int status)
        /*
        **  - - - - - - - -
        **   t _ c 2 t x y
        **  - - - - - - - -
        **
        **  Test wwaC2txy function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaC2txy, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double tta, ttb, uta, utb, x, y, xp, yp;
            double[,] rc2t = new double[3, 3];


            tta = 2400000.5;
            uta = 2400000.5;
            ttb = 53736.0;
            utb = 53736.0;
            x = 0.5791308486706011000e-3;
            y = 0.4020579816732961219e-4;
            xp = 2.55060238e-7;
            yp = 1.860359247e-6;

            WWA.wwaC2txy(tta, ttb, uta, utb, x, y, xp, yp, rc2t);

            vvd(rc2t[0, 0], -0.1810332128306279253, 1e-12,
                "wwaC2txy", "11", ref status);
            vvd(rc2t[0, 1], 0.9834769806938520084, 1e-12,
                "wwaC2txy", "12", ref status);
            vvd(rc2t[0, 2], 0.6555551248057665829e-4, 1e-12,
                "wwaC2txy", "13", ref status);

            vvd(rc2t[1, 0], -0.9834768134136142314, 1e-12,
                "wwaC2txy", "21", ref status);
            vvd(rc2t[1, 1], -0.1810332203649529312, 1e-12,
                "wwaC2txy", "22", ref status);
            vvd(rc2t[1, 2], 0.5749800843594139912e-3, 1e-12,
                "wwaC2txy", "23", ref status);

            vvd(rc2t[2, 0], 0.5773474028619264494e-3, 1e-12,
                "wwaC2txy", "31", ref status);
            vvd(rc2t[2, 1], 0.3961816546911624260e-4, 1e-12,
                "wwaC2txy", "32", ref status);
            vvd(rc2t[2, 2], 0.9999998325501746670, 1e-12,
                "wwaC2txy", "33", ref status);

        }

        static void t_cal2jd(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ c a l 2 j d
        **  - - - - - - - - -
        **
        **  Test wwaCal2jd function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaCal2jd, vvd, viv
        **
        **  This revision:  2013 August 7
        */
        {
            int j;
            double djm0 = 0, djm = 0;


            j = WWA.wwaCal2jd(2003, 06, 01, ref djm0, ref djm);

            vvd(djm0, 2400000.5, 0.0, "wwaCal2jd", "djm0", ref status);
            vvd(djm, 52791.0, 0.0, "wwaCal2jd", "djm", ref status);

            viv(j, 0, "wwaCal2jd", "j", ref status);

        }

        static void t_cp(ref int status)
        /*
        **  - - - - -
        **   t _ c p
        **  - - - - -
        **
        **  Test wwaCp function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaCp, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[] p = new double[3];
            double[] c = new double[3];

            p[0] = 0.3;
            p[1] = 1.2;
            p[2] = -2.5;

            WWA.wwaCp(p, c);

            vvd(c[0], 0.3, 0.0, "wwaCp", "1", ref status);
            vvd(c[1], 1.2, 0.0, "wwaCp", "2", ref status);
            vvd(c[2], -2.5, 0.0, "wwaCp", "3", ref status);
        }

        static void t_cpv(ref int status)
        /*
        **  - - - - - -
        **   t _ c p v
        **  - - - - - -
        **
        **  Test wwaCpv function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaCpv, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] pv = new double[2, 3];
            double[,] c = new double[2, 3];

            pv[0, 0] = 0.3;
            pv[0, 1] = 1.2;
            pv[0, 2] = -2.5;

            pv[1, 0] = -0.5;
            pv[1, 1] = 3.1;
            pv[1, 2] = 0.9;

            WWA.wwaCpv(pv, c);

            vvd(c[0, 0], 0.3, 0.0, "wwaCpv", "p1", ref status);
            vvd(c[0, 1], 1.2, 0.0, "wwaCpv", "p2", ref status);
            vvd(c[0, 2], -2.5, 0.0, "wwaCpv", "p3", ref status);

            vvd(c[1, 0], -0.5, 0.0, "wwaCpv", "v1", ref status);
            vvd(c[1, 1], 3.1, 0.0, "wwaCpv", "v2", ref status);
            vvd(c[1, 2], 0.9, 0.0, "wwaCpv", "v3", ref status);

        }

        static void t_cr(ref int status)
        /*
        **  - - - - -
        **   t _ c r
        **  - - - - -
        **
        **  Test wwaCr function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaCr, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] r = new double[3, 3];
            double[,] c = new double[3, 3];

            r[0, 0] = 2.0;
            r[0, 1] = 3.0;
            r[0, 2] = 2.0;

            r[1, 0] = 3.0;
            r[1, 1] = 2.0;
            r[1, 2] = 3.0;

            r[2, 0] = 3.0;
            r[2, 1] = 4.0;
            r[2, 2] = 5.0;

            WWA.wwaCr(r, c);

            vvd(c[0, 0], 2.0, 0.0, "wwaCr", "11", ref status);
            vvd(c[0, 1], 3.0, 0.0, "wwaCr", "12", ref status);
            vvd(c[0, 2], 2.0, 0.0, "wwaCr", "13", ref status);

            vvd(c[1, 0], 3.0, 0.0, "wwaCr", "21", ref status);
            vvd(c[1, 1], 2.0, 0.0, "wwaCr", "22", ref status);
            vvd(c[1, 2], 3.0, 0.0, "wwaCr", "23", ref status);

            vvd(c[2, 0], 3.0, 0.0, "wwaCr", "31", ref status);
            vvd(c[2, 1], 4.0, 0.0, "wwaCr", "32", ref status);
            vvd(c[2, 2], 5.0, 0.0, "wwaCr", "33", ref status);
        }

        static void t_d2dtf(ref int status)
        /*
        **  - - - - - - - -
        **   t _ d 2 d t f
        **  - - - - - - - -
        **
        **  Test wwaD2dtf function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaD2dtf, viv
        **
        **  This revision:  2013 August 7
        */
        {
            int j, iy = 0, im = 0, id = 0;
            int[] ihmsf = new int[4];

            j = WWA.wwaD2dtf("UTC", 5, 2400000.5, 49533.99999, ref iy, ref im, ref id, ihmsf);

            viv(iy, 1994, "wwaD2dtf", "y", ref status);
            viv(im, 6, "wwaD2dtf", "mo", ref status);
            viv(id, 30, "wwaD2dtf", "d", ref status);
            viv(ihmsf[0], 23, "wwaD2dtf", "h", ref status);
            viv(ihmsf[1], 59, "wwaD2dtf", "m", ref status);
            viv(ihmsf[2], 60, "wwaD2dtf", "s", ref status);
            viv(ihmsf[3], 13599, "wwaD2dtf", "f", ref status);
            viv(j, 0, "wwaD2dtf", "j", ref status);

        }

        static void t_d2tf(ref int status)
        /*
        **  - - - - - - -
        **   t _ d 2 t f
        **  - - - - - - -
        **
        **  Test wwaD2tf function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaD2tf, viv, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            int[] ihmsf = new int[4];
            char s = '+';


            WWA.wwaD2tf(4, -0.987654321, ref s, ihmsf);

            viv((int)s, '-', "wwaD2tf", "s", ref status);

            viv(ihmsf[0], 23, "wwaD2tf", "0", ref status);
            viv(ihmsf[1], 42, "wwaD2tf", "1", ref status);
            viv(ihmsf[2], 13, "wwaD2tf", "2", ref status);
            viv(ihmsf[3], 3333, "wwaD2tf", "3", ref status);

        }

        static void t_dat(ref int status)
        /*
        **  - - - - - -
        **   t _ d a t
        **  - - - - - -
        **
        **  Test wwaDat function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaDat, vvd, viv
        **
        **  This revision: 2016 July 11
        */
        {
            int j;
            double deltat = 0;


            j = WWA.wwaDat(2003, 6, 1, 0.0, ref deltat);

            vvd(deltat, 32.0, 0.0, "wwaDat", "d1", ref status);
            viv(j, 0, "wwaDat", "j1", ref status);

            j = WWA.wwaDat(2008, 1, 17, 0.0, ref deltat);

            vvd(deltat, 33.0, 0.0, "wwaDat", "d2", ref status);
            viv(j, 0, "wwaDat", "j2", ref status);

            //j = WWA.wwaDat(2015, 9, 1, 0.0, ref deltat); // old
            j = WWA.wwaDat(2017, 9, 1, 0.0, ref deltat);

            //vvd(deltat, 36.0, 0.0, "wwaDat", "d3", ref status); // old
            vvd(deltat, 37.0, 0.0, "wwaDat", "d3", ref status);

            viv(j, 0, "wwaDat", "j3", ref status);
        }

        static void t_dtdb(ref int status)
        /*
        **  - - - - - - -
        **   t _ d t d b
        **  - - - - - - -
        **
        **  Test wwaDtdb function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaDtdb, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double dtdb;


            dtdb = WWA.wwaDtdb(2448939.5, 0.123, 0.76543, 5.0123, 5525.242, 3190.0);

            vvd(dtdb, -0.1280368005936998991e-2, 1e-15, "wwaDtdb", "", ref status);

        }

        static void t_dtf2d(ref int status)
        /*
        **  - - - - - - - -
        **   t _ d t f 2 d
        **  - - - - - - - -
        **
        **  Test wwaDtf2d function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaDtf2d, vvd, viv
        **
        **  This revision:  2013 August 7
        */
        {
            double u1 = 0, u2 = 0;
            int j;


            j = WWA.wwaDtf2d("UTC", 1994, 6, 30, 23, 59, 60.13599, ref u1, ref u2);

            vvd(u1 + u2, 2449534.49999, 1e-6, "wwaDtf2d", "u", ref status);
            viv(j, 0, "wwaDtf2d", "j", ref status);

        }

        static void t_eceq06(ref int status)
        /*
        **  - - - - -
        **   t _ e c e q 0 6
        **  - - - - -
        **
        **  Test wwaEceq06 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaEceq06, vvd
        **
        **  This revision:  2016 March 12
        */
        {
            double date1, date2, dl, db, dr = 0, dd = 0;


            date1 = 2456165.5;
            date2 = 0.401182685;
            dl = 5.1;
            db = -0.9;

            WWA.wwaEceq06(date1, date2, dl, db, ref dr, ref dd);

            vvd(dr, 5.533459733613627767, 1e-14, "iauEceq06", "dr", ref status);
            vvd(dd, -1.246542932554480576, 1e-14, "iauEceq06", "dd", ref status);

        }

        static void t_ecm06(ref int status)
        /*
        **  - - - - - - - -
        **   t _ e c m 0 6
        **  - - - - - - - -
        **
        **  Test iauEcm06 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaEcm06, vvd
        **
        **  This revision:  2016 March 12
        */
        {
            double date1, date2;
            double[,] rm = new double[3, 3];


            date1 = 2456165.5;
            date2 = 0.401182685;

            WWA.wwaEcm06(date1, date2, rm);

            vvd(rm[0, 0], 0.9999952427708701137, 1e-14, "iauEcm06", "rm11", ref status);
            vvd(rm[0, 1], -0.2829062057663042347e-2, 1e-14, "iauEcm06", "rm12", ref status);
            vvd(rm[0, 2], -0.1229163741100017629e-2, 1e-14, "iauEcm06", "rm13", ref status);
            vvd(rm[1, 0], 0.3084546876908653562e-2, 1e-14, "iauEcm06", "rm21", ref status);
            vvd(rm[1, 1], 0.9174891871550392514, 1e-14, "iauEcm06", "rm22", ref status);
            vvd(rm[1, 2], 0.3977487611849338124, 1e-14, "iauEcm06", "rm23", ref status);
            vvd(rm[2, 0], 0.2488512951527405928e-5, 1e-14, "iauEcm06", "rm31", ref status);
            vvd(rm[2, 1], -0.3977506604161195467, 1e-14, "iauEcm06", "rm32", ref status);
            vvd(rm[2, 2], 0.9174935488232863071, 1e-14, "iauEcm06", "rm33", ref status);

        }

        static void t_ee00(ref int status)
        /*
        **  - - - - - - -
        **   t _ e e 0 0
        **  - - - - - - -
        **
        **  Test wwaEe00 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaEe00, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double epsa, dpsi, ee;


            epsa = 0.4090789763356509900;
            dpsi = -0.9630909107115582393e-5;

            ee = WWA.wwaEe00(2400000.5, 53736.0, epsa, dpsi);

            vvd(ee, -0.8834193235367965479e-5, 1e-18, "wwaEe00", "", ref status);

        }

        static void t_ee00a(ref int status)
        /*
        **  - - - - - - - -
        **   t _ e e 0 0 a
        **  - - - - - - - -
        **
        **  Test wwaEe00a function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaEe00a, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double ee;

            ee = WWA.wwaEe00a(2400000.5, 53736.0);

            vvd(ee, -0.8834192459222588227e-5, 1e-18, "wwaEe00a", "", ref status);

        }

        static void t_ee00b(ref int status)
        /*
        **  - - - - - - - -
        **   t _ e e 0 0 b
        **  - - - - - - - -
        **
        **  Test wwaEe00b function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaEe00b, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double ee;


            ee = WWA.wwaEe00b(2400000.5, 53736.0);

            vvd(ee, -0.8835700060003032831e-5, 1e-18, "wwaEe00b", "", ref status);

        }

        static void t_ee06a(ref int status)
        /*
        **  - - - - - - - -
        **   t _ e e 0 6 a
        **  - - - - - - - -
        **
        **  Test wwaEe06a function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaEe06a, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double ee;


            ee = WWA.wwaEe06a(2400000.5, 53736.0);

            vvd(ee, -0.8834195072043790156e-5, 1e-15, "wwaEe06a", "", ref status);
        }

        static void t_eect00(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ e e c t 0 0
        **  - - - - - - - - -
        **
        **  Test wwaEect00 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaEect00, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double eect;


            eect = WWA.wwaEect00(2400000.5, 53736.0);

            vvd(eect, 0.2046085004885125264e-8, 1e-20, "wwaEect00", "", ref status);

        }

        static void t_eform(ref int status)
        /*
        **  - - - - - - - -
        **   t _ e f o r m
        **  - - - - - - - -
        **
        **  Test wwaEform function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaEform, viv, vvd
        **
        **  This revision:  2016 March 12
        */
        {
            int j;
            double a = 0, f = 0;

            j = WWA.wwaEform(0, ref a, ref f);

            viv(j, -1, "wwaEform", "j0", ref status);

            j = WWA.wwaEform(WWA.WGS84, ref a, ref f);

            viv(j, 0, "wwaEform", "j1", ref status);
            vvd(a, 6378137.0, 1e-10, "wwaEform", "a1", ref status);
            //vvd(f, 0.0033528106647474807, 1e-18, "wwaEform", "f1", ref status);
            vvd(f, 0.3352810664747480720e-2, 1e-18, "wwaEform", "f1", ref status);

            j = WWA.wwaEform(WWA.GRS80, ref a, ref f);

            viv(j, 0, "wwaEform", "j2", ref status);
            vvd(a, 6378137.0, 1e-10, "wwaEform", "a2", ref status);
            //vvd(f, 0.0033528106811823189, 1e-18, "wwaEform", "f2", ref status);
            vvd(f, 0.3352810681182318935e-2, 1e-18, "wwaEform", "f2", ref status);

            j = WWA.wwaEform(WWA.WGS72, ref a, ref f);

            viv(j, 0, "wwaEform", "j2", ref status);
            vvd(a, 6378135.0, 1e-10, "wwaEform", "a3", ref status);
            //vvd(f, 0.0033527794541675049, 1e-18, "wwaEform", "f3", ref status);
            vvd(f, 0.3352779454167504862e-2, 1e-18, "wwaEform", "f3", ref status);

            j = WWA.wwaEform(4, ref a, ref f);
            viv(j, -1, "wwaEform", "j3", ref status);
        }

        static void t_eo06a(ref int status)
        /*
        **  - - - - - - - -
        **   t _ e o 0 6 a
        **  - - - - - - - -
        **
        **  Test wwaEo06a function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaEo06a, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double eo;


            eo = WWA.wwaEo06a(2400000.5, 53736.0);

            vvd(eo, -0.1332882371941833644e-2, 1e-15, "wwaEo06a", "", ref status);

        }

        static void t_eors(ref int status)
        /*
        **  - - - - - - -
        **   t _ e o r s
        **  - - - - - - -
        **
        **  Test wwaEors function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaEors, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] rnpb = new double[3, 3];
            double s, eo;


            rnpb[0, 0] = 0.9999989440476103608;
            rnpb[0, 1] = -0.1332881761240011518e-2;
            rnpb[0, 2] = -0.5790767434730085097e-3;

            rnpb[1, 0] = 0.1332858254308954453e-2;
            rnpb[1, 1] = 0.9999991109044505944;
            rnpb[1, 2] = -0.4097782710401555759e-4;

            rnpb[2, 0] = 0.5791308472168153320e-3;
            rnpb[2, 1] = 0.4020595661593994396e-4;
            rnpb[2, 2] = 0.9999998314954572365;

            s = -0.1220040848472271978e-7;

            eo = WWA.wwaEors(rnpb, s);

            vvd(eo, -0.1332882715130744606e-2, 1e-14, "wwaEors", "", ref status);

        }

        static void t_epb(ref int status)
        /*
        **  - - - - - -
        **   t _ e p b
        **  - - - - - -
        **
        **  Test wwaEpb function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaEpb, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double epb;


            epb = WWA.wwaEpb(2415019.8135, 30103.18648);

            vvd(epb, 1982.418424159278580, 1e-12, "wwaEpb", "", ref status);

        }

        static void t_epb2jd(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ e p b 2 j d
        **  - - - - - - - - -
        **
        **  Test wwaEpb2jd function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaEpb2jd, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double epb, djm0 = 0, djm = 0;


            epb = 1957.3;

            WWA.wwaEpb2jd(epb, ref djm0, ref djm);

            vvd(djm0, 2400000.5, 1e-9, "wwaEpb2jd", "djm0", ref status);
            vvd(djm, 35948.1915101513, 1e-9, "wwaEpb2jd", "mjd", ref status);

        }

        static void t_epj(ref int status)
        /*
        **  - - - - - -
        **   t _ e p j
        **  - - - - - -
        **
        **  Test wwaEpj function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaEpj, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double epj;


            epj = WWA.wwaEpj(2451545, -7392.5);

            vvd(epj, 1979.760438056125941, 1e-12, "wwaEpj", "", ref status);

        }

        static void t_epj2jd(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ e p j 2 j d
        **  - - - - - - - - -
        **
        **  Test wwaEpj2jd function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaEpj2jd, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double epj, djm0 = 0, djm = 0;


            epj = 1996.8;

            WWA.wwaEpj2jd(epj, ref djm0, ref djm);

            vvd(djm0, 2400000.5, 1e-9, "wwaEpj2jd", "djm0", ref status);
            vvd(djm, 50375.7, 1e-9, "wwaEpj2jd", "mjd", ref status);

        }

        static void t_epv00(ref int status)
        /*
        **  - - - - - - - -
        **   t _ e p v 0 0
        **  - - - - - - - -
        **
        **  Test wwaEpv00 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called: wwaEpv00, vvd, viv
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] pvh = new double[2, 3];
            double[,] pvb = new double[2, 3];
            int j;


            j = WWA.wwaEpv00(2400000.5, 53411.52501161, pvh, pvb);

            vvd(pvh[0, 0], -0.7757238809297706813, 1e-14,
                "wwaEpv00", "ph(x)", ref status);
            vvd(pvh[0, 1], 0.5598052241363340596, 1e-14,
                "wwaEpv00", "ph(y)", ref status);
            vvd(pvh[0, 2], 0.2426998466481686993, 1e-14,
                "wwaEpv00", "ph(z)", ref status);

            vvd(pvh[1, 0], -0.1091891824147313846e-1, 1e-15,
                "wwaEpv00", "vh(x)", ref status);
            vvd(pvh[1, 1], -0.1247187268440845008e-1, 1e-15,
                "wwaEpv00", "vh(y)", ref status);
            vvd(pvh[1, 2], -0.5407569418065039061e-2, 1e-15,
                "wwaEpv00", "vh(z)", ref status);

            vvd(pvb[0, 0], -0.7714104440491111971, 1e-14,
                "wwaEpv00", "pb(x)", ref status);
            vvd(pvb[0, 1], 0.5598412061824171323, 1e-14,
                "wwaEpv00", "pb(y)", ref status);
            vvd(pvb[0, 2], 0.2425996277722452400, 1e-14,
                "wwaEpv00", "pb(z)", ref status);

            vvd(pvb[1, 0], -0.1091874268116823295e-1, 1e-15,
                "wwaEpv00", "vb(x)", ref status);
            vvd(pvb[1, 1], -0.1246525461732861538e-1, 1e-15,
                "wwaEpv00", "vb(y)", ref status);
            vvd(pvb[1, 2], -0.5404773180966231279e-2, 1e-15,
                "wwaEpv00", "vb(z)", ref status);

            viv(j, 0, "wwaEpv00", "j", ref status);

        }

        static void t_eqec06(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ e q e c 0 6
        **  - - - - - - - - -
        **
        **  Test wwaEqec06 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaEqec06, vvd
        **
        **  This revision:  2016 March 12
        */
        {
            double date1, date2, dr, dd, dl = 0, db = 0;

            date1 = 1234.5;
            date2 = 2440000.5;
            dr = 1.234;
            dd = 0.987;

            WWA.wwaEqec06(date1, date2, dr, dd, ref dl, ref db);

            vvd(dl, 1.342509918994654619, 1e-14, "wwaEqec06", "dl", ref status);
            vvd(db, 0.5926215259704608132, 1e-14, "wwaEqec06", "db", ref status);

        }

        static void t_eqeq94(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ e q e q 9 4
        **  - - - - - - - - -
        **
        **  Test wwaEqeq94 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaEqeq94, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double eqeq;


            eqeq = WWA.wwaEqeq94(2400000.5, 41234.0);

            vvd(eqeq, 0.5357758254609256894e-4, 1e-17, "wwaEqeq94", "", ref status);

        }

        static void t_era00(ref int status)
        /*
        **  - - - - - - - -
        **   t _ e r a 0 0
        **  - - - - - - - -
        **
        **  Test wwaEra00 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaEra00, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double era00;


            era00 = WWA.wwaEra00(2400000.5, 54388.0);

            vvd(era00, 0.4022837240028158102, 1e-12, "wwaEra00", "", ref status);

        }

        static void t_fad03(ref int status)
        /*
        **  - - - - - - - -
        **   t _ f a d 0 3
        **  - - - - - - - -
        **
        **  Test wwaFad03 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaFad03, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            vvd(WWA.wwaFad03(0.80), 1.946709205396925672, 1e-12,
               "wwaFad03", "", ref status);
        }

        static void t_fae03(ref int status)
        /*
        **  - - - - - - - -
        **   t _ f a e 0 3
        **  - - - - - - - -
        **
        **  Test wwaFae03 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaFae03, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            vvd(WWA.wwaFae03(0.80), 1.744713738913081846, 1e-12,
               "wwaFae03", "", ref status);
        }

        static void t_faf03(ref int status)
        /*
        **  - - - - - - - -
        **   t _ f a f 0 3
        **  - - - - - - - -
        **
        **  Test wwaFaf03 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaFaf03, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            vvd(WWA.wwaFaf03(0.80), 0.2597711366745499518, 1e-12,
               "wwaFaf03", "", ref status);
        }

        static void t_faju03(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ f a j u 0 3
        **  - - - - - - - - -
        **
        **  Test wwaFaju03 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaFaju03, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            vvd(WWA.wwaFaju03(0.80), 5.275711665202481138, 1e-12,
               "wwaFaju03", "", ref status);
        }

        static void t_fal03(ref int status)
        /*
        **  - - - - - - - -
        **   t _ f a l 0 3
        **  - - - - - - - -
        **
        **  Test wwaFal03 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaFal03, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            vvd(WWA.wwaFal03(0.80), 5.132369751108684150, 1e-12,
               "wwaFal03", "", ref status);
        }

        static void t_falp03(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ f a l p 0 3
        **  - - - - - - - - -
        **
        **  Test wwaFalp03 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaFalp03, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            vvd(WWA.wwaFalp03(0.80), 6.226797973505507345, 1e-12,
              "wwaFalp03", "", ref status);
        }

        static void t_fama03(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ f a m a 0 3
        **  - - - - - - - - -
        **
        **  Test wwaFama03 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaFama03, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            vvd(WWA.wwaFama03(0.80), 3.275506840277781492, 1e-12,
               "wwaFama03", "", ref status);
        }

        static void t_fame03(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ f a m e 0 3
        **  - - - - - - - - -
        **
        **  Test wwaFame03 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaFame03, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            vvd(WWA.wwaFame03(0.80), 5.417338184297289661, 1e-12,
               "wwaFame03", "", ref status);
        }

        static void t_fane03(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ f a n e 0 3
        **  - - - - - - - - -
        **
        **  Test wwaFane03 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaFane03, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            vvd(WWA.wwaFane03(0.80), 2.079343830860413523, 1e-12,
               "wwaFane03", "", ref status);
        }

        static void t_faom03(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ f a o m 0 3
        **  - - - - - - - - -
        **
        **  Test wwaFaom03 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaFaom03, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            vvd(WWA.wwaFaom03(0.80), -5.973618440951302183, 1e-12,
               "wwaFaom03", "", ref status);
        }

        static void t_fapa03(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ f a p a 0 3
        **  - - - - - - - - -
        **
        **  Test wwaFapa03 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaFapa03, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            vvd(WWA.wwaFapa03(0.80), 0.1950884762240000000e-1, 1e-12,
               "wwaFapa03", "", ref status);
        }

        static void t_fasa03(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ f a s a 0 3
        **  - - - - - - - - -
        **
        **  Test wwaFasa03 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaFasa03, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            vvd(WWA.wwaFasa03(0.80), 5.371574539440827046, 1e-12,
               "wwaFasa03", "", ref status);
        }

        static void t_faur03(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ f a u r 0 3
        **  - - - - - - - - -
        **
        **  Test wwaFaur03 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaFaur03, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            vvd(WWA.wwaFaur03(0.80), 5.180636450180413523, 1e-12,
               "wwaFaur03", "", ref status);
        }

        static void t_fave03(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ f a v e 0 3
        **  - - - - - - - - -
        **
        **  Test wwaFave03 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaFave03, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            vvd(WWA.wwaFave03(0.80), 3.424900460533758000, 1e-12,
               "wwaFave03", "", ref status);
        }

        static void t_fk52h(ref int status)
        /*
        **  - - - - - - - -
        **   t _ f k 5 2 h
        **  - - - - - - - -
        **
        **  Test wwaFk52h function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaFk52h, vvd
        **
        **  This revision:  2016 December 22
        */
        {
            double r5, d5, dr5, dd5, px5, rv5, rh = 0, dh = 0, drh = 0, ddh = 0, pxh = 0, rvh = 0;


            r5 = 1.76779433;
            d5 = -0.2917517103;
            dr5 = -1.91851572e-7;
            dd5 = -5.8468475e-6;
            px5 = 0.379210;
            rv5 = -7.6;

            WWA.wwaFk52h(r5, d5, dr5, dd5, px5, rv5,
                     ref rh, ref dh, ref drh, ref ddh, ref pxh, ref rvh);

            vvd(rh, 1.767794226299947632, 1e-14,
                "wwaFk52h", "ra", ref status);
            vvd(dh, -0.2917516070530391757, 1e-14,
                "wwaFk52h", "dec", ref status);
            vvd(drh, -0.19618741256057224e-6, 1e-19,
                "wwaFk52h", "dr5", ref status);
            vvd(ddh, -0.58459905176693911e-5, 1e-19,
                "wwaFk52h", "dd5", ref status);
            vvd(pxh, 0.37921, 1e-14,
                "wwaFk52h", "px", ref status);
            //vvd(rvh, -7.6000000940000254, 1e-10,
            //    "iauFk52h", "rv", ref status);
            vvd(rvh, -7.6000000940000254, 1e-11,
                    "wwaFk52h", "rv", ref status);

        }

        static void t_fk5hip(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ f k 5 h i p
        **  - - - - - - - - -
        **
        **  Test wwaFk5hip function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaFk5hip, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] r5h = new double[3, 3];
            double[] s5h = new double[3];


            WWA.wwaFk5hip(r5h, s5h);

            vvd(r5h[0, 0], 0.9999999999999928638, 1e-14,
                "wwaFk5hip", "11", ref status);
            vvd(r5h[0, 1], 0.1110223351022919694e-6, 1e-17,
                "wwaFk5hip", "12", ref status);
            vvd(r5h[0, 2], 0.4411803962536558154e-7, 1e-17,
                "wwaFk5hip", "13", ref status);
            vvd(r5h[1, 0], -0.1110223308458746430e-6, 1e-17,
                "wwaFk5hip", "21", ref status);
            vvd(r5h[1, 1], 0.9999999999999891830, 1e-14,
                "wwaFk5hip", "22", ref status);
            vvd(r5h[1, 2], -0.9647792498984142358e-7, 1e-17,
                "wwaFk5hip", "23", ref status);
            vvd(r5h[2, 0], -0.4411805033656962252e-7, 1e-17,
                "wwaFk5hip", "31", ref status);
            vvd(r5h[2, 1], 0.9647792009175314354e-7, 1e-17,
                "wwaFk5hip", "32", ref status);
            vvd(r5h[2, 2], 0.9999999999999943728, 1e-14,
                "wwaFk5hip", "33", ref status);
            vvd(s5h[0], -0.1454441043328607981e-8, 1e-17,
                "wwaFk5hip", "s1", ref status);
            vvd(s5h[1], 0.2908882086657215962e-8, 1e-17,
                "wwaFk5hip", "s2", ref status);
            vvd(s5h[2], 0.3393695767766751955e-8, 1e-17,
                "wwaFk5hip", "s3", ref status);

        }

        static void t_fk5hz(ref int status)
        /*
        **  - - - - - - - -
        **   t _ f k 5 h z
        **  - - - - - - - -
        **
        **  Test wwaFk5hz function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaFk5hz, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double r5, d5, rh = 0, dh = 0;


            r5 = 1.76779433;
            d5 = -0.2917517103;

            WWA.wwaFk5hz(r5, d5, 2400000.5, 54479.0, ref rh, ref dh);

            vvd(rh, 1.767794191464423978, 1e-12, "wwaFk5hz", "ra", ref status);
            vvd(dh, -0.2917516001679884419, 1e-12, "wwaFk5hz", "dec", ref status);

        }

        static void t_fw2m(ref int status)
        /*
        **  - - - - - - -
        **   t _ f w 2 m
        **  - - - - - - -
        **
        **  Test wwaFw2m function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaFw2m, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double gamb, phib, psi, eps;
            double[,] r = new double[3, 3];

            gamb = -0.2243387670997992368e-5;
            phib = 0.4091014602391312982;
            psi = -0.9501954178013015092e-3;
            eps = 0.4091014316587367472;

            WWA.wwaFw2m(gamb, phib, psi, eps, r);

            vvd(r[0, 0], 0.9999995505176007047, 1e-12,
                "wwaFw2m", "11", ref status);
            vvd(r[0, 1], 0.8695404617348192957e-3, 1e-12,
                "wwaFw2m", "12", ref status);
            vvd(r[0, 2], 0.3779735201865582571e-3, 1e-12,
                "wwaFw2m", "13", ref status);

            vvd(r[1, 0], -0.8695404723772016038e-3, 1e-12,
                "wwaFw2m", "21", ref status);
            vvd(r[1, 1], 0.9999996219496027161, 1e-12,
                "wwaFw2m", "22", ref status);
            vvd(r[1, 2], -0.1361752496887100026e-6, 1e-12,
                "wwaFw2m", "23", ref status);

            vvd(r[2, 0], -0.3779734957034082790e-3, 1e-12,
                "wwaFw2m", "31", ref status);
            vvd(r[2, 1], -0.1924880848087615651e-6, 1e-12,
                "wwaFw2m", "32", ref status);
            vvd(r[2, 2], 0.9999999285679971958, 1e-12,
                "wwaFw2m", "33", ref status);

        }

        static void t_fw2xy(ref int status)
        /*
        **  - - - - - - - -
        **   t _ f w 2 x y
        **  - - - - - - - -
        **
        **  Test wwaFw2xy function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaFw2xy, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double gamb, phib, psi, eps, x = 0, y = 0;

            gamb = -0.2243387670997992368e-5;
            phib = 0.4091014602391312982;
            psi = -0.9501954178013015092e-3;
            eps = 0.4091014316587367472;

            WWA.wwaFw2xy(gamb, phib, psi, eps, ref x, ref y);

            vvd(x, -0.3779734957034082790e-3, 1e-14, "wwaFw2xy", "x", ref status);
            vvd(y, -0.1924880848087615651e-6, 1e-14, "wwaFw2xy", "y", ref status);
        }

        static void t_g2icrs(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ g 2 i c r s
        **  - - - - - - - - -
        **
        **  Test iauG2icrs function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaG2icrs, vvd
        **
        **  This revision:  2015 January 30
        */
        {
            double dl, db, dr = 0, dd = 0;

            dl = 5.5850536063818546461558105;
            db = -0.7853981633974483096156608;
            WWA.wwaG2icrs(dl, db, ref dr, ref dd);
            vvd(dr, 5.9338074302227188048671, 1e-14, "wwaG2icrs", "R", ref status);
            vvd(dd, -1.1784870613579944551541, 1e-14, "wwaG2icrs", "D", ref status);
        }

        static void t_gc2gd(ref int status)
        /*
        **  - - - - - - - -
        **   t _ g c 2 g d
        **  - - - - - - - -
        **
        **  Test wwaGc2gd function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaGc2gd, viv, vvd
        **
        **  This revision:  2016 March 12
        */
        {
            int j;
            double[] xyz = new double[] { 2e6, 3e6, 5.244e6 };
            double e = 0, p = 0, h = 0;

            j = WWA.wwaGc2gd(0, xyz, ref e, ref p, ref h);

            viv(j, -1, "wwaGc2gd", "j0", ref status);

            j = WWA.wwaGc2gd(WWA.WGS84, xyz, ref e, ref p, ref h);

            viv(j, 0, "wwaGc2gd", "j1", ref status);
            //vvd(e, 0.98279372324732907, 1e-14, "wwaGc2gd", "e1", ref status);
            vvd(e, 0.9827937232473290680, 1e-14, "wwaGc2gd", "e1", ref status);
            vvd(p, 0.97160184819075459, 1e-14, "wwaGc2gd", "p1", ref status);
            //vvd(h, 331.41724614260599, 1e-8, "wwaGc2gd", "h1", ref status);
            vvd(h, 331.4172461426059892, 1e-8, "wwaGc2gd", "h1", ref status);

            j = WWA.wwaGc2gd(WWA.GRS80, xyz, ref e, ref p, ref h);

            viv(j, 0, "wwaGc2gd", "j2", ref status);
            //vvd(e, 0.98279372324732907, 1e-14, "wwaGc2gd", "e2", ref status);
            vvd(e, 0.9827937232473290680, 1e-14, "wwaGc2gd", "e2", ref status);
            vvd(p, 0.97160184820607853, 1e-14, "wwaGc2gd", "p2", ref status);
            vvd(h, 331.41731754844348, 1e-8, "wwaGc2gd", "h2", ref status);

            j = WWA.wwaGc2gd(WWA.WGS72, xyz, ref e, ref p, ref h);

            viv(j, 0, "wwaGc2gd", "j3", ref status);
            //vvd(e, 0.98279372324732907, 1e-14, "wwaGc2gd", "e3", ref status);
            vvd(e, 0.9827937232473290680, 1e-14, "wwaGc2gd", "e3", ref status);
            //vvd(p, 0.97160181811015119, 1e-14, "wwaGc2gd", "p3", ref status);
            vvd(p, 0.9716018181101511937, 1e-14, "wwaGc2gd", "p3", ref status);
            //vvd(h, 333.27707261303181, 1e-8, "wwaGc2gd", "h3", ref status);
            vvd(h, 333.2770726130318123, 1e-8, "wwaGc2gd", "h3", ref status);

            j = WWA.wwaGc2gd(4, xyz, ref e, ref p, ref h);

            viv(j, -1, "wwaGc2gd", "j4", ref status);
        }

        static void t_gc2gde(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ g c 2 g d e
        **  - - - - - - - - -
        **
        **  Test wwaGc2gde function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaGc2gde, viv, vvd
        **
        **  This revision:  2016 March 12
        */
        {
            int j;
            double a = 6378136.0, f = 0.0033528;
            double[] xyz = new double[] { 2e6, 3e6, 5.244e6 };
            double e = 0, p = 0, h = 0;

            j = WWA.wwaGc2gde(a, f, xyz, ref e, ref p, ref h);

            viv(j, 0, "wwaGc2gde", "j", ref status);
            //vvd(e, 0.98279372324732907, 1e-14, "wwaGc2gde", "e", ref status);
            vvd(e, 0.9827937232473290680, 1e-14, "wwaGc2gde", "e", ref status);
            //vvd(p, 0.97160183775704115, 1e-14, "wwaGc2gde", "p", ref status);
            vvd(p, 0.9716018377570411532, 1e-14, "wwaGc2gde", "p", ref status);
            vvd(h, 332.36862495764397, 1e-8, "wwaGc2gde", "h", ref status);
        }

        static void t_gd2gc(ref int status)
        /*
        **  - - - - - - - -
        **   t _ g d 2 g c
        **  - - - - - - - -
        **
        **  Test wwaGd2gc function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaGd2gc, viv, vvd
        **
        **  This revision:  2016 March 12
        */
        {
            int j;
            double e = 3.1, p = -0.5, h = 2500.0;
            double[] xyz = new double[3];

            j = WWA.wwaGd2gc(0, e, p, h, xyz);

            viv(j, -1, "wwaGd2gc", "j0", ref status);

            j = WWA.wwaGd2gc(WWA.WGS84, e, p, h, xyz);

            viv(j, 0, "wwaGd2gc", "j1", ref status);
            //vvd(xyz[0], -5599000.5577049947, 1e-7, "wwaGd2gc", "0/1", ref status);
            //vvd(xyz[1], 233011.67223479203, 1e-7, "wwaGd2gc", "1/1", ref status);
            //vvd(xyz[2], -3040909.4706983363, 1e-7, "wwaGd2gc", "2/1", ref status);
            vvd(xyz[0], -5599000.5577049947, 1e-7, "wwaGd2gc", "1/1", ref status);
            vvd(xyz[1], 233011.67223479203, 1e-7, "wwaGd2gc", "2/1", ref status);
            vvd(xyz[2], -3040909.4706983363, 1e-7, "wwaGd2gc", "3/1", ref status);

            j = WWA.wwaGd2gc(WWA.GRS80, e, p, h, xyz);

            viv(j, 0, "wwaGd2gc", "j2", ref status);
            //vvd(xyz[0], -5599000.5577260984, 1e-7, "wwaGd2gc", "0/2", ref status);
            //vvd(xyz[1], 233011.6722356703, 1e-7, "wwaGd2gc", "1/2", ref status);
            //vvd(xyz[2], -3040909.4706095476, 1e-7, "wwaGd2gc", "2/2", ref status);
            vvd(xyz[0], -5599000.5577260984, 1e-7, "wwaGd2gc", "1/2", ref status);
            vvd(xyz[1], 233011.6722356702949, 1e-7, "wwaGd2gc", "2/2", ref status);
            vvd(xyz[2], -3040909.4706095476, 1e-7, "wwaGd2gc", "3/2", ref status);

            j = WWA.wwaGd2gc(WWA.WGS72, e, p, h, xyz);

            viv(j, 0, "wwaGd2gc", "j3", ref status);
            //vvd(xyz[0], -5598998.7626301490, 1e-7, "wwaGd2gc", "0/3", ref status);
            //vvd(xyz[1], 233011.5975297822, 1e-7, "wwaGd2gc", "1/3", ref status);
            //vvd(xyz[2], -3040908.6861467111, 1e-7, "wwaGd2gc", "2/3", ref status);
            vvd(xyz[0], -5598998.7626301490, 1e-7, "wwaGd2gc", "1/3", ref status);
            vvd(xyz[1], 233011.5975297822211, 1e-7, "wwaGd2gc", "2/3", ref status);
            vvd(xyz[2], -3040908.6861467111, 1e-7, "wwaGd2gc", "3/3", ref status);

            j = WWA.wwaGd2gc(4, e, p, h, xyz);

            viv(j, -1, "wwaGd2gc", "j4", ref status);
        }

        static void t_gd2gce(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ g d 2 g c e
        **  - - - - - - - - -
        **
        **  Test wwaGd2gce function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaGd2gce, viv, vvd
        **
        **  This revision:  2016 March 12
        */
        {
            int j;
            double a = 6378136.0, f = 0.0033528;
            double e = 3.1, p = -0.5, h = 2500.0;
            double[] xyz = new double[3];

            j = WWA.wwaGd2gce(a, f, e, p, h, xyz);

            viv(j, 0, "wwaGd2gce", "j", ref status);
            //vvd(xyz[0], -5598999.6665116328, 1e-7, "wwaGd2gce", "0", ref status);
            //vvd(xyz[1], 233011.63514630572, 1e-7, "wwaGd2gce", "1", ref status);
            //vvd(xyz[2], -3040909.0517314132, 1e-7, "wwaGd2gce", "2", ref status);
            vvd(xyz[0], -5598999.6665116328, 1e-7, "wwaGd2gce", "1", ref status);
            vvd(xyz[1], 233011.6351463057189, 1e-7, "wwaGd2gce", "2", ref status);
            vvd(xyz[2], -3040909.0517314132, 1e-7, "wwaGd2gce", "3", ref status);
        }

        static void t_gmst00(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ g m s t 0 0
        **  - - - - - - - - -
        **
        **  Test wwaGmst00 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaGmst00, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double theta;

            theta = WWA.wwaGmst00(2400000.5, 53736.0, 2400000.5, 53736.0);

            vvd(theta, 1.754174972210740592, 1e-12, "wwaGmst00", "", ref status);

        }

        static void t_gmst06(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ g m s t 0 6
        **  - - - - - - - - -
        **
        **  Test wwaGmst06 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaGmst06, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double theta;

            theta = WWA.wwaGmst06(2400000.5, 53736.0, 2400000.5, 53736.0);

            vvd(theta, 1.754174971870091203, 1e-12, "wwaGmst06", "", ref status);

        }

        static void t_gmst82(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ g m s t 8 2
        **  - - - - - - - - -
        **
        **  Test wwaGmst82 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaGmst82, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double theta;

            theta = WWA.wwaGmst82(2400000.5, 53736.0);

            vvd(theta, 1.754174981860675096, 1e-12, "wwaGmst82", "", ref status);

        }

        static void t_gst00a(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ g s t 0 0 a
        **  - - - - - - - - -
        **
        **  Test wwaGst00a function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaGst00a, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double theta;

            theta = WWA.wwaGst00a(2400000.5, 53736.0, 2400000.5, 53736.0);

            vvd(theta, 1.754166138018281369, 1e-12, "wwaGst00a", "", ref status);

        }

        static void t_gst00b(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ g s t 0 0 b
        **  - - - - - - - - -
        **
        **  Test wwaGst00b function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaGst00b, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double theta;

            theta = WWA.wwaGst00b(2400000.5, 53736.0);

            vvd(theta, 1.754166136510680589, 1e-12, "wwaGst00b", "", ref status);

        }

        static void t_gst06(ref int status)
        /*
        **  - - - - - - - -
        **   t _ g s t 0 6
        **  - - - - - - - -
        **
        **  Test wwaGst06 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaGst06, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] rnpb = new double[3, 3];
            double theta;


            rnpb[0, 0] = 0.9999989440476103608;
            rnpb[0, 1] = -0.1332881761240011518e-2;
            rnpb[0, 2] = -0.5790767434730085097e-3;

            rnpb[1, 0] = 0.1332858254308954453e-2;
            rnpb[1, 1] = 0.9999991109044505944;
            rnpb[1, 2] = -0.4097782710401555759e-4;

            rnpb[2, 0] = 0.5791308472168153320e-3;
            rnpb[2, 1] = 0.4020595661593994396e-4;
            rnpb[2, 2] = 0.9999998314954572365;

            theta = WWA.wwaGst06(2400000.5, 53736.0, 2400000.5, 53736.0, rnpb);

            vvd(theta, 1.754166138018167568, 1e-12, "wwaGst06", "", ref status);

        }

        static void t_gst06a(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ g s t 0 6 a
        **  - - - - - - - - -
        **
        **  Test wwaGst06a function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaGst06a, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double theta;


            theta = WWA.wwaGst06a(2400000.5, 53736.0, 2400000.5, 53736.0);

            vvd(theta, 1.754166137675019159, 1e-12, "wwaGst06a", "", ref status);

        }

        static void t_gst94(ref int status)
        /*
        **  - - - - - - - -
        **   t _ g s t 9 4
        **  - - - - - - - -
        **
        **  Test wwaGst94 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaGst94, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double theta;

            theta = WWA.wwaGst94(2400000.5, 53736.0);

            vvd(theta, 1.754166136020645203, 1e-12, "wwaGst94", "", ref status);

        }

        static void t_icrs2g(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ i c r s 2 g
        **  - - - - - - - - -
        **
        **  Test iauIcrs2g function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaIcrs2g, vvd
        **
        **  This revision:  2015 January 30
        */
        {
            double dr, dd, dl = 0, db = 0;

            dr = 5.9338074302227188048671087;
            dd = -1.1784870613579944551540570;
            WWA.wwaIcrs2g(dr, dd, ref dl, ref db);
            vvd(dl, 5.5850536063818546461558, 1e-14, "wwaIcrs2g", "L", ref status);
            vvd(db, -0.7853981633974483096157, 1e-14, "wwaIcrs2g", "B", ref status);
        }

        static void t_h2fk5(ref int status)
        /*
        **  - - - - - - - -
        **   t _ h 2 f k 5
        **  - - - - - - - -
        **
        **  Test wwaH2fk5 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaH2fk5, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double rh, dh, drh, ddh, pxh, rvh, r5 = 0, d5 = 0, dr5 = 0, dd5 = 0, px5 = 0, rv5 = 0;


            rh = 1.767794352;
            dh = -0.2917512594;
            drh = -2.76413026e-6;
            ddh = -5.92994449e-6;
            pxh = 0.379210;
            rvh = -7.6;

            WWA.wwaH2fk5(rh, dh, drh, ddh, pxh, rvh,
                     ref r5, ref d5, ref dr5, ref dd5, ref px5, ref rv5);

            vvd(r5, 1.767794455700065506, 1e-13,
                "wwaH2fk5", "ra", ref status);
            vvd(d5, -0.2917513626469638890, 1e-13,
                "wwaH2fk5", "dec", ref status);
            vvd(dr5, -0.27597945024511204e-5, 1e-18,
                "wwaH2fk5", "dr5", ref status);
            vvd(dd5, -0.59308014093262838e-5, 1e-18,
                "wwaH2fk5", "dd5", ref status);
            vvd(px5, 0.37921, 1e-13,
                "wwaH2fk5", "px", ref status);
            //vvd(rv5, -7.6000001309071126, 1e-10,
            //    "wwaH2fk5", "rv", ref status);
            vvd(rv5, -7.6000001309071126, 1e-11,
                   "wwaH2fk5", "rv", ref status);

        }

        static void t_hd2ae(ref int status)
        /*
        **  - - - - - - - -
        **   t _ h d 2 a e
        **  - - - - - - - -
        **
        **  Test iauHd2ae function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaHd2ae and vvd
        **
        **  This revision:  2017 October 21
        */
        {
            double h, d, p, a = 0, e = 0;


            h = 1.1;
            d = 1.2;
            p = 0.3;

            WWA.wwaHd2ae(h, d, p, ref a, ref e);

            vvd(a, 5.916889243730066194, 1e-13, "wwaHd2ae", "a", ref status);
            vvd(e, 0.4472186304990486228, 1e-14, "wwaHd2ae", "e", ref status);
        }

        static void t_hd2pa(ref int status)
        /*
        **  - - - - - - - -
        **   t _ h d 2 p a
        **  - - - - - - - -
        **
        **  Test iauHd2pa function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaHd2pa and vvd
        **
        **  This revision:  2017 October 21
        */
        {
            double h, d, p, q;


            h = 1.1;
            d = 1.2;
            p = 0.3;

            q = WWA.wwaHd2pa(h, d, p);

            vvd(q, 1.906227428001995580, 1e-13, "wwaHd2pa", "q", ref status);

        }

        static void t_hfk5z(ref int status)
        /*
        **  - - - - - - - -
        **   t _ h f k 5 z
        **  - - - - - - - -
        **
        **  Test wwaHfk5z function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaHfk5z, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double rh, dh, r5 = 0, d5 = 0, dr5 = 0, dd5 = 0;



            rh = 1.767794352;
            dh = -0.2917512594;

            WWA.wwaHfk5z(rh, dh, 2400000.5, 54479.0, ref r5, ref d5, ref dr5, ref dd5);

            vvd(r5, 1.767794490535581026, 1e-13,
                "wwaHfk5z", "ra", ref status);
            vvd(d5, -0.2917513695320114258, 1e-14,
                "wwaHfk5z", "dec", ref status);
            vvd(dr5, 0.4335890983539243029e-8, 1e-22,
                "wwaHfk5z", "dr5", ref status);
            vvd(dd5, -0.8569648841237745902e-9, 1e-23,
                "wwaHfk5z", "dd5", ref status);

        }

        static void t_ir(ref int status)
        /*
        **  - - - - -
        **   t _ i r
        **  - - - - -
        **
        **  Test wwaIr function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaIr, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] r = new double[3, 3];

            r[0, 0] = 2.0;
            r[0, 1] = 3.0;
            r[0, 2] = 2.0;

            r[1, 0] = 3.0;
            r[1, 1] = 2.0;
            r[1, 2] = 3.0;

            r[2, 0] = 3.0;
            r[2, 1] = 4.0;
            r[2, 2] = 5.0;

            WWA.wwaIr(r);

            vvd(r[0, 0], 1.0, 0.0, "wwaIr", "11", ref status);
            vvd(r[0, 1], 0.0, 0.0, "wwaIr", "12", ref status);
            vvd(r[0, 2], 0.0, 0.0, "wwaIr", "13", ref status);

            vvd(r[1, 0], 0.0, 0.0, "wwaIr", "21", ref status);
            vvd(r[1, 1], 1.0, 0.0, "wwaIr", "22", ref status);
            vvd(r[1, 2], 0.0, 0.0, "wwaIr", "23", ref status);

            vvd(r[2, 0], 0.0, 0.0, "wwaIr", "31", ref status);
            vvd(r[2, 1], 0.0, 0.0, "wwaIr", "32", ref status);
            vvd(r[2, 2], 1.0, 0.0, "wwaIr", "33", ref status);

        }

        static void t_jd2cal(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ j d 2 c a l
        **  - - - - - - - - -
        **
        **  Test wwaJd2cal function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaJd2cal, viv, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double dj1, dj2, fd = 0;
            int iy = 0, im = 0, id = 0, j;


            dj1 = 2400000.5;
            dj2 = 50123.9999;

            j = WWA.wwaJd2cal(dj1, dj2, ref iy, ref im, ref id, ref fd);

            viv(iy, 1996, "wwaJd2cal", "y", ref status);
            viv(im, 2, "wwaJd2cal", "m", ref status);
            viv(id, 10, "wwaJd2cal", "d", ref status);
            vvd(fd, 0.9999, 1e-7, "wwaJd2cal", "fd", ref status);
            viv(j, 0, "wwaJd2cal", "j", ref status);

        }

        static void t_jdcalf(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ j d c a l f
        **  - - - - - - - - -
        **
        **  Test wwaJdcalf function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaJdcalf, viv
        **
        **  This revision:  2013 August 7
        */
        {
            double dj1, dj2;
            int[] iydmf = new int[4];
            int j;


            dj1 = 2400000.5;
            dj2 = 50123.9999;

            j = WWA.wwaJdcalf(4, dj1, dj2, iydmf);

            viv(iydmf[0], 1996, "wwaJdcalf", "y", ref status);
            viv(iydmf[1], 2, "wwaJdcalf", "m", ref status);
            viv(iydmf[2], 10, "wwaJdcalf", "d", ref status);
            viv(iydmf[3], 9999, "wwaJdcalf", "f", ref status);

            viv(j, 0, "wwaJdcalf", "j", ref status);

        }

        static void t_ld(ref int status)
        /*
        **  - - - - -
        **   t _ l d
        **  - - - - -
        **
        **  Test wwaLd function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaLd, vvd
        *
        **  This revision:  2013 October 2
        */
        {
            double bm;
            double[] p = new double[3];
            double[] q = new double[3];
            double[] e = new double[3];
            double em, dlim;
            double[] p1 = new double[3];


            bm = 0.00028574;
            p[0] = -0.763276255;
            p[1] = -0.608633767;
            p[2] = -0.216735543;
            q[0] = -0.763276255;
            q[1] = -0.608633767;
            q[2] = -0.216735543;
            e[0] = 0.76700421;
            e[1] = 0.605629598;
            e[2] = 0.211937094;
            em = 8.91276983;
            dlim = 3e-10;

            WWA.wwaLd(bm, p, q, e, em, dlim, p1);

            vvd(p1[0], -0.7632762548968159627, 1e-12,
                        "wwaLd", "1", ref status);
            vvd(p1[1], -0.6086337670823762701, 1e-12,
                        "wwaLd", "2", ref status);
            vvd(p1[2], -0.2167355431320546947, 1e-12,
                        "wwaLd", "3", ref status);

        }

        static void t_ldn(ref int status)
        /*
        **  - - - - - -
        **   t _ l d n
        **  - - - - - -
        **
        **  Test wwaLdn function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaLdn, vvd
        **
        **  This revision:  2013 October 2
        */
        {
            int n;
            WWA.wwaLDBODY[] b = new WWA.wwaLDBODY[3];
            double[] ob = new double[3];
            double[] sc = new double[3];
            double[] sn = new double[3];

            n = 3;
            b[0].bm = 0.00028574;
            b[0].dl = 3e-10;

            b[0].pv = new double[3, 3];
            b[0].pv[0, 0] = -7.81014427;
            b[0].pv[0, 1] = -5.60956681;
            b[0].pv[0, 2] = -1.98079819;
            b[0].pv[1, 0] = 0.0030723249;
            b[0].pv[1, 1] = -0.00406995477;
            b[0].pv[1, 2] = -0.00181335842;
            b[1].bm = 0.00095435;
            b[1].dl = 3e-9;

            b[1].pv = new double[3, 3];
            b[1].pv[0, 0] = 0.738098796;
            b[1].pv[0, 1] = 4.63658692;
            b[1].pv[0, 2] = 1.9693136;
            b[1].pv[1, 0] = -0.00755816922;
            b[1].pv[1, 1] = 0.00126913722;
            b[1].pv[1, 2] = 0.000727999001;
            b[2].bm = 1.0;
            b[2].dl = 6e-6;

            b[2].pv = new double[3, 3];
            b[2].pv[0, 0] = -0.000712174377;
            b[2].pv[0, 1] = -0.00230478303;
            b[2].pv[0, 2] = -0.00105865966;
            b[2].pv[1, 0] = 6.29235213e-6;
            b[2].pv[1, 1] = -3.30888387e-7;
            b[2].pv[1, 2] = -2.96486623e-7;
            ob[0] = -0.974170437;
            ob[1] = -0.2115201;
            ob[2] = -0.0917583114;
            sc[0] = -0.763276255;
            sc[1] = -0.608633767;
            sc[2] = -0.216735543;

            WWA.wwaLdn(n, b, ob, sc, sn);

            vvd(sn[0], -0.7632762579693333866, 1e-12,
                        "wwaLdn", "1", ref status);
            vvd(sn[1], -0.6086337636093002660, 1e-12,
                        "wwaLdn", "2", ref status);
            vvd(sn[2], -0.2167355420646328159, 1e-12,
                        "wwaLdn", "3", ref status);

        }

        static void t_ldsun(ref int status)
        /*
        **  - - - - - - - -
        **   t _ l d s u n
        **  - - - - - - - -
        **
        **  Test wwaLdsun function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaLdsun, vvd
        **
        **  This revision:  2013 October 2
        */
        {
            double[] p = new double[3];
            double[] e = new double[3];
            double em;
            double[] p1 = new double[3];

            p[0] = -0.763276255;
            p[1] = -0.608633767;
            p[2] = -0.216735543;
            e[0] = -0.973644023;
            e[1] = -0.20925523;
            e[2] = -0.0907169552;
            em = 0.999809214;

            WWA.wwaLdsun(p, e, em, p1);

            vvd(p1[0], -0.7632762580731413169, 1e-12,
                        "wwaLdsun", "1", ref status);
            vvd(p1[1], -0.6086337635262647900, 1e-12,
                        "wwaLdsun", "2", ref status);
            vvd(p1[2], -0.2167355419322321302, 1e-12,
                        "wwaLdsun", "3", ref status);

        }

        static void t_lteceq(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ l t e c e q
        **  - - - - - - - - -
        **
        **  Test wwaLteceq function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaLteceq, vvd
        **
        **  This revision:  2016 March 12
        */
        {
            double epj, dl, db, dr = 0, dd = 0;


            epj = 2500.0;
            dl = 1.5;
            db = 0.6;

            WWA.wwaLteceq(epj, dl, db, ref dr, ref dd);

            vvd(dr, 1.275156021861921167, 1e-14, "iauLteceq", "dr", ref status);
            vvd(dd, 0.9966573543519204791, 1e-14, "iauLteceq", "dd", ref status);

        }

        static void t_ltecm(ref int status)
        /*
        **  - - - - - - - -
        **   t _ l t e c m
        **  - - - - - - - -
        **
        **  Test wwaLtecm function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaLtecm, vvd
        **
        **  This revision:  2016 March 12
        */
        {
            double epj;
            double[,] rm = new double[3, 3];


            epj = -3000.0;

            WWA.wwaLtecm(epj, rm);

            vvd(rm[0, 0], 0.3564105644859788825, 1e-14, "wwaLtecm", "rm11", ref status);
            vvd(rm[0, 1], 0.8530575738617682284, 1e-14, "wwaLtecm", "rm12", ref status);
            vvd(rm[0, 2], 0.3811355207795060435, 1e-14, "wwaLtecm", "rm13", ref status);
            vvd(rm[1, 0], -0.9343283469640709942, 1e-14, "wwaLtecm", "rm21", ref status);
            vvd(rm[1, 1], 0.3247830597681745976, 1e-14, "wwaLtecm", "rm22", ref status);
            vvd(rm[1, 2], 0.1467872751535940865, 1e-14, "wwaLtecm", "rm23", ref status);
            vvd(rm[2, 0], 0.1431636191201167793e-2, 1e-14, "wwaLtecm", "rm31", ref status);
            vvd(rm[2, 1], -0.4084222566960599342, 1e-14, "wwaLtecm", "rm32", ref status);
            vvd(rm[2, 2], 0.9127919865189030899, 1e-14, "wwaLtecm", "rm33", ref status);

        }

        static void t_lteqec(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ l t e q e c
        **  - - - - - - - - -
        **
        **  Test wwaLteqec function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaLteqec, vvd
        **
        **  This revision:  2016 March 12
        */
        {
            double epj, dr, dd, dl = 0, db = 0;


            epj = -1500.0;
            dr = 1.234;
            dd = 0.987;

            WWA.wwaLteqec(epj, dr, dd, ref dl, ref db);

            vvd(dl, 0.5039483649047114859, 1e-14, "wwaLteqec", "dl", ref status);
            vvd(db, 0.5848534459726224882, 1e-14, "wwaLteqec", "db", ref status);
        }

        static void t_ltp(ref int status)
        /*
        **  - - - - - -
        **   t _ l t p
        **  - - - - - -
        **
        **  Test refLtp function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  refLtp, vvd
        **
        **  This revision:  2016 March 12
        */
        {
            double epj;
            double[,] rp = new double[3, 3];


            epj = 1666.666;

            WWA.wwaLtp(epj, rp);

            vvd(rp[0, 0], 0.9967044141159213819, 1e-14, "wwaLtp", "rp11", ref status);
            vvd(rp[0, 1], 0.7437801893193210840e-1, 1e-14, "wwaLtp", "rp12", ref status);
            vvd(rp[0, 2], 0.3237624409345603401e-1, 1e-14, "wwaLtp", "rp13", ref status);
            vvd(rp[1, 0], -0.7437802731819618167e-1, 1e-14, "wwaLtp", "rp21", ref status);
            vvd(rp[1, 1], 0.9972293894454533070, 1e-14, "wwaLtp", "rp22", ref status);
            vvd(rp[1, 2], -0.1205768842723593346e-2, 1e-14, "wwaLtp", "rp23", ref status);
            vvd(rp[2, 0], -0.3237622482766575399e-1, 1e-14, "wwaLtp", "rp31", ref status);
            vvd(rp[2, 1], -0.1206286039697609008e-2, 1e-14, "wwaLtp", "rp32", ref status);
            vvd(rp[2, 2], 0.9994750246704010914, 1e-14, "wwaLtp", "rp33", ref status);
        }

        static void t_ltpb(ref int status)
        /*
        **  - - - - - - -
        **   t _ l t p b
        **  - - - - - - -
        **
        **  Test wwaLtpb function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaLtpb, vvd
        **
        **  This revision:  2016 March 12
        */
        {
            double epj;
            double[,] rpb = new double[3, 3];


            epj = 1666.666;

            WWA.wwaLtpb(epj, rpb);

            vvd(rpb[0, 0], 0.9967044167723271851, 1e-14, "wwaLtpb", "rpb11", ref status);
            vvd(rpb[0, 1], 0.7437794731203340345e-1, 1e-14, "wwaLtpb", "rpb12", ref status);
            vvd(rpb[0, 2], 0.3237632684841625547e-1, 1e-14, "wwaLtpb", "rpb13", ref status);
            vvd(rpb[1, 0], -0.7437795663437177152e-1, 1e-14, "wwaLtpb", "rpb21", ref status);
            vvd(rpb[1, 1], 0.9972293947500013666, 1e-14, "wwaLtpb", "rpb22", ref status);
            vvd(rpb[1, 2], -0.1205741865911243235e-2, 1e-14, "wwaLtpb", "rpb23", ref status);
            vvd(rpb[2, 0], -0.3237630543224664992e-1, 1e-14, "wwaLtpb", "rpb31", ref status);
            vvd(rpb[2, 1], -0.1206316791076485295e-2, 1e-14, "wwaLtpb", "rpb32", ref status);
            vvd(rpb[2, 2], 0.9994750220222438819, 1e-14, "wwaLtpb", "rpb33", ref status);
        }

        static void t_ltpecl(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ l t p e c l
        **  - - - - - - - - -
        **
        **  Test wwaLtpecl function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaLtpecl, vvd
        **
        **  This revision:  2016 March 12
        */
        {
            double epj;
            double[] vec = new double[3];


            epj = -1500.0;

            WWA.wwaLtpecl(epj, vec);

            vvd(vec[0], 0.4768625676477096525e-3, 1e-14, "wwaLtpecl", "vec1", ref status);
            vvd(vec[1], -0.4052259533091875112, 1e-14, "wwaLtpecl", "vec2", ref status);
            vvd(vec[2], 0.9142164401096448012, 1e-14, "wwaLtpecl", "vec3", ref status);
        }

        static void t_ltpequ(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ l t p e q u
        **  - - - - - - - - -
        **
        **  Test wwaLtpequ function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaLtpequ, vvd
        **
        **  This revision:  2016 March 12
        */
        {
            double epj;
            double[] veq = new double[3];


            epj = -2500.0;

            WWA.wwaLtpequ(epj, veq);

            vvd(veq[0], -0.3586652560237326659, 1e-14, "wwaLtpequ", "veq1", ref status);
            vvd(veq[1], -0.1996978910771128475, 1e-14, "wwaLtpequ", "veq2", ref status);
            vvd(veq[2], 0.9118552442250819624, 1e-14, "wwaLtpequ", "veq3", ref status);
        }

        static void t_num00a(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ n u m 0 0 a
        **  - - - - - - - - -
        **
        **  Test wwaNum00a function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaNum00a, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] rmatn = new double[3, 3];


            WWA.wwaNum00a(2400000.5, 53736.0, rmatn);

            vvd(rmatn[0, 0], 0.9999999999536227949, 1e-12,
                "wwaNum00a", "11", ref status);
            vvd(rmatn[0, 1], 0.8836238544090873336e-5, 1e-12,
                "wwaNum00a", "12", ref status);
            vvd(rmatn[0, 2], 0.3830835237722400669e-5, 1e-12,
                "wwaNum00a", "13", ref status);

            vvd(rmatn[1, 0], -0.8836082880798569274e-5, 1e-12,
                "wwaNum00a", "21", ref status);
            vvd(rmatn[1, 1], 0.9999999991354655028, 1e-12,
                "wwaNum00a", "22", ref status);
            vvd(rmatn[1, 2], -0.4063240865362499850e-4, 1e-12,
                "wwaNum00a", "23", ref status);

            vvd(rmatn[2, 0], -0.3831194272065995866e-5, 1e-12,
                "wwaNum00a", "31", ref status);
            vvd(rmatn[2, 1], 0.4063237480216291775e-4, 1e-12,
                "wwaNum00a", "32", ref status);
            vvd(rmatn[2, 2], 0.9999999991671660338, 1e-12,
                "wwaNum00a", "33", ref status);

        }

        static void t_num00b(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ n u m 0 0 b
        **  - - - - - - - - -
        **
        **  Test wwaNum00b function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaNum00b, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] rmatn = new double[3, 3];

            WWA.wwaNum00b(2400000.5, 53736, rmatn);

            vvd(rmatn[0, 0], 0.9999999999536069682, 1e-12,
                "wwaNum00b", "11", ref status);
            vvd(rmatn[0, 1], 0.8837746144871248011e-5, 1e-12,
                "wwaNum00b", "12", ref status);
            vvd(rmatn[0, 2], 0.3831488838252202945e-5, 1e-12,
                "wwaNum00b", "13", ref status);

            vvd(rmatn[1, 0], -0.8837590456632304720e-5, 1e-12,
                "wwaNum00b", "21", ref status);
            vvd(rmatn[1, 1], 0.9999999991354692733, 1e-12,
                "wwaNum00b", "22", ref status);
            vvd(rmatn[1, 2], -0.4063198798559591654e-4, 1e-12,
                "wwaNum00b", "23", ref status);

            vvd(rmatn[2, 0], -0.3831847930134941271e-5, 1e-12,
                "wwaNum00b", "31", ref status);
            vvd(rmatn[2, 1], 0.4063195412258168380e-4, 1e-12,
                "wwaNum00b", "32", ref status);
            vvd(rmatn[2, 2], 0.9999999991671806225, 1e-12,
                "wwaNum00b", "33", ref status);

        }

        static void t_num06a(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ n u m 0 6 a
        **  - - - - - - - - -
        **
        **  Test wwaNum06a function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaNum06a, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] rmatn = new double[3, 3];

            WWA.wwaNum06a(2400000.5, 53736, rmatn);

            vvd(rmatn[0, 0], 0.9999999999536227668, 1e-12,
                "wwaNum06a", "11", ref status);
            vvd(rmatn[0, 1], 0.8836241998111535233e-5, 1e-12,
                "wwaNum06a", "12", ref status);
            vvd(rmatn[0, 2], 0.3830834608415287707e-5, 1e-12,
                "wwaNum06a", "13", ref status);

            vvd(rmatn[1, 0], -0.8836086334870740138e-5, 1e-12,
                "wwaNum06a", "21", ref status);
            vvd(rmatn[1, 1], 0.9999999991354657474, 1e-12,
                "wwaNum06a", "22", ref status);
            vvd(rmatn[1, 2], -0.4063240188248455065e-4, 1e-12,
                "wwaNum06a", "23", ref status);

            vvd(rmatn[2, 0], -0.3831193642839398128e-5, 1e-12,
                "wwaNum06a", "31", ref status);
            vvd(rmatn[2, 1], 0.4063236803101479770e-4, 1e-12,
                "wwaNum06a", "32", ref status);
            vvd(rmatn[2, 2], 0.9999999991671663114, 1e-12,
                "wwaNum06a", "33", ref status);

        }

        static void t_numat(ref int status)
        /*
        **  - - - - - - - -
        **   t _ n u m a t
        **  - - - - - - - -
        **
        **  Test wwaNumat function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaNumat, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double epsa, dpsi, deps;
            double[,] rmatn = new double[3, 3];

            epsa = 0.4090789763356509900;
            dpsi = -0.9630909107115582393e-5;
            deps = 0.4063239174001678826e-4;

            WWA.wwaNumat(epsa, dpsi, deps, rmatn);

            vvd(rmatn[0, 0], 0.9999999999536227949, 1e-12,
                "wwaNumat", "11", ref status);
            vvd(rmatn[0, 1], 0.8836239320236250577e-5, 1e-12,
                "wwaNumat", "12", ref status);
            vvd(rmatn[0, 2], 0.3830833447458251908e-5, 1e-12,
                "wwaNumat", "13", ref status);

            vvd(rmatn[1, 0], -0.8836083657016688588e-5, 1e-12,
                "wwaNumat", "21", ref status);
            vvd(rmatn[1, 1], 0.9999999991354654959, 1e-12,
                "wwaNumat", "22", ref status);
            vvd(rmatn[1, 2], -0.4063240865361857698e-4, 1e-12,
                "wwaNumat", "23", ref status);

            vvd(rmatn[2, 0], -0.3831192481833385226e-5, 1e-12,
                "wwaNumat", "31", ref status);
            vvd(rmatn[2, 1], 0.4063237480216934159e-4, 1e-12,
                "wwaNumat", "32", ref status);
            vvd(rmatn[2, 2], 0.9999999991671660407, 1e-12,
                "wwaNumat", "33", ref status);

        }

        static void t_nut00a(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ n u t 0 0 a
        **  - - - - - - - - -
        **
        **  Test wwaNut00a function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaNut00a, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double dpsi = 0, deps = 0;


            WWA.wwaNut00a(2400000.5, 53736.0, ref dpsi, ref deps);

            vvd(dpsi, -0.9630909107115518431e-5, 1e-13,
                "wwaNut00a", "dpsi", ref status);
            vvd(deps, 0.4063239174001678710e-4, 1e-13,
                "wwaNut00a", "deps", ref status);

        }

        static void t_nut00b(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ n u t 0 0 b
        **  - - - - - - - - -
        **
        **  Test wwaNut00b function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaNut00b, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double dpsi = 0, deps = 0;


            WWA.wwaNut00b(2400000.5, 53736.0, ref dpsi, ref deps);

            vvd(dpsi, -0.9632552291148362783e-5, 1e-13,
                "wwaNut00b", "dpsi", ref status);
            vvd(deps, 0.4063197106621159367e-4, 1e-13,
                "wwaNut00b", "deps", ref status);

        }

        static void t_nut06a(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ n u t 0 6 a
        **  - - - - - - - - -
        **
        **  Test wwaNut06a function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaNut06a, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double dpsi = 0, deps = 0;


            WWA.wwaNut06a(2400000.5, 53736.0, ref dpsi, ref deps);

            vvd(dpsi, -0.9630912025820308797e-5, 1e-13,
                "wwaNut06a", "dpsi", ref status);
            vvd(deps, 0.4063238496887249798e-4, 1e-13,
                "wwaNut06a", "deps", ref status);

        }

        static void t_nut80(ref int status)
        /*
        **  - - - - - - - -
        **   t _ n u t 8 0
        **  - - - - - - - -
        **
        **  Test wwaNut80 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaNut80, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double dpsi = 0, deps = 0;


            WWA.wwaNut80(2400000.5, 53736.0, ref dpsi, ref deps);

            vvd(dpsi, -0.9643658353226563966e-5, 1e-13,
                "wwaNut80", "dpsi", ref status);
            vvd(deps, 0.4060051006879713322e-4, 1e-13,
                "wwaNut80", "deps", ref status);

        }

        static void t_nutm80(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ n u t m 8 0
        **  - - - - - - - - -
        **
        **  Test wwaNutm80 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaNutm80, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] rmatn = new double[3, 3];


            WWA.wwaNutm80(2400000.5, 53736.0, rmatn);

            vvd(rmatn[0, 0], 0.9999999999534999268, 1e-12,
               "wwaNutm80", "11", ref status);
            vvd(rmatn[0, 1], 0.8847935789636432161e-5, 1e-12,
               "wwaNutm80", "12", ref status);
            vvd(rmatn[0, 2], 0.3835906502164019142e-5, 1e-12,
               "wwaNutm80", "13", ref status);

            vvd(rmatn[1, 0], -0.8847780042583435924e-5, 1e-12,
               "wwaNutm80", "21", ref status);
            vvd(rmatn[1, 1], 0.9999999991366569963, 1e-12,
               "wwaNutm80", "22", ref status);
            vvd(rmatn[1, 2], -0.4060052702727130809e-4, 1e-12,
               "wwaNutm80", "23", ref status);

            vvd(rmatn[2, 0], -0.3836265729708478796e-5, 1e-12,
               "wwaNutm80", "31", ref status);
            vvd(rmatn[2, 1], 0.4060049308612638555e-4, 1e-12,
               "wwaNutm80", "32", ref status);
            vvd(rmatn[2, 2], 0.9999999991684415129, 1e-12,
               "wwaNutm80", "33", ref status);

        }

        static void t_obl06(ref int status)
        /*
        **  - - - - - - - -
        **   t _ o b l 0 6
        **  - - - - - - - -
        **
        **  Test wwaObl06 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaObl06, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            vvd(WWA.wwaObl06(2400000.5, 54388.0), 0.4090749229387258204, 1e-14,
               "wwaObl06", "", ref status);
        }

        static void t_obl80(ref int status)
        /*
        **  - - - - - - - -
        **   t _ o b l 8 0
        **  - - - - - - - -
        **
        **  Test wwaObl80 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaObl80, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double eps0;


            eps0 = WWA.wwaObl80(2400000.5, 54388.0);

            vvd(eps0, 0.4090751347643816218, 1e-14, "wwaObl80", "", ref status);

        }

        static void t_p06e(ref int status)
        /*
        **  - - - - - - -
        **   t _ p 0 6 e
        **  - - - - - - -
        **
        **  Test wwaP06e function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaP06e, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double eps0 = 0, psia = 0, oma = 0, bpa = 0, bqa = 0, pia = 0, bpia = 0,
                   epsa = 0, chia = 0, za = 0, zetaa = 0, thetaa = 0, pa = 0, gam = 0, phi = 0, psi = 0;


            WWA.wwaP06e(2400000.5, 52541.0, ref eps0, ref psia, ref oma, ref bpa,
                   ref bqa, ref pia, ref bpia, ref epsa, ref chia, ref za,
                   ref zetaa, ref thetaa, ref pa, ref gam, ref phi, ref psi);

            vvd(eps0, 0.4090926006005828715, 1e-14,
                "wwaP06e", "eps0", ref status);
            vvd(psia, 0.6664369630191613431e-3, 1e-14,
                "wwaP06e", "psia", ref status);
            vvd(oma, 0.4090925973783255982, 1e-14,
                "wwaP06e", "oma", ref status);
            vvd(bpa, 0.5561149371265209445e-6, 1e-14,
                "wwaP06e", "bpa", ref status);
            vvd(bqa, -0.6191517193290621270e-5, 1e-14,
                "wwaP06e", "bqa", ref status);
            vvd(pia, 0.6216441751884382923e-5, 1e-14,
                "wwaP06e", "pia", ref status);
            vvd(bpia, 3.052014180023779882, 1e-14,
                "wwaP06e", "bpia", ref status);
            vvd(epsa, 0.4090864054922431688, 1e-14,
                "wwaP06e", "epsa", ref status);
            vvd(chia, 0.1387703379530915364e-5, 1e-14,
                "wwaP06e", "chia", ref status);
            vvd(za, 0.2921789846651790546e-3, 1e-14,
                "wwaP06e", "za", ref status);
            vvd(zetaa, 0.3178773290332009310e-3, 1e-14,
                "wwaP06e", "zetaa", ref status);
            vvd(thetaa, 0.2650932701657497181e-3, 1e-14,
                "wwaP06e", "thetaa", ref status);
            vvd(pa, 0.6651637681381016344e-3, 1e-14,
                "wwaP06e", "pa", ref status);
            vvd(gam, 0.1398077115963754987e-5, 1e-14,
                "wwaP06e", "gam", ref status);
            vvd(phi, 0.4090864090837462602, 1e-14,
                "wwaP06e", "phi", ref status);
            vvd(psi, 0.6664464807480920325e-3, 1e-14,
                "wwaP06e", "psi", ref status);

        }

        static void t_p2pv(ref int status)
        /*
        **  - - - - - - -
        **   t _ p 2 p v
        **  - - - - - - -
        **
        **  Test wwaP2pv function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaP2pv, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[] p = new double[3];
            double[,] pv = new double[2, 3];


            p[0] = 0.25;
            p[1] = 1.2;
            p[2] = 3.0;

            pv[0, 0] = 0.3;
            pv[0, 1] = 1.2;
            pv[0, 2] = -2.5;

            pv[1, 0] = -0.5;
            pv[1, 1] = 3.1;
            pv[1, 2] = 0.9;

            WWA.wwaP2pv(p, pv);

            vvd(pv[0, 0], 0.25, 0.0, "wwaP2pv", "p1", ref status);
            vvd(pv[0, 1], 1.2, 0.0, "wwaP2pv", "p2", ref status);
            vvd(pv[0, 2], 3.0, 0.0, "wwaP2pv", "p3", ref status);

            vvd(pv[1, 0], 0.0, 0.0, "wwaP2pv", "v1", ref status);
            vvd(pv[1, 1], 0.0, 0.0, "wwaP2pv", "v2", ref status);
            vvd(pv[1, 2], 0.0, 0.0, "wwaP2pv", "v3", ref status);

        }

        static void t_p2s(ref int status)
        /*
        **  - - - - - -
        **   t _ p 2 s
        **  - - - - - -
        **
        **  Test wwaP2s function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaP2s, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[] p = new double[3];
            double theta = 0, phi = 0, r = 0;


            p[0] = 100.0;
            p[1] = -50.0;
            p[2] = 25.0;

            WWA.wwaP2s(p, ref theta, ref phi, ref r);

            vvd(theta, -0.4636476090008061162, 1e-12, "wwaP2s", "theta", ref status);
            vvd(phi, 0.2199879773954594463, 1e-12, "wwaP2s", "phi", ref status);
            vvd(r, 114.5643923738960002, 1e-9, "wwaP2s", "r", ref status);

        }

        static void t_pap(ref int status)
        /*
        **  - - - - - -
        **   t _ p a p
        **  - - - - - -
        **
        **  Test wwaPap function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPap, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[] a = new double[3];
            double[] b = new double[3];
            double theta;


            a[0] = 1.0;
            a[1] = 0.1;
            a[2] = 0.2;

            b[0] = -3.0;
            b[1] = 1e-3;
            b[2] = 0.2;

            theta = WWA.wwaPap(a, b);

            vvd(theta, 0.3671514267841113674, 1e-12, "wwaPap", "", ref status);

        }

        static void t_pas(ref int status)
        /*
        **  - - - - - -
        **   t _ p a s
        **  - - - - - -
        **
        **  Test wwaPas function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPas, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double al, ap, bl, bp, theta;


            al = 1.0;
            ap = 0.1;
            bl = 0.2;
            bp = -1.0;

            theta = WWA.wwaPas(al, ap, bl, bp);

            vvd(theta, -2.724544922932270424, 1e-12, "wwaPas", "", ref status);

        }

        static void t_pb06(ref int status)
        /*
        **  - - - - - - -
        **   t _ p b 0 6
        **  - - - - - - -
        **
        **  Test wwaPb06 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPb06, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double bzeta = 0, bz = 0, btheta = 0;


            WWA.wwaPb06(2400000.5, 50123.9999, ref bzeta, ref bz, ref btheta);

            vvd(bzeta, -0.5092634016326478238e-3, 1e-12,
                "wwaPb06", "bzeta", ref status);
            vvd(bz, -0.3602772060566044413e-3, 1e-12,
                "wwaPb06", "bz", ref status);
            vvd(btheta, -0.3779735537167811177e-3, 1e-12,
                "wwaPb06", "btheta", ref status);

        }

        static void t_pdp(ref int status)
        /*
        **  - - - - - -
        **   t _ p d p
        **  - - - - - -
        **
        **  Test wwaPdp function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPdp, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[] a = new double[3];
            double[] b = new double[3];
            double adb;

            a[0] = 2.0;
            a[1] = 2.0;
            a[2] = 3.0;

            b[0] = 1.0;
            b[1] = 3.0;
            b[2] = 4.0;

            adb = WWA.wwaPdp(a, b);

            vvd(adb, 20, 1e-12, "wwaPdp", "", ref status);

        }

        static void t_pfw06(ref int status)
        /*
        **  - - - - - - - -
        **   t _ p f w 0 6
        **  - - - - - - - -
        **
        **  Test wwaPfw06 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPfw06, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double gamb = 0, phib = 0, psib = 0, epsa = 0;


            WWA.wwaPfw06(2400000.5, 50123.9999, ref gamb, ref phib, ref psib, ref epsa);

            vvd(gamb, -0.2243387670997995690e-5, 1e-16,
                "wwaPfw06", "gamb", ref status);
            vvd(phib, 0.4091014602391312808, 1e-12,
                "wwaPfw06", "phib", ref status);
            vvd(psib, -0.9501954178013031895e-3, 1e-14,
                "wwaPfw06", "psib", ref status);
            vvd(epsa, 0.4091014316587367491, 1e-12,
                "wwaPfw06", "epsa", ref status);

        }

        static void t_plan94(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ p l a n 9 4
        **  - - - - - - - - -
        **
        **  Test wwaPlan94 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPlan94, vvd, viv
        **
        **  This revision:  2013 October 2
        */
        {
            double[,] pv = new double[2, 3];
            int j;

            j = WWA.wwaPlan94(2400000.5, 1e6, 0, pv);

            vvd(pv[0, 0], 0.0, 0.0, "wwaPlan94", "x 1", ref status);
            vvd(pv[0, 1], 0.0, 0.0, "wwaPlan94", "y 1", ref status);
            vvd(pv[0, 2], 0.0, 0.0, "wwaPlan94", "z 1", ref status);

            vvd(pv[1, 0], 0.0, 0.0, "wwaPlan94", "xd 1", ref status);
            vvd(pv[1, 1], 0.0, 0.0, "wwaPlan94", "yd 1", ref status);
            vvd(pv[1, 2], 0.0, 0.0, "wwaPlan94", "zd 1", ref status);

            viv(j, -1, "wwaPlan94", "j 1", ref status);

            j = WWA.wwaPlan94(2400000.5, 1e6, 10, pv);

            viv(j, -1, "wwaPlan94", "j 2", ref status);

            j = WWA.wwaPlan94(2400000.5, -320000, 3, pv);

            vvd(pv[0, 0], 0.9308038666832975759, 1e-11,
                "wwaPlan94", "x 3", ref status);
            vvd(pv[0, 1], 0.3258319040261346000, 1e-11,
                "wwaPlan94", "y 3", ref status);
            vvd(pv[0, 2], 0.1422794544481140560, 1e-11,
                "wwaPlan94", "z 3", ref status);

            vvd(pv[1, 0], -0.6429458958255170006e-2, 1e-11,
                "wwaPlan94", "xd 3", ref status);
            vvd(pv[1, 1], 0.1468570657704237764e-1, 1e-11,
                "wwaPlan94", "yd 3", ref status);
            vvd(pv[1, 2], 0.6406996426270981189e-2, 1e-11,
                "wwaPlan94", "zd 3", ref status);

            viv(j, 1, "wwaPlan94", "j 3", ref status);

            j = WWA.wwaPlan94(2400000.5, 43999.9, 1, pv);

            vvd(pv[0, 0], 0.2945293959257430832, 1e-11,
                "wwaPlan94", "x 4", ref status);
            vvd(pv[0, 1], -0.2452204176601049596, 1e-11,
                "wwaPlan94", "y 4", ref status);
            vvd(pv[0, 2], -0.1615427700571978153, 1e-11,
                "wwaPlan94", "z 4", ref status);

            vvd(pv[1, 0], 0.1413867871404614441e-1, 1e-11,
                "wwaPlan94", "xd 4", ref status);
            vvd(pv[1, 1], 0.1946548301104706582e-1, 1e-11,
                "wwaPlan94", "yd 4", ref status);
            vvd(pv[1, 2], 0.8929809783898904786e-2, 1e-11,
                "wwaPlan94", "zd 4", ref status);

            viv(j, 0, "wwaPlan94", "j 4", ref status);

        }

        static void t_pmat00(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ p m a t 0 0
        **  - - - - - - - - -
        **
        **  Test wwaPmat00 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPmat00, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] rbp = new double[3, 3];

            WWA.wwaPmat00(2400000.5, 50123.9999, rbp);

            vvd(rbp[0, 0], 0.9999995505175087260, 1e-12,
                "wwaPmat00", "11", ref status);
            vvd(rbp[0, 1], 0.8695405883617884705e-3, 1e-14,
                "wwaPmat00", "12", ref status);
            vvd(rbp[0, 2], 0.3779734722239007105e-3, 1e-14,
                "wwaPmat00", "13", ref status);

            vvd(rbp[1, 0], -0.8695405990410863719e-3, 1e-14,
                "wwaPmat00", "21", ref status);
            vvd(rbp[1, 1], 0.9999996219494925900, 1e-12,
                "wwaPmat00", "22", ref status);
            vvd(rbp[1, 2], -0.1360775820404982209e-6, 1e-14,
                "wwaPmat00", "23", ref status);

            vvd(rbp[2, 0], -0.3779734476558184991e-3, 1e-14,
                "wwaPmat00", "31", ref status);
            vvd(rbp[2, 1], -0.1925857585832024058e-6, 1e-14,
                "wwaPmat00", "32", ref status);
            vvd(rbp[2, 2], 0.9999999285680153377, 1e-12,
                "wwaPmat00", "33", ref status);

        }

        static void t_pmat06(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ p m a t 0 6
        **  - - - - - - - - -
        **
        **  Test wwaPmat06 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPmat06, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] rbp = new double[3, 3];

            WWA.wwaPmat06(2400000.5, 50123.9999, rbp);

            vvd(rbp[0, 0], 0.9999995505176007047, 1e-12,
                "wwaPmat06", "11", ref status);
            vvd(rbp[0, 1], 0.8695404617348208406e-3, 1e-14,
                "wwaPmat06", "12", ref status);
            vvd(rbp[0, 2], 0.3779735201865589104e-3, 1e-14,
                "wwaPmat06", "13", ref status);

            vvd(rbp[1, 0], -0.8695404723772031414e-3, 1e-14,
                "wwaPmat06", "21", ref status);
            vvd(rbp[1, 1], 0.9999996219496027161, 1e-12,
                "wwaPmat06", "22", ref status);
            vvd(rbp[1, 2], -0.1361752497080270143e-6, 1e-14,
                "wwaPmat06", "23", ref status);

            vvd(rbp[2, 0], -0.3779734957034089490e-3, 1e-14,
                "wwaPmat06", "31", ref status);
            vvd(rbp[2, 1], -0.1924880847894457113e-6, 1e-14,
                "wwaPmat06", "32", ref status);
            vvd(rbp[2, 2], 0.9999999285679971958, 1e-12,
                "wwaPmat06", "33", ref status);

        }

        static void t_pmat76(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ p m a t 7 6
        **  - - - - - - - - -
        **
        **  Test wwaPmat76 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPmat76, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] rmatp = new double[3, 3];

            WWA.wwaPmat76(2400000.5, 50123.9999, rmatp);

            vvd(rmatp[0, 0], 0.9999995504328350733, 1e-12,
                "wwaPmat76", "11", ref status);
            vvd(rmatp[0, 1], 0.8696632209480960785e-3, 1e-14,
                "wwaPmat76", "12", ref status);
            vvd(rmatp[0, 2], 0.3779153474959888345e-3, 1e-14,
                "wwaPmat76", "13", ref status);

            vvd(rmatp[1, 0], -0.8696632209485112192e-3, 1e-14,
                "wwaPmat76", "21", ref status);
            vvd(rmatp[1, 1], 0.9999996218428560614, 1e-12,
                "wwaPmat76", "22", ref status);
            vvd(rmatp[1, 2], -0.1643284776111886407e-6, 1e-14,
                "wwaPmat76", "23", ref status);

            vvd(rmatp[2, 0], -0.3779153474950335077e-3, 1e-14,
                "wwaPmat76", "31", ref status);
            vvd(rmatp[2, 1], -0.1643306746147366896e-6, 1e-14,
                "wwaPmat76", "32", ref status);
            vvd(rmatp[2, 2], 0.9999999285899790119, 1e-12,
                "wwaPmat76", "33", ref status);

        }

        static void t_pm(ref int status)
        /*
        **  - - - - -
        **   t _ p m
        **  - - - - -
        **
        **  Test wwaPm function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPm, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[] p = new double[3];
            double r;


            p[0] = 0.3;
            p[1] = 1.2;
            p[2] = -2.5;

            r = WWA.wwaPm(p);

            vvd(r, 2.789265136196270604, 1e-12, "wwaPm", "", ref status);

        }

        static void t_pmp(ref int status)
        /*
        **  - - - - - -
        **   t _ p m p
        **  - - - - - -
        **
        **  Test wwaPmp function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPmp, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[] a = new double[3];
            double[] b = new double[3];
            double[] amb = new double[3];


            a[0] = 2.0;
            a[1] = 2.0;
            a[2] = 3.0;

            b[0] = 1.0;
            b[1] = 3.0;
            b[2] = 4.0;

            WWA.wwaPmp(a, b, amb);

            vvd(amb[0], 1.0, 1e-12, "wwaPmp", "0", ref status);
            vvd(amb[1], -1.0, 1e-12, "wwaPmp", "1", ref status);
            vvd(amb[2], -1.0, 1e-12, "wwaPmp", "2", ref status);

        }

        static void t_pmpx(ref int status)
        /*
        **  - - - - - - -
        **   t _ p m p x
        **  - - - - - - -
        **
        **  Test wwaPmpx function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPmpx, vvd
        **
        **  This revision:  2013 October 2
        */
        {
            double rc, dc, pr, pd, px, rv, pmt;
            double[] pob = new double[3];
            double[] pco = new double[3];


            rc = 1.234;
            dc = 0.789;
            pr = 1e-5;
            pd = -2e-5;
            px = 1e-2;
            rv = 10.0;
            pmt = 8.75;
            pob[0] = 0.9;
            pob[1] = 0.4;
            pob[2] = 0.1;

            WWA.wwaPmpx(rc, dc, pr, pd, px, rv, pmt, pob, pco);

            //vvd(pco[0], 0.2328137623960308440, 1e-12, "wwaPmpx", "1", ref status);
            //vvd(pco[1], 0.6651097085397855317, 1e-12, "wwaPmpx", "2", ref status);
            //vvd(pco[2], 0.7095257765896359847, 1e-12, "wwaPmpx", "3", ref status);
            vvd(pco[0], 0.2328137623960308438, 1e-12, "wwaPmpx", "1", ref status);
            vvd(pco[1], 0.6651097085397855328, 1e-12, "wwaPmpx", "2", ref status);
            vvd(pco[2], 0.7095257765896359837, 1e-12, "wwaPmpx", "3", ref status);

        }

        static void t_pmsafe(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ p m s a f e
        **  - - - - - - - - -
        **
        **  Test wwaPmsafe function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPmsafe, vvd, viv
        **
        **  This revision:  2013 October 2
        */
        {
            int j;
            double ra1, dec1, pmr1, pmd1, px1, rv1, ep1a, ep1b, ep2a, ep2b,
                   ra2 = 0, dec2 = 0, pmr2 = 0, pmd2 = 0, px2 = 0, rv2 = 0;


            ra1 = 1.234;
            dec1 = 0.789;
            pmr1 = 1e-5;
            pmd1 = -2e-5;
            px1 = 1e-2;
            rv1 = 10.0;
            ep1a = 2400000.5;
            ep1b = 48348.5625;
            ep2a = 2400000.5;
            ep2b = 51544.5;

            j = WWA.wwaPmsafe(ra1, dec1, pmr1, pmd1, px1, rv1,
                          ep1a, ep1b, ep2a, ep2b,
                          ref ra2, ref dec2, ref pmr2, ref pmd2, ref px2, ref rv2);

            vvd(ra2, 1.234087484501017061, 1e-12,
                     "wwaPmsafe", "ra2", ref status);
            //vvd(dec2, 0.7888249982450468574, 1e-12,
            //         "wwaPmsafe", "dec2", ref status);
            vvd(dec2, 0.7888249982450468567, 1e-12,
                    "wwaPmsafe", "dec2", ref status);
            vvd(pmr2, 0.9996457663586073988e-5, 1e-12,
                      "wwaPmsafe", "pmr2", ref status);
            //vvd(pmd2, -0.2000040085106737816e-4, 1e-16,
            //          "wwaPmsafe", "pmd2", ref status);
            //vvd(px2, 0.9999997295356765185e-2, 1e-12,
            //         "wwaPmsafe", "px2", ref status);
            //vvd(rv2, 10.38468380113917014, 1e-10,
            //         "wwaPmsafe", "rv2", ref status);
            vvd(pmd2, -0.2000040085106754565e-4, 1e-16,
                    "wwaPmsafe", "pmd2", ref status);
            vvd(px2, 0.9999997295356830666e-2, 1e-12,
                     "wwaPmsafe", "px2", ref status);
            vvd(rv2, 10.38468380293920069, 1e-10,
                     "wwaPmsafe", "rv2", ref status);

            viv(j, 0, "wwaPmsafe", "j", ref status);

        }

        static void t_pn(ref int status)
        /*
        **  - - - - -
        **   t _ p n
        **  - - - - -
        **
        **  Test wwaPn function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPn, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[] p = new double[3];
            double r = 0;
            double[] u = new double[3];


            p[0] = 0.3;
            p[1] = 1.2;
            p[2] = -2.5;

            WWA.wwaPn(p, ref r, u);

            vvd(r, 2.789265136196270604, 1e-12, "wwaPn", "r", ref status);

            vvd(u[0], 0.1075552109073112058, 1e-12, "wwaPn", "u1", ref status);
            vvd(u[1], 0.4302208436292448232, 1e-12, "wwaPn", "u2", ref status);
            vvd(u[2], -0.8962934242275933816, 1e-12, "wwaPn", "u3", ref status);

        }

        static void t_pn00(ref int status)
        /*
        **  - - - - - - -
        **   t _ p n 0 0
        **  - - - - - - -
        **
        **  Test wwaPn00 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPn00, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double dpsi, deps, epsa = 0;
            double[,] rb = new double[3, 3];
            double[,] rp = new double[3, 3];
            double[,] rbp = new double[3, 3];
            double[,] rn = new double[3, 3];
            double[,] rbpn = new double[3, 3];

            dpsi = -0.9632552291149335877e-5;
            deps = 0.4063197106621141414e-4;

            WWA.wwaPn00(2400000.5, 53736.0, dpsi, deps,
                    ref epsa, rb, rp, rbp, rn, rbpn);

            vvd(epsa, 0.4090791789404229916, 1e-12, "wwaPn00", "epsa", ref status);

            vvd(rb[0, 0], 0.9999999999999942498, 1e-12,
                "wwaPn00", "rb11", ref status);
            vvd(rb[0, 1], -0.7078279744199196626e-7, 1e-18,
                "wwaPn00", "rb12", ref status);
            vvd(rb[0, 2], 0.8056217146976134152e-7, 1e-18,
                "wwaPn00", "rb13", ref status);

            vvd(rb[1, 0], 0.7078279477857337206e-7, 1e-18,
                "wwaPn00", "rb21", ref status);
            vvd(rb[1, 1], 0.9999999999999969484, 1e-12,
                "wwaPn00", "rb22", ref status);
            vvd(rb[1, 2], 0.3306041454222136517e-7, 1e-18,
                "wwaPn00", "rb23", ref status);

            vvd(rb[2, 0], -0.8056217380986972157e-7, 1e-18,
                "wwaPn00", "rb31", ref status);
            vvd(rb[2, 1], -0.3306040883980552500e-7, 1e-18,
                "wwaPn00", "rb32", ref status);
            vvd(rb[2, 2], 0.9999999999999962084, 1e-12,
                "wwaPn00", "rb33", ref status);

            vvd(rp[0, 0], 0.9999989300532289018, 1e-12,
                "wwaPn00", "rp11", ref status);
            vvd(rp[0, 1], -0.1341647226791824349e-2, 1e-14,
                "wwaPn00", "rp12", ref status);
            vvd(rp[0, 2], -0.5829880927190296547e-3, 1e-14,
                "wwaPn00", "rp13", ref status);

            vvd(rp[1, 0], 0.1341647231069759008e-2, 1e-14,
                "wwaPn00", "rp21", ref status);
            vvd(rp[1, 1], 0.9999990999908750433, 1e-12,
                "wwaPn00", "rp22", ref status);
            vvd(rp[1, 2], -0.3837444441583715468e-6, 1e-14,
                "wwaPn00", "rp23", ref status);

            vvd(rp[2, 0], 0.5829880828740957684e-3, 1e-14,
                "wwaPn00", "rp31", ref status);
            vvd(rp[2, 1], -0.3984203267708834759e-6, 1e-14,
                "wwaPn00", "rp32", ref status);
            vvd(rp[2, 2], 0.9999998300623538046, 1e-12,
                "wwaPn00", "rp33", ref status);

            vvd(rbp[0, 0], 0.9999989300052243993, 1e-12,
                "wwaPn00", "rbp11", ref status);
            vvd(rbp[0, 1], -0.1341717990239703727e-2, 1e-14,
                "wwaPn00", "rbp12", ref status);
            vvd(rbp[0, 2], -0.5829075749891684053e-3, 1e-14,
                "wwaPn00", "rbp13", ref status);

            vvd(rbp[1, 0], 0.1341718013831739992e-2, 1e-14,
                "wwaPn00", "rbp21", ref status);
            vvd(rbp[1, 1], 0.9999990998959191343, 1e-12,
                "wwaPn00", "rbp22", ref status);
            vvd(rbp[1, 2], -0.3505759733565421170e-6, 1e-14,
                "wwaPn00", "rbp23", ref status);

            vvd(rbp[2, 0], 0.5829075206857717883e-3, 1e-14,
                "wwaPn00", "rbp31", ref status);
            vvd(rbp[2, 1], -0.4315219955198608970e-6, 1e-14,
                "wwaPn00", "rbp32", ref status);
            vvd(rbp[2, 2], 0.9999998301093036269, 1e-12,
                "wwaPn00", "rbp33", ref status);

            vvd(rn[0, 0], 0.9999999999536069682, 1e-12,
                "wwaPn00", "rn11", ref status);
            vvd(rn[0, 1], 0.8837746144872140812e-5, 1e-16,
                "wwaPn00", "rn12", ref status);
            vvd(rn[0, 2], 0.3831488838252590008e-5, 1e-16,
                "wwaPn00", "rn13", ref status);

            vvd(rn[1, 0], -0.8837590456633197506e-5, 1e-16,
                "wwaPn00", "rn21", ref status);
            vvd(rn[1, 1], 0.9999999991354692733, 1e-12,
                "wwaPn00", "rn22", ref status);
            vvd(rn[1, 2], -0.4063198798559573702e-4, 1e-16,
                "wwaPn00", "rn23", ref status);

            vvd(rn[2, 0], -0.3831847930135328368e-5, 1e-16,
                "wwaPn00", "rn31", ref status);
            vvd(rn[2, 1], 0.4063195412258150427e-4, 1e-16,
                "wwaPn00", "rn32", ref status);
            vvd(rn[2, 2], 0.9999999991671806225, 1e-12,
                "wwaPn00", "rn33", ref status);

            vvd(rbpn[0, 0], 0.9999989440499982806, 1e-12,
                "wwaPn00", "rbpn11", ref status);
            vvd(rbpn[0, 1], -0.1332880253640848301e-2, 1e-14,
                "wwaPn00", "rbpn12", ref status);
            vvd(rbpn[0, 2], -0.5790760898731087295e-3, 1e-14,
                "wwaPn00", "rbpn13", ref status);

            vvd(rbpn[1, 0], 0.1332856746979948745e-2, 1e-14,
                "wwaPn00", "rbpn21", ref status);
            vvd(rbpn[1, 1], 0.9999991109064768883, 1e-12,
                "wwaPn00", "rbpn22", ref status);
            vvd(rbpn[1, 2], -0.4097740555723063806e-4, 1e-14,
                "wwaPn00", "rbpn23", ref status);

            vvd(rbpn[2, 0], 0.5791301929950205000e-3, 1e-14,
                "wwaPn00", "rbpn31", ref status);
            vvd(rbpn[2, 1], 0.4020553681373702931e-4, 1e-14,
                "wwaPn00", "rbpn32", ref status);
            vvd(rbpn[2, 2], 0.9999998314958529887, 1e-12,
                "wwaPn00", "rbpn33", ref status);

        }

        static void t_pn00a(ref int status)
        /*
        **  - - - - - - - -
        **   t _ p n 0 0 a
        **  - - - - - - - -
        **
        **  Test wwaPn00a function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPn00a, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double dpsi = 0, deps = 0, epsa = 0;
            double[,] rb = new double[3, 3];
            double[,] rp = new double[3, 3];
            double[,] rbp = new double[3, 3];
            double[,] rn = new double[3, 3];
            double[,] rbpn = new double[3, 3];


            WWA.wwaPn00a(2400000.5, 53736.0,
                     ref dpsi, ref deps, ref epsa, rb, rp, rbp, rn, rbpn);

            vvd(dpsi, -0.9630909107115518431e-5, 1e-12,
                "wwaPn00a", "dpsi", ref status);
            vvd(deps, 0.4063239174001678710e-4, 1e-12,
                "wwaPn00a", "deps", ref status);
            vvd(epsa, 0.4090791789404229916, 1e-12, "wwaPn00a", "epsa", ref status);

            vvd(rb[0, 0], 0.9999999999999942498, 1e-12,
                "wwaPn00a", "rb11", ref status);
            vvd(rb[0, 1], -0.7078279744199196626e-7, 1e-16,
                "wwaPn00a", "rb12", ref status);
            vvd(rb[0, 2], 0.8056217146976134152e-7, 1e-16,
                "wwaPn00a", "rb13", ref status);

            vvd(rb[1, 0], 0.7078279477857337206e-7, 1e-16,
                "wwaPn00a", "rb21", ref status);
            vvd(rb[1, 1], 0.9999999999999969484, 1e-12,
                "wwaPn00a", "rb22", ref status);
            vvd(rb[1, 2], 0.3306041454222136517e-7, 1e-16,
                "wwaPn00a", "rb23", ref status);

            vvd(rb[2, 0], -0.8056217380986972157e-7, 1e-16,
                "wwaPn00a", "rb31", ref status);
            vvd(rb[2, 1], -0.3306040883980552500e-7, 1e-16,
                "wwaPn00a", "rb32", ref status);
            vvd(rb[2, 2], 0.9999999999999962084, 1e-12,
                "wwaPn00a", "rb33", ref status);

            vvd(rp[0, 0], 0.9999989300532289018, 1e-12,
                "wwaPn00a", "rp11", ref status);
            vvd(rp[0, 1], -0.1341647226791824349e-2, 1e-14,
                "wwaPn00a", "rp12", ref status);
            vvd(rp[0, 2], -0.5829880927190296547e-3, 1e-14,
                "wwaPn00a", "rp13", ref status);

            vvd(rp[1, 0], 0.1341647231069759008e-2, 1e-14,
                "wwaPn00a", "rp21", ref status);
            vvd(rp[1, 1], 0.9999990999908750433, 1e-12,
                "wwaPn00a", "rp22", ref status);
            vvd(rp[1, 2], -0.3837444441583715468e-6, 1e-14,
                "wwaPn00a", "rp23", ref status);

            vvd(rp[2, 0], 0.5829880828740957684e-3, 1e-14,
                "wwaPn00a", "rp31", ref status);
            vvd(rp[2, 1], -0.3984203267708834759e-6, 1e-14,
                "wwaPn00a", "rp32", ref status);
            vvd(rp[2, 2], 0.9999998300623538046, 1e-12,
                "wwaPn00a", "rp33", ref status);

            vvd(rbp[0, 0], 0.9999989300052243993, 1e-12,
                "wwaPn00a", "rbp11", ref status);
            vvd(rbp[0, 1], -0.1341717990239703727e-2, 1e-14,
                "wwaPn00a", "rbp12", ref status);
            vvd(rbp[0, 2], -0.5829075749891684053e-3, 1e-14,
                "wwaPn00a", "rbp13", ref status);

            vvd(rbp[1, 0], 0.1341718013831739992e-2, 1e-14,
                "wwaPn00a", "rbp21", ref status);
            vvd(rbp[1, 1], 0.9999990998959191343, 1e-12,
                "wwaPn00a", "rbp22", ref status);
            vvd(rbp[1, 2], -0.3505759733565421170e-6, 1e-14,
                "wwaPn00a", "rbp23", ref status);

            vvd(rbp[2, 0], 0.5829075206857717883e-3, 1e-14,
                "wwaPn00a", "rbp31", ref status);
            vvd(rbp[2, 1], -0.4315219955198608970e-6, 1e-14,
                "wwaPn00a", "rbp32", ref status);
            vvd(rbp[2, 2], 0.9999998301093036269, 1e-12,
                "wwaPn00a", "rbp33", ref status);

            vvd(rn[0, 0], 0.9999999999536227949, 1e-12,
                "wwaPn00a", "rn11", ref status);
            vvd(rn[0, 1], 0.8836238544090873336e-5, 1e-14,
                "wwaPn00a", "rn12", ref status);
            vvd(rn[0, 2], 0.3830835237722400669e-5, 1e-14,
                "wwaPn00a", "rn13", ref status);

            vvd(rn[1, 0], -0.8836082880798569274e-5, 1e-14,
                "wwaPn00a", "rn21", ref status);
            vvd(rn[1, 1], 0.9999999991354655028, 1e-12,
                "wwaPn00a", "rn22", ref status);
            vvd(rn[1, 2], -0.4063240865362499850e-4, 1e-14,
                "wwaPn00a", "rn23", ref status);

            vvd(rn[2, 0], -0.3831194272065995866e-5, 1e-14,
                "wwaPn00a", "rn31", ref status);
            vvd(rn[2, 1], 0.4063237480216291775e-4, 1e-14,
                "wwaPn00a", "rn32", ref status);
            vvd(rn[2, 2], 0.9999999991671660338, 1e-12,
                "wwaPn00a", "rn33", ref status);

            vvd(rbpn[0, 0], 0.9999989440476103435, 1e-12,
                "wwaPn00a", "rbpn11", ref status);
            vvd(rbpn[0, 1], -0.1332881761240011763e-2, 1e-14,
                "wwaPn00a", "rbpn12", ref status);
            vvd(rbpn[0, 2], -0.5790767434730085751e-3, 1e-14,
                "wwaPn00a", "rbpn13", ref status);

            vvd(rbpn[1, 0], 0.1332858254308954658e-2, 1e-14,
                "wwaPn00a", "rbpn21", ref status);
            vvd(rbpn[1, 1], 0.9999991109044505577, 1e-12,
                "wwaPn00a", "rbpn22", ref status);
            vvd(rbpn[1, 2], -0.4097782710396580452e-4, 1e-14,
                "wwaPn00a", "rbpn23", ref status);

            vvd(rbpn[2, 0], 0.5791308472168152904e-3, 1e-14,
                "wwaPn00a", "rbpn31", ref status);
            vvd(rbpn[2, 1], 0.4020595661591500259e-4, 1e-14,
                "wwaPn00a", "rbpn32", ref status);
            vvd(rbpn[2, 2], 0.9999998314954572304, 1e-12,
                "wwaPn00a", "rbpn33", ref status);

        }

        static void t_pn00b(ref int status)
        /*
        **  - - - - - - - -
        **   t _ p n 0 0 b
        **  - - - - - - - -
        **
        **  Test wwaPn00b function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPn00b, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double dpsi = 0, deps = 0, epsa = 0;
            double[,] rb = new double[3, 3];
            double[,] rp = new double[3, 3];
            double[,] rbp = new double[3, 3];
            double[,] rn = new double[3, 3];
            double[,] rbpn = new double[3, 3];


            WWA.wwaPn00b(2400000.5, 53736.0, ref dpsi, ref deps, ref epsa,
                     rb, rp, rbp, rn, rbpn);

            vvd(dpsi, -0.9632552291148362783e-5, 1e-12,
                "wwaPn00b", "dpsi", ref status);
            vvd(deps, 0.4063197106621159367e-4, 1e-12,
                "wwaPn00b", "deps", ref status);
            vvd(epsa, 0.4090791789404229916, 1e-12, "wwaPn00b", "epsa", ref status);

            vvd(rb[0, 0], 0.9999999999999942498, 1e-12,
               "wwaPn00b", "rb11", ref status);
            vvd(rb[0, 1], -0.7078279744199196626e-7, 1e-16,
               "wwaPn00b", "rb12", ref status);
            vvd(rb[0, 2], 0.8056217146976134152e-7, 1e-16,
               "wwaPn00b", "rb13", ref status);

            vvd(rb[1, 0], 0.7078279477857337206e-7, 1e-16,
               "wwaPn00b", "rb21", ref status);
            vvd(rb[1, 1], 0.9999999999999969484, 1e-12,
               "wwaPn00b", "rb22", ref status);
            vvd(rb[1, 2], 0.3306041454222136517e-7, 1e-16,
               "wwaPn00b", "rb23", ref status);

            vvd(rb[2, 0], -0.8056217380986972157e-7, 1e-16,
               "wwaPn00b", "rb31", ref status);
            vvd(rb[2, 1], -0.3306040883980552500e-7, 1e-16,
               "wwaPn00b", "rb32", ref status);
            vvd(rb[2, 2], 0.9999999999999962084, 1e-12,
               "wwaPn00b", "rb33", ref status);

            vvd(rp[0, 0], 0.9999989300532289018, 1e-12,
               "wwaPn00b", "rp11", ref status);
            vvd(rp[0, 1], -0.1341647226791824349e-2, 1e-14,
               "wwaPn00b", "rp12", ref status);
            vvd(rp[0, 2], -0.5829880927190296547e-3, 1e-14,
               "wwaPn00b", "rp13", ref status);

            vvd(rp[1, 0], 0.1341647231069759008e-2, 1e-14,
               "wwaPn00b", "rp21", ref status);
            vvd(rp[1, 1], 0.9999990999908750433, 1e-12,
               "wwaPn00b", "rp22", ref status);
            vvd(rp[1, 2], -0.3837444441583715468e-6, 1e-14,
               "wwaPn00b", "rp23", ref status);

            vvd(rp[2, 0], 0.5829880828740957684e-3, 1e-14,
               "wwaPn00b", "rp31", ref status);
            vvd(rp[2, 1], -0.3984203267708834759e-6, 1e-14,
               "wwaPn00b", "rp32", ref status);
            vvd(rp[2, 2], 0.9999998300623538046, 1e-12,
               "wwaPn00b", "rp33", ref status);

            vvd(rbp[0, 0], 0.9999989300052243993, 1e-12,
               "wwaPn00b", "rbp11", ref status);
            vvd(rbp[0, 1], -0.1341717990239703727e-2, 1e-14,
               "wwaPn00b", "rbp12", ref status);
            vvd(rbp[0, 2], -0.5829075749891684053e-3, 1e-14,
               "wwaPn00b", "rbp13", ref status);

            vvd(rbp[1, 0], 0.1341718013831739992e-2, 1e-14,
               "wwaPn00b", "rbp21", ref status);
            vvd(rbp[1, 1], 0.9999990998959191343, 1e-12,
               "wwaPn00b", "rbp22", ref status);
            vvd(rbp[1, 2], -0.3505759733565421170e-6, 1e-14,
               "wwaPn00b", "rbp23", ref status);

            vvd(rbp[2, 0], 0.5829075206857717883e-3, 1e-14,
               "wwaPn00b", "rbp31", ref status);
            vvd(rbp[2, 1], -0.4315219955198608970e-6, 1e-14,
               "wwaPn00b", "rbp32", ref status);
            vvd(rbp[2, 2], 0.9999998301093036269, 1e-12,
               "wwaPn00b", "rbp33", ref status);

            vvd(rn[0, 0], 0.9999999999536069682, 1e-12,
               "wwaPn00b", "rn11", ref status);
            vvd(rn[0, 1], 0.8837746144871248011e-5, 1e-14,
               "wwaPn00b", "rn12", ref status);
            vvd(rn[0, 2], 0.3831488838252202945e-5, 1e-14,
               "wwaPn00b", "rn13", ref status);

            vvd(rn[1, 0], -0.8837590456632304720e-5, 1e-14,
               "wwaPn00b", "rn21", ref status);
            vvd(rn[1, 1], 0.9999999991354692733, 1e-12,
               "wwaPn00b", "rn22", ref status);
            vvd(rn[1, 2], -0.4063198798559591654e-4, 1e-14,
               "wwaPn00b", "rn23", ref status);

            vvd(rn[2, 0], -0.3831847930134941271e-5, 1e-14,
               "wwaPn00b", "rn31", ref status);
            vvd(rn[2, 1], 0.4063195412258168380e-4, 1e-14,
               "wwaPn00b", "rn32", ref status);
            vvd(rn[2, 2], 0.9999999991671806225, 1e-12,
               "wwaPn00b", "rn33", ref status);

            vvd(rbpn[0, 0], 0.9999989440499982806, 1e-12,
               "wwaPn00b", "rbpn11", ref status);
            vvd(rbpn[0, 1], -0.1332880253640849194e-2, 1e-14,
               "wwaPn00b", "rbpn12", ref status);
            vvd(rbpn[0, 2], -0.5790760898731091166e-3, 1e-14,
               "wwaPn00b", "rbpn13", ref status);

            vvd(rbpn[1, 0], 0.1332856746979949638e-2, 1e-14,
               "wwaPn00b", "rbpn21", ref status);
            vvd(rbpn[1, 1], 0.9999991109064768883, 1e-12,
               "wwaPn00b", "rbpn22", ref status);
            vvd(rbpn[1, 2], -0.4097740555723081811e-4, 1e-14,
               "wwaPn00b", "rbpn23", ref status);

            vvd(rbpn[2, 0], 0.5791301929950208873e-3, 1e-14,
               "wwaPn00b", "rbpn31", ref status);
            vvd(rbpn[2, 1], 0.4020553681373720832e-4, 1e-14,
               "wwaPn00b", "rbpn32", ref status);
            vvd(rbpn[2, 2], 0.9999998314958529887, 1e-12,
               "wwaPn00b", "rbpn33", ref status);

        }

        static void t_pn06a(ref int status)
        /*
        **  - - - - - - - -
        **   t _ p n 0 6 a
        **  - - - - - - - -
        **
        **  Test wwaPn06a function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPn06a, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double dpsi = 0, deps = 0, epsa = 0;
            double[,] rb = new double[3, 3];
            double[,] rp = new double[3, 3];
            double[,] rbp = new double[3, 3];
            double[,] rn = new double[3, 3];
            double[,] rbpn = new double[3, 3];


            WWA.wwaPn06a(2400000.5, 53736.0, ref dpsi, ref deps, ref epsa,
                     rb, rp, rbp, rn, rbpn);

            vvd(dpsi, -0.9630912025820308797e-5, 1e-12,
                "wwaPn06a", "dpsi", ref status);
            vvd(deps, 0.4063238496887249798e-4, 1e-12,
                "wwaPn06a", "deps", ref status);
            vvd(epsa, 0.4090789763356509926, 1e-12, "wwaPn06a", "epsa", ref status);

            vvd(rb[0, 0], 0.9999999999999942497, 1e-12,
                "wwaPn06a", "rb11", ref status);
            vvd(rb[0, 1], -0.7078368960971557145e-7, 1e-14,
                "wwaPn06a", "rb12", ref status);
            vvd(rb[0, 2], 0.8056213977613185606e-7, 1e-14,
                "wwaPn06a", "rb13", ref status);

            vvd(rb[1, 0], 0.7078368694637674333e-7, 1e-14,
                "wwaPn06a", "rb21", ref status);
            vvd(rb[1, 1], 0.9999999999999969484, 1e-12,
                "wwaPn06a", "rb22", ref status);
            vvd(rb[1, 2], 0.3305943742989134124e-7, 1e-14,
                "wwaPn06a", "rb23", ref status);

            vvd(rb[2, 0], -0.8056214211620056792e-7, 1e-14,
                "wwaPn06a", "rb31", ref status);
            vvd(rb[2, 1], -0.3305943172740586950e-7, 1e-14,
                "wwaPn06a", "rb32", ref status);
            vvd(rb[2, 2], 0.9999999999999962084, 1e-12,
                "wwaPn06a", "rb33", ref status);

            vvd(rp[0, 0], 0.9999989300536854831, 1e-12,
                "wwaPn06a", "rp11", ref status);
            vvd(rp[0, 1], -0.1341646886204443795e-2, 1e-14,
                "wwaPn06a", "rp12", ref status);
            vvd(rp[0, 2], -0.5829880933488627759e-3, 1e-14,
                "wwaPn06a", "rp13", ref status);

            vvd(rp[1, 0], 0.1341646890569782183e-2, 1e-14,
                "wwaPn06a", "rp21", ref status);
            vvd(rp[1, 1], 0.9999990999913319321, 1e-12,
                "wwaPn06a", "rp22", ref status);
            vvd(rp[1, 2], -0.3835944216374477457e-6, 1e-14,
                "wwaPn06a", "rp23", ref status);

            vvd(rp[2, 0], 0.5829880833027867368e-3, 1e-14,
                "wwaPn06a", "rp31", ref status);
            vvd(rp[2, 1], -0.3985701514686976112e-6, 1e-14,
                "wwaPn06a", "rp32", ref status);
            vvd(rp[2, 2], 0.9999998300623534950, 1e-12,
                "wwaPn06a", "rp33", ref status);

            vvd(rbp[0, 0], 0.9999989300056797893, 1e-12,
                "wwaPn06a", "rbp11", ref status);
            vvd(rbp[0, 1], -0.1341717650545059598e-2, 1e-14,
                "wwaPn06a", "rbp12", ref status);
            vvd(rbp[0, 2], -0.5829075756493728856e-3, 1e-14,
                "wwaPn06a", "rbp13", ref status);

            vvd(rbp[1, 0], 0.1341717674223918101e-2, 1e-14,
                "wwaPn06a", "rbp21", ref status);
            vvd(rbp[1, 1], 0.9999990998963748448, 1e-12,
                "wwaPn06a", "rbp22", ref status);
            vvd(rbp[1, 2], -0.3504269280170069029e-6, 1e-14,
                "wwaPn06a", "rbp23", ref status);

            vvd(rbp[2, 0], 0.5829075211461454599e-3, 1e-14,
                "wwaPn06a", "rbp31", ref status);
            vvd(rbp[2, 1], -0.4316708436255949093e-6, 1e-14,
                "wwaPn06a", "rbp32", ref status);
            vvd(rbp[2, 2], 0.9999998301093032943, 1e-12,
                "wwaPn06a", "rbp33", ref status);

            vvd(rn[0, 0], 0.9999999999536227668, 1e-12,
                "wwaPn06a", "rn11", ref status);
            vvd(rn[0, 1], 0.8836241998111535233e-5, 1e-14,
                "wwaPn06a", "rn12", ref status);
            vvd(rn[0, 2], 0.3830834608415287707e-5, 1e-14,
                "wwaPn06a", "rn13", ref status);

            vvd(rn[1, 0], -0.8836086334870740138e-5, 1e-14,
                "wwaPn06a", "rn21", ref status);
            vvd(rn[1, 1], 0.9999999991354657474, 1e-12,
                "wwaPn06a", "rn22", ref status);
            vvd(rn[1, 2], -0.4063240188248455065e-4, 1e-14,
                "wwaPn06a", "rn23", ref status);

            vvd(rn[2, 0], -0.3831193642839398128e-5, 1e-14,
                "wwaPn06a", "rn31", ref status);
            vvd(rn[2, 1], 0.4063236803101479770e-4, 1e-14,
                "wwaPn06a", "rn32", ref status);
            vvd(rn[2, 2], 0.9999999991671663114, 1e-12,
                "wwaPn06a", "rn33", ref status);

            vvd(rbpn[0, 0], 0.9999989440480669738, 1e-12,
                "wwaPn06a", "rbpn11", ref status);
            vvd(rbpn[0, 1], -0.1332881418091915973e-2, 1e-14,
                "wwaPn06a", "rbpn12", ref status);
            vvd(rbpn[0, 2], -0.5790767447612042565e-3, 1e-14,
                "wwaPn06a", "rbpn13", ref status);

            vvd(rbpn[1, 0], 0.1332857911250989133e-2, 1e-14,
                "wwaPn06a", "rbpn21", ref status);
            vvd(rbpn[1, 1], 0.9999991109049141908, 1e-12,
                "wwaPn06a", "rbpn22", ref status);
            vvd(rbpn[1, 2], -0.4097767128546784878e-4, 1e-14,
                "wwaPn06a", "rbpn23", ref status);

            vvd(rbpn[2, 0], 0.5791308482835292617e-3, 1e-14,
                "wwaPn06a", "rbpn31", ref status);
            vvd(rbpn[2, 1], 0.4020580099454020310e-4, 1e-14,
                "wwaPn06a", "rbpn32", ref status);
            vvd(rbpn[2, 2], 0.9999998314954628695, 1e-12,
                "wwaPn06a", "rbpn33", ref status);

        }

        static void t_pn06(ref int status)
        /*
        **  - - - - - - -
        **   t _ p n 0 6
        **  - - - - - - -
        **
        **  Test wwaPn06 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPn06, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double dpsi, deps, epsa = 0;
            double[,] rb = new double[3, 3];
            double[,] rp = new double[3, 3];
            double[,] rbp = new double[3, 3];
            double[,] rn = new double[3, 3];
            double[,] rbpn = new double[3, 3];


            dpsi = -0.9632552291149335877e-5;
            deps = 0.4063197106621141414e-4;

            WWA.wwaPn06(2400000.5, 53736.0, dpsi, deps,
                    ref epsa, rb, rp, rbp, rn, rbpn);

            vvd(epsa, 0.4090789763356509926, 1e-12, "wwaPn06", "epsa", ref status);

            vvd(rb[0, 0], 0.9999999999999942497, 1e-12,
                "wwaPn06", "rb11", ref status);
            vvd(rb[0, 1], -0.7078368960971557145e-7, 1e-14,
                "wwaPn06", "rb12", ref status);
            vvd(rb[0, 2], 0.8056213977613185606e-7, 1e-14,
                "wwaPn06", "rb13", ref status);

            vvd(rb[1, 0], 0.7078368694637674333e-7, 1e-14,
                "wwaPn06", "rb21", ref status);
            vvd(rb[1, 1], 0.9999999999999969484, 1e-12,
                "wwaPn06", "rb22", ref status);
            vvd(rb[1, 2], 0.3305943742989134124e-7, 1e-14,
                "wwaPn06", "rb23", ref status);

            vvd(rb[2, 0], -0.8056214211620056792e-7, 1e-14,
                "wwaPn06", "rb31", ref status);
            vvd(rb[2, 1], -0.3305943172740586950e-7, 1e-14,
                "wwaPn06", "rb32", ref status);
            vvd(rb[2, 2], 0.9999999999999962084, 1e-12,
                "wwaPn06", "rb33", ref status);

            vvd(rp[0, 0], 0.9999989300536854831, 1e-12,
                "wwaPn06", "rp11", ref status);
            vvd(rp[0, 1], -0.1341646886204443795e-2, 1e-14,
                "wwaPn06", "rp12", ref status);
            vvd(rp[0, 2], -0.5829880933488627759e-3, 1e-14,
                "wwaPn06", "rp13", ref status);

            vvd(rp[1, 0], 0.1341646890569782183e-2, 1e-14,
                "wwaPn06", "rp21", ref status);
            vvd(rp[1, 1], 0.9999990999913319321, 1e-12,
                "wwaPn06", "rp22", ref status);
            vvd(rp[1, 2], -0.3835944216374477457e-6, 1e-14,
                "wwaPn06", "rp23", ref status);

            vvd(rp[2, 0], 0.5829880833027867368e-3, 1e-14,
                "wwaPn06", "rp31", ref status);
            vvd(rp[2, 1], -0.3985701514686976112e-6, 1e-14,
                "wwaPn06", "rp32", ref status);
            vvd(rp[2, 2], 0.9999998300623534950, 1e-12,
                "wwaPn06", "rp33", ref status);

            vvd(rbp[0, 0], 0.9999989300056797893, 1e-12,
                "wwaPn06", "rbp11", ref status);
            vvd(rbp[0, 1], -0.1341717650545059598e-2, 1e-14,
                "wwaPn06", "rbp12", ref status);
            vvd(rbp[0, 2], -0.5829075756493728856e-3, 1e-14,
                "wwaPn06", "rbp13", ref status);

            vvd(rbp[1, 0], 0.1341717674223918101e-2, 1e-14,
                "wwaPn06", "rbp21", ref status);
            vvd(rbp[1, 1], 0.9999990998963748448, 1e-12,
                "wwaPn06", "rbp22", ref status);
            vvd(rbp[1, 2], -0.3504269280170069029e-6, 1e-14,
                "wwaPn06", "rbp23", ref status);

            vvd(rbp[2, 0], 0.5829075211461454599e-3, 1e-14,
                "wwaPn06", "rbp31", ref status);
            vvd(rbp[2, 1], -0.4316708436255949093e-6, 1e-14,
                "wwaPn06", "rbp32", ref status);
            vvd(rbp[2, 2], 0.9999998301093032943, 1e-12,
                "wwaPn06", "rbp33", ref status);

            vvd(rn[0, 0], 0.9999999999536069682, 1e-12,
                "wwaPn06", "rn11", ref status);
            vvd(rn[0, 1], 0.8837746921149881914e-5, 1e-14,
                "wwaPn06", "rn12", ref status);
            vvd(rn[0, 2], 0.3831487047682968703e-5, 1e-14,
                "wwaPn06", "rn13", ref status);

            vvd(rn[1, 0], -0.8837591232983692340e-5, 1e-14,
                "wwaPn06", "rn21", ref status);
            vvd(rn[1, 1], 0.9999999991354692664, 1e-12,
                "wwaPn06", "rn22", ref status);
            vvd(rn[1, 2], -0.4063198798558931215e-4, 1e-14,
                "wwaPn06", "rn23", ref status);

            vvd(rn[2, 0], -0.3831846139597250235e-5, 1e-14,
                "wwaPn06", "rn31", ref status);
            vvd(rn[2, 1], 0.4063195412258792914e-4, 1e-14,
                "wwaPn06", "rn32", ref status);
            vvd(rn[2, 2], 0.9999999991671806293, 1e-12,
                "wwaPn06", "rn33", ref status);

            vvd(rbpn[0, 0], 0.9999989440504506688, 1e-12,
                "wwaPn06", "rbpn11", ref status);
            vvd(rbpn[0, 1], -0.1332879913170492655e-2, 1e-14,
                "wwaPn06", "rbpn12", ref status);
            vvd(rbpn[0, 2], -0.5790760923225655753e-3, 1e-14,
                "wwaPn06", "rbpn13", ref status);

            vvd(rbpn[1, 0], 0.1332856406595754748e-2, 1e-14,
                "wwaPn06", "rbpn21", ref status);
            vvd(rbpn[1, 1], 0.9999991109069366795, 1e-12,
                "wwaPn06", "rbpn22", ref status);
            vvd(rbpn[1, 2], -0.4097725651142641812e-4, 1e-14,
                "wwaPn06", "rbpn23", ref status);

            vvd(rbpn[2, 0], 0.5791301952321296716e-3, 1e-14,
                "wwaPn06", "rbpn31", ref status);
            vvd(rbpn[2, 1], 0.4020538796195230577e-4, 1e-14,
                "wwaPn06", "rbpn32", ref status);
            vvd(rbpn[2, 2], 0.9999998314958576778, 1e-12,
                "wwaPn06", "rbpn33", ref status);

        }

        static void t_pnm00a(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ p n m 0 0 a
        **  - - - - - - - - -
        **
        **  Test wwaPnm00a function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPnm00a, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] rbpn = new double[3, 3];


            WWA.wwaPnm00a(2400000.5, 50123.9999, rbpn);

            vvd(rbpn[0, 0], 0.9999995832793134257, 1e-12,
                "wwaPnm00a", "11", ref status);
            vvd(rbpn[0, 1], 0.8372384254137809439e-3, 1e-14,
                "wwaPnm00a", "12", ref status);
            vvd(rbpn[0, 2], 0.3639684306407150645e-3, 1e-14,
                "wwaPnm00a", "13", ref status);

            vvd(rbpn[1, 0], -0.8372535226570394543e-3, 1e-14,
                "wwaPnm00a", "21", ref status);
            vvd(rbpn[1, 1], 0.9999996486491582471, 1e-12,
                "wwaPnm00a", "22", ref status);
            vvd(rbpn[1, 2], 0.4132915262664072381e-4, 1e-14,
                "wwaPnm00a", "23", ref status);

            vvd(rbpn[2, 0], -0.3639337004054317729e-3, 1e-14,
                "wwaPnm00a", "31", ref status);
            vvd(rbpn[2, 1], -0.4163386925461775873e-4, 1e-14,
                "wwaPnm00a", "32", ref status);
            vvd(rbpn[2, 2], 0.9999999329094390695, 1e-12,
                "wwaPnm00a", "33", ref status);

        }

        static void t_pnm00b(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ p n m 0 0 b
        **  - - - - - - - - -
        **
        **  Test wwaPnm00b function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPnm00b, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] rbpn = new double[3, 3];


            WWA.wwaPnm00b(2400000.5, 50123.9999, rbpn);

            vvd(rbpn[0, 0], 0.9999995832776208280, 1e-12,
                "wwaPnm00b", "11", ref status);
            vvd(rbpn[0, 1], 0.8372401264429654837e-3, 1e-14,
                "wwaPnm00b", "12", ref status);
            vvd(rbpn[0, 2], 0.3639691681450271771e-3, 1e-14,
                "wwaPnm00b", "13", ref status);

            vvd(rbpn[1, 0], -0.8372552234147137424e-3, 1e-14,
                "wwaPnm00b", "21", ref status);
            vvd(rbpn[1, 1], 0.9999996486477686123, 1e-12,
                "wwaPnm00b", "22", ref status);
            vvd(rbpn[1, 2], 0.4132832190946052890e-4, 1e-14,
                "wwaPnm00b", "23", ref status);

            vvd(rbpn[2, 0], -0.3639344385341866407e-3, 1e-14,
                "wwaPnm00b", "31", ref status);
            vvd(rbpn[2, 1], -0.4163303977421522785e-4, 1e-14,
                "wwaPnm00b", "32", ref status);
            vvd(rbpn[2, 2], 0.9999999329092049734, 1e-12,
                "wwaPnm00b", "33", ref status);

        }

        static void t_pnm06a(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ p n m 0 6 a
        **  - - - - - - - - -
        **
        **  Test wwaPnm06a function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPnm06a, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] rbpn = new double[3, 3];


            WWA.wwaPnm06a(2400000.5, 50123.9999, rbpn);

            vvd(rbpn[0, 0], 0.9999995832794205484, 1e-12,
                "wwaPnm06a", "11", ref status);
            vvd(rbpn[0, 1], 0.8372382772630962111e-3, 1e-14,
                "wwaPnm06a", "12", ref status);
            vvd(rbpn[0, 2], 0.3639684771140623099e-3, 1e-14,
                "wwaPnm06a", "13", ref status);

            vvd(rbpn[1, 0], -0.8372533744743683605e-3, 1e-14,
                "wwaPnm06a", "21", ref status);
            vvd(rbpn[1, 1], 0.9999996486492861646, 1e-12,
                "wwaPnm06a", "22", ref status);
            vvd(rbpn[1, 2], 0.4132905944611019498e-4, 1e-14,
                "wwaPnm06a", "23", ref status);

            vvd(rbpn[2, 0], -0.3639337469629464969e-3, 1e-14,
                "wwaPnm06a", "31", ref status);
            vvd(rbpn[2, 1], -0.4163377605910663999e-4, 1e-14,
                "wwaPnm06a", "32", ref status);
            vvd(rbpn[2, 2], 0.9999999329094260057, 1e-12,
                "wwaPnm06a", "33", ref status);

        }

        static void t_pnm80(ref int status)
        /*
        **  - - - - - - - -
        **   t _ p n m 8 0
        **  - - - - - - - -
        **
        **  Test wwaPnm80 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPnm80, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] rmatpn = new double[3, 3];


            WWA.wwaPnm80(2400000.5, 50123.9999, rmatpn);

            vvd(rmatpn[0, 0], 0.9999995831934611169, 1e-12,
                "wwaPnm80", "11", ref status);
            vvd(rmatpn[0, 1], 0.8373654045728124011e-3, 1e-14,
                "wwaPnm80", "12", ref status);
            vvd(rmatpn[0, 2], 0.3639121916933106191e-3, 1e-14,
                "wwaPnm80", "13", ref status);

            vvd(rmatpn[1, 0], -0.8373804896118301316e-3, 1e-14,
                "wwaPnm80", "21", ref status);
            vvd(rmatpn[1, 1], 0.9999996485439674092, 1e-12,
                "wwaPnm80", "22", ref status);
            vvd(rmatpn[1, 2], 0.4130202510421549752e-4, 1e-14,
                "wwaPnm80", "23", ref status);

            vvd(rmatpn[2, 0], -0.3638774789072144473e-3, 1e-14,
                "wwaPnm80", "31", ref status);
            vvd(rmatpn[2, 1], -0.4160674085851722359e-4, 1e-14,
                "wwaPnm80", "32", ref status);
            vvd(rmatpn[2, 2], 0.9999999329310274805, 1e-12,
                "wwaPnm80", "33", ref status);

        }

        static void t_pom00(ref int status)
        /*
        **  - - - - - - - -
        **   t _ p o m 0 0
        **  - - - - - - - -
        **
        **  Test wwaPom00 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPom00, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double xp, yp, sp;
            double[,] rpom = new double[3, 3];


            xp = 2.55060238e-7;
            yp = 1.860359247e-6;
            sp = -0.1367174580728891460e-10;

            WWA.wwaPom00(xp, yp, sp, rpom);

            vvd(rpom[0, 0], 0.9999999999999674721, 1e-12,
                "wwaPom00", "11", ref status);
            vvd(rpom[0, 1], -0.1367174580728846989e-10, 1e-16,
                "wwaPom00", "12", ref status);
            vvd(rpom[0, 2], 0.2550602379999972345e-6, 1e-16,
                "wwaPom00", "13", ref status);

            vvd(rpom[1, 0], 0.1414624947957029801e-10, 1e-16,
                "wwaPom00", "21", ref status);
            vvd(rpom[1, 1], 0.9999999999982695317, 1e-12,
                "wwaPom00", "22", ref status);
            vvd(rpom[1, 2], -0.1860359246998866389e-5, 1e-16,
                "wwaPom00", "23", ref status);

            vvd(rpom[2, 0], -0.2550602379741215021e-6, 1e-16,
                "wwaPom00", "31", ref status);
            vvd(rpom[2, 1], 0.1860359247002414021e-5, 1e-16,
                "wwaPom00", "32", ref status);
            vvd(rpom[2, 2], 0.9999999999982370039, 1e-12,
                "wwaPom00", "33", ref status);

        }

        static void t_ppp(ref int status)
        /*
        **  - - - - - -
        **   t _ p p p
        **  - - - - - -
        **
        **  Test wwaPpp function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPpp, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[] a = new double[3];
            double[] b = new double[3];
            double[] apb = new double[3];


            a[0] = 2.0;
            a[1] = 2.0;
            a[2] = 3.0;

            b[0] = 1.0;
            b[1] = 3.0;
            b[2] = 4.0;

            WWA.wwaPpp(a, b, apb);

            vvd(apb[0], 3.0, 1e-12, "wwaPpp", "0", ref status);
            vvd(apb[1], 5.0, 1e-12, "wwaPpp", "1", ref status);
            vvd(apb[2], 7.0, 1e-12, "wwaPpp", "2", ref status);

        }

        static void t_ppsp(ref int status)
        /*
        **  - - - - - - -
        **   t _ p p s p
        **  - - - - - - -
        **
        **  Test wwaPpsp function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPpsp, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[] a = new double[3];
            double s;
            double[] b = new double[3];
            double[] apsb = new double[3];


            a[0] = 2.0;
            a[1] = 2.0;
            a[2] = 3.0;

            s = 5.0;

            b[0] = 1.0;
            b[1] = 3.0;
            b[2] = 4.0;

            WWA.wwaPpsp(a, s, b, apsb);

            vvd(apsb[0], 7.0, 1e-12, "wwaPpsp", "0", ref status);
            vvd(apsb[1], 17.0, 1e-12, "wwaPpsp", "1", ref status);
            vvd(apsb[2], 23.0, 1e-12, "wwaPpsp", "2", ref status);

        }

        static void t_pr00(ref int status)
        /*
        **  - - - - - - -
        **   t _ p r 0 0
        **  - - - - - - -
        **
        **  Test wwaPr00 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPr00, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double dpsipr = 0, depspr = 0;

            WWA.wwaPr00(2400000.5, 53736, ref dpsipr, ref depspr);

            vvd(dpsipr, -0.8716465172668347629e-7, 1e-22,
               "wwaPr00", "dpsipr", ref status);
            vvd(depspr, -0.7342018386722813087e-8, 1e-22,
               "wwaPr00", "depspr", ref status);

        }

        static void t_prec76(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ p r e c 7 6
        **  - - - - - - - - -
        **
        **  Test wwaPrec76 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPrec76, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double ep01, ep02, ep11, ep12, zeta = 0, z = 0, theta = 0;


            ep01 = 2400000.5;
            ep02 = 33282.0;
            ep11 = 2400000.5;
            ep12 = 51544.0;

            WWA.wwaPrec76(ep01, ep02, ep11, ep12, ref zeta, ref z, ref theta);

            vvd(zeta, 0.5588961642000161243e-2, 1e-12,
                "wwaPrec76", "zeta", ref status);
            vvd(z, 0.5589922365870680624e-2, 1e-12,
                "wwaPrec76", "z", ref status);
            vvd(theta, 0.4858945471687296760e-2, 1e-12,
                "wwaPrec76", "theta", ref status);

        }

        static void t_pv2p(ref int status)
        /*
        **  - - - - - - -
        **   t _ p v 2 p
        **  - - - - - - -
        **
        **  Test wwaPv2p function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPv2p, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] pv = new double[2, 3];
            double[] p = new double[3];


            pv[0, 0] = 0.3;
            pv[0, 1] = 1.2;
            pv[0, 2] = -2.5;

            pv[1, 0] = -0.5;
            pv[1, 1] = 3.1;
            pv[1, 2] = 0.9;

            WWA.wwaPv2p(pv, p);

            vvd(p[0], 0.3, 0.0, "wwaPv2p", "1", ref status);
            vvd(p[1], 1.2, 0.0, "wwaPv2p", "2", ref status);
            vvd(p[2], -2.5, 0.0, "wwaPv2p", "3", ref status);

        }

        static void t_pv2s(ref int status)
        /*
        **  - - - - - - -
        **   t _ p v 2 s
        **  - - - - - - -
        **
        **  Test wwaPv2s function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPv2s, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] pv = new double[2, 3];
            double theta = 0, phi = 0, r = 0, td = 0, pd = 0, rd = 0;


            pv[0, 0] = -0.4514964673880165;
            pv[0, 1] = 0.03093394277342585;
            pv[0, 2] = 0.05594668105108779;

            pv[1, 0] = 1.292270850663260e-5;
            pv[1, 1] = 2.652814182060692e-6;
            pv[1, 2] = 2.568431853930293e-6;

            WWA.wwaPv2s(pv, ref theta, ref phi, ref r, ref td, ref pd, ref rd);

            vvd(theta, 3.073185307179586515, 1e-12, "wwaPv2s", "theta", ref status);
            vvd(phi, 0.1229999999999999992, 1e-12, "wwaPv2s", "phi", ref status);
            vvd(r, 0.4559999999999999757, 1e-12, "wwaPv2s", "r", ref status);
            vvd(td, -0.7800000000000000364e-5, 1e-16, "wwaPv2s", "td", ref status);
            vvd(pd, 0.9010000000000001639e-5, 1e-16, "wwaPv2s", "pd", ref status);
            vvd(rd, -0.1229999999999999832e-4, 1e-16, "wwaPv2s", "rd", ref status);

        }

        static void t_pvdpv(ref int status)
        /*
        **  - - - - - - - -
        **   t _ p v d p v
        **  - - - - - - - -
        **
        **  Test wwaPvdpv function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPvdpv, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] a = new double[2, 3];
            double[,] b = new double[2, 3];
            double[] adb = new double[2];


            a[0, 0] = 2.0;
            a[0, 1] = 2.0;
            a[0, 2] = 3.0;

            a[1, 0] = 6.0;
            a[1, 1] = 0.0;
            a[1, 2] = 4.0;

            b[0, 0] = 1.0;
            b[0, 1] = 3.0;
            b[0, 2] = 4.0;

            b[1, 0] = 0.0;
            b[1, 1] = 2.0;
            b[1, 2] = 8.0;

            WWA.wwaPvdpv(a, b, adb);

            vvd(adb[0], 20.0, 1e-12, "wwaPvdpv", "1", ref status);
            vvd(adb[1], 50.0, 1e-12, "wwaPvdpv", "2", ref status);

        }

        static void t_pvm(ref int status)
        /*
        **  - - - - - -
        **   t _ p v m
        **  - - - - - -
        **
        **  Test wwaPvm function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPvm, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] pv = new double[2, 3];
            double r = 0, s = 0;


            pv[0, 0] = 0.3;
            pv[0, 1] = 1.2;
            pv[0, 2] = -2.5;

            pv[1, 0] = 0.45;
            pv[1, 1] = -0.25;
            pv[1, 2] = 1.1;

            WWA.wwaPvm(pv, ref r, ref s);

            vvd(r, 2.789265136196270604, 1e-12, "wwaPvm", "r", ref status);
            vvd(s, 1.214495780149111922, 1e-12, "wwaPvm", "s", ref status);

        }

        static void t_pvmpv(ref int status)
        /*
        **  - - - - - - - -
        **   t _ p v m p v
        **  - - - - - - - -
        **
        **  Test wwaPvmpv function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPvmpv, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] a = new double[2, 3];
            double[,] b = new double[2, 3];
            double[,] amb = new double[2, 3];


            a[0, 0] = 2.0;
            a[0, 1] = 2.0;
            a[0, 2] = 3.0;

            a[1, 0] = 5.0;
            a[1, 1] = 6.0;
            a[1, 2] = 3.0;

            b[0, 0] = 1.0;
            b[0, 1] = 3.0;
            b[0, 2] = 4.0;

            b[1, 0] = 3.0;
            b[1, 1] = 2.0;
            b[1, 2] = 1.0;

            WWA.wwaPvmpv(a, b, amb);

            vvd(amb[0, 0], 1.0, 1e-12, "wwaPvmpv", "11", ref status);
            vvd(amb[0, 1], -1.0, 1e-12, "wwaPvmpv", "21", ref status);
            vvd(amb[0, 2], -1.0, 1e-12, "wwaPvmpv", "31", ref status);

            vvd(amb[1, 0], 2.0, 1e-12, "wwaPvmpv", "12", ref status);
            vvd(amb[1, 1], 4.0, 1e-12, "wwaPvmpv", "22", ref status);
            vvd(amb[1, 2], 2.0, 1e-12, "wwaPvmpv", "32", ref status);

        }

        static void t_pvppv(ref int status)
        /*
        **  - - - - - - - -
        **   t _ p v p p v
        **  - - - - - - - -
        **
        **  Test wwaPvppv function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPvppv, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] a = new double[2, 3];
            double[,] b = new double[2, 3];
            double[,] apb = new double[2, 3];


            a[0, 0] = 2.0;
            a[0, 1] = 2.0;
            a[0, 2] = 3.0;

            a[1, 0] = 5.0;
            a[1, 1] = 6.0;
            a[1, 2] = 3.0;

            b[0, 0] = 1.0;
            b[0, 1] = 3.0;
            b[0, 2] = 4.0;

            b[1, 0] = 3.0;
            b[1, 1] = 2.0;
            b[1, 2] = 1.0;

            WWA.wwaPvppv(a, b, apb);

            vvd(apb[0, 0], 3.0, 1e-12, "wwaPvppv", "p1", ref status);
            vvd(apb[0, 1], 5.0, 1e-12, "wwaPvppv", "p2", ref status);
            vvd(apb[0, 2], 7.0, 1e-12, "wwaPvppv", "p3", ref status);

            vvd(apb[1, 0], 8.0, 1e-12, "wwaPvppv", "v1", ref status);
            vvd(apb[1, 1], 8.0, 1e-12, "wwaPvppv", "v2", ref status);
            vvd(apb[1, 2], 4.0, 1e-12, "wwaPvppv", "v3", ref status);

        }

        static void t_pvstar(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ p v s t a r
        **  - - - - - - - - -
        **
        **  Test wwaPvstar function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPvstar, vvd, viv
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] pv = new double[2, 3];
            double ra = 0, dec = 0, pmr = 0, pmd = 0, px = 0, rv = 0;
            int j;


            pv[0, 0] = 126668.5912743160601;
            pv[0, 1] = 2136.792716839935195;
            pv[0, 2] = -245251.2339876830091;

            pv[1, 0] = -0.4051854035740712739e-2;
            pv[1, 1] = -0.6253919754866173866e-2;
            pv[1, 2] = 0.1189353719774107189e-1;

            j = WWA.wwaPvstar(pv, ref ra, ref dec, ref pmr, ref pmd, ref px, ref rv);

            vvd(ra, 0.1686756e-1, 1e-12, "wwaPvstar", "ra", ref status);
            vvd(dec, -1.093989828, 1e-12, "wwaPvstar", "dec", ref status);
            //vvd(pmr, -0.178323516e-4, 1e-16, "wwaPvstar", "pmr", ref status);
            //vvd(pmd, 0.2336024047e-5, 1e-16, "wwaPvstar", "pmd", ref status);
            vvd(pmr, -0.1783235160000472788e-4, 1e-16, "wwaPvstar", "pmr", ref status);
            vvd(pmd, 0.2336024047000619347e-5, 1e-16, "wwaPvstar", "pmd", ref status);

            vvd(px, 0.74723, 1e-12, "wwaPvstar", "px", ref status);
            //vvd(rv, -21.6, 1e-11, "wwaPvstar", "rv", ref status);
            vvd(rv, -21.60000010107306010, 1e-11, "wwaPvstar", "rv", ref status);

            viv(j, 0, "wwaPvstar", "j", ref status);

        }

        static void t_pvtob(ref int status)
        /*
        **  - - - - - - - -
        **   t _ p v t o b
        **  - - - - - - - -
        **
        **  Test wwaPvtob function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPvtob, vvd
        **
        **  This revision:  2013 October 2
        */
        {
            double elong, phi, hm, xp, yp, sp, theta;
            double[,] pv = new double[2, 3];


            elong = 2.0;
            phi = 0.5;
            hm = 3000.0;
            xp = 1e-6;
            yp = -0.5e-6;
            sp = 1e-8;
            theta = 5.0;

            WWA.wwaPvtob(elong, phi, hm, xp, yp, sp, theta, pv);

            vvd(pv[0, 0], 4225081.367071159207, 1e-5,
                          "wwaPvtob", "p(1)", ref status);
            vvd(pv[0, 1], 3681943.215856198144, 1e-5,
                          "wwaPvtob", "p(2)", ref status);
            vvd(pv[0, 2], 3041149.399241260785, 1e-5,
                          "wwaPvtob", "p(3)", ref status);
            vvd(pv[1, 0], -268.4915389365998787, 1e-9,
                          "wwaPvtob", "v(1)", ref status);
            vvd(pv[1, 1], 308.0977983288903123, 1e-9,
                          "wwaPvtob", "v(2)", ref status);
            vvd(pv[1, 2], 0, 0,
                          "wwaPvtob", "v(3)", ref status);

        }

        static void t_pvu(ref int status)
        /*
        **  - - - - - -
        **   t _ p v u
        **  - - - - - -
        **
        **  Test wwaPvu function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPvu, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] pv = new double[2, 3];
            double[,] upv = new double[2, 3];


            pv[0, 0] = 126668.5912743160734;
            pv[0, 1] = 2136.792716839935565;
            pv[0, 2] = -245251.2339876830229;

            pv[1, 0] = -0.4051854035740713039e-2;
            pv[1, 1] = -0.6253919754866175788e-2;
            pv[1, 2] = 0.1189353719774107615e-1;

            WWA.wwaPvu(2920.0, pv, upv);

            vvd(upv[0, 0], 126656.7598605317105, 1e-12,
                "wwaPvu", "p1", ref status);
            vvd(upv[0, 1], 2118.531271155726332, 1e-12,
                "wwaPvu", "p2", ref status);
            vvd(upv[0, 2], -245216.5048590656190, 1e-12,
                "wwaPvu", "p3", ref status);

            vvd(upv[1, 0], -0.4051854035740713039e-2, 1e-12,
                "wwaPvu", "v1", ref status);
            vvd(upv[1, 1], -0.6253919754866175788e-2, 1e-12,
                "wwaPvu", "v2", ref status);
            vvd(upv[1, 2], 0.1189353719774107615e-1, 1e-12,
                "wwaPvu", "v3", ref status);

        }

        static void t_pvup(ref int status)
        /*
        **  - - - - - - -
        **   t _ p v u p
        **  - - - - - - -
        **
        **  Test wwaPvup function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPvup, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] pv = new double[2, 3];
            double[] p = new double[3];


            pv[0, 0] = 126668.5912743160734;
            pv[0, 1] = 2136.792716839935565;
            pv[0, 2] = -245251.2339876830229;

            pv[1, 0] = -0.4051854035740713039e-2;
            pv[1, 1] = -0.6253919754866175788e-2;
            pv[1, 2] = 0.1189353719774107615e-1;

            WWA.wwaPvup(2920.0, pv, p);

            vvd(p[0], 126656.7598605317105, 1e-12, "wwaPvup", "1", ref status);
            vvd(p[1], 2118.531271155726332, 1e-12, "wwaPvup", "2", ref status);
            vvd(p[2], -245216.5048590656190, 1e-12, "wwaPvup", "3", ref status);

        }

        static void t_pvxpv(ref int status)
        /*
        **  - - - - - - - -
        **   t _ p v x p v
        **  - - - - - - - -
        **
        **  Test wwaPvxpv function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPvxpv, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] a = new double[2, 3];
            double[,] b = new double[2, 3];
            double[,] axb = new double[2, 3];


            a[0, 0] = 2.0;
            a[0, 1] = 2.0;
            a[0, 2] = 3.0;

            a[1, 0] = 6.0;
            a[1, 1] = 0.0;
            a[1, 2] = 4.0;

            b[0, 0] = 1.0;
            b[0, 1] = 3.0;
            b[0, 2] = 4.0;

            b[1, 0] = 0.0;
            b[1, 1] = 2.0;
            b[1, 2] = 8.0;

            WWA.wwaPvxpv(a, b, axb);

            vvd(axb[0, 0], -1.0, 1e-12, "wwaPvxpv", "p1", ref status);
            vvd(axb[0, 1], -5.0, 1e-12, "wwaPvxpv", "p2", ref status);
            vvd(axb[0, 2], 4.0, 1e-12, "wwaPvxpv", "p3", ref status);

            vvd(axb[1, 0], -2.0, 1e-12, "wwaPvxpv", "v1", ref status);
            vvd(axb[1, 1], -36.0, 1e-12, "wwaPvxpv", "v2", ref status);
            vvd(axb[1, 2], 22.0, 1e-12, "wwaPvxpv", "v3", ref status);

        }

        static void t_pxp(ref int status)
        /*
        **  - - - - - -
        **   t _ p x p
        **  - - - - - -
        **
        **  Test wwaPxp function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaPxp, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[] a = new double[3];
            double[] b = new double[3];
            double[] axb = new double[3];


            a[0] = 2.0;
            a[1] = 2.0;
            a[2] = 3.0;

            b[0] = 1.0;
            b[1] = 3.0;
            b[2] = 4.0;

            WWA.wwaPxp(a, b, axb);

            vvd(axb[0], -1.0, 1e-12, "wwaPxp", "1", ref status);
            vvd(axb[1], -5.0, 1e-12, "wwaPxp", "2", ref status);
            vvd(axb[2], 4.0, 1e-12, "wwaPxp", "3", ref status);

        }

        static void t_refco(ref int status)
        /*
        **  - - - - - - - -
        **   t _ r e f c o
        **  - - - - - - - -
        **
        **  Test wwaRefco function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaRefco, vvd
        **
        **  This revision:  2013 October 2
        */
        {
            double phpa, tc, rh, wl, refa = 0, refb = 0;


            phpa = 800.0;
            tc = 10.0;
            rh = 0.9;
            wl = 0.4;

            WWA.wwaRefco(phpa, tc, rh, wl, ref refa, ref refb);

            vvd(refa, 0.2264949956241415009e-3, 1e-15,
                      "wwaRefco", "refa", ref status);
            vvd(refb, -0.2598658261729343970e-6, 1e-18,
                      "wwaRefco", "refb", ref status);

        }

        static void t_rm2v(ref int status)
        /*
        **  - - - - - - -
        **   t _ r m 2 v
        **  - - - - - - -
        **
        **  Test wwaRm2v function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaRm2v, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] r = new double[3, 3];
            double[] w = new double[3];


            r[0, 0] = 0.00;
            r[0, 1] = -0.80;
            r[0, 2] = -0.60;

            r[1, 0] = 0.80;
            r[1, 1] = -0.36;
            r[1, 2] = 0.48;

            r[2, 0] = 0.60;
            r[2, 1] = 0.48;
            r[2, 2] = -0.64;

            WWA.wwaRm2v(r, w);

            vvd(w[0], 0.0, 1e-12, "wwaRm2v", "1", ref status);
            vvd(w[1], 1.413716694115406957, 1e-12, "wwaRm2v", "2", ref status);
            vvd(w[2], -1.884955592153875943, 1e-12, "wwaRm2v", "3", ref status);

        }

        static void t_rv2m(ref int status)
        /*
        **  - - - - - - -
        **   t _ r v 2 m
        **  - - - - - - -
        **
        **  Test wwaRv2m function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaRv2m, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[] w = new double[3];
            double[,] r = new double[3, 3];


            w[0] = 0.0;
            w[1] = 1.41371669;
            w[2] = -1.88495559;

            WWA.wwaRv2m(w, r);

            vvd(r[0, 0], -0.7071067782221119905, 1e-14, "wwaRv2m", "11", ref status);
            vvd(r[0, 1], -0.5656854276809129651, 1e-14, "wwaRv2m", "12", ref status);
            vvd(r[0, 2], -0.4242640700104211225, 1e-14, "wwaRv2m", "13", ref status);

            vvd(r[1, 0], 0.5656854276809129651, 1e-14, "wwaRv2m", "21", ref status);
            vvd(r[1, 1], -0.0925483394532274246, 1e-14, "wwaRv2m", "22", ref status);
            vvd(r[1, 2], -0.8194112531408833269, 1e-14, "wwaRv2m", "23", ref status);

            vvd(r[2, 0], 0.4242640700104211225, 1e-14, "wwaRv2m", "31", ref status);
            vvd(r[2, 1], -0.8194112531408833269, 1e-14, "wwaRv2m", "32", ref status);
            vvd(r[2, 2], 0.3854415612311154341, 1e-14, "wwaRv2m", "33", ref status);

        }

        static void t_rx(ref int status)
        /*
        **  - - - - -
        **   t _ r x
        **  - - - - -
        **
        **  Test wwaRx function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaRx, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double phi;
            double[,] r = new double[3, 3];


            phi = 0.3456789;

            r[0, 0] = 2.0;
            r[0, 1] = 3.0;
            r[0, 2] = 2.0;

            r[1, 0] = 3.0;
            r[1, 1] = 2.0;
            r[1, 2] = 3.0;

            r[2, 0] = 3.0;
            r[2, 1] = 4.0;
            r[2, 2] = 5.0;

            WWA.wwaRx(phi, r);

            vvd(r[0, 0], 2.0, 0.0, "wwaRx", "11", ref status);
            vvd(r[0, 1], 3.0, 0.0, "wwaRx", "12", ref status);
            vvd(r[0, 2], 2.0, 0.0, "wwaRx", "13", ref status);

            vvd(r[1, 0], 3.839043388235612460, 1e-12, "wwaRx", "21", ref status);
            vvd(r[1, 1], 3.237033249594111899, 1e-12, "wwaRx", "22", ref status);
            vvd(r[1, 2], 4.516714379005982719, 1e-12, "wwaRx", "23", ref status);

            vvd(r[2, 0], 1.806030415924501684, 1e-12, "wwaRx", "31", ref status);
            vvd(r[2, 1], 3.085711545336372503, 1e-12, "wwaRx", "32", ref status);
            vvd(r[2, 2], 3.687721683977873065, 1e-12, "wwaRx", "33", ref status);

        }

        static void t_rxp(ref int status)
        /*
        **  - - - - - -
        **   t _ r x p
        **  - - - - - -
        **
        **  Test wwaRxp function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaRxp, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] r = new double[3, 3];
            double[] p = new double[3];
            double[] rp = new double[3];


            r[0, 0] = 2.0;
            r[0, 1] = 3.0;
            r[0, 2] = 2.0;

            r[1, 0] = 3.0;
            r[1, 1] = 2.0;
            r[1, 2] = 3.0;

            r[2, 0] = 3.0;
            r[2, 1] = 4.0;
            r[2, 2] = 5.0;

            p[0] = 0.2;
            p[1] = 1.5;
            p[2] = 0.1;

            WWA.wwaRxp(r, p, rp);

            vvd(rp[0], 5.1, 1e-12, "wwaRxp", "1", ref status);
            vvd(rp[1], 3.9, 1e-12, "wwaRxp", "2", ref status);
            vvd(rp[2], 7.1, 1e-12, "wwaRxp", "3", ref status);

        }

        static void t_rxpv(ref int status)
        /*
        **  - - - - - - -
        **   t _ r x p v
        **  - - - - - - -
        **
        **  Test wwaRxpv function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaRxpv, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] r = new double[3, 3];
            double[,] pv = new double[2, 3];
            double[,] rpv = new double[2, 3];


            r[0, 0] = 2.0;
            r[0, 1] = 3.0;
            r[0, 2] = 2.0;

            r[1, 0] = 3.0;
            r[1, 1] = 2.0;
            r[1, 2] = 3.0;

            r[2, 0] = 3.0;
            r[2, 1] = 4.0;
            r[2, 2] = 5.0;

            pv[0, 0] = 0.2;
            pv[0, 1] = 1.5;
            pv[0, 2] = 0.1;

            pv[1, 0] = 1.5;
            pv[1, 1] = 0.2;
            pv[1, 2] = 0.1;

            WWA.wwaRxpv(r, pv, rpv);

            vvd(rpv[0, 0], 5.1, 1e-12, "wwaRxpv", "11", ref status);
            vvd(rpv[1, 0], 3.8, 1e-12, "wwaRxpv", "12", ref status);

            vvd(rpv[0, 1], 3.9, 1e-12, "wwaRxpv", "21", ref status);
            vvd(rpv[1, 1], 5.2, 1e-12, "wwaRxpv", "22", ref status);

            vvd(rpv[0, 2], 7.1, 1e-12, "wwaRxpv", "31", ref status);
            vvd(rpv[1, 2], 5.8, 1e-12, "wwaRxpv", "32", ref status);

        }

        static void t_rxr(ref int status)
        /*
        **  - - - - - -
        **   t _ r x r
        **  - - - - - -
        **
        **  Test wwaRxr function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaRxr, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] a = new double[3, 3];
            double[,] b = new double[3, 3];
            double[,] atb = new double[3, 3];


            a[0, 0] = 2.0;
            a[0, 1] = 3.0;
            a[0, 2] = 2.0;

            a[1, 0] = 3.0;
            a[1, 1] = 2.0;
            a[1, 2] = 3.0;

            a[2, 0] = 3.0;
            a[2, 1] = 4.0;
            a[2, 2] = 5.0;

            b[0, 0] = 1.0;
            b[0, 1] = 2.0;
            b[0, 2] = 2.0;

            b[1, 0] = 4.0;
            b[1, 1] = 1.0;
            b[1, 2] = 1.0;

            b[2, 0] = 3.0;
            b[2, 1] = 0.0;
            b[2, 2] = 1.0;

            WWA.wwaRxr(a, b, atb);

            vvd(atb[0, 0], 20.0, 1e-12, "wwaRxr", "11", ref status);
            vvd(atb[0, 1], 7.0, 1e-12, "wwaRxr", "12", ref status);
            vvd(atb[0, 2], 9.0, 1e-12, "wwaRxr", "13", ref status);

            vvd(atb[1, 0], 20.0, 1e-12, "wwaRxr", "21", ref status);
            vvd(atb[1, 1], 8.0, 1e-12, "wwaRxr", "22", ref status);
            vvd(atb[1, 2], 11.0, 1e-12, "wwaRxr", "23", ref status);

            vvd(atb[2, 0], 34.0, 1e-12, "wwaRxr", "31", ref status);
            vvd(atb[2, 1], 10.0, 1e-12, "wwaRxr", "32", ref status);
            vvd(atb[2, 2], 15.0, 1e-12, "wwaRxr", "33", ref status);

        }

        static void t_ry(ref int status)
        /*
        **  - - - - -
        **   t _ r y
        **  - - - - -
        **
        **  Test wwaRy function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaRy, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double theta;
            double[,] r = new double[3, 3];


            theta = 0.3456789;

            r[0, 0] = 2.0;
            r[0, 1] = 3.0;
            r[0, 2] = 2.0;

            r[1, 0] = 3.0;
            r[1, 1] = 2.0;
            r[1, 2] = 3.0;

            r[2, 0] = 3.0;
            r[2, 1] = 4.0;
            r[2, 2] = 5.0;

            WWA.wwaRy(theta, r);

            vvd(r[0, 0], 0.8651847818978159930, 1e-12, "wwaRy", "11", ref status);
            vvd(r[0, 1], 1.467194920539316554, 1e-12, "wwaRy", "12", ref status);
            vvd(r[0, 2], 0.1875137911274457342, 1e-12, "wwaRy", "13", ref status);

            vvd(r[1, 0], 3, 1e-12, "wwaRy", "21", ref status);
            vvd(r[1, 1], 2, 1e-12, "wwaRy", "22", ref status);
            vvd(r[1, 2], 3, 1e-12, "wwaRy", "23", ref status);

            vvd(r[2, 0], 3.500207892850427330, 1e-12, "wwaRy", "31", ref status);
            vvd(r[2, 1], 4.779889022262298150, 1e-12, "wwaRy", "32", ref status);
            vvd(r[2, 2], 5.381899160903798712, 1e-12, "wwaRy", "33", ref status);

        }

        static void t_rz(ref int status)
        /*
        **  - - - - -
        **   t _ r z
        **  - - - - -
        **
        **  Test wwaRz function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaRz, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double psi;
            double[,] r = new double[3, 3];


            psi = 0.3456789;

            r[0, 0] = 2.0;
            r[0, 1] = 3.0;
            r[0, 2] = 2.0;

            r[1, 0] = 3.0;
            r[1, 1] = 2.0;
            r[1, 2] = 3.0;

            r[2, 0] = 3.0;
            r[2, 1] = 4.0;
            r[2, 2] = 5.0;

            WWA.wwaRz(psi, r);

            vvd(r[0, 0], 2.898197754208926769, 1e-12, "wwaRz", "11", ref status);
            vvd(r[0, 1], 3.500207892850427330, 1e-12, "wwaRz", "12", ref status);
            vvd(r[0, 2], 2.898197754208926769, 1e-12, "wwaRz", "13", ref status);

            vvd(r[1, 0], 2.144865911309686813, 1e-12, "wwaRz", "21", ref status);
            vvd(r[1, 1], 0.865184781897815993, 1e-12, "wwaRz", "22", ref status);
            vvd(r[1, 2], 2.144865911309686813, 1e-12, "wwaRz", "23", ref status);

            vvd(r[2, 0], 3.0, 1e-12, "wwaRz", "31", ref status);
            vvd(r[2, 1], 4.0, 1e-12, "wwaRz", "32", ref status);
            vvd(r[2, 2], 5.0, 1e-12, "wwaRz", "33", ref status);

        }

        static void t_s00a(ref int status)
        /*
        **  - - - - - - -
        **   t _ s 0 0 a
        **  - - - - - - -
        **
        **  Test wwaS00a function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaS00a, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double s;


            s = WWA.wwaS00a(2400000.5, 52541.0);

            vvd(s, -0.1340684448919163584e-7, 1e-18, "wwaS00a", "", ref status);

        }

        static void t_s00b(ref int status)
        /*
        **  - - - - - - -
        **   t _ s 0 0 b
        **  - - - - - - -
        **
        **  Test wwaS00b function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaS00b, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double s;


            s = WWA.wwaS00b(2400000.5, 52541.0);

            vvd(s, -0.1340695782951026584e-7, 1e-18, "wwaS00b", "", ref status);

        }

        static void t_s00(ref int status)
        /*
        **  - - - - - -
        **   t _ s 0 0
        **  - - - - - -
        **
        **  Test wwaS00 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaS00, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double x, y, s;


            x = 0.5791308486706011000e-3;
            y = 0.4020579816732961219e-4;

            s = WWA.wwaS00(2400000.5, 53736.0, x, y);

            vvd(s, -0.1220036263270905693e-7, 1e-18, "wwaS00", "", ref status);

        }

        static void t_s06a(ref int status)
        /*
        **  - - - - - - -
        **   t _ s 0 6 a
        **  - - - - - - -
        **
        **  Test wwaS06a function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaS06a, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double s;


            s = WWA.wwaS06a(2400000.5, 52541.0);

            vvd(s, -0.1340680437291812383e-7, 1e-18, "wwaS06a", "", ref status);

        }

        static void t_s06(ref int status)
        /*
        **  - - - - - -
        **   t _ s 0 6
        **  - - - - - -
        **
        **  Test wwaS06 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaS06, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double x, y, s;


            x = 0.5791308486706011000e-3;
            y = 0.4020579816732961219e-4;

            s = WWA.wwaS06(2400000.5, 53736.0, x, y);

            vvd(s, -0.1220032213076463117e-7, 1e-18, "wwaS06", "", ref status);

        }

        static void t_s2c(ref int status)
        /*
        **  - - - - - -
        **   t _ s 2 c
        **  - - - - - -
        **
        **  Test wwaS2c function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaS2c, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[] c = new double[3];


            WWA.wwaS2c(3.0123, -0.999, c);

            vvd(c[0], -0.5366267667260523906, 1e-12, "wwaS2c", "1", ref status);
            vvd(c[1], 0.0697711109765145365, 1e-12, "wwaS2c", "2", ref status);
            vvd(c[2], -0.8409302618566214041, 1e-12, "wwaS2c", "3", ref status);

        }

        static void t_s2p(ref int status)
        /*
        **  - - - - - -
        **   t _ s 2 p
        **  - - - - - -
        **
        **  Test wwaS2p function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaS2p, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[] p = new double[3];


            WWA.wwaS2p(-3.21, 0.123, 0.456, p);

            vvd(p[0], -0.4514964673880165228, 1e-12, "wwaS2p", "x", ref status);
            vvd(p[1], 0.0309339427734258688, 1e-12, "wwaS2p", "y", ref status);
            vvd(p[2], 0.0559466810510877933, 1e-12, "wwaS2p", "z", ref status);

        }

        static void t_s2pv(ref int status)
        /*
        **  - - - - - - -
        **   t _ s 2 p v
        **  - - - - - - -
        **
        **  Test wwaS2pv function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaS2pv, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] pv = new double[2, 3];


            WWA.wwaS2pv(-3.21, 0.123, 0.456, -7.8e-6, 9.01e-6, -1.23e-5, pv);

            vvd(pv[0, 0], -0.4514964673880165228, 1e-12, "wwaS2pv", "x", ref status);
            vvd(pv[0, 1], 0.0309339427734258688, 1e-12, "wwaS2pv", "y", ref status);
            vvd(pv[0, 2], 0.0559466810510877933, 1e-12, "wwaS2pv", "z", ref status);

            vvd(pv[1, 0], 0.1292270850663260170e-4, 1e-16,
                "wwaS2pv", "vx", ref status);
            vvd(pv[1, 1], 0.2652814182060691422e-5, 1e-16,
                "wwaS2pv", "vy", ref status);
            vvd(pv[1, 2], 0.2568431853930292259e-5, 1e-16,
                "wwaS2pv", "vz", ref status);

        }

        static void t_s2xpv(ref int status)
        /*
        **  - - - - - - - -
        **   t _ s 2 x p v
        **  - - - - - - - -
        **
        **  Test wwaS2xpv function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaS2xpv, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double s1, s2;
            double[,] pv = new double[2, 3];
            double[,] spv = new double[2, 3];


            s1 = 2.0;
            s2 = 3.0;

            pv[0, 0] = 0.3;
            pv[0, 1] = 1.2;
            pv[0, 2] = -2.5;

            pv[1, 0] = 0.5;
            pv[1, 1] = 2.3;
            pv[1, 2] = -0.4;

            WWA.wwaS2xpv(s1, s2, pv, spv);

            vvd(spv[0, 0], 0.6, 1e-12, "wwaS2xpv", "p1", ref status);
            vvd(spv[0, 1], 2.4, 1e-12, "wwaS2xpv", "p2", ref status);
            vvd(spv[0, 2], -5.0, 1e-12, "wwaS2xpv", "p3", ref status);

            vvd(spv[1, 0], 1.5, 1e-12, "wwaS2xpv", "v1", ref status);
            vvd(spv[1, 1], 6.9, 1e-12, "wwaS2xpv", "v2", ref status);
            vvd(spv[1, 2], -1.2, 1e-12, "wwaS2xpv", "v3", ref status);

        }

        static void t_sepp(ref int status)
        /*
        **  - - - - - - -
        **   t _ s e p p
        **  - - - - - - -
        **
        **  Test wwaSepp function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaSepp, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[] a = new double[3];
            double[] b = new double[3];
            double s;


            a[0] = 1.0;
            a[1] = 0.1;
            a[2] = 0.2;

            b[0] = -3.0;
            b[1] = 1e-3;
            b[2] = 0.2;

            s = WWA.wwaSepp(a, b);

            vvd(s, 2.860391919024660768, 1e-12, "wwaSepp", "", ref status);

        }

        static void t_seps(ref int status)
        /*
        **  - - - - - - -
        **   t _ s e p s
        **  - - - - - - -
        **
        **  Test wwaSeps function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaSeps, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double al, ap, bl, bp, s;


            al = 1.0;
            ap = 0.1;

            bl = 0.2;
            bp = -3.0;

            s = WWA.wwaSeps(al, ap, bl, bp);

            vvd(s, 2.346722016996998842, 1e-14, "wwaSeps", "", ref status);

        }

        static void t_sp00(ref int status)
        /*
        **  - - - - - - -
        **   t _ s p 0 0
        **  - - - - - - -
        **
        **  Test wwaSp00 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaSp00, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            vvd(WWA.wwaSp00(2400000.5, 52541.0),
               -0.6216698469981019309e-11, 1e-12, "wwaSp00", "", ref status);

        }

        static void t_starpm(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ s t a r p m
        **  - - - - - - - - -
        **
        **  Test wwaStarpm function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaStarpm, vvd, viv
        **
        **  This revision:  2013 August 7
        */
        {
            double ra1, dec1, pmr1, pmd1, px1, rv1;
            double ra2 = 0, dec2 = 0, pmr2 = 0, pmd2 = 0, px2 = 0, rv2 = 0;
            int j;


            ra1 = 0.01686756;
            dec1 = -1.093989828;
            pmr1 = -1.78323516e-5;
            pmd1 = 2.336024047e-6;
            px1 = 0.74723;
            rv1 = -21.6;

            j = WWA.wwaStarpm(ra1, dec1, pmr1, pmd1, px1, rv1,
                          2400000.5, 50083.0, 2400000.5, 53736.0,
                          ref ra2, ref dec2, ref pmr2, ref pmd2, ref px2, ref rv2);

            //vvd(ra2, 0.01668919069414242368, 1e-13,
            //    "wwaStarpm", "ra", ref status);
            //vvd(dec2, -1.093966454217127879, 1e-13,
            //    "wwaStarpm", "dec", ref status);
            //vvd(pmr2, -0.1783662682155932702e-4, 1e-17,
            //    "wwaStarpm", "pmr", ref status);
            //vvd(pmd2, 0.2338092915987603664e-5, 1e-17,
            //    "wwaStarpm", "pmd", ref status);
            //vvd(px2, 0.7473533835323493644, 1e-13,
            //    "wwaStarpm", "px", ref status);
            //vvd(rv2, -21.59905170476860786, 1e-11,
            //    "wwaStarpm", "rv", ref status);
            vvd(ra2, 0.01668919069414256149, 1e-13,
                "wwaStarpm", "ra", ref status);
            vvd(dec2, -1.093966454217127897, 1e-13,
                "wwaStarpm", "dec", ref status);
            vvd(pmr2, -0.1783662682153176524e-4, 1e-17,
                "wwaStarpm", "pmr", ref status);
            vvd(pmd2, 0.2338092915983989595e-5, 1e-17,
                "wwaStarpm", "pmd", ref status);
            vvd(px2, 0.7473533835317719243, 1e-13,
                "wwaStarpm", "px", ref status);
            vvd(rv2, -21.59905170476417175, 1e-11,
                "wwaStarpm", "rv", ref status);

            viv(j, 0, "wwaStarpm", "j", ref status);

        }

        static void t_starpv(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ s t a r p v
        **  - - - - - - - - -
        **
        **  Test wwaStarpv function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaStarpv, vvd, viv
        **
        **  This revision:  2013 August 7
        */
        {
            double ra, dec, pmr, pmd, px, rv;
            double[,] pv = new double[2, 3];
            int j;


            ra = 0.01686756;
            dec = -1.093989828;
            pmr = -1.78323516e-5;
            pmd = 2.336024047e-6;
            px = 0.74723;
            rv = -21.6;

            j = WWA.wwaStarpv(ra, dec, pmr, pmd, px, rv, pv);

            vvd(pv[0, 0], 126668.5912743160601, 1e-10,
                "wwaStarpv", "11", ref status);
            vvd(pv[0, 1], 2136.792716839935195, 1e-12,
                "wwaStarpv", "12", ref status);
            vvd(pv[0, 2], -245251.2339876830091, 1e-10,
                "wwaStarpv", "13", ref status);

            //vvd(pv[1, 0], -0.4051854035740712739e-2, 1e-13,
            //    "wwaStarpv", "21", ref status);
            //vvd(pv[1, 1], -0.6253919754866173866e-2, 1e-15,
            //    "wwaStarpv", "22", ref status);
            //vvd(pv[1, 2], 0.1189353719774107189e-1, 1e-13,
            //    "wwaStarpv", "23", ref status);
            vvd(pv[1, 0], -0.4051854008955659551e-2, 1e-13,
                "wwaStarpv", "21", ref status);
            vvd(pv[1, 1], -0.6253919754414777970e-2, 1e-15,
                "wwaStarpv", "22", ref status);
            vvd(pv[1, 2], 0.1189353714588109341e-1, 1e-13,
                "wwaStarpv", "23", ref status);

            viv(j, 0, "wwaStarpv", "j", ref status);

        }

        static void t_sxp(ref int status)
        /*
        **  - - - - - -
        **   t _ s x p
        **  - - - - - -
        **
        **  Test wwaSxp function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaSxp, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double s;
            double[] p = new double[3];
            double[] sp = new double[3];


            s = 2.0;

            p[0] = 0.3;
            p[1] = 1.2;
            p[2] = -2.5;

            WWA.wwaSxp(s, p, sp);

            vvd(sp[0], 0.6, 0.0, "wwaSxp", "1", ref status);
            vvd(sp[1], 2.4, 0.0, "wwaSxp", "2", ref status);
            vvd(sp[2], -5.0, 0.0, "wwaSxp", "3", ref status);

        }


        static void t_sxpv(ref int status)
        /*
        **  - - - - - - -
        **   t _ s x p v
        **  - - - - - - -
        **
        **  Test wwaSxpv function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaSxpv, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double s;
            double[,] pv = new double[2, 3];
            double[,] spv = new double[2, 3];


            s = 2.0;

            pv[0, 0] = 0.3;
            pv[0, 1] = 1.2;
            pv[0, 2] = -2.5;

            pv[1, 0] = 0.5;
            pv[1, 1] = 3.2;
            pv[1, 2] = -0.7;

            WWA.wwaSxpv(s, pv, spv);

            vvd(spv[0, 0], 0.6, 0.0, "wwaSxpv", "p1", ref status);
            vvd(spv[0, 1], 2.4, 0.0, "wwaSxpv", "p2", ref status);
            vvd(spv[0, 2], -5.0, 0.0, "wwaSxpv", "p3", ref status);

            vvd(spv[1, 0], 1.0, 0.0, "wwaSxpv", "v1", ref status);
            vvd(spv[1, 1], 6.4, 0.0, "wwaSxpv", "v2", ref status);
            vvd(spv[1, 2], -1.4, 0.0, "wwaSxpv", "v3", ref status);

        }

        static void t_taitt(ref int status)
        /*
        **  - - - - - - - -
        **   t _ t a i t t
        **  - - - - - - - -
        **
        **  Test wwaTaitt function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaTaitt, vvd, viv
        **
        **  This revision:  2013 August 7
        */
        {
            double t1 = 0, t2 = 0;
            int j;


            j = WWA.wwaTaitt(2453750.5, 0.892482639, ref t1, ref t2);

            vvd(t1, 2453750.5, 1e-6, "wwaTaitt", "t1", ref status);
            vvd(t2, 0.892855139, 1e-12, "wwaTaitt", "t2", ref status);
            viv(j, 0, "wwaTaitt", "j", ref status);

        }

        static void t_taiut1(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ t a i u t 1
        **  - - - - - - - - -
        **
        **  Test wwaTaiut1 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaTaiut1, vvd, viv
        **
        **  This revision:  2013 August 7
        */
        {
            double u1 = 0, u2 = 0;
            int j;


            j = WWA.wwaTaiut1(2453750.5, 0.892482639, -32.6659, ref u1, ref u2);

            vvd(u1, 2453750.5, 1e-6, "wwaTaiut1", "u1", ref status);
            vvd(u2, 0.8921045614537037037, 1e-12, "wwaTaiut1", "u2", ref status);
            viv(j, 0, "wwaTaiut1", "j", ref status);

        }

        static void t_taiutc(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ t a i u t c
        **  - - - - - - - - -
        **
        **  Test wwaTaiutc function.
        **
        **  Returned:
        **     status    LOGICAL     TRUE = success, FALSE = fail
        **
        **  Called:  wwaTaiutc, vvd, viv
        **
        **  This revision:  2013 October 3
        */
        {
            double u1 = 0, u2 = 0;
            int j;


            j = WWA.wwaTaiutc(2453750.5, 0.892482639, ref u1, ref u2);

            vvd(u1, 2453750.5, 1e-6, "wwaTaiutc", "u1", ref status);
            vvd(u2, 0.8921006945555555556, 1e-12, "wwaTaiutc", "u2", ref status);
            viv(j, 0, "wwaTaiutc", "j", ref status);

        }

        static void t_tcbtdb(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ t c b t d b
        **  - - - - - - - - -
        **
        **  Test wwaTcbtdb function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaTcbtdb, vvd, viv
        **
        **  This revision:  2013 August 7
        */
        {
            double b1 = 0, b2 = 0;
            int j;


            j = WWA.wwaTcbtdb(2453750.5, 0.893019599, ref b1, ref b2);

            vvd(b1, 2453750.5, 1e-6, "wwaTcbtdb", "b1", ref status);
            vvd(b2, 0.8928551362746343397, 1e-12, "wwaTcbtdb", "b2", ref status);
            viv(j, 0, "wwaTcbtdb", "j", ref status);

        }

        static void t_tcgtt(ref int status)
        /*
        **  - - - - - - - -
        **   t _ t c g t t
        **  - - - - - - - -
        **
        **  Test wwaTcgtt function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaTcgtt, vvd, viv
        **
        **  This revision:  2013 August 7
        */
        {
            double t1 = 0, t2 = 0;
            int j;


            j = WWA.wwaTcgtt(2453750.5, 0.892862531, ref t1, ref t2);

            vvd(t1, 2453750.5, 1e-6, "wwaTcgtt", "t1", ref status);
            vvd(t2, 0.8928551387488816828, 1e-12, "wwaTcgtt", "t2", ref status);
            viv(j, 0, "wwaTcgtt", "j", ref status);

        }

        static void t_tdbtcb(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ t d b t c b
        **  - - - - - - - - -
        **
        **  Test wwaTdbtcb function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaTdbtcb, vvd, viv
        **
        **  This revision:  2013 August 7
        */
        {
            double b1 = 0, b2 = 0;
            int j;


            j = WWA.wwaTdbtcb(2453750.5, 0.892855137, ref b1, ref b2);

            vvd(b1, 2453750.5, 1e-6, "wwaTdbtcb", "b1", ref status);
            vvd(b2, 0.8930195997253656716, 1e-12, "wwaTdbtcb", "b2", ref status);
            viv(j, 0, "wwaTdbtcb", "j", ref status);

        }

        static void t_tdbtt(ref int status)
        /*
        **  - - - - - - - -
        **   t _ t d b t t
        **  - - - - - - - -
        **
        **  Test wwaTdbtt function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaTdbtt, vvd, viv
        **
        **  This revision:  2013 August 7
        */
        {
            double t1 = 0, t2 = 0;
            int j;


            j = WWA.wwaTdbtt(2453750.5, 0.892855137, -0.000201, ref t1, ref t2);

            vvd(t1, 2453750.5, 1e-6, "wwaTdbtt", "t1", ref status);
            vvd(t2, 0.8928551393263888889, 1e-12, "wwaTdbtt", "t2", ref status);
            viv(j, 0, "wwaTdbtt", "j", ref status);

        }

        static void t_tf2a(ref int status)
        /*
        **  - - - - - - -
        **   t _ t f 2 a
        **  - - - - - - -
        **
        **  Test wwaTf2a function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaTf2a, vvd, viv
        **
        **  This revision:  2013 August 7
        */
        {
            double a = 0;
            int j;


            j = WWA.wwaTf2a('+', 4, 58, 20.2, ref a);

            vvd(a, 1.301739278189537429, 1e-12, "wwaTf2a", "a", ref status);
            viv(j, 0, "wwaTf2a", "j", ref status);

        }

        static void t_tf2d(ref int status)
        /*
        **  - - - - - - -
        **   t _ t f 2 d
        **  - - - - - - -
        **
        **  Test wwaTf2d function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaTf2d, vvd, viv
        **
        **  This revision:  2013 August 7
        */
        {
            double d = 0;
            int j;


            j = WWA.wwaTf2d(' ', 23, 55, 10.9, ref d);

            vvd(d, 0.9966539351851851852, 1e-12, "wwaTf2d", "d", ref status);
            viv(j, 0, "wwaTf2d", "j", ref status);
        }

        static void t_tpors(ref int status)
        /*
        **  - - - - - - - -
        **   t _ t p o r s
        **  - - - - - - - -
        **
        **  Test iauTpors function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaTpors, vvd, viv
        **
        **  This revision:  2017 October 21
        */
        {
            double xi, eta, ra, dec, az1 = 0, bz1 = 0, az2 = 0, bz2 = 0;
            int n;


            xi = -0.03;
            eta = 0.07;
            ra = 1.3;
            dec = 1.5;

            n = WWA.wwaTpors(xi, eta, ra, dec, ref az1, ref bz1, ref az2, ref bz2);

            vvd(az1, 1.736621577783208748, 1e-13, "iauTpors", "az1", ref status);
            vvd(bz1, 1.436736561844090323, 1e-13, "iauTpors", "bz1", ref status);

            vvd(az2, 4.004971075806584490, 1e-13, "iauTpors", "az2", ref status);
            vvd(bz2, 1.565084088476417917, 1e-13, "iauTpors", "bz2", ref status);

            viv(n, 2, "wwaTpors", "n", ref status);

        }

        static void t_tporv(ref int status)
        /*
        **  - - - - - - - -
        **   t _ t p o r v
        **  - - - - - - - -
        **
        **  Test iauTporv function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaTporv, wwaS2c, vvd, viv
        **
        **  This revision:  2017 October 21
        */
        {
            double xi, eta, ra, dec;
            double[] v = new double[3];
            double[] vz1 = new double[3];
            double[] vz2 = new double[3];
            int n;


            xi = -0.03;
            eta = 0.07;
            ra = 1.3;
            dec = 1.5;
            WWA.wwaS2c(ra, dec, v);

            n = WWA.wwaTporv(xi, eta, v, vz1, vz2);

            vvd(vz1[0], -0.02206252822366888610, 1e-15,
                "wwaTporv", "x1", ref status);
            vvd(vz1[1], 0.1318251060359645016, 1e-14,
                "wwaTporv", "y1", ref status);
            vvd(vz1[2], 0.9910274397144543895, 1e-14,
                "wwaTporv", "z1", ref status);

            vvd(vz2[0], -0.003712211763801968173, 1e-16,
                "wwaTporv", "x2", ref status);
            vvd(vz2[1], -0.004341519956299836813, 1e-16,
                "wwaTporv", "y2", ref status);
            vvd(vz2[2], 0.9999836852110587012, 1e-14,
                "wwaTporv", "z2", ref status);

            viv(n, 2, "wwaTporv", "n", ref status);

        }

        static void t_tpsts(ref int status)
        /*
        **  - - - - - - - -
        **   t _ t p s t s
        **  - - - - - - - -
        **
        **  Test iauTpsts function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaTpsts, vvd
        **
        **  This revision:  2017 October 21
        */
        {
            double xi, eta, raz, decz, ra = 0, dec = 0;


            xi = -0.03;
            eta = 0.07;
            raz = 2.3;
            decz = 1.5;

            WWA.wwaTpsts(xi, eta, raz, decz, ref ra, ref dec);

            vvd(ra, 0.7596127167359629775, 1e-14, "wwaTpsts", "ra", ref status);
            vvd(dec, 1.540864645109263028, 1e-13, "wwaTpsts", "dec", ref status);

        }

        static void t_tpstv(ref int status)
        /*
        **  - - - - - - - -
        **   t _ t p s t v
        **  - - - - - - - -
        **
        **  Test iauTpstv function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaTpstv, wwaS2c, vvd
        **
        **  This revision:  2017 October 21
        */
        {
            double xi, eta, raz, decz;
            double[] vz = new double[3];
            double[] v = new double[3];


            xi = -0.03;
            eta = 0.07;
            raz = 2.3;
            decz = 1.5;
            WWA.wwaS2c(raz, decz, vz);

            WWA.wwaTpstv(xi, eta, vz, v);

            vvd(v[0], 0.02170030454907376677, 1e-15, "wwaTpstv", "x", ref status);
            vvd(v[1], 0.02060909590535367447, 1e-15, "wwaTpstv", "y", ref status);
            vvd(v[2], 0.9995520806583523804, 1e-14, "wwaTpstv", "z", ref status);

        }

        static void t_tpxes(ref int status)
        /*
        **  - - - - - - - -
        **   t _ t p x e s
        **  - - - - - - - -
        **
        **  Test iauTpxes function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaTpxes, vvd, viv
        **
        **  This revision:  2017 October 21
        */
        {
            double ra, dec, raz, decz, xi = 0, eta = 0;
            int j;


            ra = 1.3;
            dec = 1.55;
            raz = 2.3;
            decz = 1.5;

            j = WWA.wwaTpxes(ra, dec, raz, decz, ref xi, ref eta);

            vvd(xi, -0.01753200983236980595, 1e-15, "wwaTpxes", "xi", ref status);
            vvd(eta, 0.05962940005778712891, 1e-15, "wwaTpxes", "eta", ref status);

            viv(j, 0, "wwaTpxes", "j", ref status);

        }

        static void t_tpxev(ref int status)
        /*
        **  - - - - - - - -
        **   t _ t p x e v
        **  - - - - - - - -
        **
        **  Test iauTpxev function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaTpxev, wwaS2c, vvd
        **
        **  This revision:  2017 October 21
        */
        {
            double ra, dec, raz, decz, xi = 0, eta = 0;
            double[] v = new double[3];
            double[] vz = new double[3];
            int j;


            ra = 1.3;
            dec = 1.55;
            raz = 2.3;
            decz = 1.5;
            WWA.wwaS2c(ra, dec, v);
            WWA.wwaS2c(raz, decz, vz);

            j = WWA.wwaTpxev(v, vz, ref xi, ref eta);

            vvd(xi, -0.01753200983236980595, 1e-15, "wwaTpxev", "xi", ref status);
            vvd(eta, 0.05962940005778712891, 1e-15, "wwaTpxev", "eta", ref status);

            viv(j, 0, "wwaTpxev", "j", ref status);

        }

        static void t_tr(ref int status)
        /*
        **  - - - - -
        **   t _ t r
        **  - - - - -
        **
        **  Test wwaTr function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaTr, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] r = new double[3, 3];
            double[,] rt = new double[3, 3];


            r[0, 0] = 2.0;
            r[0, 1] = 3.0;
            r[0, 2] = 2.0;

            r[1, 0] = 3.0;
            r[1, 1] = 2.0;
            r[1, 2] = 3.0;

            r[2, 0] = 3.0;
            r[2, 1] = 4.0;
            r[2, 2] = 5.0;

            WWA.wwaTr(r, rt);

            vvd(rt[0, 0], 2.0, 0.0, "wwaTr", "11", ref status);
            vvd(rt[0, 1], 3.0, 0.0, "wwaTr", "12", ref status);
            vvd(rt[0, 2], 3.0, 0.0, "wwaTr", "13", ref status);

            vvd(rt[1, 0], 3.0, 0.0, "wwaTr", "21", ref status);
            vvd(rt[1, 1], 2.0, 0.0, "wwaTr", "22", ref status);
            vvd(rt[1, 2], 4.0, 0.0, "wwaTr", "23", ref status);

            vvd(rt[2, 0], 2.0, 0.0, "wwaTr", "31", ref status);
            vvd(rt[2, 1], 3.0, 0.0, "wwaTr", "32", ref status);
            vvd(rt[2, 2], 5.0, 0.0, "wwaTr", "33", ref status);

        }

        static void t_trxp(ref int status)
        /*
        **  - - - - - - -
        **   t _ t r x p
        **  - - - - - - -
        **
        **  Test wwaTrxp function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaTrxp, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] r = new double[3, 3];
            double[] p = new double[3];
            double[] trp = new double[3];


            r[0, 0] = 2.0;
            r[0, 1] = 3.0;
            r[0, 2] = 2.0;

            r[1, 0] = 3.0;
            r[1, 1] = 2.0;
            r[1, 2] = 3.0;

            r[2, 0] = 3.0;
            r[2, 1] = 4.0;
            r[2, 2] = 5.0;

            p[0] = 0.2;
            p[1] = 1.5;
            p[2] = 0.1;

            WWA.wwaTrxp(r, p, trp);

            vvd(trp[0], 5.2, 1e-12, "wwaTrxp", "1", ref status);
            vvd(trp[1], 4.0, 1e-12, "wwaTrxp", "2", ref status);
            vvd(trp[2], 5.4, 1e-12, "wwaTrxp", "3", ref status);

        }

        static void t_trxpv(ref int status)
        /*
        **  - - - - - - - -
        **   t _ t r x p v
        **  - - - - - - - -
        **
        **  Test wwaTrxpv function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaTrxpv, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] r = new double[3, 3];
            double[,] pv = new double[2, 3];
            double[,] trpv = new double[2, 3];


            r[0, 0] = 2.0;
            r[0, 1] = 3.0;
            r[0, 2] = 2.0;

            r[1, 0] = 3.0;
            r[1, 1] = 2.0;
            r[1, 2] = 3.0;

            r[2, 0] = 3.0;
            r[2, 1] = 4.0;
            r[2, 2] = 5.0;

            pv[0, 0] = 0.2;
            pv[0, 1] = 1.5;
            pv[0, 2] = 0.1;

            pv[1, 0] = 1.5;
            pv[1, 1] = 0.2;
            pv[1, 2] = 0.1;

            WWA.wwaTrxpv(r, pv, trpv);

            vvd(trpv[0, 0], 5.2, 1e-12, "wwaTrxpv", "p1", ref status);
            vvd(trpv[0, 1], 4.0, 1e-12, "wwaTrxpv", "p1", ref status);
            vvd(trpv[0, 2], 5.4, 1e-12, "wwaTrxpv", "p1", ref status);

            vvd(trpv[1, 0], 3.9, 1e-12, "wwaTrxpv", "v1", ref status);
            vvd(trpv[1, 1], 5.3, 1e-12, "wwaTrxpv", "v2", ref status);
            vvd(trpv[1, 2], 4.1, 1e-12, "wwaTrxpv", "v3", ref status);

        }

        static void t_tttai(ref int status)
        /*
        **  - - - - - - - -
        **   t _ t t t a i
        **  - - - - - - - -
        **
        **  Test wwaTttai function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaTttai, vvd, viv
        **
        **  This revision:  2013 August 7
        */
        {
            double a1 = 0, a2 = 0;
            int j;


            j = WWA.wwaTttai(2453750.5, 0.892482639, ref a1, ref a2);

            vvd(a1, 2453750.5, 1e-6, "wwaTttai", "a1", ref status);
            vvd(a2, 0.892110139, 1e-12, "wwaTttai", "a2", ref status);
            viv(j, 0, "wwaTttai", "j", ref status);

        }

        static void t_tttcg(ref int status)
        /*
        **  - - - - - - - -
        **   t _ t t t c g
        **  - - - - - - - -
        **
        **  Test wwaTttcg function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaTttcg, vvd, viv
        **
        **  This revision:  2013 August 7
        */
        {
            double g1 = 0, g2 = 0;
            int j;


            j = WWA.wwaTttcg(2453750.5, 0.892482639, ref g1, ref g2);

            vvd(g1, 2453750.5, 1e-6, "wwaTttcg", "g1", ref status);
            vvd(g2, 0.8924900312508587113, 1e-12, "wwaTttcg", "g2", ref status);
            viv(j, 0, "wwaTttcg", "j", ref status);
        }

        static void t_tttdb(ref int status)
        /*
        **  - - - - - - - -
        **   t _ t t t d b
        **  - - - - - - - -
        **
        **  Test wwaTttdb function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaTttdb, vvd, viv
        **
        **  This revision:  2013 August 7
        */
        {
            double b1 = 0, b2 = 0;
            int j;


            j = WWA.wwaTttdb(2453750.5, 0.892855139, -0.000201, ref b1, ref b2);

            vvd(b1, 2453750.5, 1e-6, "wwaTttdb", "b1", ref status);
            vvd(b2, 0.8928551366736111111, 1e-12, "wwaTttdb", "b2", ref status);
            viv(j, 0, "wwaTttdb", "j", ref status);

        }

        static void t_ttut1(ref int status)
        /*
        **  - - - - - - - -
        **   t _ t t u t 1
        **  - - - - - - - -
        **
        **  Test wwaTtut1 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaTtut1, vvd, viv
        **
        **  This revision:  2013 August 7
        */
        {
            double u1 = 0, u2 = 0;
            int j;


            j = WWA.wwaTtut1(2453750.5, 0.892855139, 64.8499, ref u1, ref u2);

            vvd(u1, 2453750.5, 1e-6, "wwaTtut1", "u1", ref status);
            vvd(u2, 0.8921045614537037037, 1e-12, "wwaTtut1", "u2", ref status);
            viv(j, 0, "wwaTtut1", "j", ref status);

        }

        static void t_ut1tai(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ u t 1 t a i
        **  - - - - - - - - -
        **
        **  Test wwaUt1tai function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaUt1tai, vvd, viv
        **
        **  This revision:  2013 August 7
        */
        {
            double a1 = 0, a2 = 0;
            int j;


            j = WWA.wwaUt1tai(2453750.5, 0.892104561, -32.6659, ref a1, ref a2);

            vvd(a1, 2453750.5, 1e-6, "wwaUt1tai", "a1", ref status);
            vvd(a2, 0.8924826385462962963, 1e-12, "wwaUt1tai", "a2", ref status);
            viv(j, 0, "wwaUt1tai", "j", ref status);

        }

        static void t_ut1tt(ref int status)
        /*
        **  - - - - - - - -
        **   t _ u t 1 t t
        **  - - - - - - - -
        **
        **  Test wwaUt1tt function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaUt1tt, vvd, viv
        **
        **  This revision:  2013 October 3
        */
        {
            double t1 = 0, t2 = 0;
            int j;


            j = WWA.wwaUt1tt(2453750.5, 0.892104561, 64.8499, ref t1, ref t2);

            vvd(t1, 2453750.5, 1e-6, "wwaUt1tt", "t1", ref status);
            vvd(t2, 0.8928551385462962963, 1e-12, "wwaUt1tt", "t2", ref status);
            viv(j, 0, "wwaUt1tt", "j", ref status);

        }

        static void t_ut1utc(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ u t 1 u t c
        **  - - - - - - - - -
        **
        **  Test wwaUt1utc function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaUt1utc, vvd, viv
        **
        **  This revision:  2013 August 7
        */
        {
            double u1 = 0, u2 = 0;
            int j;


            j = WWA.wwaUt1utc(2453750.5, 0.892104561, 0.3341, ref u1, ref u2);

            vvd(u1, 2453750.5, 1e-6, "wwaUt1utc", "u1", ref status);
            vvd(u2, 0.8921006941018518519, 1e-12, "wwaUt1utc", "u2", ref status);
            viv(j, 0, "wwaUt1utc", "j", ref status);

        }

        static void t_utctai(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ u t c t a i
        **  - - - - - - - - -
        **
        **  Test wwaUtctai function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaUtctai, vvd, viv
        **
        **  This revision:  2013 August 7
        */
        {
            double u1 = 0, u2 = 0;
            int j;


            j = WWA.wwaUtctai(2453750.5, 0.892100694, ref u1, ref u2);

            vvd(u1, 2453750.5, 1e-6, "wwaUtctai", "u1", ref status);
            vvd(u2, 0.8924826384444444444, 1e-12, "wwaUtctai", "u2", ref status);
            viv(j, 0, "wwaUtctai", "j", ref status);

        }

        static void t_utcut1(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ u t c u t 1
        **  - - - - - - - - -
        **
        **  Test wwaUtcut1 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaUtcut1, vvd, viv
        **
        **  This revision:  2013 August 7
        */
        {
            double u1 = 0, u2 = 0;
            int j;


            j = WWA.wwaUtcut1(2453750.5, 0.892100694, 0.3341, ref u1, ref u2);

            vvd(u1, 2453750.5, 1e-6, "wwaUtcut1", "u1", ref status);
            vvd(u2, 0.8921045608981481481, 1e-12, "wwaUtcut1", "u2", ref status);
            viv(j, 0, "wwaUtcut1", "j", ref status);

        }

        static void t_xy06(ref int status)
        /*
        **  - - - - - - -
        **   t _ x y 0 6
        **  - - - - - - -
        **
        **  Test wwaXy06 function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaXy06, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double x = 0, y = 0;


            WWA.wwaXy06(2400000.5, 53736.0, ref x, ref y);

            vvd(x, 0.5791308486706010975e-3, 1e-15, "wwaXy06", "x", ref status);
            vvd(y, 0.4020579816732958141e-4, 1e-16, "wwaXy06", "y", ref status);

        }

        static void t_xys00a(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ x y s 0 0 a
        **  - - - - - - - - -
        **
        **  Test wwaXys00a function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaXys00a, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double x = 0, y = 0, s = 0;


            WWA.wwaXys00a(2400000.5, 53736.0, ref x, ref y, ref s);

            vvd(x, 0.5791308472168152904e-3, 1e-14, "wwaXys00a", "x", ref status);
            vvd(y, 0.4020595661591500259e-4, 1e-15, "wwaXys00a", "y", ref status);
            vvd(s, -0.1220040848471549623e-7, 1e-18, "wwaXys00a", "s", ref status);

        }

        static void t_xys00b(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ x y s 0 0 b
        **  - - - - - - - - -
        **
        **  Test wwaXys00b function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaXys00b, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double x = 0, y = 0, s = 0;


            WWA.wwaXys00b(2400000.5, 53736.0, ref x, ref y, ref s);

            vvd(x, 0.5791301929950208873e-3, 1e-14, "wwaXys00b", "x", ref status);
            vvd(y, 0.4020553681373720832e-4, 1e-15, "wwaXys00b", "y", ref status);
            vvd(s, -0.1220027377285083189e-7, 1e-18, "wwaXys00b", "s", ref status);

        }

        static void t_xys06a(ref int status)
        /*
        **  - - - - - - - - -
        **   t _ x y s 0 6 a
        **  - - - - - - - - -
        **
        **  Test wwaXys06a function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaXys06a, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double x = 0, y = 0, s = 0;


            WWA.wwaXys06a(2400000.5, 53736.0, ref x, ref y, ref s);

            vvd(x, 0.5791308482835292617e-3, 1e-14, "wwaXys06a", "x", ref status);
            vvd(y, 0.4020580099454020310e-4, 1e-15, "wwaXys06a", "y", ref status);
            vvd(s, -0.1220032294164579896e-7, 1e-18, "wwaXys06a", "s", ref status);

        }

        static void t_zp(ref int status)
        /*
        **  - - - - -
        **   t _ z p
        **  - - - - -
        **
        **  Test wwaZp function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaZp, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[] p = new double[3];


            p[0] = 0.3;
            p[1] = 1.2;
            p[2] = -2.5;

            WWA.wwaZp(p);

            vvd(p[0], 0.0, 0.0, "wwaZp", "1", ref status);
            vvd(p[1], 0.0, 0.0, "wwaZp", "2", ref status);
            vvd(p[2], 0.0, 0.0, "wwaZp", "3", ref status);

        }

        static void t_zpv(ref int status)
        /*
        **  - - - - - -
        **   t _ z p v
        **  - - - - - -
        **
        **  Test wwaZpv function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaZpv, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] pv = new double[2, 3];


            pv[0, 0] = 0.3;
            pv[0, 1] = 1.2;
            pv[0, 2] = -2.5;

            pv[1, 0] = -0.5;
            pv[1, 1] = 3.1;
            pv[1, 2] = 0.9;

            WWA.wwaZpv(pv);

            vvd(pv[0, 0], 0.0, 0.0, "wwaZpv", "p1", ref status);
            vvd(pv[0, 1], 0.0, 0.0, "wwaZpv", "p2", ref status);
            vvd(pv[0, 2], 0.0, 0.0, "wwaZpv", "p3", ref status);

            vvd(pv[1, 0], 0.0, 0.0, "wwaZpv", "v1", ref status);
            vvd(pv[1, 1], 0.0, 0.0, "wwaZpv", "v2", ref status);
            vvd(pv[1, 2], 0.0, 0.0, "wwaZpv", "v3", ref status);

        }

        static void t_zr(ref int status)
        /*
        **  - - - - -
        **   t _ z r
        **  - - - - -
        **
        **  Test wwaZr function.
        **
        **  Returned:
        **     status    int         FALSE = success, TRUE = fail
        **
        **  Called:  wwaZr, vvd
        **
        **  This revision:  2013 August 7
        */
        {
            double[,] r = new double[3, 3];


            r[0, 0] = 2.0;
            r[1, 0] = 3.0;
            r[2, 0] = 2.0;

            r[0, 1] = 3.0;
            r[1, 1] = 2.0;
            r[2, 1] = 3.0;

            r[0, 2] = 3.0;
            r[1, 2] = 4.0;
            r[2, 2] = 5.0;

            WWA.wwaZr(r);

            vvd(r[0, 0], 0.0, 0.0, "wwaZr", "00", ref status);
            vvd(r[1, 0], 0.0, 0.0, "wwaZr", "01", ref status);
            vvd(r[2, 0], 0.0, 0.0, "wwaZr", "02", ref status);

            vvd(r[0, 1], 0.0, 0.0, "wwaZr", "10", ref status);
            vvd(r[1, 1], 0.0, 0.0, "wwaZr", "11", ref status);
            vvd(r[2, 1], 0.0, 0.0, "wwaZr", "12", ref status);

            vvd(r[0, 2], 0.0, 0.0, "wwaZr", "20", ref status);
            vvd(r[1, 2], 0.0, 0.0, "wwaZr", "21", ref status);
            vvd(r[2, 2], 0.0, 0.0, "wwaZr", "22", ref status);

        }

        static int Main(string[] args)
        /*
        **  - - - - -
        **   m a i n
        **  - - - - -
        **
        **  This revision:  2013 October 3
        */
        {
            int status;

            /* If any command-line argument, switch to verbose reporting. */
            if (args.Length >= 1)
            {
                verbose = 1;
                //argv[0, 0] += 0;    /* to avoid compiler warnings */
            }

            /* Preset the &status to FALSE = success. */
            status = 0;

            /* Test all of the SOFA functions. */
            t_a2af(ref status);
            t_a2tf(ref status);
            t_ab(ref status);
            t_ae2hd(ref status); // new 2
            t_af2a(ref status);
            t_anp(ref status);
            t_anpm(ref status);
            t_apcg(ref status);
            t_apcg13(ref status);
            t_apci(ref status);
            t_apci13(ref status);
            t_apco(ref status);
            t_apco13(ref status);
            t_apcs(ref status);
            t_apcs13(ref status);
            t_aper(ref status);
            t_aper13(ref status);
            t_apio(ref status);
            t_apio13(ref status);
            t_atci13(ref status);
            t_atciq(ref status);
            t_atciqn(ref status);
            t_atciqz(ref status);
            t_atco13(ref status);
            t_atic13(ref status);
            t_aticq(ref status);
            t_aticqn(ref status);
            t_atio13(ref status);
            t_atioq(ref status);
            t_atoc13(ref status);
            t_atoi13(ref status);
            t_atoiq(ref status);
            t_bi00(ref status);
            t_bp00(ref status);
            t_bp06(ref status);
            t_bpn2xy(ref status);
            t_c2i00a(ref status);
            t_c2i00b(ref status);
            t_c2i06a(ref status);
            t_c2ibpn(ref status);
            t_c2ixy(ref status);
            t_c2ixys(ref status);
            t_c2s(ref status);
            t_c2t00a(ref status);
            t_c2t00b(ref status);
            t_c2t06a(ref status);
            t_c2tcio(ref status);
            t_c2teqx(ref status);
            t_c2tpe(ref status);
            t_c2txy(ref status);
            t_cal2jd(ref status);
            t_cp(ref status);
            t_cpv(ref status);
            t_cr(ref status);
            t_d2dtf(ref status);
            t_d2tf(ref status);
            t_dat(ref status);
            t_dtdb(ref status);
            t_dtf2d(ref status);
            t_eceq06(ref status); // new
            t_ecm06(ref status); // new
            t_ee00(ref status);
            t_ee00a(ref status);
            t_ee00b(ref status);
            t_ee06a(ref status);
            t_eect00(ref status);
            t_eform(ref status);
            t_eo06a(ref status);
            t_eors(ref status);
            t_epb(ref status);
            t_epb2jd(ref status);
            t_epj(ref status);
            t_epj2jd(ref status);
            t_epv00(ref status);
            t_eqec06(ref status); // new
            t_eqeq94(ref status);
            t_era00(ref status);
            t_fad03(ref status);
            t_fae03(ref status);
            t_faf03(ref status);
            t_faju03(ref status);
            t_fal03(ref status);
            t_falp03(ref status);
            t_fama03(ref status);
            t_fame03(ref status);
            t_fane03(ref status);
            t_faom03(ref status);
            t_fapa03(ref status);
            t_fasa03(ref status);
            t_faur03(ref status);
            t_fave03(ref status);
            t_fk52h(ref status);
            t_fk5hip(ref status);
            t_fk5hz(ref status);
            t_fw2m(ref status);
            t_fw2xy(ref status);
            t_g2icrs(ref status);
            t_gc2gd(ref status);
            t_gc2gde(ref status);
            t_gd2gc(ref status);
            t_gd2gce(ref status);
            t_gmst00(ref status);
            t_gmst06(ref status);
            t_gmst82(ref status);
            t_gst00a(ref status);
            t_gst00b(ref status);
            t_gst06(ref status);
            t_gst06a(ref status);
            t_gst94(ref status);
            t_h2fk5(ref status);
            t_hd2ae(ref status); // new 2
            t_hd2pa(ref status); // new 2
            t_hfk5z(ref status);
            t_icrs2g(ref status);
            t_ir(ref status);
            t_jd2cal(ref status);
            t_jdcalf(ref status);
            t_ld(ref status);
            t_ldn(ref status);
            t_ldsun(ref status);
            t_lteceq(ref status); // new
            t_ltecm(ref status); // new
            t_lteqec(ref status); // new
            t_ltp(ref status); // new
            t_ltpb(ref status); // new
            t_ltpecl(ref status); // new
            t_ltpequ(ref status); // new
            t_num00a(ref status);
            t_num00b(ref status);
            t_num06a(ref status);
            t_numat(ref status);
            t_nut00a(ref status);
            t_nut00b(ref status);
            t_nut06a(ref status);
            t_nut80(ref status);
            t_nutm80(ref status);
            t_obl06(ref status);
            t_obl80(ref status);
            t_p06e(ref status);
            t_p2pv(ref status);
            t_p2s(ref status);
            t_pap(ref status);
            t_pas(ref status);
            t_pb06(ref status);
            t_pdp(ref status);
            t_pfw06(ref status);
            t_plan94(ref status);
            t_pmat00(ref status);
            t_pmat06(ref status);
            t_pmat76(ref status);
            t_pm(ref status);
            t_pmp(ref status);
            t_pmpx(ref status);
            t_pmsafe(ref status);
            t_pn(ref status);
            t_pn00(ref status);
            t_pn00a(ref status);
            t_pn00b(ref status);
            t_pn06a(ref status);
            t_pn06(ref status);
            t_pnm00a(ref status);
            t_pnm00b(ref status);
            t_pnm06a(ref status);
            t_pnm80(ref status);
            t_pom00(ref status);
            t_ppp(ref status);
            t_ppsp(ref status);
            t_pr00(ref status);
            t_prec76(ref status);
            t_pv2p(ref status);
            t_pv2s(ref status);
            t_pvdpv(ref status);
            t_pvm(ref status);
            t_pvmpv(ref status);
            t_pvppv(ref status);
            t_pvstar(ref status);
            t_pvtob(ref status);
            t_pvu(ref status);
            t_pvup(ref status);
            t_pvxpv(ref status);
            t_pxp(ref status);
            t_refco(ref status);
            t_rm2v(ref status);
            t_rv2m(ref status);
            t_rx(ref status);
            t_rxp(ref status);
            t_rxpv(ref status);
            t_rxr(ref status);
            t_ry(ref status);
            t_rz(ref status);
            t_s00a(ref status);
            t_s00b(ref status);
            t_s00(ref status);
            t_s06a(ref status);
            t_s06(ref status);
            t_s2c(ref status);
            t_s2p(ref status);
            t_s2pv(ref status);
            t_s2xpv(ref status);
            t_sepp(ref status);
            t_seps(ref status);
            t_sp00(ref status);
            t_starpm(ref status);
            t_starpv(ref status);
            t_sxp(ref status);
            t_sxpv(ref status);
            t_taitt(ref status);
            t_taiut1(ref status);
            t_taiutc(ref status);
            t_tcbtdb(ref status);
            t_tcgtt(ref status);
            t_tdbtcb(ref status);
            t_tdbtt(ref status);
            t_tf2a(ref status);
            t_tf2d(ref status);
            t_tpors(ref status); // new 2
            t_tporv(ref status); // new 2
            t_tpsts(ref status); // new 2
            t_tpstv(ref status); // new 2
            t_tpxes(ref status); // new 2
            t_tpxev(ref status); // new 2
            t_tr(ref status);
            t_trxp(ref status);
            t_trxpv(ref status);
            t_tttai(ref status);
            t_tttcg(ref status);
            t_tttdb(ref status);
            t_ttut1(ref status);
            t_ut1tai(ref status);
            t_ut1tt(ref status);
            t_ut1utc(ref status);
            t_utctai(ref status);
            t_utcut1(ref status);
            t_xy06(ref status);
            t_xys00a(ref status);
            t_xys00b(ref status);
            t_xys06a(ref status);
            t_zp(ref status);
            t_zpv(ref status);
            t_zr(ref status);

            /* Report, set up an appropriate exit status, and finish. */
            if (status != 0)
            {
                Console.WriteLine("SOFA library validation failed!\n");
            }
            else
            {
                Console.WriteLine("SOFA library validation successful\n");
            }
            return status;
        }
    }
}
