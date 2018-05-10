using System;
using P2;
using System.Collections.Generic;

namespace TesteP2
{
    class MainClass
    {
        public static void Main(string[] args)
        {

            Activity.numeroAmostra = 5;
            // Criando atividades
            var entrada = new Activity
            {
                Nome = "Entrada",
                Media = 0,
                DesvPad = 0
            };
            var lavagem = new Activity
            {
                Nome = "Lavagem",
                Media = 10,
                DesvPad = 2,
                Print = true
            };
            var sabao = new Activity
            {
                Nome = "Sabao",
                Media = 6,
                DesvPad = 1,
                Print = true
            };
            var enxague = new Activity
            {
                Nome = "Enxague",
                Media = 12,
                DesvPad = 2,
                Print = true
            };
            var centroManutencao = new Activity
            {
                Nome = "Centro Manutenção",
                Media = 18,
                DesvPad = 5,
                Print = true
            };
            var lanchonete = new Activity
            {
                Nome = "Lanchonete",
                Media = 20,
                DesvPad = 4,
                Print = true
            };
            var aspirador = new Activity
            {
                Nome = "Aspirador",
                Media = 16,
                DesvPad = 4,
                Print = true
            };
            var intersec = new Activity
            {
                Nome = "Intersec"
            };
            var saida1 = new Activity
            {
                Nome = "Saída1"
            };
            var saida2 = new Activity
            {
                Nome = "Saída2"
            };
            var saida3 = new Activity
            {
                Nome = "Saída3"
            };

            // Conectando

            entrada.AddConection(lavagem, 0.7);
            entrada.AddConection(centroManutencao);
            lavagem.AddConection(sabao);
            lavagem.AddConection(aspirador, 0.5);
            sabao.AddConection(enxague);
            enxague.AddConection(aspirador);
            aspirador.AddConection(lanchonete);
            aspirador.AddConection(saida2, 0.5);
            centroManutencao.AddConection(saida1,0.2);
            centroManutencao.AddConection(intersec);
            intersec.AddConection(lanchonete);
            intersec.AddConection(lavagem, 0.5);
            lanchonete.AddConection(saida3);

            entrada.SetupConnections();

            // Checando
            PrintActivity(lavagem);
            PrintActivity(sabao);
            PrintActivity(enxague);
            PrintActivity(aspirador);
            PrintActivity(saida2);
            PrintActivity(centroManutencao);
            PrintActivity(saida1);
            PrintActivity(lanchonete);
            PrintActivity(saida3);

            var entradas = new List<SItem>
            {
                new SItem
                {
                    Inicio = 0,
                    Nome = "Carro1"
                },
                new SItem
                {
                    Inicio = 4,
                    Nome = "Carro2"
                },
                new SItem
                {
                    Inicio = 7,
                    Nome = "Carro3"
                },
                new SItem
                {
                    Inicio = 13,
                    Nome = "Carro4"
                },
                new SItem
                {
                    Inicio = 15,
                    Nome = "Carro5"
                }
            };



            Console.WriteLine("\n\n======Teste 2======");

            foreach (SItem i in entradas)
            {
                entrada.FilaEntrada.Add(i);
            }

            entrada.Run();


        }

        static void PrintActivity(Activity act)
        {
            Console.WriteLine("{0}: {1}", act.Nome, act.NumeroConexoes);
        }
    }
}
