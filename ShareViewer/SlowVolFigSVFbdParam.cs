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

        public SlowVolFigSVFbdParam(int zmin, int zmax, int z, double ymin, double ymax, double y, double wmin, double wmax, double w)
        {
            ZMin = zmin;
            ZMax = zmax;
            Z = z;
            YMin = ymin;
            YMax = ymax;
            Y = y;
            WMin = wmin;
            WMax = wmax;
            W = w;
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
                if (value >= zMin) zMax = value; else zMax = zMin;
            }
        }

        [Category("Parameter")]
        [Description("Setting")]
        public int Z
        {
            get => z;
            set
            {
                if (value >= zMin && value <= zMax) z = value;
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
                if (value >= yMin) yMax = value; else yMax = yMin; 
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
                if (value >= wMin && value <= wMax)
                {
                    w = value;
                }
            }
        }

        public bool DiffersFrom(SlowVolFigSVFbdParam other)
        {
            return (Z != other.Z || Y != other.Y || W != other.W);
        }

    }

}
