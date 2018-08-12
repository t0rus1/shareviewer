using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareViewer
{
    [Serializable]
    public class SlowPriceParam
    {
        public SlowPriceParam()
        {

        }

        public SlowPriceParam(double zmin, double zmax, double ymin, double ymax, double zDefault)
        {
            zMin = zmin;
            if (zmax >= zMin) zMax = zmax; else zMax = zMin;

            //the yMin and Ymax will be applied to Ya,Yb,Yc and Yd
            yMin = ymin;
            if (ymax >= yMin) yMax = ymax; else yMax = yMin;

            if (zDefault >= zMin && zDefault <= zMax) z = zDefault; else z = zMin;
        }

        private double z;
        private double ya;
        private double yb;
        private double yc;
        private double yd;

        private double zMin;
        private double zMax;
        private double yMin;
        private double yMax;

        [Category("Parameter")]
        [Description("Setting")]
        public double Z
        {
            get => z;
            set
            {
                if (value >= zMin && value <= zMax) z = value;
            }
        }

        [Category("Parameter")]
        [Description("Exponent Ya")]
        public double Ya
        {
            get => ya;
            set
            {
                if (value >= yMin && value <= yMax) ya = value;
            }
        }

        [Category("Parameter")]
        [Description("Exponent Yb")]
        public double Yb
        {
            get => yb;
            set
            {
                if (value >= yMin && value <= yMax) yb = value;
            }
        }

        [Category("Parameter")]
        [Description("Exponent Yc")]
        public double Yc
        {
            get => yc;
            set
            {
                if (value >= yMin && value <= yMax) yc = value;
            }
        }

        [Category("Parameter")]
        [Description("Exponent Yd")]
        public double Yd
        {
            get => yd;
            set
            {
                if (value >= yMin && value <= yMax) yd = value;
            }
        }

        [Category("Parameter")]
        [Description("Lower limit for Z")]
        [ReadOnly(true)]
        public double ZMin { get => zMin; set => zMin = value; }

        [Category("Parameter")]
        [Description("Upper limit for Z")]
        [ReadOnly(true)]
        public double ZMax
        {
            get => zMax;
            set
            {
                if (value >= zMin) zMax = value;
            }
        }

        [Category("Parameter")]
        [Description("Lower limit for each Y exponent")]
        [ReadOnly(true)]
        public double YMin { get => yMin; set => yMin = value; }

        [Category("Parameter")]
        [Description("Upper limit for each Y exponent")]
        [ReadOnly(true)]
        public double YMax
        {
            get => yMax;
            set
            {
                if (value >= yMin) yMax = value;
            }
        }


        public bool DiffersFrom(SlowPriceParam other)
        {
            return (this.Z != other.Z) || (this.Ya != other.Ya) || (this.Yb != other.Yb) || (this.Yc != other.Yc) || (this.Yd != other.Yd);
        }


    }
}
