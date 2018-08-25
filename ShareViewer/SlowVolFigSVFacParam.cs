using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareViewer
{
    [Serializable]
    public class SlowVolFigSVFacParam
    {
        public SlowVolFigSVFacParam()
        {

        }

        public SlowVolFigSVFacParam(double x, double y, int z, double w)
        {
            XMin = 1.0;
            XMax = 5.0;
            this.X = x;

            YMin = 0;
            YMax = 0.005;
            this.Y = y;

            ZMin = 104;
            ZMax = 999;
            this.Z = z;

            WMin = 1.0;
            WMax = 99.000;
            this.W = w;

            ForceValid();
        }


        private double x;
        private double xMin;
        private double xMax;

        private double y;
        private double yMin;
        private double yMax;

        private int z;
        private int zMin;
        private int zMax;

        private double wMin;
        private double wMax;
        private double w;


        [Description("Lower limit for X")]
        [ReadOnly(true)]
        public double XMin { get => xMin; set => xMin = value;  }

        [Category("Parameter")]
        [Description("Upper limit for X")]
        [ReadOnly(true)]
        public double XMax { get => xMax; set => xMax = value; }

        [Category("Parameter")]
        [Description("Setting")]
        public double X { get => x; set => x = value; }

        [Category("Parameter")]
        [Description("Lower limit for Y")]
        [ReadOnly(true)]
        public double YMin { get => yMin; set => yMin = value; }

        [Category("Parameter")]
        [Description("Upper limit for Y")]
        [ReadOnly(true)]
        public double YMax { get => yMax; set => yMax = value; }

        [Category("Parameter")]
        [Description("Setting")]
        public double Y { get => y; set => y = value; }


        [Category("Parameter")]
        [Description("Lower limit for Z")]
        [ReadOnly(true)]
        public int ZMin { get => zMin; set => zMin = value; }

        [Category("Parameter")]
        [Description("Upper limit for Z")]
        [ReadOnly(true)]
        public int ZMax { get => zMax; set => zMax = value; }

        [Category("Parameter")]
        [Description("Setting")]
        public int Z { get => z; set => z = value; }

        [Category("Parameter")]
        [Description("Lower limit for W")]
        [ReadOnly(true)]
        public double WMin { get => wMin; set => wMin = value; }

        [Category("Parameter")]
        [Description("Upper limit for W")]
        [ReadOnly(true)]
        public double WMax { get => wMax; set => wMax = value; }

        [Category("Parameter")]
        [Description("Setting")]
        public double W { get => w; set => w = value; }

        public bool DiffersFrom(SlowVolFigSVFacParam other)
        {
            return (this.X != other.X || this.Y != other.Y || this.Z != other.Z || this.W != other.W);
        }

        public void ForceValid()
        {
            if (x < xMin) x = xMin;
            if (x > xMax) x = xMax;

            if (y < yMin) y = yMin;
            if (y > yMax) y = yMax;

            if (z < zMin) z = zMin;
            if (z > zMax) z = zMax;

            if (w < wMin) w = wMin;
            if (w > wMax) w = wMax;
        }

    }

}
