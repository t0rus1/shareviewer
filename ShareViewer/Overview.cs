using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareViewer
{
    internal class Overview
    {
        internal Overview(string name)
        {
            shareName = name;
        }

        private string shareName;                  //2.  Name of Share
        private uint   sumOfVolumes;               //3.  49 Sum of volumes from row 10298 to 10401
        private double lastPrice;                  //4.  11 Price of row 10401
        private double dayBeforePrice;             //5.  Price of row 10297
        private double priceFactor;                //6.  Price of row 10401 / Price of row 10297
        private double lastPGc;                    //7.  23 Price-Gradient PGc of row 10401
        private double lastPGd;                    //8.  26 Price-Gradient PGd of row 10401
        private double bigLastDayPGa;              //9.  13 The biggest PGa of row 10298 to 10401
        private double bigLastDayPGF;              //10. 18 The biggest PGF of All-table column 15 to 17, row 10298 to 10401
        private double lastDHLFPc;                 //11. 30 The DHLFPc of row 10401
        private double lastDHLFPd;                 //12. 34 The DHLFPd of row 10401
        private double unused1;                    //13. Not used
        private double lastDLLFPc;                 //14. 40 The DLLFPc of row 10401
        private double lastDLLFPd;                 //15. 44 The DLLFPd of row 10401
        private double lastDaySumOfPGa;            //16. 64 Sum of Points Gradient a of row 10298 to 10401
        private double lastDaySumOfPGb;            //17. 66 Sum of Points Gradient b of row 10298 to 10401
        private double lastDaySumOfPGc;            //18. 68 Sum of Points Gradient c of row 10298 to 10401
        private double lastDaySumOfPtsVola;        //19. 73 Sum of Points Volume a of row 10298 to 10401 
        private double lastDaySumOfPtsVolb;        //20. 75 Sum of Points Volume b of row 10298 to 10401 
        private double lastDaySumOfPtsVolc;        //21. 77 Sum of Points Volume c of row 10298 to 10401 
        private double lastDaySumOfPtsVolLastDay;   //22. 79 Sum of Points Volume d of row 10298 to 10401 
        private double lastDaySumOfPtsHLc;         //23. 70 Sum of Points Points High Line HLc of row 10298 to 10401
        private double lastDaySumOfPtsHLastDay;    //24. 70 Sum of Points Points High Line HLastDay of row 10298 to 10401
        private double lastDaySumOfPtsVolaa;       //25. 73 Sum of Points Volume a of row 10298 to 10401 
        private double lastDaySumOfPtsVolbb;       //26. 75 Sum of Points Volume b of row 10298 to 10401 
        private double sumOfSumCols64_79;          //27. 83 Sum of Sum of points of columns 64 to 79 (Only for row 1)
        private double sumOfSumCols64_79trf;       //28. 84 The same as column 83, but when transfer it to the Overview, it is another treatment.
        private bool lazy;

        public string ShareName { get => shareName; set => shareName = value; }
        public uint   SumOfVolumes { get => sumOfVolumes; set => sumOfVolumes = value; }
        public double LastPrice { get => lastPrice; set => lastPrice = value; }
        public double DayBeforePrice { get => dayBeforePrice; set => dayBeforePrice = value; }
        public double PriceFactor { get => priceFactor; set => priceFactor = value; }
        public double LastPGc { get => lastPGc; set => lastPGc = value; }
        public double LastPGd { get => lastPGd; set => lastPGd = value; }
        public double BigLastDayPGa { get => bigLastDayPGa; set => bigLastDayPGa = value; }
        public double BigLastDayPGF { get => bigLastDayPGF; set => bigLastDayPGF = value; }
        public double LastDHLFPc { get => lastDHLFPc; set => lastDHLFPc = value; }
        public double LastDHLFPd { get => lastDHLFPd; set => lastDHLFPd = value; }
        public double Unused1 { get => unused1; set => unused1 = value; }
        public double LastDLLFPc { get => lastDLLFPc; set => lastDLLFPc = value; }
        public double LastDLLFPd { get => lastDLLFPd; set => lastDLLFPd = value; }
        public double LastDaySumOfPGa { get => lastDaySumOfPGa; set => lastDaySumOfPGa = value; }
        public double LastDaySumOfPGb { get => lastDaySumOfPGb; set => lastDaySumOfPGb = value; }
        public double LastDaySumOfPGc { get => lastDaySumOfPGc; set => lastDaySumOfPGc = value; }
        public double LastDaySumOfPtsVola { get => lastDaySumOfPtsVola; set => lastDaySumOfPtsVola = value; }
        public double LastDaySumOfPtsVolb { get => lastDaySumOfPtsVolb; set => lastDaySumOfPtsVolb = value; }
        public double LastDaySumOfPtsVolc { get => lastDaySumOfPtsVolc; set => lastDaySumOfPtsVolc = value; }
        public double LastDaySumOfPtsVolLastDay { get => lastDaySumOfPtsVolLastDay; set => lastDaySumOfPtsVolLastDay = value; }
        public double LastDaySumOfPtsHLc { get => lastDaySumOfPtsHLc; set => lastDaySumOfPtsHLc = value; }
        public double LastDaySumOfPtsHLastDay { get => lastDaySumOfPtsHLastDay; set => lastDaySumOfPtsHLastDay = value; }
        public double LastDaySumOfPtsVolaa { get => lastDaySumOfPtsVolaa; set => lastDaySumOfPtsVolaa = value; }
        public double LastDaySumOfPtsVolbb { get => lastDaySumOfPtsVolbb; set => lastDaySumOfPtsVolbb = value; }
        public double SumOfSumCols64_79 { get => sumOfSumCols64_79; set => sumOfSumCols64_79 = value; }
        public double SumOfSumCols64_79trf { get => sumOfSumCols64_79trf; set => sumOfSumCols64_79trf = value; }
        public bool Lazy { get => lazy; set => lazy = value; }
    }
}
