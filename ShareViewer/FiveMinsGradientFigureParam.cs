﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareViewer
{
    [Serializable]
    public class FiveMinsGradientFigureParam
    {
        public FiveMinsGradientFigureParam()
        {

        }

        public FiveMinsGradientFigureParam( int zmin, int zmax, int z, double xmin, double xmax, double x, double ymin, double ymax, double y)
        {
            zMin = zmin;
            if (zmax >= zMin) zMax = zmax; else zMax = zMin;
            if (z >= zMin && z <= zMax) this.z = z; else this.z = zMin;

            xMin = xmin;
            if (xmax >= xMin) xMax = xmax; else xMax = xMin;
            if (x >= xMin && x <= xMax) this.x = x; else this.x = xMin;

            yMin = ymin;
            if (ymax >= yMin) yMax = ymax; else yMax = yMin;
            if (y >= yMin && y <= yMax) this.y = y; else this.y = yMin;
        }

        private double x;
        private double y;
        private int z;

        private double xMin;
        private double xMax;
        private double yMin;
        private double yMax;

        private int zMin;
        private int zMax;

        [Category("Parameter")]
        [Description("X")]
        public double X
        {
            get => x;
            set
            {
                if (value >= xMin && value <= xMax) x = value;
            }

        }

        [Category("Parameter")]
        [Description("Y")]
        public double Y
        {
            get => y;
            set
            {
                if (value >= yMin && value <= yMax) y = value;
            }
        }

        [Category("Parameter")]
        [Description("Z")]
        public int Z
        {
            get => z;
            set
            {
                if (value >= zMin && value <= zMax) z= value;
            }
        }

        [Category("Parameter")]
        [Description("Lower limit for X")]
        [ReadOnly(true)]
        public double XMin { get => xMin;  set => xMin = value;  }

        [Category("Parameter")]
        [Description("Upper limit for X")]
        [ReadOnly(true)]
        public double XMax
        {
            get => xMax;
            set
            {
                if (value >= xMin) { xMax = value; } else { xMax = xMin; }
            }
        }

        [Category("Parameter")]
        [Description("Lower limit for Y")]
        [ReadOnly(true)]
        public double YMin { get => yMin; set => yMin = value; }

        [Category("Parameter")]
        [Description("Upper limit for Y")]
        [ReadOnly(true)]
        public double YMax
        {
            get => yMax;
            set
            {
                if (value >= yMin) { yMax = value; } else { yMax = yMin; }
            }
        }

        [Category("Parameter")]
        [Description("Lower limit for Z")]
        [ReadOnly(true)]
        public int ZMin { get => zMin; set => zMin = value; }

        [Category("Parameter")]
        [Description("Upper limit for Z")]
        [ReadOnly(true)]
        public int ZMax
        {
            get => zMax;
            set
            {
                if (value >= zMin) { zMax = value; } else { zMax = zMin; }
            }
        }

        public bool DiffersFrom(FiveMinsGradientFigureParam other)
        {
            return (this.X != other.X) || (this.Y != other.Y) || (this.Z != other.Z);
        }


    }
}