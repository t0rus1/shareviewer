using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShareViewer
{
    [Serializable]
    internal class Overview
    {
        internal Overview(string name, int number)
        {
            shareName = name;
            shareNumber = number;
        }

        private string shareName;                  //2.  Name of Share
        private uint lastDayVol;                   //3.  49 Sum of volumes from row 10298 to 10401
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
        private double lastDaySumOfPtsVold;        //22. 79 Sum of Points Volume d of row 10298 to 10401 
        private double lastDaySumOfPtsHLc;         //23. 70 Sum of Points Points High Line HLc of row 10298 to 10401
        private double lastDaySumOfPtsHLd;         //24. 70 Sum of Points Points High Line HLastDay of row 10298 to 10401
        private double lastDaySumOfPtsVolaa;       //25. 73 Sum of Points Volume a of row 10298 to 10401 
        private double lastDaySumOfPtsVolbb;       //26. 75 Sum of Points Volume b of row 10298 to 10401 
        private double sumOfSumCols64_79;          //27. 83 Sum of Sum of points of columns 64 to 79 (Only for row 1)
        private double sumOfSumCols64_79trf;       //28. 84 The same as column 83, but when transfer it to the Overview, it is another treatment.
        private bool lazy;
        private int shareNumber;

        [Hint("")]
        public bool Lazy { get => lazy; set => lazy = value; }
        [Hint("")]
        public string ShareName { get => shareName; set => shareName = value; }
        [Hint("")]
        public uint LastDayVol { get => lastDayVol; set => lastDayVol = value; }
        [Hint("")]
        public double LastPrice { get => lastPrice; set => lastPrice = value; }
        [Hint("")]
        public double DayBeforePrice { get => dayBeforePrice; set => dayBeforePrice = value; }
        [Hint("")]
        public double PriceFactor { get => priceFactor; set => priceFactor = value; }
        [Hint("")]
        public double LastPGc { get => lastPGc; set => lastPGc = value; }
        [Hint("")]
        public double LastPGd { get => lastPGd; set => lastPGd = value; }
        [Hint("")]
        public double BigLastDayPGa { get => bigLastDayPGa; set => bigLastDayPGa = value; }
        [Hint("")]
        public double BigLastDayPGF { get => bigLastDayPGF; set => bigLastDayPGF = value; }
        [Hint("")]
        public double LastDHLFPc { get => lastDHLFPc; set => lastDHLFPc = value; }
        [Hint("")]
        public double LastDHLFPd { get => lastDHLFPd; set => lastDHLFPd = value; }
        [Hint("")]
        public double Unused1 { get => unused1; set => unused1 = value; }
        [Hint("")]
        public double LastDLLFPc { get => lastDLLFPc; set => lastDLLFPc = value; }
        [Hint("")]
        public double LastDLLFPd { get => lastDLLFPd; set => lastDLLFPd = value; }
        [Hint("")]
        public double LastDaySumOfPGa { get => lastDaySumOfPGa; set => lastDaySumOfPGa = value; }
        [Hint("")]
        public double LastDaySumOfPGb { get => lastDaySumOfPGb; set => lastDaySumOfPGb = value; }
        [Hint("")]
        public double LastDaySumOfPGc { get => lastDaySumOfPGc; set => lastDaySumOfPGc = value; }
        [Hint("")]
        public double LastDaySumOfPtsVola { get => lastDaySumOfPtsVola; set => lastDaySumOfPtsVola = value; }
        [Hint("")]
        public double LastDaySumOfPtsVolb { get => lastDaySumOfPtsVolb; set => lastDaySumOfPtsVolb = value; }
        [Hint("")]
        public double LastDaySumOfPtsVolc { get => lastDaySumOfPtsVolc; set => lastDaySumOfPtsVolc = value; }
        [Hint("")]
        public double LastDaySumOfPtsVold { get => lastDaySumOfPtsVold; set => lastDaySumOfPtsVold = value; }
        [Hint("")]
        public double LastDaySumOfPtsHLc { get => lastDaySumOfPtsHLc; set => lastDaySumOfPtsHLc = value; }
        [Hint("")]
        public double LastDaySumOfPtsHLd { get => lastDaySumOfPtsHLd; set => lastDaySumOfPtsHLd = value; }
        [Hint("")]
        public double LastDaySumOfPtsVolaa { get => lastDaySumOfPtsVolaa; set => lastDaySumOfPtsVolaa = value; }
        [Hint("")]
        public double LastDaySumOfPtsVolbb { get => lastDaySumOfPtsVolbb; set => lastDaySumOfPtsVolbb = value; }
        [Hint("")]
        public double SumOfSumCols64_79 { get => sumOfSumCols64_79; set => sumOfSumCols64_79 = value; }
        [Hint("")]
        public double SumOfSumCols64_79trf { get => sumOfSumCols64_79trf; set => sumOfSumCols64_79trf = value; }
        [Hint("")]
        public int ShareNumber { get => shareNumber; set => shareNumber = value; }


        public static int NameToIndex(string colName)
        {
            switch (colName)
            {
                case "Lazy": return 0;
                case "ShareName": return 1;
                case "LastDayVol": return 2;
                case "LastPrice": return 3;
                case "DayBeforePrice": return 4;
                case "PriceFactor": return 5;
                case "LastPGc": return 6;
                case "LastPGd": return 7;
                case "BigLastDayPGa": return 8;
                case "BigLastDayPGF": return 9;
                case "LastDHLFPc": return 10;
                case "LastDHLFPd": return 11;
                case "Unused1": return 12;
                case "LastDLLFPc": return 13;
                case "LastDLLFPd": return 14;
                case "LastDaySumOfPGa": return 15;
                case "LastDaySumOfPGb": return 16;
                case "LastDaySumOfPGc": return 17;
                case "LastDaySumOfPtsVola": return 18;
                case "LastDaySumOfPtsVolb": return 19;
                case "LastDaySumOfPtsVolc": return 20;
                case "LastDaySumOfPtsVold": return 21;
                case "LastDaySumOfPtsHLc": return 22;
                case "LastDaySumOfPtsHLd": return 23;
                case "LastDaySumOfPtsVolaa": return 24;
                case "LastDaySumOfPtsVolbb": return 25;
                case "SumOfSumCols64_79": return 26;
                case "SumOfSumCols64_79trf": return 27;
                case "ShareNumber": return 28;

                default: return -1;
            }
        }

        public static List<int> InitialViewIndices()
        {
            return new List<int>() {
                NameToIndex("Lazy"),
                //NameToIndex("ShareNumber"),
                NameToIndex("ShareName"),
                NameToIndex("LastDayVol"),
                NameToIndex("LastPrice"),
                NameToIndex("DayBeforePrice"),
                NameToIndex("PriceFactor")
            };
        }

        public static string NameToFormat(string colName)
        {
            switch (colName)
            {
                //case "Lazy": return                           //0;
                //case "ShareName": return                      //1;
                case "LastDayVol": return "N0";               //2;
                case "LastPrice": return "N3";                  //3;
                case "DayBeforePrice": return "N3";             //4;
                case "PriceFactor": return "N3";                //5;
                case "LastPGc": return "N3";                    //6;
                case "LastPGd": return "N3";                    //7;
                case "BigLastDayPGa": return "N3";              //8;
                case "BigLastDayPGF": return "N3";              //9;
                case "LastDHLFPc": return "N3";                 //10;
                case "LastDHLFPd": return "N3";                 //11;
                //case "Unused1": return //12;
                case "LastDLLFPc": return "N3";                 //13;
                case "LastDLLFPd": return "N3";                 //14;
                case "LastDaySumOfPGa": return "N1";            //15;
                case "LastDaySumOfPGb": return "N1";            //16;
                case "LastDaySumOfPGc": return "N1";            //17;
                case "LastDaySumOfPtsVola": return "N1";        //18;
                case "LastDaySumOfPtsVolb": return "N1";        //19;
                case "LastDaySumOfPtsVolc": return "N1";        //20;
                case "LastDaySumOfPtsVold": return "N1";  //21;
                case "LastDaySumOfPtsHLc": return "N1";         //22;
                case "LastDaySumOfPtsHLd": return "N1";    //23;
                case "LastDaySumOfPtsVolaa": return "N1";       //24;
                case "LastDaySumOfPtsVolbb": return "N1";       //25;
                case "SumOfSumCols64_79": return "N1";          //26;
                case "SumOfSumCols64_79trf": return "N1";       //27;
                case "ShareNumber": return "N0";                //28;

                default: return "N3";

            }

        }

        // get the hint attribute from a property name
        public static string PropNameToHint(string colName)
        {
            return typeof(Overview).GetProperty(colName).GetCustomAttribute<HintAttribute>().Hint;
        }


    }
}
