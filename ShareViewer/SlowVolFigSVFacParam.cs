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

        public SlowVolFigSVFacParam(int zmin, int zmax, int z, double ymin, double ymax, double y)
        {
            //assume values will obey the ranges allowed
            zMin = zmin;
            zMax = zmax;
            this.z = z;

            yMin = ymin;
            yMax = ymax;
            this.y = y;
        }

        private int z;
        private int zMin;
        private int zMax;
        private double y;
        private double yMin;
        private double yMax;


        [Category("Parameter")]
        [Description("Lower limit for Z")]
        [ReadOnly(true)]
        public int ZMin
        {
            get => zMin; set => zMin = value;
        }

        [Category("Parameter")]
        [Description("Upper limit for Z")]
        [ReadOnly(true)]
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
        [Description("Lower limit for Y")]
        [ReadOnly(true)]
        public double YMin
        {
            get => yMin; set => yMin = value;
        }

        [Category("Parameter")]
        [Description("Upper limit for Y")]
        [ReadOnly(true)]
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

        public bool DiffersFrom(SlowVolFigSVFacParam other)
        {
            return (this.Y != other.Y || this.Z != other.Z);
        }

    }

}
