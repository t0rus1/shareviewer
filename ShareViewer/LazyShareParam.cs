using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShareViewer
{
    [Serializable]
    public class LazyShareParam
    {
        private double from;
        private double to;
        private double setting;

        public LazyShareParam()
        {
        }

        public LazyShareParam(double initial)
        {
            this.from = 10.0;
            this.to = 1000000;
            this.setting = initial;
            ForceValid();
        }

        [Category("Parameter")]
        [Description("Minimum Value")]
        [ReadOnly(true)]
        public double From { get => from; set => from = value; }

        [Category("Parameter")]
        [Description("Maximum Value")]
        [ReadOnly(true)]
        public double To { get => to; set => to = value; }

        [Category("Parameter")]
        [Description("(Z) adjust to suit")]
        public double Setting { get => setting; set => setting = value; }

        public bool DiffersFrom(LazyShareParam other)
        {
            return this.Setting != other.Setting;
        }

        public void ForceValid()
        {
            if (setting < from) setting = from;
            if (setting > to) setting = to;
        }

        public string Summarize()
        {
            string summary = "[Lazy Share Parameters] ";
            foreach (PropertyInfo property in typeof(LazyShareParam).GetProperties())
            {
                summary += $"{property.Name}: {property.GetValue(this).ToString()}; ";       
            }
            return summary;
        }

    }
}
