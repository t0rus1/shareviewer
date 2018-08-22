using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
            this.from = 0;
            this.to = 0;
            this.setting = 0;
        }

        public LazyShareParam(double from, double to, double initial)
        {
            this.from = from;
            this.to = to;
            this.setting = initial;
        }

        [Category("Parameter")]
        [Description("Minimum Value")]
        //[ReadOnly(true)]
        public double From { get => from; set => from = value; }

        [Category("Parameter")]
        [Description("Maximum Value")]
        //[ReadOnly(true)]
        public double To
        {
            get => to;
            set
            {
                //if (value >= this.from) to = value;
                to = value;
            }
        }

        [Category("Parameter")]
        [Description("(Z) adjust to suit")]
        public double Setting { get => setting; set => setting = value; }

        public bool DiffersFrom(LazyShareParam other)
        {
            return this.Setting != other.Setting;
        }

    }
}
