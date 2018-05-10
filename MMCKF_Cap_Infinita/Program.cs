using System;
using System.Collections.Generic;
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
            List<Modelos.MMCKF_C_Inf.CPU> cpus = new List<Modelos.MMCKF_C_Inf.CPU>
            {
                new Modelos.MMCKF_C_Inf.CPU
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
                new Modelos.MMCKF_C_Inf.CPU
                {
                    Name = "C1",
                    A = a,
                    MI = 0.25,
                    KF = kf,
                    C = c
                },
                new Modelos.MMCKF_C_Inf.CPU
                {
                    Name = "C2",
                    A = a,
                    TS = 10,
                    KF = kf,
                    C = c
                },
                new Modelos.MMCKF_C_Inf.CPU
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

            Type type1 = new Modelos.MMCKF_C_Inf.CPU().GetType();

            //Console.WriteLine("Pn: {0}", cpus[1].Pn(4));
            // Tabela do exercício 1˜
            excel.Linhas.Add("Exercicio 1 - MMCKF");
            excel.AdicionarNomesCPUs(cpus.ConvertAll(x => (CPUBase)x));
            excel.AdicionarPropriedades(cpus.ConvertAll(x => (object)x), type1);

            // Pula linha
            excel.Linhas.Add(Environment.NewLine);
            excel.Linhas.Add(Environment.NewLine);

            // Tabela grafico Tr x A
            excel.Linhas.Add("Grafico Tr x A");
            excel.AdicionarNomesCPUs(cpus.ConvertAll(x => (CPUBase)x), false, "A");
            for (int i = 100; i <= 2000; i += 100)
            {
                string linha = String.Format("{0}:", i);
                foreach (Modelos.MMCKF_C_Inf.CPU cpu in cpus)
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
                foreach (Modelos.MMCKF_C_Inf.CPU cpu in cpus)
                {
                    cpu.A = (double)i/3600.0;
                    linha += String.Format("{0}:", cpu.Tw);
                }
                excel.Linhas.Add(linha);
            }

            // Testar para modelo MMC

            List<Modelos.MMC_Inf.CPU> mmcCpus = new List<Modelos.MMC_Inf.CPU>();

            foreach (Modelos.MMCKF_C_Inf.CPU cpu in cpus)
            {
                mmcCpus.Add(new Modelos.MMC_Inf.CPU
                {
                    Name = cpu.Name,
                    A = cpu.A,
                    C = cpu.C,
                    TS = cpu.TS
                });
            }

            Type type2 = new Modelos.MMC_Inf.CPU().GetType();

            // Tabela do MMC
            excel.Linhas.Add("Exercicio 1 - MMC");
            excel.AdicionarNomesCPUs(mmcCpus.ConvertAll(x => (CPUBase)x));
            excel.AdicionarPropriedades(mmcCpus.ConvertAll(x => (object)x), type2);

            // Pula linha
            excel.Linhas.Add(Environment.NewLine);
            excel.Linhas.Add(Environment.NewLine);

            // Tabela grafico Tr x A
            excel.Linhas.Add("Grafico Tr x A");
            excel.AdicionarNomesCPUs(mmcCpus.ConvertAll(x => (CPUBase)x), false, "A");
            for (int i = 100; i <= 2000; i += 100)
            {
                string linha = String.Format("{0}:", i);
                foreach (Modelos.MMC_Inf.CPU cpu in mmcCpus)
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
            excel.AdicionarNomesCPUs(mmcCpus.ConvertAll(x => (CPUBase)x), false, "A");
            for (int i = 100; i <= 2000; i += 100)
            {
                string linha = String.Format("{0}:", i);
                foreach (Modelos.MMC_Inf.CPU cpu in mmcCpus)
                {
                    cpu.A = (double)i / 3600.0;
                    linha += String.Format("{0}:", cpu.Tw);
                }
                excel.Linhas.Add(linha);
            }

            // Escreve o arquivo
            excel.EscreveLinhas();

        }
    }
}
