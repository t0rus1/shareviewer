using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareViewer
{
    [Serializable]
    public class SlowVolFigSVFbdParam
    {
        public SlowVolFigSVFbdParam()
        {

        }

        public SlowVolFigSVFbdParam(int z, double y, double w)
        {
            ZMin = 104;
            ZMax = 2000;
            Z = z;
            YMin = 0;
            YMax = 0.005;
            Y = y;
            WMin = 1.0;
            WMax = 99.0;
            W = w;
            ForceValid();
        }

        private int zMin;
        private int zMax;
        private int z;

        private double yMin;
        private double yMax;
        private double y;

        private double wMin;
        private double wMax;
        private double w;

        [Category("Parameter")]
        [Description("Lower limit for Z")]
        //[ReadOnly(true)]
        public int ZMin { get => zMin; set => zMin = value;  }

        [Category("Parameter")]
        [Description("Upper limit for Z")]
        //[ReadOnly(true)]
        public int ZMax { get => zMax; set => zMax = value; }

        [Category("Parameter")]
        [Description("Setting")]
        public int Z { get => z; set => z = value; }

        [Category("Parameter")]
        [Description("Lower limit for Y")]
        //[ReadOnly(true)]
        public double YMin { get => yMin; set => yMin = value; }

        [Category("Parameter")]
        [Description("Upper limit for Y")]
        //[ReadOnly(true)]
        public double YMax { get => yMax; set => yMax = value; }

        [Category("Parameter")]
        [Description("Setting")]
        public double Y { get => y; set => y = value; }

        [Category("Parameter")]
        [Description("Lower limit for W")]
        //[ReadOnly(true)]
        public double WMin { get => wMin; set => wMin = value; }

        [Category("Parameter")]
        [Description("Upper limit for W")]
        //[ReadOnly(true)]
        public double WMax { get => wMax; set => wMax = value; }

        [Category("Parameter")]
        [Description("Setting")]
        public double W { get => w; set => w = value; }

        public bool DiffersFrom(SlowVolFigSVFbdParam other)
        {
            return (Z != other.Z || Y != other.Y || W != other.W);
        }

        public void ForceValid()
        {
            if (z < zMin) z = zMin;
            if (z > zMax) z = zMax;

            if (y < yMin) y = yMin;
            if (y > yMax) y = yMax;

            if (w < wMin) w = wMin;
            if (w > wMax) w = wMax;

        }

    }

}
