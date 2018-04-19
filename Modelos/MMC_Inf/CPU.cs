using System;
using Matematica;

namespace Modelos.MMC_Inf
{
    public class CPU : CPUBase
    {
        public int C { get; set; }

        public double RO
        {
            get
            {
                return A / (C * MI);
            }
        }

        public double P0
        {
            get
            {
                Somatoria somatoria1 = new Somatoria
                {
                    Inicio = 0,
                    Fim = C - 1
                };

                somatoria1.SomFunc = () =>
                {
                    return (1.0 / Funcoes.Fatorial(somatoria1.Atual)) * Math.Pow(A / MI, somatoria1.Atual);
                };

                double resultado = somatoria1.Resultado;
                resultado += (1.0 / Funcoes.Fatorial(C)) * Math.Pow(A / MI, C) * (1.0 / (1.0 - RO));
                return Math.Pow(resultado, -1);
            }
        }

        public double Lw 
        {
            get
            {
                //return (RO * Delta) / (1.0 - RO);
                double resultado = Math.Pow(C * RO, C + 1) / (C * Funcoes.Fatorial(C) * Math.Pow(1.0 - RO, 2));
                return resultado * P0;
            }
        }

        public double Tw
        {
            get
            {
                return Lw / A;
                //return (A * Math.Pow(R, C - 1)) / ((C * Funcoes.Fatorial(C)) * Math.Pow(1 - RO, 2)) * P0;
            }
        }

        public double Ls
        {
            get
            {
                return Lw + R;
            }
        }

        public double Delta
        {
            get
            {
                return (Math.Pow(C * RO, C) / (Funcoes.Fatorial(C) * (1 - RO))) * P0;
            }
        }

        public double Tr
        {
            get
            {
                return Ls / A;
            }
            /*get
            {
                return (1.0 / MI) * (1 + RO / (C * (1 - RO)));
            }*/
        }

        public double Pn(int n)
        {
            if (n < C)
                return (1.0 / Funcoes.Fatorial(n)) * Math.Pow(A / MI, n) * P0;
            else
                return (1.0 / (Funcoes.Fatorial(C) * Math.Pow(C, n - C))) * Math.Pow(A / MI, n) * P0;
        }
    }
}
