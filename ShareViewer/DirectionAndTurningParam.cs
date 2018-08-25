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
        private double pGcThreshold;


        public DirectionAndTurningParam()
        {
        }

        public DirectionAndTurningParam(double zinitial)
        {
            this.from = 0.99999;
            this.to = 1.01;
            this.pGcThreshold = 1;
            this.z = zinitial;
            ForceValid();
        }

        [Category("Parameter")]
        [Description("Minimum Value")]
        //[ReadOnly(true)]
        public double From { get => from;  set => from = value;  }

        [Category("Parameter")]
        [Description("Maximum Value")]
        //[ReadOnly(true)]
        public double To {  get => this.to; set => this.to = value; }

        [Category("Parameter")]
        [Description("(Setting) adjust to suit")]
        public double Z  { get => z; set => z = value; }

        [Category("Parameter")]
        [Description("PGc minimum threshold")]
        public double PGcThreshold { get => pGcThreshold; set => pGcThreshold = value; }

        public bool DiffersFrom(DirectionAndTurningParam other)
        {
            return (this.Z != other.Z || this.PGcThreshold != other.PGcThreshold);
        }

        public void ForceValid()
        {
            if (z < from) z = from;
            if (z > to) z = to;
        }
    }


}
