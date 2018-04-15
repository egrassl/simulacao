using System;
using System.Collections.Generic;
using System.Linq;

namespace Matematica
{
    public static class Funcoes
    {
        public static double Fatorial(int n){
            if (n == 1 || n == 0)
                return 1;
            else
                return n * Fatorial(n - 1);
        }

        public static double MediaAmostras(Func<double> calcAmostra, int n)
        {
            List<double> amostras = new List<double>();
            for (int i = 0; i < n; i++)
            {
                amostras.Add(calcAmostra());
            }
            return amostras.Average();
        }
    }
}
