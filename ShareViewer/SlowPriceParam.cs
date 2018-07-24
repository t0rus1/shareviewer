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
            zMax = zmax;
            yMin = ymin;
            yMax = ymax;
            z = zDefault;
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
        public double Z { get => z; set => z = value; }

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

        [Category("Parameter")]
        [Description("Lower limit for Z")]
        [ReadOnly(true)]
        public double ZMin { get => zMin; set => zMin = value; }

        [Category("Parameter")]
        [Description("Upper limit for Z")]
        [ReadOnly(true)]
        public double ZMax { get => zMax; set => zMax = value; }

        [Category("Parameter")]
        [Description("Lower limit for each Y exponent")]
        [ReadOnly(true)]
        public double YMin { get => yMin; set => yMin = value; }

        [Category("Parameter")]
        [Description("Upper limit for each Y exponent")]
        [ReadOnly(true)]
        public double YMax { get => yMax; set => yMax = value; }

    }
}
