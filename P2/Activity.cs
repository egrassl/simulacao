using System;
using System.Collections.Generic;
using System.Linq;

namespace P2
{
    public class Activity
    {      
        bool areConnectionsSetted = false;

        Random rand = new Random();

		public Func<double> CalcularTS { get; set; }

        public bool Print { get; set; }

        public static int numeroAmostra { get; set; }

        public int Media { get; set; }
        
        public int DesvPad { get; set; }

        public int NumberProcessed { get; set; }

        public List<SItem> FilaEntrada { get; set; }

        public List<SItem> Resultado { get; set; }

        public List<Tuple<Activity, double>> Connections { get; set; }

        public double TempoEspera { get; set; }
        
        public double TempoOcioso { get; set; }

        public string Nome { get; set; }

        public int NumeroConexoes { get; set; }

        public void SetupConnections()
        {
            if (areConnectionsSetted)
                return;
            foreach (Tuple<Activity,double> tup in Connections)
            {
                var act = tup.Item1;
                act.NumeroConexoes++;
                act.SetupConnections();
            }
            areConnectionsSetted = true;
        }

        public Activity()
        {
            Connections = new List<Tuple<Activity, double>>();
            FilaEntrada = new List<SItem>();
            Resultado = new List<SItem>();
            Print = true;
        }

        public void Run()
        {
            if (!FilaEntrada.Any() && NumberProcessed == NumeroConexoes)
            {
                RunConnectedActivities();
                return;
            }
            else if (!FilaEntrada.Any() || NumberProcessed < NumeroConexoes)
                return;
            FilaEntrada = FilaEntrada.OrderBy(x => x.Inicio).ToList();
            foreach (SItem item in FilaEntrada)
            {
				var tempo =  CalcularTS();
                var newItem = AddItemToFilaProcesso(new SItem { Nome = item.Nome, Inicio = item.Inicio }, tempo);
                Resultado.Add(newItem);
                sendToConnection(new SItem { Inicio = newItem.Fim, Nome = newItem.Nome });
            }
            if (Print)
                //PrintAtividadesEntrada();
                PrintAtividadesResultado();
            RunConnectedActivities();
        }

        public void PreencheItensEntrada(List<SItem> itens)
        {
            foreach(SItem item in itens)
            {
                FilaEntrada.Add(item);
            }
        }

        public void sendToConnection(SItem newItem)
        {
            if (Connections == null || !Connections.Any())
                return;
            var rand1 = rand.NextDouble();
            var prob = Connections.OrderBy(x => x.Item2);
            foreach (Tuple<Activity, double> tup in prob)
            {
                if (rand1 <= tup.Item2)
                {
                    tup.Item1.FilaEntrada.Add(newItem);
                    return;
                }
            }
        }

        public void AddConection(Activity conn, double prob = 1.0)
        {
            Connections.Add(new Tuple<Activity, double>(conn, prob));
        }

        void RunConnectedActivities()
        {
            foreach (Tuple<Activity,double> tup in Connections)
            {
                var act = tup.Item1;
                act.NumberProcessed++;
                act.Run();
            }
        }

        SItem AddItemToFilaProcesso(SItem newItem, double tempo)
        {
            var ultimoItem = Resultado.OrderByDescending(x => x.Fim).FirstOrDefault();
            if (ultimoItem == null)
            {
                newItem.Fim = newItem.Inicio + tempo;
                return newItem;
            }
            if (ultimoItem.Fim < newItem.Inicio )
            {
                TempoOcioso += newItem.Inicio - ultimoItem.Fim;
            }
            else if (ultimoItem.Fim > newItem.Inicio)
            {
                TempoEspera += ultimoItem.Fim - newItem.Inicio;
                newItem.Inicio = ultimoItem.Fim;
            }
            newItem.Fim = newItem.Inicio + tempo;
            return newItem;
        }

        void PrintAtividadesEntrada()
        {
            Console.WriteLine("{0}:", Nome);
            foreach (SItem i in FilaEntrada)
            {
                Console.WriteLine("Chegada {0}, Fim {1}", i.Inicio, i.Fim);
            }
        }

        void PrintAtividadesResultado()
        {
            Console.WriteLine("{0}:", Nome);
            foreach (SItem i in Resultado)
            {
                Console.WriteLine("{0} - Chegada {1}, Fim {2}",i.Nome, i.Inicio, i.Fim);
            }
            Console.WriteLine("Tempo Espera {0} - Tempo Ocioso {1}\n", TempoEspera, TempoOcioso);
        }
    }
}
