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

        public SlowVolFigSVFacParam(
            double xmin, double xmax, double x, 
            double ymin, double ymax, double y, 
            int zmin, int zmax, int z, 
            double wmin, double wmax, double w)
        {
            //values will get coerced
            XMin = xmin;  XMax = xmax;  this.X = x;

            YMin = ymin; YMax = ymax; this.Y = y;

            ZMin = zmin; ZMax = zmax; this.Z = z;

            WMin = wmin; WMax = wmax; this.W = w;
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
        //[ReadOnly(true)]
        public double XMin
        {
            get => xMin; set => xMin = value;
        }

        [Category("Parameter")]
        [Description("Upper limit for X")]
        //[ReadOnly(true)]
        public double XMax
        {
            get => xMax;
            set
            {
                if (value >= xMin) xMax = value; else xMax = xMin;
            }
        }

        [Category("Parameter")]
        [Description("Setting")]
        public double X
        {
            get => x;
            set
            {
                if (value >= xMin && value <= xMax) x = value;
            }
        }

        [Category("Parameter")]
        [Description("Lower limit for Y")]
        //[ReadOnly(true)]
        public double YMin
        {
            get => yMin; set => yMin = value;
        }

        [Category("Parameter")]
        [Description("Upper limit for Y")]
        //[ReadOnly(true)]
        public double YMax
        {
            get => yMax;
            set
            {
                if (value >= yMin) yMax = value;
            }
        }

        [Category("Parameter")]
        [Description("Setting")]
        public double Y
        {
            get => y;
            set
            {
                if (value >= yMin && value <= yMax) y = value;
            }
        }


        [Category("Parameter")]
        [Description("Lower limit for Z")]
        //[ReadOnly(true)]
        public int ZMin
        {
            get => zMin; set => zMin = value;
        }

        [Category("Parameter")]
        [Description("Upper limit for Z")]
        //[ReadOnly(true)]
        public int ZMax
        {
            get => zMax;
            set
            {
                if (value >= zMin) zMax = value;
            }
        }

        [Category("Parameter")]
        [Description("Setting")]
        public int Z
        {
            get => z;
            set
            {
                if (value <= zMax && value >= zMin) z = value;
            }
        }

        [Category("Parameter")]
        [Description("Lower limit for W")]
        //[ReadOnly(true)]
        public double WMin
        {
            get => wMin; set => wMin = value;
        }

        [Category("Parameter")]
        [Description("Upper limit for W")]
        //[ReadOnly(true)]
        public double WMax
        {
            get => wMax;
            set
            {
                if (value >= wMin) wMax = value; else wMax = wMin;
            }
        }

        [Category("Parameter")]
        [Description("Setting")]
        public double W
        {
            get => w;
            set
            {
                if (value >= wMin && value <= wMax) w = value;
            }
        }


        public bool DiffersFrom(SlowVolFigSVFacParam other)
        {
            return (this.X != other.X || this.Y != other.Y || this.Z != other.Z || this.W != other.W);
        }

    }

}
