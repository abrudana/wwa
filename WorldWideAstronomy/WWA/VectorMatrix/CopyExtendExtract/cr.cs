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
        /// Copy an r-matrix.
        /// 
        /// This function is part of the International Astronomical Union's
        /// SOFA (Standards Of Fundamental Astronomy) software collection.
        /// </summary>
        /// Given:
        /// <param name="r">r-matrix to be copied</param>
        /// Returned:
        /// <param name="c">copy</param>
        public static void wwaCr(double[,] r, double[,] c)
        {
            if (c == null)
                c = new double[3, 3];

            Array.Copy(r, c, r.Length);

            //wwaCp(CopyArray(r, 0), d);
            //wwaCp(CopyArray(r, 1), d);
            //wwaCp(CopyArray(r, 2), d);

            return;
        }
    }
}
