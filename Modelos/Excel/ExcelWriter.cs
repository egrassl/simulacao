using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Modelos.Excel
{
    public class ExcelWriter
    {
        public List<string> Linhas { get; set; }

        private string _path;

        public ExcelWriter(string path)
        {
            _path = path;
            Linhas = new List<string>();
        }

        public void EscreveLinhas()
        {
            using (StreamWriter sw = File.CreateText(_path))
            {
                foreach (string linha in Linhas)
                {
                    sw.WriteLine(linha);
                }
            }
        }

        public void AdicionarNomesCPUs(List<CPUBase> cpus, bool addSpacing = true, string prefix = "")
        {
            string linha = (addSpacing ? "," + prefix  : prefix) + (prefix != "" ? "," : "");
            foreach (CPUBase cpu in cpus)
            {
                linha += string.Format("{0},", cpu.Name);
            }
            Linhas.Add(linha);
        }

        public void AdicionarPropriedades(List<CPUBase> cpus, Type type)
        {
            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                if (propertyInfo.Name == "Name" || propertyInfo.Name == "C" || propertyInfo.Name == "KF"
                    || propertyInfo.Name == "MediaAmostras" || propertyInfo.Name == "NumeroAmostras" )
                    continue;
                string linha = string.Format("{0}", propertyInfo.Name);
                foreach (CPUBase cpu in cpus)
                {
                    linha += String.Format(",{0}", propertyInfo.GetValue(Convert.ChangeType(cpu,type)));
                }
                Linhas.Add(linha);
            }
        }
    }
}
