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

        public DirectionAndTurningParam(double from, double to, int threshold, double initial)
        {
            this.from = from;
            this.to = to;
            this.pGcThreshold = threshold;
            this.z = initial;
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
        [Description("(Setting) adjust to suit")]
        public double Z { get => z; set => z = value; }

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
