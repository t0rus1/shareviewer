using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareViewer
{
    [Serializable]
    public class MakeSlowVolumeParam
    {
        private double yMin;
        private double yMax;
        private double ya;
        private double yb;
        private double yc;
        private double yd;

        public MakeSlowVolumeParam()
        {

        }

        public MakeSlowVolumeParam(double ymin, double ymax, double ya, double yb, double yc, double yd)
        {

            //the yMin and Ymax will be applied to Ya,Yb,Yc and Yd
            this.yMin = ymin;
            YMax = ymax;  // is validated to be > yMin

            Ya = ya; // also gets validated
            Yb = yb;
            Yc = yc;
            Yd = yc;

        }

        [Category("Parameter")]
        [Description("Lower limit for Y")]
        //[ReadOnly(true)]
        public double YMin
        {
            get => yMin;
            set { yMin = value; }
        }

        [Category("Parameter")]
        [Description("Upper limit for Y")]
        //[ReadOnly(true)]
        public double YMax
        {
            get => yMax;
            set
            {
                //if (value >= yMin) yMax = value; else yMax = yMin;
                yMax = value;
            }
        }

        [Category("Parameter")]
        [Description("Setting")]
        public double Ya
        {
            get => ya;
            set
            {
                //if (value >= yMin && value <= yMax) ya = value;
                ya = value;
            }
        }

        [Category("Parameter")]
        [Description("Setting")]
        public double Yb
        {
            get => yb;
            set
            {
                //if (value >= yMin && value <= yMax) yb = value;
                yb = value;
            }
        }

        [Category("Parameter")]
        [Description("Setting")]
        public double Yc
        {
            get => yc;
            set
            {
                //if (value >= yMin && value <= yMax) yc = value;
                yc = value;
            }
        }

        [Category("Parameter")]
        [Description("Setting")]
        public double Yd
        {
            get => yd;
            set
            {
                //if (value >= yMin && value <= yMax) yd = value;
                yd = value;
            }
        }

        public bool DiffersFrom(MakeSlowVolumeParam other)
        {
            return (this.Ya != other.Ya) || (this.Yb != other.Yb) || (this.Yc != other.Yc) || (this.Yd != other.Yd);
        }

    }

}
