using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShareViewer
{
    [Serializable]
    public class MakeLowLineParam
    {

        public MakeLowLineParam()
        {

        }

        public MakeLowLineParam(double zDefault)
        {
            zMin = 0;
            zMax = 0.01;
            z = zDefault;
            ForceValid();
        }

        private double zMin;
        private double zMax;
        private double z;

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

        public bool DiffersFrom(MakeLowLineParam other)
        {
            return this.Z != other.Z;
        }

        public void ForceValid()
        {
            if (z < zMin) z = zMin;
            if (z > zMax) z = zMax;
        }

        public string Summarize()
        {
            string summary = "[Low Line Parameters] ";
            foreach (PropertyInfo property in typeof(MakeLowLineParam).GetProperties())
            {
                summary += $"{property.Name}: {property.GetValue(this).ToString()}; ";
            }
            return summary;
        }

    }


}
