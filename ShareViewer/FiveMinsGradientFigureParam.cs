using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareViewer
{
    [Serializable]
    public class FiveMinsGradientFigureParam
    {
        public FiveMinsGradientFigureParam()
        {

        }

        public FiveMinsGradientFigureParam(int z, double x, double y)
        {
            zMin = 104;
            zMax = 999;
            this.z = z;

            xMin = 1.0;
            xMax = 5.0;
            this.x = x;

            yMin = 0;
            yMax = 0.005;
            this.z = z;

            ForceValid();
        }


        private double xMin;
        private double xMax;
        private double x;

        private double yMin;
        private double yMax;
        private double y;

        private int zMin;
        private int zMax;
        private int z;

        [Category("Parameter")]
        [Description("Lower limit for X")]
        //[ReadOnly(true)]
        public double XMin { get => xMin; set => xMin = value; }

        [Category("Parameter")]
        [Description("Upper limit for X")]
        //[ReadOnly(true)]
        public double XMax { get => xMax; set => xMax = value; }

        [Category("Parameter")]
        [Description("X")]
        public double X { get => x; set => x = value; }

        [Category("Parameter")]
        [Description("Lower limit for Y")]
        //[ReadOnly(true)]
        public double YMin { get => yMin; set => yMin = value; }

        [Category("Parameter")]
        [Description("Upper limit for Y")]
        //[ReadOnly(true)]
        public double YMax { get => yMax; set => yMax = value; }

        [Category("Parameter")]
        [Description("Y")]
        public double Y { get => y; set => y = value; }

        [Category("Parameter")]
        [Description("Lower limit for Z")]
        //[ReadOnly(true)]
        public int ZMin { get => zMin; set => zMin = value; }

        [Category("Parameter")]
        [Description("Upper limit for Z")]
        //[ReadOnly(true)]
        public int ZMax { get => zMax; set => zMax = value; }

        [Category("Parameter")]
        [Description("Z")]
        public int Z { get => z; set => z = value; }

        public bool DiffersFrom(FiveMinsGradientFigureParam other)
        {
            return (this.X != other.X) || (this.Y != other.Y) || (this.Z != other.Z);
        }

        public void ForceValid()
        {
            if (x < xMin) x = xMin;
            if (x > xMax) x = xMax;

            if (y < yMin) y = yMin;
            if (y > yMax) y = yMax;

            if (z < zMin) z = zMin;
            if (z > zMax) z = zMax;

        }

    }
}
