using System;
using System.Collections.Generic;
using P2;

namespace Aula09
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var rand = new Random();
            int n = 10;
            var carros = new List<SItem>();
            int chegada = 0;
            for (int i = 0; i < n; i++)
            {
                carros.Add(new SItem
                {
                    Nome = String.Format("Carro {0}", i + 1),
                    Inicio = chegada
                });
                chegada += (int)Math.Round((-8) * Math.Log(rand.NextDouble()));
            }

            var pontoA = new Activity
            {
                Print = false
            };
            var lavaRapido = new Activity
            {
                Nome = "Lava Rápido",
                Media = 10,
                DesvPad = 2,
                Print = true
            };
            var pontoC = new Activity
            {
                Print = false
            };
            var centroManutenção = new Activity
            {
                Nome = "Centro de Manutenção",
                Media = 18,
                DesvPad = 4,
                Print = true
            };
            var pontoB = new Activity
            {
                Print = false
            };
            var secagem = new Activity
            {
                Nome = "Secagem",
                Media = 6,
                DesvPad = 2
            };
            var aspirador = new Activity
            {
                Nome = "Aspirador",
                Media = 12,
                DesvPad = 3
            };

            var abastecimento = new Activity
            {
                Nome = "Abastecimento",
                Print = false
            };
            var alcool = new Activity
            {
                Nome = "Alcool",
                Media = 5,
                DesvPad = 0,
                Print = true
            };
            var gasolina = new Activity
            {
                Nome = "Gasolina",
                Media = 5,
                DesvPad = 0,
                Print = true
            };
            var diesel = new Activity
            {
                Nome = "Disel",
                Media = 8,
                DesvPad = 0,
                Print = true
            };
            var lanchonete = new Activity
            {
                Nome = "Lanchonete",
                Media = 20,
                DesvPad = 2
            };
            var pagamento = new Activity
            {
                Nome = "Pagamento",
                Media = 10,
                DesvPad = 3
            };
            var pontoE = new Activity
            {
                Print = false
            };
            var pontoF = new Activity
            {
                Print = false
            };
            var pontoG = new Activity
            {
                Print = false
            };

            pontoA.PreencheItensEntrada(carros);

            //PontoA
            pontoA.AddConection(lavaRapido,0.6);
            pontoA.AddConection(centroManutenção);

            //Lava Rapido
            lavaRapido.AddConection(pontoC);

            //Centro Manutenção
            centroManutenção.AddConection(pontoB);

            //PontoB
            pontoB.AddConection(lavaRapido, 0.8);

            //PontoC
            pontoC.AddConection(aspirador, 0.5);
            pontoC.AddConection(secagem);

            //Secagem
            secagem.AddConection(abastecimento);

            //Aspirador
            aspirador.AddConection(abastecimento);

            //Abastecimento e pontoD
            abastecimento.AddConection(alcool, 0.3);
            abastecimento.AddConection(gasolina, 0.8);
            abastecimento.AddConection(diesel);

            //Alcool
            alcool.AddConection(pontoE);

            //Gasolina
            gasolina.AddConection(pontoF);

            //Diesel
            diesel.AddConection(pontoG);

            //Ponto E
            pontoE.AddConection(pagamento, 0.7);
            pontoE.AddConection(lanchonete);

            //Ponto F
            pontoF.AddConection(pagamento, 0.5);
            pontoF.AddConection(lanchonete);

            //Ponto G
            pontoG.AddConection(pagamento, 0.3);
            pontoG.AddConection(lanchonete);

            //Lanchonete
            lanchonete.AddConection(pagamento);

            pontoA.SetupConnections();
            pontoA.Run();
        }
    }
}
