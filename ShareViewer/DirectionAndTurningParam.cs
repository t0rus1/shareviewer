using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareViewer
{

    [Serializable]
    public class DirectionAndTurningParam
    {
        private double from;
        private double to;
        private double z;
        private int pGcThreshold;


        public DirectionAndTurningParam()
        {
            this.from = 0;
            this.to = 0;
            this.z = 0;
            this.pGcThreshold = 100000;
        }

        public DirectionAndTurningParam(double from, double to, int threshold, double zinitial)
        {
            this.from = from;
            if (to >= this.from) { this.to = to; } else { this.to = from; }
            this.pGcThreshold = threshold;
            if (zinitial >= this.from && zinitial <= this.to) this.z = zinitial; else this.z = this.from; 
        }

        [Category("Parameter")]
        [Description("Minimum Value")]
        [ReadOnly(true)]
        public double From { get => from;  set => from = value;  }

        [Category("Parameter")]
        [Description("Maximum Value")]
        [ReadOnly(true)]
        public double To
        {
            get => to;
            set
            {
                if (value >= this.from) { this.to = value; } else { this.to = this.from; }
            }
        }

        [Category("Parameter")]
        [Description("(Setting) adjust to suit")]
        public double Z
        {
            get => z;
            set
            {
                if (value >= from && value <= to)
                {
                    z = value;
                }
            }
        }

        [Category("Parameter")]
        [Description("PGc minimum threshold")]
        [ReadOnly(true)]
        public int PGcThreshold { get => pGcThreshold; set => pGcThreshold = value; }

        public bool DiffersFrom(DirectionAndTurningParam other)
        {
            return (this.Z != other.Z);
        }


    }


}
