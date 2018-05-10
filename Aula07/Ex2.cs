using System;
using Matematica;
using Modelos;
using System.Collections.Generic;
using Modelos.Excel;
using System.Linq;
using System.Runtime.InteropServices;


namespace Aula07
{
    public class Ex2
    {
        double a = 100.0 / 3600.0;

        int c = 5;

        public void Run()
        {
            List<Modelos.MMC_Inf.CPU> servidoresA = new List<Modelos.MMC_Inf.CPU>
            {
                new Modelos.MMC_Inf.CPU
                {
                    Name = "Servidores",
                    A = a,
                    C = c,
                    TS = 2.0 * 60.0
                }
            };

            string path = string.Empty;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                path = "C:\\Users\\T-Gamer\\Desktop\\dadosExcel.txt";
            else
                path = "/Users/coala/Desktop/dadosExcel.txt";

            ExcelWriter excel = new ExcelWriter(path);

            Type typeA = new Modelos.MMC_Inf.CPU().GetType();

            // Tabela do exercício 2 A
            excel.Linhas.Add("Exercicio 2 A");
            excel.AdicionarPropriedades(servidoresA.ConvertAll(x => (object)x), typeA);

            // Pula linha
            excel.Linhas.Add(Environment.NewLine);
            excel.Linhas.Add(Environment.NewLine);

            List<Modelos.MMC_Inf.CPU> servidoresB = new List<Modelos.MMC_Inf.CPU>();

            for (int i = 0; i < 5; i++){
                servidoresB.Add(new Modelos.MMC_Inf.CPU
                {
                    Name = String.Format("Servidor {0}", i + 1),
                    A = a / 5.0,
                    C = 1,
                    TS = 2.0 * 60.0
                });
            }

            // Tabela do exercício 2B
            excel.Linhas.Add("Exercicio 2 B");
            excel.AdicionarNomesCPUs(servidoresB.ConvertAll(x => (CPUBase)x));
            excel.AdicionarPropriedades(servidoresB.ConvertAll(x => (object)x), typeA);

            // Pula linha
            excel.Linhas.Add(Environment.NewLine);
            excel.Linhas.Add(Environment.NewLine);

            // ======= Exercicio 3 =======

            List<Modelos.MMC_Inf.CPU> servidoresC = new List<Modelos.MMC_Inf.CPU>
            {
                new Modelos.MMC_Inf.CPU
                {
                    Name = "Servidores",
                    A = 20.0 / 3600.0,
                    C = 4,
                    NumeroAmostras = 1000,
                    MediaAmostras = () =>
                    {
                        Random random = new Random();
                        return (-5.0 * 60.0) * Math.Log(random.NextDouble());
                    }
                }
            };

            // Tabela do exercício 3 A
            excel.Linhas.Add("Exercicio 3 A");
            excel.AdicionarPropriedades(servidoresC.ConvertAll(x => (object)x), typeA);

            // Pula linha
            excel.Linhas.Add(Environment.NewLine);
            excel.Linhas.Add(Environment.NewLine);

            List<Modelos.MMC_Inf.CPU> servidoresD = new List<Modelos.MMC_Inf.CPU>();

            for (int i = 0; i < 4; i++)
            {
                servidoresD.Add(new Modelos.MMC_Inf.CPU
                {
                    Name = String.Format("Servidor {0}", i + 1),
                    A = (20.0 / 3600.0) / 4.0,
                    C = 1,
                    TS = servidoresC[0].TS
                });
            }

            // Tabela do exercício 3B
            excel.Linhas.Add("Exercicio 3 B");
            excel.AdicionarNomesCPUs(servidoresD.ConvertAll(x => (CPUBase)x));
            excel.AdicionarPropriedades(servidoresD.ConvertAll(x => (object)x), typeA);

            // Pula linha
            excel.Linhas.Add(Environment.NewLine);
            excel.Linhas.Add(Environment.NewLine);

            excel.EscreveLinhas();
        }
    }
}
