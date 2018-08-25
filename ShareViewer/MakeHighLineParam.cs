using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareViewer
{
    [Serializable]
    public class MakeHighLineParam
    {
        public MakeHighLineParam()
        {

        }

        public MakeHighLineParam(double zDefault)
        {
            zMin = 0;
            zMax = 0.001;
            z = zDefault;
            ForceValid();
        }


        private double zMin;
        private double zMax;
        private double z;

        [Category("Parameter")]
        [Description("Lower limit for Z")]
        //[ReadOnly(true)]
        public double ZMin { get => zMin; set => zMin = value; }

        [Category("Parameter")]
        [Description("Upper limit for Z")]
        //[ReadOnly(true)]
        public double ZMax { get => zMax; set=> zMax = value; }

        [Category("Parameter")]
        [Description("Setting")]
        public double Z { get => z; set => z = value; }

        public bool DiffersFrom(MakeHighLineParam other)
        {
            return this.Z != other.Z;
        }

        public void ForceValid()
        {
            if (z < zMin) z = zMin;
            if (z > zMax) z = zMax;
        }

    }

}
