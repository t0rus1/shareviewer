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
        private double x;
        private double xMin;
        private double xMax;

        public MakeSlowVolumeParam()
        {

        }

        public MakeSlowVolumeParam(double ymin, double ymax, double ya, double yb, double yc, double yd, double x)
        {

            //the yMin and Ymax will be applied to Ya,Yb,Yc and Yd
            yMin = ymin;
            YMax = ymax;
            Ya = ya;
            Yb = yb;
            Yc = yc;
            Yd = yc;
            XMin = 0.4;
            XMax = 1.0;
            X = x; //new property, for dealing with large volumes in FV at 17:35
            ForceValid();
        }

        [Category("Parameter")]
        [Description("Setting")]
        //[ReadOnly(true)]
        public double XMin { get => xMin; set => xMin = value; }

        [Category("Parameter")]
        [Description("Setting")]
        //[ReadOnly(true)]
        public double XMax { get => xMax; set => xMax = value; }

        [Category("Parameter")]
        [Description("Setting")]
        public double X { get => x; set => x = value; }


        [Category("Parameter")]
        [Description("Lower limit for Y")]
        //[ReadOnly(true)]
        public double YMin { get => yMin; set => yMin = value; }

        [Category("Parameter")]
        [Description("Upper limit for Y")]
        //[ReadOnly(true)]
        public double YMax {  get => yMax; set => yMax = value;  }

        [Category("Parameter")]
        [Description("Setting")]
        public double Ya  {  get => ya; set => ya = value;  }

        [Category("Parameter")]
        [Description("Setting")]
        public double Yb { get => yb;  set => yb = value; }

        [Category("Parameter")]
        [Description("Setting")]
        public double Yc { get => yc; set => yc = value; }

        [Category("Parameter")]
        [Description("Setting")]
        public double Yd { get => yd; set => yd = value;  }

        public bool DiffersFrom(MakeSlowVolumeParam other)
        {
            return (this.Ya != other.Ya) || (this.Yb != other.Yb) || (this.Yc != other.Yc) || (this.Yd != other.Yd) || (this.X != other.X);
        }

        public void ForceValid()
        {
            if (x < xMin) x = xMin;
            if (x > xMax) x = xMax;

            if (ya < yMin) ya = yMin;
            if (ya > yMax) ya = yMax;

            if (yb < yMin) yb = yMin;
            if (yb > yMax) yb = yMax;

            if (yc < yMin) yc = yMin;
            if (yc > yMax) yc = yMax;

            if (yd < yMin) yd = yMin;
            if (yd > yMax) yd = yMax;

        }

    }

}
