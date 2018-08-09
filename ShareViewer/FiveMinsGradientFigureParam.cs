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

        public FiveMinsGradientFigureParam( int zmin, int zmax, int z, double xmin, double xmax, double x, double ymin, double ymax, double y)
        {
            zMin = zmin;
            zMax = zmax;
            this.z = z;

            xMin = xmin;
            xMax = xmax;
            this.x = x;

            yMin = ymin;
            yMax = ymax;
            this.y = y;
        }

        private double x;
        private double y;
        private int z;
        private double xMin;
        private double xMax;
        private double yMin;
        private double yMax;
        private int zMin;
        private int zMax;

        [Category("Parameter")]
        [Description("X")]
        public double X { get => x; set => x = value; }

        [Category("Parameter")]
        [Description("Y")]
        public double Y { get => y; set => y = value; }

        [Category("Parameter")]
        [Description("Z")]
        public int Z { get => z; set => z = value; }

        [Category("Parameter")]
        [Description("Lower limit for X")]
        [ReadOnly(true)]
        public double XMin { get => xMin; set => xMin = value; }

        [Category("Parameter")]
        [Description("Upper limit for X")]
        [ReadOnly(true)]
        public double XMax { get => xMax; set => xMax = value; }

        [Category("Parameter")]
        [Description("Lower limit for Y")]
        [ReadOnly(true)]
        public double YMin { get => yMin; set => yMin = value; }

        [Category("Parameter")]
        [Description("Upper limit for Y")]
        [ReadOnly(true)]
        public double YMax { get => yMax; set => yMax = value; }

        [Category("Parameter")]
        [Description("Lower limit for Z")]
        [ReadOnly(true)]
        public int ZMin { get => zMin; set => zMin = value; }

        [Category("Parameter")]
        [Description("Upper limit for Z")]
        [ReadOnly(true)]
        public int ZMax { get => zMax; set => zMax = value; }

        public bool DiffersFrom(FiveMinsGradientFigureParam other)
        {
            return (this.X != other.X) || (this.Y != other.Y) || (this.Z != other.Z);
        }


    }
}
