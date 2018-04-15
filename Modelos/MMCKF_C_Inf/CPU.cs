using System;
using Matematica;
using Modelos;

namespace Modelos.MMCKF_C_Inf
{
    public class CPU : CPUBase
    {
        public int C { get; set; }

        public int KF { get; set; }

        public double RO
        {
            get
            {
                return A / (C * MI);
            }
        }

        public double AEF 
        {
            get 
            {
                return A * (1 - Pn(KF));
            }
        }

        public double U
        {
            get 
            {
                return AEF / (C * MI);
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
                    return (1.0 / Funcoes.Fatorial(somatoria1.Atual) * Math.Pow(A / MI, somatoria1.Atual));
                };

                Somatoria somatoria2 = new Somatoria
                {
                    Inicio = C + 1,
                    Fim = KF
                };

                somatoria2.SomFunc = () =>
                {
                    return Math.Pow(A / (C * MI), somatoria2.Atual - C);
                };

                double resultado = somatoria1.Resultado + ((1 / Funcoes.Fatorial(C)) * Math.Pow(A / MI, C)) * somatoria2.Resultado;

                return Math.Pow(resultado, -1);
            }
        }

        public double Ls
        {
            get
            {
                Somatoria somatoria = new Somatoria
                {
                    Inicio = 1,
                    Fim = KF
                };

                somatoria.SomFunc = () =>
                {
                    return somatoria.Atual * Pn(somatoria.Atual);
                };

                return somatoria.Resultado;
            }
        }

        public double Lw
        {
            get
            {
                Somatoria somatoria = new Somatoria
                {
                    Inicio = C + 1,
                    Fim = KF
                };
                somatoria.SomFunc = () =>
                {
                    return (somatoria.Atual - C) * Pn(somatoria.Atual);
                };
                return somatoria.Resultado;
            }
        }

        public double Tw
        {
            get
            {
                return Lw / AEF;
            }
        }

        public double Tr
        {
            get
            {
                return Ls / AEF;
            }
        }

        public double Pn(int n)
        {
            if (n < C)
                return (1.0 / Funcoes.Fatorial(n)) * Math.Pow(A / MI, n) * P0;
            else if (n <= KF)
                return 1.0 / (Funcoes.Fatorial(C) * Math.Pow(C, n-C)) * Math.Pow(A / MI, n) * P0;
            else
                return 0.0;
        }
    }
}
