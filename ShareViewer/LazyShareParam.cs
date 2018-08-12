﻿using System;
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
            if (to >= this.from) this.to = to; else this.to = this.from;
            if (initial >= this.from && initial <= this.to) this.setting = initial; else this.setting = this.from;
        }

        [Category("Parameter")]
        [Description("Minimum Value")]
        [ReadOnly(true)]
        public double From { get => from; set => from = value; }

        [Category("Parameter")]
        [ReadOnly(true)]
        [Description("Maximum Value")]
        public double To
        {
            get => to;
            set
            {
                if (value >= this.from) to = value;
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