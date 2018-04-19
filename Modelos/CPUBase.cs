using System;
using Matematica;

namespace Modelos
{
    public class CPUBase
    {
        private double? _ts;

        private double? _mi;

        public string Name { get; set; }

        public double A { get; set; }

        public Func<double> MediaAmostras { get; set; }

        public int NumeroAmostras { get; set; }

        public double TS
        {
            get
            {
                if (_ts == null && _mi != null)
                {
                    return 1.0 / (double)_mi;
                }
                else if (_ts == null && MediaAmostras != null)
                {
                    _ts = Funcoes.MediaAmostras(MediaAmostras, NumeroAmostras);
                    return (double)_ts;
                }
                else
                    return (double)_ts;
            }
            set
            {
                _ts = value;
            }
        }

        public double MI
        {
            get
            {
                if (_mi == null && _ts == null && MediaAmostras != null)
                    return 1.0 / TS;
                if (_mi == null && _ts != null)
                    return 1.0 / (double)_ts;
                else
                    return (double)_mi;
            }
            set
            {
                _mi = value;
            }
        }

        public double R
        {
            get
            {
                return A / MI;
            }
        }
    }
}
