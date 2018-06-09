using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.Distributions;
using P2;

namespace P2_Final
{
    class MainClass
    {
		// Atraso do Roteador
        static double aRoteador = 0.00015;
        // Capacidade das LANs = 100 Mbps = 100 * 1024000 * 0.8 bps
        static double capLan = 100 * 1024000 * 0.8;
        // Processamento Cliente
        static double procCliente = 0.005;
        // Velocidade Internet
        static double vInternet = 16000;
		// Overhead Protocolo
		static double overHeadProtocolo = 1.1;

        public static void Main(string[] args)
        {
			// Semente aleatória para o programa
            Random rand = new Random();         

			// Tempo Servidor de Aplicações Interação 2
			Func<double> tSA2 = () =>
			{
				return GetUniform(rand, 40, 60);
			};
			// Tempo Servidor de Aplicações Interação 3
			Func<double> tSA3 = () =>
			{
				return GetUniform(rand, 60, 120);
			};

			// Tempo Servidor Web Interação 1
			Func<double> tSW1 = () =>
			{
				return GetUniform(rand, 4, 6);
			};
			// Tempo Servidor Web Interação 2-2
			Func<double> tSW22 = () =>
			{
				return GetUniform(rand, 5, 7);
			};
			// Tempo Servidor Web Interação 2-5
			Func<double> tSW25 = () =>
			{
				return GetUniform(rand, 7, 10);
			};
			// Tempo Servidor Web Interação 3
			Func<double> tSW3 = () =>
			{
				return GetUniform(rand, 9, 12);
			};

			// Tempo Banco de Dados
			Func<double> tBD = () =>
			{
				return GetUniform(rand, 15, 30) + GetUniform(rand, 50, 400);
			};

			// Funções de Calcular tamanho
			Func<double> m1 = () =>
			{
				return GetTriangular(rand, 100, 200, 250) * overHeadProtocolo;
			};
			Func<double> m2 = () =>
			{
				return GetTriangular(rand, 100, 200, 300) * overHeadProtocolo;
			};
			Func<double> m3 = () =>
			{
				return GetTriangular(rand, 250, 400, 450) * overHeadProtocolo;
			};
			Func<double> m4 = () =>
			{
				return GetTriangular(rand, 1500, 2500, 3000) * overHeadProtocolo;
			};
			Func<double> m5 = () =>
			{
				return GetTriangular(rand, 1500, 2100, 2800) * overHeadProtocolo;
			};
			Func<double> m6 = () =>
			{
				return GetTriangular(rand, 400, 550, 800) * overHeadProtocolo;
			};
			Func<double> m7 = () =>
			{
				return GetTriangular(rand, 2000, 3000, 3500) * overHeadProtocolo;
			};
			Func<double> m8 = () =>
			{
				return GetTriangular(rand, 1800, 2000, 2300) * overHeadProtocolo;
			};
			Func<double> m9 = () =>
			{
				return GetTriangular(rand, 1500, 2100, 2800) * overHeadProtocolo;
			};         
   
			var no1 = new Activity
			{
				Nome = "Requisição do Cliente (Nó 1)",
				Print = true,
                CalcularTS = () =>
				{
					var tamanho = m1();
					return GetTempoInternet(tamanho) + aRoteador + GetTempoLan(tamanho) + procCliente;
				}
			};

			var intersec1 = new Activity
			{
				Nome = "Intersecção 1",
                Print = false,
                CalcularTS = () =>
                {
                    return 0.0;
                }
			};

			var no2_int1 = new Activity
			{
				Nome = "Servidor Web - Retornar ao Cliente (Nó 2 - Interação 1)",
                Print = true,
                CalcularTS = () =>
				{
					var tamanho = m2();
					return GetTempoLan(tamanho) + aRoteador + GetTempoInternet(tamanho) + procCliente + tSW1();
				}
			};

			var no2_int2 = new Activity
			{
				Nome = "Servidor Web - Ir para o Servidor de Aplicação (Nó 2 - Interação 2)",
				Print = true,
				CalcularTS = () =>
				{
					var tamanho = m3();
					return GetTempoLan(tamanho) + tSW22();
				}
			};

			var no3 = new Activity
			{
				Nome = "Cliente - Interação 1 (Nó 3)",
				Print = true,
                CalcularTS = () =>
                {
                    return 0.0;
                }
			};

			var intersec2 = new Activity
			{
				Nome = "Intersecção 2",
                Print = false,
                CalcularTS = () =>
				{
					return 0.0;
				}
			};

			var no4_int2 = new Activity
			{
				Nome = "Servidor de Aplicação - Ir para Servidor Web (Nó 4 - Interação 2)",
				Print = true,
                CalcularTS = () =>
				{
					var tamanho = m4();
					return GetTempoLan(tamanho) + tSA2();
				}
			};

			var no4_int3 = new Activity
			{
				Nome = "Servidor de Aplicação - Ir para Servidor de Banco de Dados (Nó 4 - Interação 3)",
                Print = true,
                CalcularTS = () =>
				{
					var tamanho = m6();
					return GetTempoLan(tamanho) + tSA3();
				}
			};

			var no5 = new Activity
			{
				Nome = "Servidor Web - Ir para o Cliente (Nó 5)",
				Print = true,
				CalcularTS = () =>
				{
					var tamanho = m5();
					return GetTempoLan(tamanho) + aRoteador + GetTempoInternet(tamanho) + procCliente + tSW25();
				}
			};

			var no6 = new Activity
			{
				Nome = "Cliente - Interação 2 (Nó 6)",
				Print = true,
                CalcularTS = () =>
                {
                    return 0.0;
                }
			};

			var no7 = new Activity
			{
				Nome = "Servidor de Banco de Dados - Ir para Servidor de Aplicação (Nó 7)",
				Print = true,
				CalcularTS = () =>
				{
					var tamanho = m7();
					return GetTempoLan(tamanho) + tBD();
				}
			};

			var no8 = new Activity
			{
				Nome = "Servidor de Aplicação - Ir para o Servidor Web (Nó 8)",
				Print = true,
				CalcularTS = () =>
				{
					var tamanho = m8();
					return GetTempoLan(tamanho) + tSA3();
				}
			};

			var no9 = new Activity
			{
				Nome = "Servidor Web - Ir para o Cliente (Nó 9)",
				Print = true,
				CalcularTS = () =>
				{
					var tamanho = m9();
					return GetTempoLan(tamanho) + aRoteador + GetTempoInternet(tamanho) + procCliente + tSW3();
				}
			};

			var no10 = new Activity
			{
				Nome = "Cliente - Interação 3 (Nó 10)",
				Print = true,
                CalcularTS = () =>
                {
                    return 0.0;
                }
			};

			var visitas = new List<SItem>();
			var ritmoChegada = 3600.0 / 250.0;
			var chegada = 0.0;
			// Gera As visitas
            for (int i = 0; i < 30; i++)
            {
				visitas.Add(new SItem
				{
					Nome = String.Format("Visita {0}", i + 1),
					Inicio = chegada
				});
				chegada += (-ritmoChegada) * Math.Log(rand.NextDouble());
            }
            
			foreach (SItem visita in visitas.OrderBy( v => v.Inicio))
			{
				Console.WriteLine(string.Format("{0} chegou: {1}", visita.Nome, visita.Inicio));
			}
            
			Console.WriteLine();

            // Preenche a fila de chegada de a1
			no1.PreencheItensEntrada(visitas);

			// Configura as conexões entre as atividades

            // Nó 1 para a decisão entre Interação 1 e 2
			no1.AddConection(intersec1);

			// Decisão entre Interação 1 e 2
			intersec1.AddConection(no2_int1, 0.05);
			intersec1.AddConection(no2_int2);

			// Nó 2 na interação 1 indo para o Cliente
			no2_int1.AddConection(no3);

			// Nó 2 na interação 2 indo para o Servidor de Aplicação - Interseção entre Interação 2 e 3
			no2_int2.AddConection(intersec2);

			// Decisão entre Interação 2 e 3
			intersec2.AddConection(no4_int2, 0.2);
			intersec2.AddConection(no4_int3);

			// Nó 4
			no4_int2.AddConection(no5);
			no4_int3.AddConection(no7);

			// Nó 5
			no5.AddConection(no6);

			// Nó 7
			no7.AddConection(no8);

			// Nó 8
			no8.AddConection(no9);

			// Nó 9
			no9.AddConection(no10);
            

			// Connecta as atividades (Independente neste caso)
			no1.SetupConnections();

			// Roda a simulação;
			no1.Run();
        }      

        static double GetTriangular(Random random, double lower, double mode, double upper)
		{
			return Triangular.Sample(random, lower, upper, mode);
		}

        static double GetUniform(Random random, double lower, double upper)
		{
			return ContinuousUniform.Sample(random, lower, upper) / 1000;
		}

        static double GetTempoInternet(double tamanho)
		{
			return tamanho / vInternet;
		}

        static double GetTempoLan(double tamanho)
		{
			return tamanho / capLan;
		}
    }
}
