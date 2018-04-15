using System;

namespace Matematica
{
    public class Somatoria
    {
        public int Inicio { get; set; }

        public int Fim { get; set; }

        public int Atual { get; set; }

        public Func<double> SomFunc { get; set; }

        public double Resultado
        {
            get
            {
                double resultado = 0.0;
                for (int i = Inicio; i <= Fim; i++)
                {
                    Atual = i;
                    resultado += SomFunc();
                }
                return (double)resultado;
            }
        }
    }
}
