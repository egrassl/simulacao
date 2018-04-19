using System;
namespace Modelos.MM1_Inf
{
    public class CPU : CPUBase
    {
        public int K { get; set; }

        public double Lw
        {
            get
            {
                return Math.Pow(R, 2) / (1.0 - R);
            }
        }

        public double Tw
        {
            get
            {
                return (R * TS * K) / (1 - R);
            }
        }

        public double Ls
        {
            get
            {
                return R / (1 - R);
            }
        }

        public double P0
        {
            get
            {
                return 1 - R;
            }
        }

        public double Tr
        {
            get
            {
                return Tw + TS;
            }
        }

        public double Pporcento(int p)
        {
            return (Math.Log10(1 - (double)p / 100.0) / Math.Log10(R)) - 1;
        }

        public double Pn(int n)
        {
            return (1 - R) * Math.Pow(R, n);
        }
    }
}
