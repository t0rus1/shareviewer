using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareViewer
{
    public class Param
    {
        private double from;
        private double to;
        private double setting;

        public Param(double from, double to, double initial)
        {
            this.from = from;
            this.to = to;
            this.setting = initial;
        }

        [Category("Parameter")]
        [Description("Minimum Value")]
        public double From { get => from; set => from = value; }

        [Category("Parameter")]
        [Description("Maximum Value")]
        public double To { get => to; set => to = value; }

        [Category("Parameter")]
        [Description("Z (Threshold)")]
        public double Setting { get => setting; set => setting = value; }

    }
}
