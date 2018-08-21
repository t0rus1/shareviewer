using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

        public MakeLowLineParam(double zmin, double zmax, double zDefault)
        {
            zMin = zmin;
            //if (zmax >= zMin) zMax = zmax; else zMax = zMin;
            zMax = zmax;
            //if (zDefault >= zMin && zDefault <= zmax) z = zDefault; else z = zMin;
            z = zDefault;
        }

        private double zMin;
        private double zMax;
        private double z;

        [Category("Parameter")]
        [Description("Lower limit for Z")]
        //[ReadOnly(true)]
        public double ZMin { get => zMin; set => zMin = value; }


        [Category("Parameter")]
        [Description("Upper limit for Z")]
        //[ReadOnly(true)]
        public double ZMax
        {
            get => zMax;
            set
            {
                //if (value >= zMin) zMax = value;
                zMax = value;
            }
        }


        [Category("Parameter")]
        [Description("Setting")]
        public double Z
        {
            get => z;
            set
            {
                //if (z >= zMin && z <= zMax)
                //{
                    z = value;
                //}
            }
        }

        public bool DiffersFrom(MakeLowLineParam other)
        {
            return this.Z != other.Z;
        }


    }


}
