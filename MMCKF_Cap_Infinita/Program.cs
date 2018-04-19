using System;
using System.Collections.Generic;
using Modelos.MMCKF_C_Inf;
using Modelos.Excel;
using Modelos;
using System.Runtime.InteropServices;

namespace Lab06
{
    class MainClass
    {
        public static double a = 110.0 / 3600.0;

        public static int c = 3;

        public static int kf = 6;

        public static void Main(string[] args)
        {
            List<CPU> cpus = new List<CPU>
            {
                new CPU
                {
                    Name = "Servidor",
                    A = a,
                    NumeroAmostras = 20,
                    C = c,
                    KF = kf,
                    MediaAmostras = () =>
                    {
                        Random random = new Random();
                        return (-8) * Math.Log(random.NextDouble());
                    }
                },
                new CPU
                {
                    Name = "C1",
                    A = a,
                    MI = 0.25,
                    KF = kf,
                    C = c
                },
                new CPU
                {
                    Name = "C2",
                    A = a,
                    TS = 10,
                    KF = kf,
                    C = c
                },
                new CPU
                {
                    Name = "C3",
                    A = a,
                    NumeroAmostras = 10,
                    C = c,
                    KF = kf,
                    MediaAmostras = () =>
                    {
                        Random random = new Random();
                        return (-12) * Math.Log(random.NextDouble());
                    }
                }
            };


            string path = string.Empty;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                path = "C:\\Users\\T-Gamer\\Desktop\\dadosExcel.txt";
            else
                path = "/Users/coala/Desktop/dadosExcel.txt";

            ExcelWriter excel = new ExcelWriter(path);

            Type type = new CPU().GetType();

            Console.WriteLine("Pn: {0}", cpus[1].Pn(4));
            // Tabela do exercício 1˜
            excel.Linhas.Add("Exercicio 1");
            excel.AdicionarNomesCPUs(cpus.ConvertAll(x => (CPUBase)x));
            excel.AdicionarPropriedades(cpus.ConvertAll(x => (CPUBase)x), type);

            // Pula linha
            excel.Linhas.Add(Environment.NewLine);
            excel.Linhas.Add(Environment.NewLine);

            // Tabela grafico Tr x A
            excel.Linhas.Add("Grafico Tr x A");
            excel.AdicionarNomesCPUs(cpus.ConvertAll(x => (CPUBase)x), false, "A");
            for (int i = 100; i <= 2000; i += 100)
            {
                string linha = String.Format("{0}:", i);
                foreach (CPU cpu in cpus)
                {
                    cpu.A = (double)i / 3600.0;
                    linha += String.Format("{0}:", cpu.Tr);
                }
                excel.Linhas.Add(linha);
            }

            // Pula linha
            excel.Linhas.Add(Environment.NewLine);
            excel.Linhas.Add(Environment.NewLine);

            // Tabela grafico Tw x A
            excel.Linhas.Add("Grafico Tw x A");
            excel.AdicionarNomesCPUs(cpus.ConvertAll(x => (CPUBase)x), false, "A");
            for (int i = 100; i <= 2000; i += 100)
            {
                string linha = String.Format("{0}:", i);
                foreach (CPU cpu in cpus)
                {
                    cpu.A = (double)i/3600.0;
                    linha += String.Format("{0}:", cpu.Tw);
                }
                excel.Linhas.Add(linha);
            }

            // Escreve o arquivo
            excel.EscreveLinhas();

        }
    }
}
