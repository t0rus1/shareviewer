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

        public SlowPriceParam(double zDefault, double ya, double yb, double yc, double yd)
        {
            zMin = 100;
            zMax = 99999;
            //the yMin and Ymax apply to Ya,Yb,Yc and Yd
            yMin = 0;
            yMax = 0.9999;
            z = zDefault;
            this.ya = ya;
            ForceValid();
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
        [Description("Lower limit for Z")]
        [ReadOnly(true)]
        public double ZMin { get => zMin; set => zMin = value; }

        [Category("Parameter")]
        [Description("Upper limit for Z")]
        [ReadOnly(true)]
        public double ZMax { get => zMax; set => zMax = value; }

        [Category("Parameter")]
        [Description("Setting")]
        public double Z { get => z; set => z = value; }

        [Category("Parameter")]
        [Description("Lower limit for Y exponent")]
        [ReadOnly(true)]
        public double YMin { get => yMin; set => yMin = value; }

        [Category("Parameter")]
        [Description("Upper limit for Y exponent")]
        [ReadOnly(true)]
        public double YMax { get => yMax; set => yMax = value; }

        [Category("Parameter")]
        [Description("Exponent Ya")]
        public double Ya { get => ya; set => ya = value; }

        [Category("Parameter")]
        [Description("Exponent Yb")]
        public double Yb { get => yb; set => yb = value; }

        [Category("Parameter")]
        [Description("Exponent Yc")]
        public double Yc { get => yc; set => yc = value; }

        [Category("Parameter")]
        [Description("Exponent Yd")]
        public double Yd { get => yd; set => yd = value; }

        public bool DiffersFrom(SlowPriceParam other)
        {
            return (this.Z != other.Z) || (this.Ya != other.Ya) || (this.Yb != other.Yb) || (this.Yc != other.Yc) || (this.Yd != other.Yd);
        }

        public void ForceValid()
        {
            if (z < zMin) z = zMin;
            if (z > zMax) z = zMax;

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
